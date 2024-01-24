using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Dto
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; }

        public bool IsShow { get; private set; }


        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public static ErrorDto Create(string error, bool isShow)
        {
            return new ErrorDto()
            {
                Errors = new List<string> { error },
                IsShow = isShow
            };
        }

        public static ErrorDto Create(List<string>errors, bool isShow)
        {
            return new ErrorDto()
            {
                Errors = errors,
                IsShow = isShow
            };
        }
    }
}
