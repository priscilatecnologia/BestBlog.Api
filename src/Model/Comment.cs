using FluentValidation;
using System;

namespace Model
{
    public record Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {
            RuleFor(x => x.Content).Must(x => !string.IsNullOrEmpty(x) && x.Length <= 120);
            RuleFor(x => x.Author).Must(x => !string.IsNullOrEmpty(x) && x.Length <= 30);
            RuleFor(x => x.PostId).NotNull().NotEmpty();
        }
    }
}