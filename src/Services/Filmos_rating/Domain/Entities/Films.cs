using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Filmos_Rating_CleanArchitecture.Domain.Entities
{
    public class Films
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id_film { get; set; }
        public string Name_Film { get; set; }
    }
}
