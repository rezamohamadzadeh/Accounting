using DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    /// <summary>
    /// Its SendMail Job Dto
    /// </summary>
    public class JobsModelDto
    {
        public BackUpDatabaseDto BackUpDatabase { get; set; }
        public SendEmailBySelectTimeDto SendEmailBySelectTime { get; set; }
        public SendEmailBySelectWeekDayTimeDto SendEmailBySelectWeekDayTime { get; set; }
        public DeleteLocalFolderContentByDateRangeDto DeleteLocalFolderContentByDateRange { get; set; }
        public DeleteRemoteFolderContentByDateRangeDto DeleteRemoteFolderContentByDateRange { get; set; }
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
    public class SendAffiliatesSellReportToAdminDto
    {

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

        [Display(Name = "DayOfWeek")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DayOfWeek DayOfWeek { get; set; }

    }

    public class BackUpDatabaseDto
    {

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

        [Display(Name = "DayOfWeek")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DayOfWeek DayOfWeek { get; set; }

        [Display(Name = "DatabaseName")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DatabaseName DatabaseName { get; set; }



    }

    public class SendEmailBySelectTimeDto
    {

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

    }
    public class ChangeUSDToNewCurrencyDto
    {

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

        
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please Select {0}")]
        public double Amount { get; set; }

        
        [Display(Name = "Currency")]
        [Required(ErrorMessage = "Please Select {0}")]
        public string Currency { get; set; }


    }

    /// <summary>
    /// Delete Folder Content by date range
    /// </summary>

    public class DeleteLocalFolderContentByDateRangeDto
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
    public class DeleteRemoteFolderContentByDateRangeDto
    {

        [Display(Name = "StartDate")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "RemoteAddress")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [RegularExpression(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", ErrorMessage = "Please enter valid ftp url")]
        public string RemoteAddress { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please Select {0}")]
        public DateTime Time { get; set; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }

}
