using AutoMapper;
using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions;
using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;
using EFCoreCodeFirstSampleWEBAPI.BLL.Services.SQLServices;
using EFCoreCodeFirstSampleWEBAPI.BLL.UnitTests.Mocks;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using EventBus.Messages.Events;
using MassTransit;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.UnitTests
{
    public class FilmsServiceTest
    {
        private readonly Mapper _mapper;
        private readonly FakeRepositoryWrapper _repoWraper;
        private readonly Mock<IPublishEndpoint> _publishEndpoint;

        public FilmsServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<MappingProfile>()
                );
            _mapper = new Mapper(config);
            _repoWraper = new FakeRepositoryWrapper();
            _publishEndpoint = new Mock<IPublishEndpoint>();
        }

        [Fact]
        public void GetById_ValidIdPassed_ReturnsTaskFilmsDTO()
        {
            //Arrange
            _repoWraper._films.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(new Films()));

            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);

            //Act
            var Result = service.GetById(1);

            //Assert
            Assert.IsType<Task<FilmsDTO>>(Result);
        }

        [Fact]
        public async Task GetById_InvalidIdPassed_ReturnsFilmsNotFoundException()
        {
            //Arrange
            Films film = null;
            _repoWraper._films.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(film));

            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);

            //Act + Assert            
            await Assert.ThrowsAsync<FilmsNotFoundException>(() => service.GetById(1));
        }

        [Fact]
        public void GetWithDetailsById_ValidIdPassed_ReturnsTaskFilmsDetailDTO()
        {
            //Arrange
            _repoWraper._films.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(new Films()));

            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);

            //Act
            var Result = service.GetWithDetailsById(1);

            //Assert
            Assert.IsType<Task<FilmsDetailDTO>>(Result);
        }

        [Fact]
        public async Task GetWithDetailsById_InvalidIdPassed_ReturnsFilmsNotFoundException()
        {
            //Arrange
            Films film = null;
            _repoWraper._films.Setup(x => x.GetByIdWithDetailsAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(film));

            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);

            //Act + Assert            
            await Assert.ThrowsAsync<FilmsNotFoundException>(() => service.GetWithDetailsById(1));
        }

        [Fact]
        public async Task Post_ValidDtoPassed_ReturnsTaskFilmsDTO()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var filmDto = new FilmsForCreationDto() { NameFilm = "valid" };

            //Act
            var Result = service.Post(filmDto);

            //Assert
            await Assert.IsType<Task<FilmsDTO>>(Result);
            _repoWraper._films.Verify(x => x.Add(It.IsAny<Films>()), Times.Once);
            //_publishEndpoint.Verify(x => x.Publish(It.IsAny<FilmsDtoEvent>()), Times.Once); // something  wrong
        }

        [Fact]
        public async Task Post_InvalidDtoPassed_ReturnsBadRequestException()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var filmDto = new FilmsForCreationDto();

            //Act + Assert            
            await Assert.ThrowsAsync<BadRequestException>(() => service.Post(filmDto));
            _repoWraper._films.Verify(x => x.Add(It.IsAny<Films>()), Times.Never);
        }

        [Fact]
        public async Task Post_InvalidNullDtoPassed_ReturnsBadRequestException()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            FilmsForCreationDto filmDto = null;

            //Act + Assert            
            await Assert.ThrowsAsync<BadRequestException>(() => service.Post(filmDto));
            _repoWraper._films.Verify(x => x.Add(It.IsAny<Films>()), Times.Never);
        }

        [Fact]
        public async Task Put_ValidIdAndDtoPassed_NoReturns()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var id = 1;
            var filmDto = new FilmsForCreationDto() { NameFilm = "valid" };
            Films Film = new Films();

            _repoWraper._films.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(Film));

            //Act
            var Result = service.Put(id, filmDto);

            //Assert
            //await Assert.IsType<Task<VoidTaskResult>>(Result);
            _repoWraper._films.Verify(x => x.Update(It.IsAny<Films>()), Times.Once);
        }

        [Fact]
        public async Task Put_InvalidDtoPassed_ReturnsBadRequestException()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var id = 1;
            var filmDto = new FilmsForCreationDto();

            //Act + Assert            
            await Assert.ThrowsAsync<BadRequestException>(() => service.Put(id, filmDto));
            _repoWraper._films.Verify(x => x.Update(It.IsAny<Films>()), Times.Never);
        }

        [Fact]
        public async Task Put_InvalidNullDtoPassed_ReturnsBadRequestException()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var id = 1;
            FilmsForCreationDto filmDto = null;

            //Act + Assert            
            await Assert.ThrowsAsync<BadRequestException>(() => service.Put(id, filmDto));
            _repoWraper._films.Verify(x => x.Update(It.IsAny<Films>()), Times.Never);
        }

        [Fact]
        public async Task Put_InvalidIdPassed_ReturnsFilmsNotFoundException()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var id = 1;
            var filmDto = new FilmsForCreationDto() { NameFilm = "valid" };
            Films nullFilm = null;

            _repoWraper._films.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(nullFilm));

            //Act + Assert            
            await Assert.ThrowsAsync<FilmsNotFoundException>(() => service.Put(id, filmDto));
            _repoWraper._films.Verify(x => x.Update(It.IsAny<Films>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ValidIdPassed_NoReturns()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var id = 1;
            Films Film = new Films();

            _repoWraper._films.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(Film));

            //Act
            var Result = service.Delete(id);

            //Assert
            //await Assert.IsType<Task<VoidTaskResult>>(Result);
            _repoWraper._films.Verify(x => x.Delete(It.IsAny<Films>()), Times.Once);
        }

        [Fact]
        public async Task Delete_InvalidIdPassed_ReturnsFilmsNotFoundException()
        {
            //Arrange
            var service = new FilmsService(_repoWraper, _mapper, _publishEndpoint.Object);
            var id = 1;
            Films nullFilm = null;

            _repoWraper._films.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                          .Returns(Task.FromResult(nullFilm));

            //Act + Assert            
            await Assert.ThrowsAsync<FilmsNotFoundException>(() => service.Delete(id));
            _repoWraper._films.Verify(x => x.Delete(It.IsAny<Films>()), Times.Never);
        }
    }
}
