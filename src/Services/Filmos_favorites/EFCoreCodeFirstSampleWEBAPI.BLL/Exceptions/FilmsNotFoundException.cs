using EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions
{
    public sealed class FilmsNotFoundException : NotFoundException
    {
        public FilmsNotFoundException(int Id)
            : base($"The film with the identifier {Id} was not found.")
        {
        }
    }
}
