using EFCoreCodeFirstSampleWEBAPI.BLL.DataTransferObjects;
using EFCoreCodeFirstSampleWEBAPI.IntergationTests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreCodeFirstSampleWEBAPI.IntegrationTests.Controllers.Films
{
    public class Get : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Get(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_Correct_ReturnsListFilmsDto()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/Films");

            response.EnsureSuccessStatusCode();

            var rez = await Utilities.GetResponseContent<IEnumerable<FilmsDTO>>(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<FilmsDTO>>(rez);
            Assert.NotEmpty(rez);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetById_ValidId_ReturnsFilmsDto(int id)
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/Films/" + id);

            response.EnsureSuccessStatusCode();

            var rez = await Utilities.GetResponseContent<FilmsDTO>(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.IsAssignableFrom<FilmsDTO>(rez);
            Assert.Equal(id, rez.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public async Task GetById_nonexistId_ExpectCode404(int id)
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/Films/" + id);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
