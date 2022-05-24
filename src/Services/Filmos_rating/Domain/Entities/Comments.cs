using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Filmos_Rating_CleanArchitecture.Domain.Entities
{
    public class Comments
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id_comment { get; set; }
        public int _id_sql_film { get; set; }
        public int _id_sql_user { get; set; }
        public string Text { get; set; }
    }
}
