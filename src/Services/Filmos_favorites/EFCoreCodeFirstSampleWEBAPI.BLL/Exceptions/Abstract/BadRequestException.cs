using System;

namespace EFCoreCodeFirstSampleWEBAPI.BLL.Exceptions.Abstract
{
    public sealed class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
