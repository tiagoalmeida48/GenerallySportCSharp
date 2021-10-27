using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace GenerallySport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImagesController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public UploadImagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUpload{
            public IFormFile Files { get; set; }
        }  

        public string Post ([FromForm] FileUpload file)
        {
            if (file.Files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + @"\uploadsFotos\"))
                        Directory.CreateDirectory(_environment.WebRootPath + @"\uploadsFotos\");

                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + @"\uploadsFotos\" + file.Files.FileName))
                    {
                        file.Files.CopyTo(fileStream);
                        fileStream.Flush();
                        return @"\uploadsFotos\" + file.Files.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "Unsuccessfull";
        }
    }

}
