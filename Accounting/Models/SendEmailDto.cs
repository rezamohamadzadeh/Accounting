using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    /// <summary>
    /// Its SendMail Job Dto
    /// </summary>
    public class SendEmailDto
    {
        public SendEmailBySelectWeekDayTimeDto SendEmailBySelectWeekDayTime { get; set; }
        public SendEmailBySelectTimeDto SendEmailBySelectTime { get; set; }
    }
    public class SendEmailBySelectWeekDayTimeDto
    {
        
        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

        [Display(Name = "DayOfWeek")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DayOfWeek DayOfWeek { get; set; }

    }
    public class SendEmailBySelectTimeDto
    {
        
        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

    }
        

}
