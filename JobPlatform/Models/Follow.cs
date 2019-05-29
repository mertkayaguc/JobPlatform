using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobPlatform.Models
{
    public class Follow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string WithId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
