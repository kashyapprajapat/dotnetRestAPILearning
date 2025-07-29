using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotrestapiwithmongo.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Course { get; set; }
    }
}
