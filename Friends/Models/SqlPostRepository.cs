using Friends.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friends.Models
{
    public class SqlPostRepository : IPostRepository, IRpositoryComment, ILikeRepository
    {
        private readonly AppDbContext Context;

        public SqlPostRepository(AppDbContext Context)
        {
            this.Context = Context;
        }


        #region Post

        public Post Add(Post Post)
        {
            Context.Posts.Add(Post);
            Context.SaveChanges();
            return Post;
        }

        public Post Delete(int id)
        {
            Post Post = Context.Posts.Find(id);
            if (Post != null)
            {
                Context.Posts.Remove(Post);
                Context.SaveChanges();
            }
            return Post;
        }

        public IEnumerable<Post> GetAllPost()
        {
            return Context.Posts.Include(u => u.User).Include(L => L.UserPostLikes).
                Include(C => C.UserPostComments).OrderByDescending(d=>d.CreationDate);
        }

        public Post GetPostById(int id)
        {
            Post Post = Context.Posts.Find(id);
            return Post;
        }

        public Post Update(Post PostChanges)
        {
            var Post = Context.Posts.Attach(PostChanges);
            Post.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return PostChanges;
        }

        #endregion


        #region Comment


        public Comment Add(Comment comment)
        {
            Context.Comments.Add(comment);
            Context.SaveChanges();
            return comment;
        }


        public IEnumerable<Comment> GetCommentPost(int postid)
        {
            Post post = Context.Posts.Find(postid);
            var comments = Context.Comments.Include(u => u.UserPostComments).
                Where(id => id.UserPostComments.PostID == postid);
            return comments;
        }

        public Comment GetCommenttById(int id)
        {
            Comment comment = Context.Comments.Find(id);
            return comment;
        }

        public Comment Update(Comment comment)
        {
            var newcomment = Context.Comments.Attach(comment);
            newcomment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return comment;
        }

        Comment IRpositoryComment.Delete(int id)
        {
            Comment comment = Context.Comments.Find(id);
            if (comment != null)
            {
                Context.Remove(comment);
                Context.SaveChanges();
            }
            return comment;
        }

        #endregion

        #region Likes
        public UserPostLike AddLike(UserPostLike Like)
        {
            //check if user like this post before
            var result = Context.UserPostLikes.Find(Like.UserID, Like.PostID);
            if (result != null)
            {
                Context.UserPostLikes.Remove(result);
            }
            else
            {
                Context.UserPostLikes.Add(Like);
            }
            Context.SaveChanges();
            return Like;
        }

        public UserPostLike GetLikeById(int id)
        {
            var result = Context.UserPostLikes.Find(id);
            return result;

        }

        public IEnumerable<UserPostLike> GetPostLikes(int postid)
        {
            var result = Context.Posts.Find(postid);
            return result.UserPostLikes;
        }

        public IEnumerable<Post> GetAllPostToPerson(string id)
        {
            return Context.Posts.Where(p => p.User.Id == id).Include(u => u.User).Include(L => L.UserPostLikes).
                Include(C => C.UserPostComments).OrderByDescending(d => d.CreationDate);
        }
        #endregion
    }
}
