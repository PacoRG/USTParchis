using Domain.Model;
using Domain.Model.Enums;
using Domain.Persistence.Repositories;
using DomainServices.Services;
using DomainServices.Services.Band;
using DomainServices.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Services.Tests
{
    public class AuthorServcieTest
    {
        [Fact]
        public async Task Should_Get_All_Authors()
        {
            var authorList = new List<Author>();
            var author = new Author{ Name = "FirstAuthor"};
            var author2 = new Author { Name = "SecondAuthorAuthor" };

            var repositoryMock = new Mock<IGenericRepository<Author>>();
            repositoryMock.Setup(x => x.GetAllAsyn()).Returns(Task.FromResult((ICollection<Author>)authorList)).Verifiable();

            var sut = new AuthorService(repositoryMock.Object);

            var authors = await sut.GetAll();

            repositoryMock.VerifyAll();

            Assert.Equal((ICollection<Author>)authorList, authors);
        }
    }
}
