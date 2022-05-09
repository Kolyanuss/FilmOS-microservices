using System;

namespace AppMyFilm.DAL.Exceptions.Abstract
{
    public sealed class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
