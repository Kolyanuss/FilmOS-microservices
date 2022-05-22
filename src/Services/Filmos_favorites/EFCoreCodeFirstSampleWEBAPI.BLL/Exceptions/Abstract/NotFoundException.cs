using System;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message)
            : base(message)
        {
        }
    }
}
