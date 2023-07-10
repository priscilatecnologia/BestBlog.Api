using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _repository;
        private readonly ICommentRepository _commentRepository;
        private readonly IValidator<Post> _validator;

        public PostController(ILogger<PostController> logger, ICommentRepository commentRepository, IPostRepository repository, IValidator<Post> validator)
        {
            _repository = repository;
            _logger = logger;
            _commentRepository = commentRepository;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAll()
        {
            var result = _repository.GetAll();
            return result.Any() ? Ok(result) : NotFound();
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Post> Get([FromRoute] Guid id)
        {
            var result = _repository.Get(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public ActionResult<Post> Post([FromBody] Post post)
        {
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid)
            {
                var message = new StringBuilder();
                validationResult.Errors.ForEach(delegate (ValidationFailure error)
                {
                    message.AppendLine(string.Format($"{error.PropertyName}: '{error.ErrorMessage}'"));
                });

                return BadRequest(new { Message = message.ToString(), Model = post });
            }

            var result = _repository.Create(post);
            return result is not null ? Ok(result) : UnprocessableEntity(post);
        }

        [HttpPut("{id:guid}")]
        public ActionResult<Post> Put([FromRoute] Guid id, [FromBody] Post post)
        {
            var result = _repository.Update(post);
            return result is not null ? Ok(result) : UnprocessableEntity(post);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var post = _repository.Get(id);
            if (post is null)
                return NotFound();

            var result = _repository.Delete(id);
            return result ? Ok(result) : UnprocessableEntity(id);
        }

        [HttpGet("{id:guid}/comments")]
        public ActionResult<IEnumerable<Comment>> GetComments([FromRoute] Guid id)
        {
            var result = _commentRepository.GetByPostId(id);
            return result.Any() ? Ok(result) : NotFound();
        }
    }
}