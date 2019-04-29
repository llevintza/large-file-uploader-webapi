using Microsoft.AspNetCore.Mvc;

namespace LargeFileUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        public void Post()
        {
        }
    }
}
