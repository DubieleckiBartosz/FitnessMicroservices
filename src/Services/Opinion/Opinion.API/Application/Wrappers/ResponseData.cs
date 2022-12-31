namespace Opinion.API.Application.Wrappers;

public class ResponseData<T>
{
    public bool Success { get; }
    public string? Message { get; set; }

    public T Data { get; }

    private ResponseData(T data, string? message)
    {
        Data = data;
        Message = message;
        Success = true;
    }
     
    public static ResponseData<T> Ok(T data, string? message)
    {
        return new ResponseData<T>(data, message);
    }
}