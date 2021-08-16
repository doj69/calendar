using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Appointment.API.Entities
{
    public class AppointmentEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime Start { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime End { get; set; }
        public string Color { get; set; }
        public bool Timed { get; set; }
    }
}
