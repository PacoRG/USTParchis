using Domain.Model;
using Domain.Model.Enums;
using Domain.Persistence.Repositories;
using DomainServices.Services;
using DomainServices.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Services.Tests
{
    public class GameServiceTests
    {
        [Fact]
        public async Task Should_Break_If_Null_Game_Is_Passed()
        {
            var repositoryMock = new Mock<IGenericRepository<Game>>();
            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x => x.Validate(It.IsAny<Game>())).Returns(new List<ValidationModel>());

            var sut = new GameService(repositoryMock.Object, validationMock.Object);

            try
            {
                await sut.SaveGame(null);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public async Task Should_Add_If_Not_Exists()
        {
            var game = new Game
            {
                Id = 5,
                Name = "DoremIpusm"
            };

            Game emptyGame = null;
            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x => x.Validate(It.IsAny<Game>())).Returns(new List<ValidationModel>());
            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(emptyGame)).Verifiable();
            repositoryMock.Setup(x => x.AddAsyn(game)).Returns(Task.FromResult(game)).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var sut = new GameService(repositoryMock.Object, validationMock.Object);

            await sut.SaveGame(game);

            repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Should_Set_Correct_Values_On_Add()
        {
            var game = new Game
            {
                Id = 5,
                Name = "DoremIpusm",
                CreatedAt = DateTime.MinValue,
                ModifiedAt = DateTime.MinValue
            };

            Game emptyGame = null;
            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x => x.Validate(It.IsAny<Game>())).Returns(new List<ValidationModel>());
            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(emptyGame)).Verifiable();
            repositoryMock.Setup(x => x.AddAsyn(game)).Returns(Task.FromResult(game)).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var sut = new GameService(repositoryMock.Object, validationMock.Object);

            await sut.SaveGame(game);


            Assert.Equal(GameState.InProgress, game.State);
            Assert.NotEqual(DateTime.MinValue, game.CreatedAt);
            Assert.NotEqual(DateTime.MinValue, game.ModifiedAt);
        }

        [Fact]
        public async Task Should_Update_If_Exists()
        {
            var game = new Game
            {
                Id = 5,
                Name = "DoremIpusm"
            };

            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x => x.Validate(It.IsAny<Game>())).Returns(new List<ValidationModel>());
            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(game)).Verifiable();
            repositoryMock.Setup(x => x.UpdateAsyn(game, 5)).Returns(Task.FromResult(game)).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var sut = new GameService(repositoryMock.Object, validationMock.Object);

            await sut.SaveGame(game);

            repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Should_Set_Correct_Values_On_Update()
        {
            var game = new Game
            {
                Id = 5,
                Name = "DoremIpusm",
                ModifiedAt = DateTime.MinValue
            };

            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x => x.Validate(It.IsAny<Game>())).Returns(new List<ValidationModel>());
            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(game)).Verifiable();
            repositoryMock.Setup(x => x.UpdateAsyn(game, 5)).Returns(Task.FromResult(game)).Verifiable();
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var sut = new GameService(repositoryMock.Object, validationMock.Object);
            await sut.SaveGame(game);

            Assert.NotEqual(DateTime.MinValue, game.ModifiedAt);
        }

        [Fact]
        public async Task Should_Not_Call_SaveChanges_On_Validation_Incorrect()
        {
            var game = new Game { Id = 5 };
            var errorList = new List<ValidationModel>
            {
                new ValidationModel("asd", "", "", "")
            };

            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x =>
                x.Validate(It.IsAny<Game>()))
                .Returns(errorList);

            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.GetAsync(5)).Returns(Task.FromResult(game));
            repositoryMock.Setup(x => x.UpdateAsyn(game, 5)).Returns(Task.FromResult(game));
            repositoryMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(9)).Verifiable();

            var sut = new GameService(repositoryMock.Object, validationMock.Object);
            await sut.SaveGame(game);

            repositoryMock.Verify(service => service.Save(), Times.Never());
        }

        [Fact]
        public async Task Should_Call_Validation()
        {
            var game = new Game { Id = 5 };
            var errorList = new List<ValidationModel>();


            var validationMock = new Mock<IValidationService<Game>>();
            validationMock.Setup(x =>
                x.Validate(It.IsAny<Game>()))
                .Returns(errorList)
                .Verifiable();

            var repositoryMock = new Mock<IGenericRepository<Game>>();

            var sut = new GameService(repositoryMock.Object, validationMock.Object);
            await sut.SaveGame(game);

            validationMock.VerifyAll();
        }
    }
}
