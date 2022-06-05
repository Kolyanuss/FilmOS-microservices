using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.IntergationTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreCodeFirstSampleWEBAPI.IntegrationTests.Controllers.Films
{
    public class Put : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Put(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Put_ValidIdAndValidRequest_ExpectCode204NoContent(int id)
        {
            // Arrange
            var client = _factory.GetAnonymousClient();
            string putFilm = "Put Film";

            // Aact
            var responsePut = await client.PutAsync("/api/Films/" + id, Utilities.GetRequestContent(
                new FilmsForCreationDto()
                {
                    Country = "Country with id " + id,
                    Data = new DateTime(1991, 01, 01),
                    FKDescriptionId = 1,
                    NameFilm = putFilm
                }));
            responsePut.EnsureSuccessStatusCode();

            var responseGet = await client.GetAsync("/api/Films/" + id);
            responseGet.EnsureSuccessStatusCode();
            var rez = await Utilities.GetResponseContent<FilmsDTO>(responseGet);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, responsePut.StatusCode);

            Assert.Equal(System.Net.HttpStatusCode.OK, responseGet.StatusCode);
            Assert.IsAssignableFrom<FilmsDTO>(rez);
            Assert.Equal(id, rez.Id);
            Assert.Equal(putFilm, rez.NameFilm);
        }
/*
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public async Task Put_nonexistId_ExpectCode404NotFound(int id)
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync("/api/Films/" + id);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }*/
    }
}
