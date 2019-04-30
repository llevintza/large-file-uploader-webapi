using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using IRU.LargeFileUploader.WebApp.Attributes;
using IRU.LargeFileUploader.WebApp.Models.File;
using IRU.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace IRU.LargeFileUploader.WebApp.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IDataService _dataService;

        private readonly ILogger<FileController> _log;

        public FileController(IDataService dataService, ILogger<FileController> log)
        {
            this._dataService = dataService;
            this._log = log;
        }

        /// <summary>
        /// Uploads multipart file
        /// </summary>
        /// <remarks>
        ///     Uploads large CSV file
        /// </remarks>
        /// <response code="200">File processing result.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FileUploadResultModel), (int)HttpStatusCode.OK)]
        [Route("upload")]
        [DisableFormValueModelBinding]
        public async Task<FileUploadResultModel> UploadAsync(IFormFile file, CancellationToken cancellationToken)
        {
            //try
            //{
            //    //var boundary = Request.GetMultipartBoundary();

            //    //if (string.IsNullOrWhiteSpace(boundary))
            //    //    throw new Exception("could not identify the boundary");


            //    //var reader = new MultipartReader(boundary, Request.Body, 1024);
            //    //var section = await reader.ReadNextSectionAsync(cancellationToken);
            //    ////var str = await section.ReadAsStringAsync();

            //    //var fileSection = section.AsFileSection();
            //    //var fileSectionFileStream = fileSection.FileStream;

            //    ////this._log.LogDebug($"str = {str}");

            //    //while (section != null)
            //    //{
            //        // process each image
            //        //const int chunkSize = 1024;
            //        //var buffer = new byte[chunkSize];
            //        //var bytesRead = 0;
            //        //var fileName = GetFileName(section.ContentDisposition);
            //        var buffer = new StringBuilder();

            //        using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
            //        {
            //            //do
            //            //{
            //            //    bytesRead = await section.Body.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
            //            //    stream.Write(buffer, 0, bytesRead);

            //            //} while (bytesRead > 0);
            //            do
            //            {
            //                var value = await streamReader.ReadLineAsync();
            //                buffer.AppendLine(value);
            //            }
            //            while (!streamReader.EndOfStream);

            //            //var value = await streamReader.ReadLineAsync();
            //            var fullValue = buffer.ToString();
                        
            //            this._log.LogInformation(fullValue);
            //        }

            //    //    section = await reader.ReadNextSectionAsync(cancellationToken);
            //    //}

            //}
            //catch (Exception exception)
            //{
            //    this._log.LogError(exception.Message);
            //    throw;
            //}
            
            await this._dataService.ProcessFileAsync(file.OpenReadStream(), cancellationToken);

            var result = new FileUploadResultModel
            {
                FileName = file.FileName,
                Size = file.Length,
                Status = ProcessingStatuses.Success
            };

            return result;
        }
    }
}
