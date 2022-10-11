namespace VS.core.API.Error.Model
{
    public class ErrorReponse
    {

        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public object? Data { get; set; }

        public bool? Success { get; set; }

        public ErrorReponse()
        {
            Success = true;
        }
    }
}
