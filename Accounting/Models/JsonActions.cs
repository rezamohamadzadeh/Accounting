using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Models
{
    /// <summary>
    /// return json custom result
    /// </summary>
    public class JsonActions : ControllerBase
    {
        private JsonResultContent jsonResult;
        protected JsonResultContent JsonResultContent
        {
            get
            {
                if (jsonResult == null)
                    jsonResult = new JsonResultContent();
                return jsonResult;
            }
        }

        public JsonResultContent SuccessResult(string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.Success;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = true;
            return JsonResultContent;
        }

        public JsonResultContent SuccessResult(object data, string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.Success;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = true;
            JsonResultContent.Data = data;
            return JsonResultContent;
        }

        public JsonResultContent ErrorResult(string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.Error;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = false;
            return JsonResultContent;
        }

        public JsonResultContent WarningResult(string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.Warning;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = false;
            return JsonResultContent;
        }
        public JsonResultContent WarningResult(object data,string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.Warning;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = false;
            JsonResultContent.Data = data;
            return JsonResultContent;
        }

        public JsonResultContent ForbiddenResult(string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.Forbidden;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = false;
            return JsonResultContent;
        }
        public JsonResultContent UnauthorizedResult(string message = null)
        {
            JsonResultContent.StatusCode = JsonStatusCode.UnAuthorized;
            JsonResultContent.Message = message ?? JsonResultContent.StatusCode.ToDisplay();
            JsonResultContent.IsSuccess = false;
            return JsonResultContent;
        }

    }
}
