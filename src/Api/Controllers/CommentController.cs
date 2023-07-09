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

namespace Api.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _repository;

        public CommentController(ILogger<CommentController> logger)
        {
            _logger = logger;
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
            catch (Exception)
            {
                // TODO: Request for the best answare
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
        public ActionResult<Comment> Post([FromBody] Comment comment)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put([FromRoute] Guid id, [FromBody] Comment comment)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}