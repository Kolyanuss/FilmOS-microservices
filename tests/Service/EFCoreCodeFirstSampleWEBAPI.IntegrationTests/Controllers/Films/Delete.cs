using EFCoreCodeFirstSampleWEBAPI.IntergationTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFCoreCodeFirstSampleWEBAPI.IntegrationTests.Controllers.Films
{
    public class Delete : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public Delete(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async Task Delete_ValidId_ExpectCode204NoContent(int id)
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync("/api/Films/" + id);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public async Task Delete_nonexistId_ExpectCode404NotFound(int id)
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.DeleteAsync("/api/Films/" + id);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
