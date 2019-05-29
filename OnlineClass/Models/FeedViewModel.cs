using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPlatform.Models
{
    public class FeedViewModel
    {
        public dynamic Profile { get; set; }

        public List<dynamic> Users { get; set; }

        public List<dynamic> Posts { get; set; }

        public List<dynamic> Jobs { get; set; }

        public Post Post { get; set; }

        public Job Job{ get; set; }
    }
}
