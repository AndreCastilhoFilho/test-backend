using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using PrBanco.Domain.Entities;
using PrBanco.Domain.ValueObjects;
using System;

namespace PrBanco.Data.MongoDataAccess.ClassMaps
{
    public class PersonClassMap : MongoDbClassMap<Person>
    {
        public override void Map(BsonClassMap<Person> cm)
        {
            MapNestedValueObjects();

            cm.AutoMap();
            cm.MapCreator(d => new Person(d.Id, new Name(d.Name.FirstName, d.Name.LastName), new Email(d.Email.Address), new Phone(d.Phone.Number), d.Address));
            cm.MapMember(d => d.Name).SetIsRequired(true);
            cm.MapMember(d => d.Email).SetIsRequired(true);
            cm.MapMember(d => d.Phone);
            cm.MapMember(d => d.Address);
        }


        private static void MapNestedValueObjects()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Address)))
                BsonClassMap.RegisterClassMap<Address>(dcm =>
                {
                    dcm.AutoMap();
                    dcm.MapCreator(d => new Address(d.Street, d.Number, d.Neighborhood, d.City, d.State, d.Country, d.ZipCode));
                });


            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
                BsonClassMap.RegisterClassMap<Entity>(dcm =>
                {
                    dcm.AutoMap();
                    dcm.MapIdProperty(c => c.Id).SetSerializer(new GuidSerializer())
                    .SetIdGenerator(GuidGenerator.Instance);

                });
        }
    }
}
