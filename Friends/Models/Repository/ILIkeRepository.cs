using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models
{
    public interface ILikeRepository
    {
        UserPostLike GetLikeById(int id);
        IEnumerable<UserPostLike> GetPostLikes(int postid);
        UserPostLike AddLike(UserPostLike Like);
    }
}
