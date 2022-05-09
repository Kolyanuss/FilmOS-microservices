using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Filmos_Rating_CleanArchitecture.Domain.Entities
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id_user { get; set; }
        public string User_name { get; set; }
        public bool Is_admin { get; set; }
    }
}
