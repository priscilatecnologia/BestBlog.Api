using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        IEnumerable<Comment> GetByPostId(Guid postId);
        Comment UpdateByPostId(Guid postId, Comment comment);
        Comment DeleteByPostId(Guid postId, Comment comment);
    }
}