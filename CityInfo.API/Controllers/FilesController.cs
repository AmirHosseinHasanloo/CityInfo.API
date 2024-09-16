using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/Files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private FileExtensionContentTypeProvider _fileProvider;

        public FilesController(FileExtensionContentTypeProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(int fileId)
        {
            string filePath = "IMG_20240503_222711.rar";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            if (!_fileProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
