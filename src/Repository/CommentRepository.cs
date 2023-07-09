using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments;
        }

        public Comment Get(Guid id)
        {
            return _context.Comments.Find(id);
        }

        public Comment Create(Comment comment)
        {
            return _context.Comments.Add(comment).Entity;
        }

        public Comment Update(Comment comment)
        {
            return _context.Comments.Update(comment).Entity;
        }

        public bool Delete(Guid id)
        {
            var entity = _context.Comments.Find(id);
            var result = _context.Comments.Remove(entity);
            return result.State == EntityState.Deleted;
        }

        public IEnumerable<Comment> GetByPostId(Guid postId)
        {
            return _context.Comments.Where(x => x.PostId == postId);
        }

        public Comment UpdateByPostId(Guid postId, Comment comment)
        {
            var entity = _context. Comments.Where(x => x.PostId == postId && x.Id == comment.Id).FirstOrDefault();
            return _context.Comments.Update(entity).Entity;
        }

        public Comment DeleteByPostId(Guid postId, Comment comment)
        {
            var entity = _context.Comments.Where(x => x.PostId == postId && x.Id == comment.Id).FirstOrDefault();
            return _context.Comments.Remove(entity).Entity;
        }
    }
}