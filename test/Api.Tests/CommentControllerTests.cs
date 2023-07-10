using Api.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Api.Tests
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly IValidator<Comment> _validator;
        private readonly CommentController _controllerUnitTesting;

        public CommentControllerTests()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _postRepository = new Mock<IPostRepository>();
            _validator = new CommentValidation();
            _controllerUnitTesting = new CommentController(null, _commentRepository.Object, _postRepository.Object, _validator);
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

        public static IEnumerable<object[]> SplitCountData =>
            new List<object[]>
            {
                new object[] { typeof(BadRequestObjectResult), new Comment { Author = "Author Should not have more than 30 characters", Content = "New Content", CreationDate = System.DateTime.Now, PostId = Guid.NewGuid() } },
                new object[] { typeof(BadRequestObjectResult), new Comment { Author = "Author", Content = "New Content with more than 120 characters. New Content with more than 120 characters. New Content with more than 120 chara", CreationDate = System.DateTime.Now, PostId = Guid.NewGuid() } },
                new object[] { typeof(BadRequestObjectResult), new Comment { Author = "Author", Content = "New Content With no PostId", CreationDate = System.DateTime.Now  } },
                new object[] { typeof(NotFoundObjectResult), new Comment { Author = "Author", Content = "New Content With", CreationDate = System.DateTime.Now, PostId =  Guid.NewGuid()} }
            };

        [Theory, MemberData(nameof(SplitCountData))]
        public void Insert_Returns_BedRequest_When_Author_have_more_than_30_characters(Type typeOfResult, Comment inTheory)
        {
            // Arrange
            var comment = inTheory;
            _postRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns<Post>(null);

            // Act
            var actual = _controllerUnitTesting.Post(comment);

            // Assert
            Assert.IsType(typeOfResult, actual.Result);
        }
    }
}