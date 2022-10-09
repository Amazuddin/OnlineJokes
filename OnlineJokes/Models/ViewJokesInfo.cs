using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineJokes.Models
{
    public class ViewJokesInfo
    {
        public int Id { get; set; }
        public int JokesCategoryId { get; set; }
        public string WriterName { get; set; }
        public string JokesName { get; set; }
        public List<string> Jokes { get; set; }
        public string Image { get; set; }
    }
}