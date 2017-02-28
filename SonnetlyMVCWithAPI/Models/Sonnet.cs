using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SonnetlyMVCWithAPI.Models
{
    public class Sonnet
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Desc { get; set; }
        public bool Public { get; set; }

        [Required]
        [Display(Name = "Original URL")]
        public string OriginalUrl { get; set; }

        [Display(Name = "Sonnetly URL")]
        public string NewUrl { get; set; }

        [Display(Name = "Overall Clicks")]
        public int NumClicks { get; set; }

        [Display(Name = "Created by")]
        public string OwnerId { get; set; }

        protected virtual ApplicationUser Owner { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "dd MMM yyyy")]
        public DateTime Created { get; set; }

    }
}