using System;
using System.Collections.Generic;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Model;
using Moq;
using Repository;
using Xunit;

namespace Api.Tests
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly CommentController _controllerUnitTesting;

        public CommentControllerTests()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _controllerUnitTesting = new CommentController(null, _commentRepository.Object);
        }

        [Fact]
        public void GetAll_Returns_Existing_Comments()
        {
            // Arrange
            var expected = new List<Comment>()
            {
                { new Comment { Author = "Author", Content = "New Content", CreationDate = System.DateTime.Now, PostId = Guid.NewGuid() } }
            };

            _commentRepository.Setup(x => x.GetAll()).Returns(expected);   

            // Act
            var actual = _controllerUnitTesting.GetAll();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actual.Result);
            Assert.Equal(expected, okObjectResult.Value);
        }

        [Fact]
        public void Insert_Returns_BedRequest_When_Author_have_more_than_30_characters()
        {
            // Arrange
            var comment = new Comment { Author = "Author have more than 30 characters", Content = "New Content", CreationDate = System.DateTime.Now, PostId = Guid.NewGuid() } ;

            // Act
            var actual = _controllerUnitTesting.Post(comment);

            //// Assert
            //var okObjectResult = Assert.IsType<BedRequestResult>(actual.Result);
            //Assert.Equal(expected, okObjectResult.Value);
        }

    }
}