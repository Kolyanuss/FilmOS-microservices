using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Filmos_Rating_CleanArchitecture.Domain.Entities
{
    public class Comment
    {
        public Comment()
        {
            Films = new HashSet<Films>();
            Users = new HashSet<Users>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id_comment { get; set; }
        public string Text { get; set; }
        public ICollection<Films> Films { get; private set; }
        public ICollection<Users> Users { get; private set; }
    }
}
