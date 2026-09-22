namespace Library.Domain.Common;

// Базовий клас для всіх сутностей.
// Кожна сутність ідентифікується унікальним Id — це ключова ознака Entity у DDD.
public abstract class Entity
{
    public Guid Id { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other || GetType() != other.GetType())
            return false;
        return Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();
}
