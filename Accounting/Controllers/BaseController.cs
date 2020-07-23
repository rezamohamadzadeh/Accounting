using System;
using System.Linq;
using Accounting.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quartz.Impl.Triggers;

namespace Accounting.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// custom messages for show in view
        /// </summary>
        protected const string SuccessMessage = "Your information submited successfully";
        protected const string SuccessAddMessage = "Your information added successfully";
        protected const string SuccessDeleteMessage = "Your information deleted successfully";
        protected const string SuccessEditMessage = "Your information edited successfully";
        protected const string ErrorMessage = "The operation failed \n";
        protected const string ErrorMessageForGetInformation = "Calling information was difficult \n";
        protected const string ErrorMessageCheckEmail = "This email is not exist \n";
        protected const string SuccessMessageCheckEmail = "This email is exists \n";
        protected const string SuccessSendEmailMessage = "Your email sent successfully";
        protected const string SuccessPaymentMessage = "Your payment did successfully";
        protected const string ErrorSendEmailMessage = "Your email failed";


        /// <summary>
        /// get ModelState errors
        /// </summary>
        /// <param name="result"></param>
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        /// <summary>
        /// Convert WeekDayToWeedayStrName
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="lastChar"></param>
        protected void ConvertWeekDayToWeekStrName(CronTriggerImpl cron,char lastChar)
        {
            switch (lastChar)
            {
                case '1':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "SUN";
                    break;
                case '2':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "MON";
                    break;
                case '3':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "TUE";
                    break;
                case '4':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "WED";
                    break;
                case '5':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "THU";
                    break;
                case '6':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "FRI";
                    break;
                case '7':
                    cron.CronExpressionString = cron.CronExpressionString.Remove(cron.CronExpressionString.Length - 1, 1) + "SAT";
                    break;

                default:
                    break;
            }
        }
}
    /// <summary>
    /// datatable properties
    /// </summary>
    public class AjaxResult
    {
        public string draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public object data { get; set; }
    }
}