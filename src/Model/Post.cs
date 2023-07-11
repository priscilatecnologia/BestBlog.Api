using FluentValidation;
using System;

namespace Model
{
    public record Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor(x => x.Content).Must(x => !string.IsNullOrEmpty(x) && x.Length <= 120).WithMessage("Should not have more than 30 characters");
            RuleFor(x => x.Title).Must(x => !string.IsNullOrEmpty(x) && x.Length <= 30).WithMessage("Should not have more than 30 characters");
        }
    }
}