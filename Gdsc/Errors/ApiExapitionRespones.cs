namespace Gdsc.Errors
{
    public class ApiExapitionRespones : ApiErroeResponse
    {

        public string Details { get; set; }
        public ApiExapitionRespones(int statusCode ,string errorMsg = null ,string details = null) :base(statusCode , errorMsg)
        {
            Details = details;

        }
    }
}
