using SkillManagement.DataAccess.Interfaces;

namespace SkillManagement.DataAccess.Entities.SQLEntities
{
    public class SQLUser : IEntity<long>
    {
        public long Id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public bool is_admin { get; set; }
    }
}
