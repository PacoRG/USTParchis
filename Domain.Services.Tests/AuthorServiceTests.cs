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
            var validationMock = new Mock<IValidationService>();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);

            var authors = await sut.GetAll();

            repositoryMock.VerifyAll();

            Assert.Equal(authorList, authors);
        }

        [Fact]
        public async Task Should_Delete_Author()
        {
            var author = new Author { Id = 1};
            var repositoryMock = new Mock<IGenericRepository<Author>>();
            repositoryMock.Setup(x => x.GetAsync(1)).Returns(Task.FromResult(author)).Verifiable();
            repositoryMock.Setup(x => x.DeleteAsyn(1)).Returns(Task.CompletedTask).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(It.IsAny<int>())).Verifiable();
            var validationMock = new Mock<IValidationService>();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);

            await sut.Delete(1);

            repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Should_Throw_Exception_If_Author_Does_Not_Exist()
        {
            var repositoryMock = new Mock<IGenericRepository<Author>>();
            repositoryMock.Setup(x => x.GetAsync(1)).Returns(Task.FromResult<Author>(null)).Verifiable();
            var validationMock = new Mock<IValidationService>();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);

            await Assert.ThrowsAsync<Exception>(async () => await sut.Delete(1));
        }

        [Fact]
        public async Task Should_Break_If_Null_On_Save()
        {
            var sut = new AuthorService(null, null);

            await Assert.ThrowsAsync<NullReferenceException>(async () => await sut.Save(null));
        }


        [Fact]
        public async Task Should_Call_Validation()
        {
            var author = new Author { Id = 5 };
            var errorList = new List<ValidationModel>();

            var validationMock = new Mock<IValidationService>();
            validationMock.Setup(x =>
                x.Validate(It.IsAny<Author>()))
                .Returns(errorList)
                .Verifiable();

            var repositoryMock = new Mock<IGenericRepository<Author>>();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);
            await sut.Save(author);

            validationMock.VerifyAll();
        }

        [Fact]
        public async Task Should_Not_Call_SaveChanges_On_Validation_Incorrect()
        {
            var author = new Author { Id = 5 };
            var errorList = new List<ValidationModel>
            {
                new ValidationModel("asd", "", "", "")
            };

            var validationMock = new Mock<IValidationService>();
            validationMock.Setup(x =>
                x.Validate(It.IsAny<Author>()))
                .Returns(errorList)
                .Verifiable();

            var repositoryMock = new Mock<IGenericRepository<Author>>();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);
            var validationError = await sut.Save(author);

            Assert.True(validationError.Count > 0);
            repositoryMock.Verify(service => service.Save(), Times.Never());
        }

        [Fact]
        public async Task Should_Add_If_Not_Exists()
        {
            var author = new Author
            {
                Id = 5,
                Name = "DoremIpusm"
            };

            Author emptyAuthor = null;
            var validationMock = new Mock<IValidationService>();
            validationMock.Setup(x => x.Validate(It.IsAny<Author>())).Returns(new List<ValidationModel>());
            var repositoryMock = new Mock<IGenericRepository<Author>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(emptyAuthor)).Verifiable();
            repositoryMock.Setup(x => x.AddAsyn(author)).Returns(Task.FromResult(author)).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);

            await sut.Save(author);

            repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Should_Update_If_Exists()
        {
            var author = new Author
            {
                Id = 5,
                Name = "DoremIpusm"
            };

            var validationMock = new Mock<IValidationService>();
            validationMock.Setup(x => x.Validate(It.IsAny<Author>())).Returns(new List<ValidationModel>());
            var repositoryMock = new Mock<IGenericRepository<Author>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(author)).Verifiable();
            repositoryMock.Setup(x => x.UpdateAsyn(author, 5)).Returns(Task.FromResult(author)).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var sut = new AuthorService(repositoryMock.Object, validationMock.Object);

            await sut.Save(author);

            repositoryMock.VerifyAll();
        }

    }
}
