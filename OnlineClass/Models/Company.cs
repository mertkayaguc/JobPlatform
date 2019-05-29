using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobPlatform.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string CompId { get; set; }

        public string Name { get; set; }

        private string photoUrl;

        public string PhotoUrl
        {
            get
            {
                return $"/uploads/{photoUrl.Replace("/uploads/", "")}";
            }
            set
            {
                photoUrl = value;
            }
        }

        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
