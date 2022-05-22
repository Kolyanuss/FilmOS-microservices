using System;

namespace Shoping.DAL.Exceptions.Abstract
{
    public sealed class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
