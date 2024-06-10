using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApiii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile form, string productcode)
        {
            try
            {
                if (form == null || form.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                string filename = form.FileName; // Dosyanın orijinal adını al
                string filepath = GetFilePath(productcode);
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string imagepath = Path.Combine(filepath, filename);

                using (FileStream stream = new FileStream(imagepath, FileMode.Create))
                {
                    await form.CopyToAsync(stream);
                }

                string imageUrl = $"{Request.Scheme}://{Request.Host}/Upload/product/{productcode}/{filename}";
                return Ok(imageUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("GetImages")]
        public IActionResult GetImages(string productcode)
        {
            try
            {
                string folderPath = Path.Combine(_environment.WebRootPath, "Upload", "product", productcode);

                if (Directory.Exists(folderPath))
                {
                    var files = Directory.GetFiles(folderPath);

                    var imageUrls = files.Select(file => new
                    {
                        url = $"{Request.Scheme}://{Request.Host}/Upload/product/{productcode}/{Path.GetFileName(file)}"
                    }).ToList();

                    return Ok(imageUrls);
                }
                else
                {
                    return NotFound("No images found for the specified product code.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private string GetFilePath(string productcode)
        {
            return Path.Combine(_environment.WebRootPath, "Upload", "product", productcode);
        }
    }
}
