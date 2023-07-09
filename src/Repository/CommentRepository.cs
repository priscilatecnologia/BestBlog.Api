using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context) {}

        public IEnumerable<Comment> GetByPostId(Guid postId)
        {
            return Context.Comments.Where(x => x.PostId == postId);
        }

        public Comment UpdateByPostId(Guid postId, Comment comment)
        {
            var entity = Context. Comments.Where(x => x.PostId == postId && x.Id == comment.Id).FirstOrDefault();
            return Context.Comments.Update(entity).Entity;
        }

        public Comment DeleteByPostId(Guid postId, Comment comment)
        {
            var entity = Context.Comments.Where(x => x.PostId == postId && x.Id == comment.Id).FirstOrDefault();
            return Context.Comments.Remove(entity).Entity;
        }
    }
}