using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Utilities.Wrappers.WrapperGeneric
{
    public class AuthApiResponseGenericModel<T> where T : class  
    {
        public T? Data { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public ErrorDto? Errors { get; set; }

        public bool Success { get; set; }

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
