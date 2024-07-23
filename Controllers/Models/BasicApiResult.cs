namespace vMAPI.Controllers.Models;

public class BasicApiResult<T>
{
    public bool Result { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }

    public BasicApiResult(bool result, string message = "", T data = default!)
    {
        this.Result = result;
        this.Message = message;
        this.Data = data;
    }
}
