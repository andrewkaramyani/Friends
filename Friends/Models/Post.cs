using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Friends.Models.Enums;

namespace Friends.Models
{
    public class Post
    {
        public Post()
        {
            UserPostLikes = new List<UserPostLike>();
            UserPostComments = new List<UserPostComment>();
        }
        public int ID { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        //[ForeignKey("User")]
        //public string UserID { get; set; }

        [Required]
        public PostStatus State { get; set; }

        public virtual Person User { get; set; }

        public virtual List<UserPostComment> UserPostComments { get; set; }

        public virtual List<UserPostLike> UserPostLikes { get; set; }
    }
}
