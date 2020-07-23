using Accounting.Controllers;
using Accounting.Models;
using AutoMapper;
using Common.Extensions;
using Common.Images;
using DAL.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quartz;
using Quartz.Impl.Triggers;
using Repository.InterFace;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IEmailSender _emailSender;

        public HomeController(UserManager<ApplicationUser> userManager,
            IMapper mapper, IUnitOfWork uow,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _mapper = mapper;
            _uow = uow;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            feachDayOfWeek();
            return View();
        }


        /// <summary>
        /// Send Email By Select Dayofweek and Time
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmailBySelectWeekDayTime(SendEmailBySelectWeekDayTimeDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    feachDayOfWeek();
                    return View();
                }

                var cron = CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(model.DayOfWeek, model.Time.Hour, model.Time.Minute).Build() as CronTriggerImpl;
                var lastChar = cron.CronExpressionString[cron.CronExpressionString.Length - 1];// get dayofweek
                ConvertWeekDayToWeekStrName(cron, lastChar);
                RecurringJob.AddOrUpdate("SendEmailBySelectWeekDayTime", () => AccountingAffiliateAmountSell(), cron.CronExpressionString, TimeZoneInfo.Local);


                feachDayOfWeek();
                ViewBag.SuccessMessage = SuccessMessage;

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ErrorMessage + ex.Message;
                return View("Index");
            }
            
        }

        /// <summary>
        /// Send Email By Select Only Time
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmailBySelectTime(SendEmailBySelectTimeDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    feachDayOfWeek();
                    return View();
                }
                var cron = CronScheduleBuilder.DailyAtHourAndMinute(model.Time.Hour, model.Time.Minute).Build() as CronTriggerImpl;
                RecurringJob.AddOrUpdate("SendEmailBySelectTime", () => AccountingAffiliateAmountSell(), cron.CronExpressionString, TimeZoneInfo.Local);


                feachDayOfWeek();
                ViewBag.SuccessMessage = SuccessMessage;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ErrorMessage + ex.Message;
                return View("Index");
            }

        }


        /// <summary>
        /// manage user profile
        /// </summary>
        /// <returns></returns>

        public async Task<IActionResult> Profile()
        {

            try
            {
                var userDto = _mapper.Map<ProfileDto>(await _userManager.FindByIdAsync(UserExtention.GetUserId(User)));
                if (userDto == null)
                {
                    ViewBag.ErrorMessage = ErrorMessageForGetInformation;
                    return View();
                }
                return View(userDto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ErrorMessageForGetInformation + " \n " + ex.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileDto profileDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(profileDto.Id))
                    {
                        ViewBag.ErrorMessage = ErrorMessageForGetInformation;
                        return View(profileDto);
                    }

                    var user = await _userManager.FindByIdAsync(profileDto.Id);

                    if (profileDto.Files != null)
                    {
                        Upload uploader = new Upload();
                        Delete delete = new Delete();
                        if (user.Image != null)
                        {
                            string deletPath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/UserProfile", user.Image
                            );
                            delete.DeleteImage(deletPath);
                        }


                        profileDto.Image = Guid.NewGuid().ToString() + Path.GetExtension(profileDto.Files.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/UserProfile", profileDto.Image

                        );


                        string DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserProfile");
                        await uploader.UploadImage(savePath, DirectoryPath, profileDto.Files);
                        await _signInManager.SignOutAsync();

                    }
                    else
                        profileDto.Image = user.Image;



                    user.Name = profileDto.Name;
                    user.Image = profileDto.Image;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        ViewBag.SuccessMessage = SuccessEditMessage;
                    }
                    else
                        ViewBag.ErrorMessage = ErrorMessage;

                    await _signInManager.RefreshSignInAsync(user);
                    TempData["Image"] = user.Image == null ? "" : user.Image;
                }
                return View(profileDto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ErrorMessage + " \n " + ex.Message;
                return View(profileDto);
            }

        }


        /// <summary>
        /// feach DayOfWeek Enum in view
        /// </summary>
        private void feachDayOfWeek()
        {
            var dayOfWeek = new List<SelectListItem>();
            dayOfWeek.Add(new SelectListItem
            {
                Text = "Please Select Day Of Week",
                Value = ""
            });
            foreach (DayOfWeek eVal in Enum.GetValues(typeof(DayOfWeek)))
            {
                dayOfWeek.Add(new SelectListItem { Text = Enum.GetName(typeof(DayOfWeek), eVal), Value = eVal.ToString() });
            }

            ViewBag.DayOfWeek = dayOfWeek;
        }


        /// <summary>
        /// My Job : Calc Affiliate Sell Amounts
        /// </summary>
        /// <returns></returns>
        public async Task AccountingAffiliateAmountSell()
        {
            var affiliates = _uow.AffiliateRepo.Get();

            foreach (var item in affiliates)
            {
                var affiliatecomision = _uow.AffiliateRepo.Get(d => d.Email == item.Email).FirstOrDefault();
                var Comision = affiliatecomision == null ? 0 : affiliatecomision.comision;
                var affiliateSell = _uow.SellRepo.Get(d => d.AffiliateCode == item.Code && d.PayStatus != PayStatus.Registered && d.CreateAt > DateTime.Now.AddDays(-1)).Select(d => new Tb_Sell { Price = d.Price }).ToList();
                var sumSell = affiliateSell == null ? 0 : affiliateSell.Sum(d => d.Price);

                var CommisionAmount = (sumSell * Comision) / 100;
                await _emailSender.SendEmailAsync(item.Email.Trim(), "Your Today Ammount Comission", "Your Today Ammount Comission Is : " + CommisionAmount.ToString());
            }
        }
    }
}