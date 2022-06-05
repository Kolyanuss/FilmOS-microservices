using EFCoreCodeFirstSampleWEBAPI.DAL;
using EFCoreCodeFirstSampleWEBAPI.DAL.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSampleWEBAPI.IntergationTests.Common
{
    class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(MyAppContext context)
        {
            context.Films.Add(
            new Films
            {
                NameFilm = "Film for test",
                ReleaseData = new DateTime(1991, 01, 01),
                Country = "memory",
                FKDescriptionId = 1
            });
            context.Films.Add(
            new Films
            {
                NameFilm = "Film for DELETE",
                ReleaseData = new DateTime(1991, 01, 01),
                Country = "memory",
                FKDescriptionId = 1
            });
            context.Films.Add(
            new Films
            {
                NameFilm = "Film for DELETE2",
                ReleaseData = new DateTime(1991, 01, 01),
                Country = "memory",
                FKDescriptionId = 1
            });
            context.SaveChanges();
        }
    }
}
