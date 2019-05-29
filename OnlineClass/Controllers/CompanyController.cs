using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPlatform.Models;

namespace JobPlatform.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly OnlineClassContext _context;
        private string currentUserId;

        public CompanyController(OnlineClassContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Companies.ToList());
        }
    }
}