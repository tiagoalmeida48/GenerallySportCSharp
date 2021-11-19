using Microsoft.AspNetCore.Http;

namespace GenerallySport.Models
{
    public class FileUpload
    {
        public IFormFile Files { get; set; }
        public string CPF { get; set; }
    }
}
