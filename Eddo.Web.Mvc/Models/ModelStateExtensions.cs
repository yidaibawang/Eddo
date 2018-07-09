using Eddo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Eddo.Web.Mvc.Models
{
    public static class ModelStateExtensions
    {
        public static MvcAjaxResponse ToMvcAjaxResponse(ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return new MvcAjaxResponse();
            }

            var validationErrors = new List<ValidationErrorInfo>();

            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    validationErrors.Add(new ValidationErrorInfo(error.ErrorMessage, state.Key));
                }
            }

            var errorInfo = new ErrorInfo("您的请求无效")
            {
                ValidationErrors = validationErrors.ToArray()
            };

            return new MvcAjaxResponse(errorInfo);
        }
    }
}
