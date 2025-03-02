using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppTreino.Domain.Models
{
    public abstract class Entity
    {
        public long Id { get; set; }
        protected Entity()
        {

        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
    public enum EntityState
    {
        Insert = 1,
        Alter = 2,
        Delete = 3
    }

    public abstract class EntityDataBase : Entity
    {

        public void SetDeleteEntity(long UserDeletedId)
        {
            Active = false;
            EntityState = EntityState.Delete;
            DeleteDate = DateTime.Now;
            this.UserDeletedId = UserDeletedId;
        }

        public void SetUpdateEntity(long UserUpdatedId)
        {
            Active = true;
            EntityState = EntityState.Delete;
            DateUpdate = DateTime.Now;
            this.UserUpdatedId = UserUpdatedId;
        }

        public void SetId(long id)
        {
            Id = id;
        }
        public void SetInsertEntity(long UserInsertedId)
        {
            Active = true;
            EntityState = EntityState.Insert;
            DateRegister = DateTime.Now;
            this.UserInsertedId = UserInsertedId;
        }


        protected EntityDataBase() : base()
        {

            Active = true;
            EntityState = EntityState.Insert;
        }

        public bool Active { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long UserInsertedId { get; set; }
        public long? UserUpdatedId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public long? UserDeletedId { get; set; }

        [NotMapped]
        public EntityState EntityState { get; set; }

    }
}
