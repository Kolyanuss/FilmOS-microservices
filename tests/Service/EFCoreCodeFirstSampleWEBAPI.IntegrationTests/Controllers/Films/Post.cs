using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.IntergationTests.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreCodeFirstSampleWEBAPI.IntegrationTests.Controllers.Films
{
    public class Post : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Post(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_ValidRequest_ExpectRedirectToGetByIdAndCode201Created()
        {
            // Arrange
            var client = _factory.GetAnonymousClient();
            string PostFilm = "Post Film";
            var newFilm = new FilmsForCreationDto()
            {
                Country = "Country",
                Data = new DateTime(1991, 01, 01),
                FKDescriptionId = 1,
                NameFilm = PostFilm
            };

            // Aact
            var responsePost = await client.PostAsync("/api/Films/", Utilities.GetRequestContent(newFilm));
            responsePost.EnsureSuccessStatusCode();
            var rez = await Utilities.GetResponseContent<FilmsDTO>(responsePost);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, responsePost.StatusCode);
            Assert.IsType<FilmsDTO>(rez);
            Assert.Equal(6, rez.Id);
            Assert.Equal(newFilm.NameFilm, rez.NameFilm);
            Assert.Equal(newFilm.Country, rez.Country);
            Assert.Equal(newFilm.Data, rez.Data);
            Assert.Equal(newFilm.FKDescriptionId, rez.FKDescriptionId);
        }


        [Fact]
        public async Task Post_InvalidRequest_ExpectCode400Badrequest()
        {
            // Arrange
            var client = _factory.GetAnonymousClient();

            // Aact
            var responsePost = await client.PostAsync("/api/Films/", Utilities.GetRequestContent(
                new FilmsForCreationDto()
                {
                    Country = "Country",
                    Data = new DateTime(1991, 01, 01),
                    FKDescriptionId = 1,
                    NameFilm = ""
                }));

            // Assert            
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, responsePost.StatusCode);
        }

        [Fact]
        public async Task Post_EmptyRequest_ExpectCode400Badrequest()
        {
            // Arrange
            var client = _factory.GetAnonymousClient();
            string PostFilm = "Post Film";
            FilmsForCreationDto emprtyfilm = null;
            // Aact
            var responsePost = await client.PostAsync("/api/Films/", Utilities.GetRequestContent(emprtyfilm));

            // Assert            
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, responsePost.StatusCode);
        }
    }
}
