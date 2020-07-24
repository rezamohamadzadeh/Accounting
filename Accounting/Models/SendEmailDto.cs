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
        public DeleteFolderContentByDateRangeDto DeleteFolderContentByDateRange { get; set; }
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
    /// <summary>
    /// Delete Folder Content by date range
    /// </summary>

    public class DeleteFolderContentByDateRangeDto
    {

        [Display(Name = "StartDate")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "FolderName")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string FolderName { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }


    }
}
