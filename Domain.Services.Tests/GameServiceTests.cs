using Domain.Model;
using Domain.Model.Enums;
using Domain.Persistence.Repositories;
using DomainServices.Services;
using Moq;
using System;
using Xunit;

namespace Domain.Services.Tests
{
    public class GameServiceTests
    {
        [Fact]
        public void Should_Break_If_Null_Game_Is_Passed()
        {
            var repositoryMock = new Mock<IGenericRepository<Game>>();

            var sut = new GameService(repositoryMock.Object);

            try
            {
                sut.SaveGame(null);
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void Should_Add_If_Not_Exists()
        {
            var game = new Game();
            game.Id = 5;
            game.Name = "DoremIpusm";

            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.Get(5)).Returns<Game>(null).Verifiable();
            repositoryMock.Setup(x => x.Add(game)).Verifiable();
            repositoryMock.Setup(x => x.Save()).Verifiable();

            var sut = new GameService(repositoryMock.Object);
            sut.SaveGame(game);

            repositoryMock.VerifyAll();
        }

        [Fact]
        public void Should_Set_Correct_Values_On_Add()
        {
            var game = new Game();
            game.Id = 5;
            game.Name = "DoremIpusm";
            game.CreatedAt = DateTime.MinValue;
            game.ModifiedAt = DateTime.MinValue;

            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.Get(5)).Returns<Game>(null);
            repositoryMock.Setup(x => x.Add(game));
            repositoryMock.Setup(x => x.Save());

            var sut = new GameService(repositoryMock.Object);
            sut.SaveGame(game);

            Assert.Equal(GameState.InProgress, game.State);
            Assert.NotEqual(DateTime.MinValue, game.CreatedAt);
            Assert.NotEqual(DateTime.MinValue, game.ModifiedAt);
        }

        [Fact]
        public void Should_Update_If_Not_Exists()
        {
            var game = new Game();
            game.Id = 5;
            game.Name = "DoremIpusm";

            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.Get(5)).Returns(game).Verifiable();
            repositoryMock.Setup(x => x.Update(game, 5)).Verifiable();
            repositoryMock.Setup(x => x.Save()).Verifiable();

            var sut = new GameService(repositoryMock.Object);
            sut.SaveGame(game);

            repositoryMock.VerifyAll();
        }

        [Fact]
        public void Should_Set_Correct_Values_On_Update()
        {
            var game = new Game();
            game.Id = 5;
            game.Name = "DoremIpusm";
            game.ModifiedAt = DateTime.MinValue;

            var repositoryMock = new Mock<IGenericRepository<Game>>();
            repositoryMock.Setup(x => x.Get(5)).Returns(game).Verifiable();
            repositoryMock.Setup(x => x.Update(game, 5)).Verifiable();
            repositoryMock.Setup(x => x.Save()).Verifiable();

            var sut = new GameService(repositoryMock.Object);
            sut.SaveGame(game);

            Assert.NotEqual(DateTime.MinValue, game.ModifiedAt);
        }
    }
}
