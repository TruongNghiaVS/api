namespace VS.core.API.model
{
    public class UserInput
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }

    public class InputIdRequest
    {
        public string? Id { get; set; }
    }

    public class AssignedRequest
    {
        public string? Id { get; set; }

        public string? AssignedId { get; set; }
    }

}
