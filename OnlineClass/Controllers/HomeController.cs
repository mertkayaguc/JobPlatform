using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPlatform.Models;

namespace JobPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnlineClassContext _context;
        private string currentUserId;


        public HomeController(OnlineClassContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Feed");
        }

        [Authorize]
        public IActionResult Feed()
        {
            ViewData["Message"] = "Your contact page.";

            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = new List<dynamic>();
            var posts = new List<dynamic>();
            var jobs = new List<dynamic>();

            var dbUsers = _context.Users.ToList();
            var dbPosts = _context.Posts.OrderByDescending(x => x.Date).ToList();
            var dbJobs = _context.Jobs.OrderByDescending(x => x.Date).ToList();

            foreach (var item in dbUsers)
            {
                if (item.Id != currentUserId)
                {
                    dynamic o = new ExpandoObject();
                    o.Id = item.Id;
                    o.UserName = item.UserName;
                    o.PhotoUrl = item.PhotoUrl;

                    users.Add(o);
                }
            }

            foreach (var item in dbPosts)
            {
                var user = dbUsers.Where(x => x.Id == item.UserId).FirstOrDefault();

                dynamic o = new ExpandoObject();
                o.UserName = $"{user.Name} {user.Surname}";
                o.PhotoUrl = user.PhotoUrl;
                o.Id = item.Id;
                o.Title = item.Title;
                o.Text = item.Text;
                o.Date = item.Date;
                o.UserId = item.UserId;
                o.LikeCount = _context.Likes.Count(x => x.PostId == item.Id);

                posts.Add(o);
            }

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

            dynamic profile = new ExpandoObject();
            profile.Following = _context.Follows.Count(x => x.UserId == currentUserId);
            profile.Followers = _context.Follows.Count(x => x.WithId == currentUserId);

            var myModel = new FeedViewModel() { Users = users, Posts = posts, Profile = profile, Jobs= jobs };

            return View(myModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SharePost(FeedViewModel model)
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var post = model.Post;
            post.UserId = currentUserId;

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Feed");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ShareJob(FeedViewModel model)
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var job = model.Job;
            job.CompanyId = currentUserId;

            _context.Jobs.Add(job);
            _context.SaveChanges();

            return RedirectToAction("Feed");
        }

        [Authorize]
        public IActionResult LikePost(string id)
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_context.Likes.ToList().Exists(x => x.PostId == id && x.UserId == currentUserId))
            {
                _context.Likes.Remove(_context.Likes.Where(x => x.PostId == id && x.UserId == currentUserId).FirstOrDefault());
            }
            else
            {
                _context.Likes.Add(new Like()
                {
                    PostId = id,
                    UserId = currentUserId,
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Feed");
        }

        [Authorize]
        public IActionResult DeletePost(string id)
        {
            _context.Posts.Remove(_context.Posts.Where(x => x.Id == id).FirstOrDefault());
            _context.SaveChanges();

            return RedirectToAction("Feed");
        }

        [Authorize]
        public IActionResult FollowUser(string id)
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_context.Follows.ToList().Exists(x => x.UserId == currentUserId && x.WithId == id))
            {
                _context.Follows.Remove(_context.Follows.Where(x => x.UserId == currentUserId && x.WithId == id).FirstOrDefault());
            }
            else
            {
                _context.Follows.Add(new Follow()
                {
                    UserId = currentUserId,
                    WithId = id
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Feed");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
