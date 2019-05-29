using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobPlatform.Models;

namespace JobPlatform.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly OnlineClassContext _context;
        private string currentUserId;


        public ProfileController(OnlineClassContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var users = new List<dynamic>();
            var posts = new List<dynamic>();

            var dbUsers = _context.Users.ToList();
            var dbPosts = _context.Posts.Where(x => x.UserId == currentUserId).OrderByDescending(x => x.Date).ToList();

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

            dynamic profile = new ExpandoObject();
            profile.PhotoUrl = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().PhotoUrl;
            profile.Name = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().Name;
            profile.Surname = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().Surname;
            profile.About = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().About;
            profile.Education = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().Education;
            profile.Skills = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().Skills;
            profile.WorkInfo = _context.Users.Where(x => x.Id == currentUserId).FirstOrDefault().WorkInfo;
            profile.Following = _context.Follows.Count(x => x.UserId == currentUserId);
            profile.Followers = _context.Follows.Count(x => x.WithId == currentUserId);

            var myModel = new FeedViewModel() { Users = users, Posts = posts, Profile = profile };

            return View(myModel);
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            if (id == null)
                return Index();

            currentUserId = id;

            var users = new List<dynamic>();
            var posts = new List<dynamic>();

            var dbUsers = _context.Users.ToList();
            var dbPosts = _context.Posts.Where(x => x.UserId == id).OrderByDescending(x => x.Date).ToList();

            foreach (var item in dbUsers)
            {
                if (item.Id != id)
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

            dynamic profile = new ExpandoObject();
            profile.PhotoUrl = _context.Users.Where(x => x.Id == id).FirstOrDefault().PhotoUrl;
            profile.Name = _context.Users.Where(x => x.Id == id).FirstOrDefault().Name;
            profile.Surname = _context.Users.Where(x => x.Id == id).FirstOrDefault().Surname;
            profile.Following = _context.Follows.Count(x => x.UserId == id);
            profile.Followers = _context.Follows.Count(x => x.WithId == id);

            var myModel = new FeedViewModel() { Users = users, Posts = posts, Profile = profile };

            return View(myModel);
        }
    }
}