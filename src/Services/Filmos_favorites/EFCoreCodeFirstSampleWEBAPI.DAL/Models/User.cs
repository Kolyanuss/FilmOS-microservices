using System.Collections.Generic;

namespace EFCoreCodeFirstSampleWEBAPI.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<FilmsUsers> FilmsUsers { get; set; }

        public User()
        {
            FilmsUsers = new List<FilmsUsers>();
        }
    }
}
