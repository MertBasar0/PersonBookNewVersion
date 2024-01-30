using System.Net;
using System.Text.Json.Serialization;
using Utilities.Dto;

namespace Utilities.Wrappers.WrapperGeneric
{
    public class AuthApiResponseGenericModel<T> where T : class  
    {
        public T? Data { get; set; } = null;

        public HttpStatusCode HttpStatusCode { get; set; }

        public ErrorDto? ErrorDto { get; set; }  = null;

        public bool IsSuccess { get; set; } = false;

        public static AuthApiResponseGenericModel<T> Success(T data, HttpStatusCode statusCode)
        {
            return new AuthApiResponseGenericModel<T> { Data = data, HttpStatusCode = statusCode, IsSuccess = true };
        }

        public static AuthApiResponseGenericModel<T> Success(HttpStatusCode statusCode)
        {
            return new AuthApiResponseGenericModel<T> { Data = default, HttpStatusCode = statusCode, IsSuccess = true };
        }
        
        public static AuthApiResponseGenericModel<T> Fail(ErrorDto errorDto, HttpStatusCode statusCode)
        {
            return new AuthApiResponseGenericModel<T>
            {
                ErrorDto = errorDto,
                HttpStatusCode = statusCode,
                IsSuccess = false
            };
        }

        public static AuthApiResponseGenericModel<T> Fail(string errorMessage, HttpStatusCode statusCode, bool isShow)
        {
            var errorDto = ErrorDto.Create(errorMessage, isShow);
            
            return new AuthApiResponseGenericModel<T> { ErrorDto = errorDto, HttpStatusCode = statusCode, IsSuccess = false };
        }
    }
}
