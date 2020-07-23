using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Accounting.Models
{
    public class JsonResultContent
    {
        public string Message { get; set; }
        public JsonStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }


    }
    public enum JsonStatusCode
    {
        [Display(Name = "The request was successful")]
        Success,
        [Display(Name = "The request encountered an error")]
        Error,
        [Display(Name = "Submitted parameters are invalid")]
        Warning,
        [Display(Name = "You do not have access to this section")]
        Forbidden,
        [Display(Name = "You are unauthorized")]
        UnAuthorized

    }
    public static class EnumExtention
    {
        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {


            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public enum DisplayProperty
        {
            Description,
            GroupName,
            Name,
            Prompt,
            ShortName,
            Order
        }
    }

}
