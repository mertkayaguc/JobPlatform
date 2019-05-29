using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPlatform.Models;

namespace JobPlatform.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly OnlineClassContext _context;
        private string currentUserId;

        private readonly IHostingEnvironment _hostingEnvironment;

        public JobController(OnlineClassContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult JobApplication(string id)
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var job = _context.Jobs.Where(x => x.Id == id).FirstOrDefault();

            var model = new JobApplication()
            {
                JobId = id,
                UserId = currentUserId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> JobApplication(JobApplication model, IFormFile CvUrl)
        {
            if (_context.JobApplications.ToList().Exists(x => x.UserId == model.UserId && x.JobId == model.JobId) == false)
            {
                model.Id = null;
                if (CvUrl != null)
                {
                    string dirPath = Path.Combine(_hostingEnvironment.WebRootPath, @"cv\");
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + CvUrl.FileName;
                    using (var fileStream = new FileStream(dirPath + fileName, FileMode.Create))
                    {
                        await CvUrl.CopyToAsync(fileStream);
                    }

                    model.CvUrl = fileName;
                }

                _context.JobApplications.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewData["err"] = "sddds";
            return View();
        }

        public IActionResult Index()
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var jobs = new List<dynamic>();
            var dbJobs = _context.Jobs.OrderByDescending(x => x.Date).ToList();

            foreach (var item in dbJobs)
            {
                var compnay = _context.Companies.Where(x => x.CompId == item.CompanyId).FirstOrDefault();

                if (compnay != null)
                {
                    dynamic o = new ExpandoObject();
                    o.UserName = compnay.Name;
                    o.PhotoUrl = compnay.PhotoUrl;
                    o.Id = item.Id;
                    o.Title = item.Title;
                    o.Text = item.Text;
                    o.Date = item.Date;
                    o.UserId = item.CompanyId;

                    jobs.Add(o);
                }
            }

            var myModel = new FeedViewModel()
            {
                Jobs = jobs
            };

            return View(myModel);
        }
    }
}