using Flunt.Notifications;
using System;

namespace PrBanco.Domain.Entities
{
    public class Entity : Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            Updated = DateTimeOffset.Now;
        }

        public Guid Id { get; protected set; }
        public DateTimeOffset Created { get; protected set; }
        public DateTimeOffset Updated { get; protected set; }

    }
}
