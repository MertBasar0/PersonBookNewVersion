using System.Net;
using System.Text.Json.Serialization;
using Utilities.Dtos;

namespace Utilities.Wrappers.WrapperGeneric
{
    public class AuthApiResponseGenericModel<T> where T : class  
    {
        public T? Data { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public ErrorDto? ErrorDto { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }

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
            var errorDto = new ErrorDto(errorMessage, isShow);
            
            return new AuthApiResponseGenericModel<T> { ErrorDto = errorDto, HttpStatusCode = statusCode, IsSuccess = false };
        }

        public AuthApiResponseGenericModel() //Buradaki boş constructure'ın kaldırıl kaldırılmayacağına sonra karar verilecek. Kullanım mantığıyla alakalı deneyime ihtiyaç var.
        {
            
        }

        public AuthApiResponseGenericModel(T? data, HttpStatusCode httpStatusCode, ErrorDto? errors)
        {
            Data = data;
            HttpStatusCode = httpStatusCode;
            Errors = errors;


            if(HttpStatusCode != HttpStatusCode.OK) { Success = false; }
        }
    }
}
