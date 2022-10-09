using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineJokes.Models
{
    public class ClientComment
    {
        public long Id { get; set; }
        [ForeignKey("JokesInfoId")]
        public JokesInfo JokesInfo { get; set; }
        public int JokesInfoId { get; set; }
        public DateTime CommentDateTime { get; set; }
        public string Comment { get; set; }
    }
}