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
    public class PostControllerTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly IValidator<Post> _validator;
        private readonly PostController _controllerUnitTesting;

        public PostControllerTests()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _postRepository = new Mock<IPostRepository>();
            _validator = new PostValidation();
            _controllerUnitTesting = new PostController(null, _commentRepository.Object, _postRepository.Object, _validator);
        }

        public static IEnumerable<object[]> SplitCountData =>
            new List<object[]>
            {
                new object[] { typeof(BadRequestObjectResult), new Post { Title = "Title Should not have more than 30 characters", Content = "New Content", CreationDate = System.DateTime.Now } },
                new object[] { typeof(BadRequestObjectResult), new Post { Title = "Title", Content = "New Content with more than 120 characters. New Content with more than 120 characters. New Content with more than 120 chara", CreationDate = System.DateTime.Now } },
            };

        [Theory, MemberData(nameof(SplitCountData))]
        public void Insert_Returns_BedRequest_When_Author_have_more_than_30_characters(Type typeOfResult, Post inTheory)
        {
            // Arrange
            _postRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns<Post>(null);

            // Act
            var actual = _controllerUnitTesting.Post(inTheory);

            // Assert
            Assert.IsType(typeOfResult, actual.Result);
        }
    }
}