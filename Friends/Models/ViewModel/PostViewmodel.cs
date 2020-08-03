using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models.ViewModel
{
    public class PostViewmodel
    {
        public PostViewmodel()
        {
            IEnumerable<Post> posts = Enumerable.Empty<Post>();
        }
        public IEnumerable<Post> Posts { get; set; }
        public Post post { get; set; }
        public UserPostComment userComment { get; set; }
    }
}
