using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Model;
using Repository;

namespace Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _repository;
        private readonly ICommentRepository _commentRepository;

        public PostController(ILogger<PostController> logger, IPostRepository repository)
        {
            _repository = repository;
            _logger = logger;
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
            var result = _repository.Create (post);
            return result is not null ? Ok(result) : UnprocessableEntity(post);
        }

        [HttpPut("{id:guid}")]
        public ActionResult<Post> Put([FromRoute] Guid id, [FromBody] Post post)
        {
            var result = _repository.Update (post);
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