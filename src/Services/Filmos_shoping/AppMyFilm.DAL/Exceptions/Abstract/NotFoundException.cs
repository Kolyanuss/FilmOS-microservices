using System;

namespace AppMyFilm.DAL.Exceptions.Abstract
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message)
            : base(message)
        {
        }
    }
}
