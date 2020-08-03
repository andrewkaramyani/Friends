using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models.Repository
{
    public interface IRepositoryUnitWork
    {
        #region like
        UserPostLike GetLikeById(int id);
        IEnumerable<UserPostLike> GetPostLikes(int postid);
        UserPostLike AddLike(UserPostLike Like);
        #endregion

        #region post
        Post GetPostById(int id);
        IEnumerable<Post> GetAllPost();
        Post Add(Post post);
        Post Update(Post post);
        Post Delete(int id);

        IEnumerable<Post> GetAllPostToPerson(string id);

        #endregion
        #region comment
        Comment GetCommenttById(int id);
        IEnumerable<Comment> GetCommentPost(int postid);
        UserPostComment AddComment(UserPostComment usercomment);
        Comment Update(Comment comment);
        Comment DeleteComment(int id);
        #endregion
    }
}
