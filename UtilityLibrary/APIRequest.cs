namespace UtilityLibrary
{
    public class APIRequest
    {
        public APIMethodType ApiMethodType { get; set; } = APIMethodType.GET;
        public string? Url { get; set; }
        public object? Data { get; set; }
    }
}
