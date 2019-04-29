using System.Net;
using System.Threading;
using System.Threading.Tasks;

using IRU.LargeFileUploader.WebApp.Models.File;
using IRU.Services;

using Microsoft.AspNetCore.Mvc;

namespace IRU.LargeFileUploader.WebApp.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IDataService _dataService;

        public FileController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        /// <summary>
        /// Uploads multipart file
        /// </summary>
        /// <remarks>
        ///     Uploads lager CSV file
        /// </remarks>
        /// <response code="200">File processing result.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FileUploadResultModel), (int)HttpStatusCode.OK)]
        [Route("upload")]
        public async Task<FileUploadResultModel> Upload(CancellationToken cancellationToken)
        {
            var result = new FileUploadResultModel
            {
                FileName = "get_from_request.csv", Size = 123, Status = ProcessingStatuses.Success
            };

            return await Task.FromResult(result);
        }
    }
}
