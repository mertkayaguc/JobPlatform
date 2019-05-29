using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobPlatform.Models
{
    public class JobApplication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string JobId { get; set; }

        public string CvUrl { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
