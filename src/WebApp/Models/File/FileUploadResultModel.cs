namespace IRU.LargeFileUploader.WebApp.Models.File
{
    public class FileUploadResultModel
    {
        public string FileName { get; set; }

        public ProcessingStatuses Status { get; set; }

        public long Size { get; set; }
    }
}
