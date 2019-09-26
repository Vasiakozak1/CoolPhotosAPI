namespace CoolPhotosAPI.BL.Exceptions
{
    public class FileAlreadyExistsException: BaseException
    {
        public FileAlreadyExistsException(string fileName) 
            : base($"File you have just tried to upload ({fileName}) already exists"
                  , System.Net.HttpStatusCode.BadRequest) { }
    }
}
