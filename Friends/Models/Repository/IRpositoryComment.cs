using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models.Repository
{
    public interface IRpositoryComment
    {
        Comment GetCommenttById(int id);
        IEnumerable<Comment> GetCommentPost(int postid);
        Comment Add(Comment comment);
        Comment Update(Comment comment);
        Comment Delete(int id);
    }
}
