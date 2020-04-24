using System;
namespace PhoenixFutureApiSdk.Model
{

    public class ResponseModel
    {
        public ResponseStatus Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ResponseModel<T> : ResponseModel
    {
        public T Response { get; set; }
    }
}
