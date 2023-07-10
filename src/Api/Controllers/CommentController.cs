using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Repository;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _repository;
        private readonly IPostRepository _postRepository;
        private readonly IValidator<Comment> _validator;

        public CommentController(ILogger<CommentController> logger, ICommentRepository repository, IPostRepository postRepository, IValidator<Comment> validator)
        {
            _repository = repository;
            _logger = logger;
            _postRepository = postRepository;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<Comment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Comment>> GetAll()
        {
            try
            {
                var result = _repository.GetAll();
                return result.Any() ? Ok(result) : NotFound();
            }
            catch (Exception e)
            {
                // TODO: Request for the best answare
                _logger.LogError(e, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Comment> Get([FromRoute][Required] Guid id)
        {
            try
            {
                var result = _repository.Get(id);
                return result is not null ? Ok(result) : NotFound();
            }
            catch (Exception)
            {
                // TODO: Request for the best answare
                return ValidationProblem();
            }
        }

        [HttpPost]
        public ActionResult<Comment> Post([FromBody][Required] Comment comment)
        {
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid)
            {
                var message = new StringBuilder();
                validationResult.Errors.ForEach(delegate (ValidationFailure error) {
                    message.AppendLine(string.Format($"{error.PropertyName}: '{error.ErrorMessage}'"));
                });

                return BadRequest(new { Message = message.ToString(), Model = comment });
            }

            try
            {
                var post = _postRepository.Get(comment.PostId);
                if (post is null)
                    return BadRequest(new { Message = "The post reference notfound", Model = comment });

                var result = _repository.Create(comment);
                return result is not null ? Ok(result) : throw new Exception();
            }
            catch (Exception e)
            {
                // TODO: Request for the best answare
                _logger.LogError(e, e.Message);
                return ValidationProblem();
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put([FromRoute][Required] Guid id, [FromBody][Required] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                // O modelo é inválido, retorne uma resposta adequada
                return BadRequest(ModelState);
            }

            try
            {
                var post = _postRepository.Get(comment.PostId);
                if (post is null)
                    return BadRequest(new { Message = "The post reference notfound", Model = comment });

                var result = _repository.Update(comment);
                return result is not null ? Ok(result) : throw new Exception();
            }
            catch (Exception e)
            {
                // TODO: Request for the best answare
                _logger.LogError(e, e.Message);
                return ValidationProblem();
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute][Required] Guid id)
        {
            try
            {
                var comment = _repository.Get(id);
                if (comment is null)
                    return NotFound();

                var result = _repository.Delete(id);
                return result ? Ok(result) : UnprocessableEntity(id);
            }
            catch (Exception e)
            {
                // TODO: Request for the best answare
                _logger.LogError(e, e.Message);
                return ValidationProblem();
            }
        }
    }
}