namespace CoolPhotosAPI.BL.Exceptions
{
    public class FileFormatException: BaseException
    {
        public FileFormatException(): base("Files uploaded must be images", System.Net.HttpStatusCode.BadRequest) { }
    }
}
