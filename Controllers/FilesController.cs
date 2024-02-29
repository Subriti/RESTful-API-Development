using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _extensionContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) 
        { 
            _extensionContentTypeProvider = fileExtensionContentTypeProvider?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }    

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "getting-started-with-rest-slides.pdf";
            if(!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if(!_extensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes= System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
