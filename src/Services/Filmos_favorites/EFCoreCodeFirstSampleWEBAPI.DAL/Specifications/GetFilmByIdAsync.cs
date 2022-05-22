using EFCoreCodeFirstSampleWEBAPI.DAL.Models;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Specifications
{
    public class GetFilmByIdAsync : BaseSpecifcation<Films>
    {
        public GetFilmByIdAsync(int id) : base(e => e.Id == id) { }
    }
}
