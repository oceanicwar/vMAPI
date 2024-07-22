namespace vMAPI.Controllers.Models;

public class BasicApiResult
{
    public bool Result { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }

    public BasicApiResult(bool result, string message = "")
    {
        this.Result = result;
        this.Message = message;
    }
}
