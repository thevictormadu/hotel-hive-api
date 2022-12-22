using Newtonsoft.Json;

namespace HotelManagement.Core
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public Response(int statusCode, bool success, string msg, T data)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
        }
        public Response()
        {
        }
        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Response<T> Fail(string errorMessage, int statusCode = 404)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static Response<T> Success(string successMessage, T data, int statusCode = 200)
        {
            return new Response<T> { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
