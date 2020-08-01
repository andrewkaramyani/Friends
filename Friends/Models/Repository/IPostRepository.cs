using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models
{
    public interface IPostRepository
    {
        Post GetPostById(int id);
        IEnumerable<Post> GetAllPost();
        Post Add(Post post);
        Post Update(Post post);
        Post Delete(int id);

        IEnumerable<Post> GetAllPostToPerson(string id);


    }
}
