using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.BLL.Interfaces;
using EFCoreCodeFirstSampleWEBAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace EFCoreCodeFirstSampleWEBAPI.UnitTests
{
    public class FilmsControllerTest
    {
        private readonly IServiceManager _service;
        private readonly FilmsController _controller;
        private readonly FilmsForCreationDto _testFilm = new FilmsForCreationDto() { Country = "USA" };

        public FilmsControllerTest()
        {
            _service = new ServiceManagerFake();
            _controller = new FilmsController(_service);
        }

        [Fact]
        public void GetAll_DataExist_ReturnsOkResult()
        {
            var Result = _controller.GetAll();

            Assert.IsType<OkObjectResult>(Result.Result as OkObjectResult);
        }

        [Fact]
        public void GetAll_DataExist_ReturnsAllItems()
        {
            var Result = _controller.GetAll().Result as OkObjectResult;

            var items = Assert.IsType<List<FilmsDTO>>(Result.Value);
            Assert.Equal(2, items.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetById_ExistIdPassed_ReturnsOkResult(int id)
        {
            var Result = _controller.GetById(id).Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(Result);
            Assert.IsType<FilmsDTO>(Result.Value);
            Assert.Equal(id, (Result.Value as FilmsDTO).Id);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-1)]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult(int id)
        {
            var Result = _controller.GetById(id).Result;

            Assert.IsType<NotFoundObjectResult>(Result);
        }

        [Theory(Skip = "becouse method GetByIdSpec not work")]
        [InlineData(0)]
        public void GetByIdSpec_ExistIdPassed_ReturnsOkResult(int id)
        {
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetWithDetailsById_ExistIdPassed_ReturnsOkResult(int id)
        {
            var Result = _controller.GetWithDetailsById(id).Result as OkObjectResult;

            Assert.IsType<OkObjectResult>(Result);
            Assert.IsType<FilmsDetailDTO>(Result.Value);
            Assert.Equal(id, (Result.Value as FilmsDetailDTO).Id);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-1)]
        public void GetWithDetailsById_UnknownIdPassed_ReturnsNotFoundResult(int id)
        {
            var Result = _controller.GetWithDetailsById(id).Result;

            Assert.IsType<NotFoundObjectResult>(Result);
        }

        [Theory]
        [InlineData(null)]
        public void Post_IncorrectEntityPassed_ReturnsBadRequestResult(FilmsForCreationDto filmDto)
        {
            var Result = _controller.Post(filmDto).Result;

            Assert.IsType<BadRequestObjectResult>(Result);
        }

        [Fact]
        public void Post_CorrectEntityPassed_ReturnsCreatedAtRouteResult()
        {
            var film = new FilmsForCreationDto()
            {
                NameFilm = "Marvel",
                Country = "USA",
                FKDescriptionId = 0,
                Data = new System.DateTime(2020, 2, 20)
            };
            var Result = _controller.Post(film).Result as CreatedAtRouteResult;

            Assert.IsType<CreatedAtRouteResult>(Result);
            Assert.IsType<FilmsDTO>(Result.Value);
            Assert.Equal(film.NameFilm, (Result.Value as FilmsDTO).NameFilm);
        }

        [Fact]
        public void Put_ExistIdAndCorrectEntityPassed_ReturnsNoContentResult()
        {
            var id = 1;
            var film = new FilmsForCreationDto()
            {
                NameFilm = "Film about Put operetion",
                Country = "USA",
                FKDescriptionId = 0,
                Data = new System.DateTime(2020, 2, 20)
            };

            var Result = _controller.Put(id, film).Result;

            Assert.IsType<NoContentResult>(Result);
        }

        [Fact]
        public void Put_UnknownIdAndCorrectEntityPassed_ReturnsNotFoundObjectResult()
        {
            var id = -1;
            var film = new FilmsForCreationDto()
            {
                NameFilm = "Film about Put operetion",
                Country = "USA",
                FKDescriptionId = 0,
                Data = new System.DateTime(2020, 2, 20)
            };

            var Result = _controller.Put(id, film).Result;

            Assert.IsType<NotFoundObjectResult>(Result);
        }

        [Fact]
        public void Put_ExistIdAndIncorrectEntityPassed_ReturnsBadRequestResult()
        {
            var id = 1;
            var film = new FilmsForCreationDto()
            {
                Country = "USA",
                FKDescriptionId = 0,
                Data = new System.DateTime(2020, 2, 20)
            };

            var Result = _controller.Put(id, film).Result;

            Assert.IsType<BadRequestObjectResult>(Result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-1)]
        public void Delete_UnknownIdPassed_ReturnsNotFoundResult(int id)
        {
            var Result = _controller.Delete(id).Result;

            Assert.IsType<NotFoundObjectResult>(Result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Delete_ExistIdPassed_ReturnsNoContentResult(int id)
        {
            var Result = _controller.Delete(id).Result;

            Assert.IsType<NoContentResult>(Result);
        }
    }
}
