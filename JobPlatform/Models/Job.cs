using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobPlatform.Models
{
    public class Job
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
