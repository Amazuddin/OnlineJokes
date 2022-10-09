using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineJokes.Models
{
    public class GetAllJokeInfoByCategory
    {
        public int Id { get; set; }
        public int JokesCategoryId { get; set; }
        [Display(Name = "Writer Name")]
        public string WriterName { get; set; }
        [Display(Name = "Jokes Name")]
        public string JokesName { get; set; }
        public string Jokes { get; set; }
        public string Image { get; set; }
        public string UrlLink { get; set; }
    }
}