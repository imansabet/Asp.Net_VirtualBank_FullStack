namespace Common.Wrapper;

public class ResponseWrapper<T>
{
    public bool IsSuccessful { get; set; }
    public List<string> Messages { get; set; }
    public T Data { get; set; }
    public ResponseWrapper<T> Success(T data,string message = null)
    {
        IsSuccessful = true;
        Messages = [message];
        Data = data;

        return this;
    }
    public ResponseWrapper<T> Failed( string message = null)
    {
        IsSuccessful = false;
        Messages = [message];

        return this;
    }

}
 