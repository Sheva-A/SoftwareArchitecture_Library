namespace Library.Domain.Common;

// Базовий клас для об'єктів-значень (Value Object).
// На відміну від Entity, ValueObject не має власного Id —
// два об'єкти вважаються рівними, якщо рівні всі їхні атрибути.
public abstract class ValueObject
{
    // Підкласи повертають усі атрибути, що беруть участь у порівнянні
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
        => GetEqualityComponents()
            .Select(c => c?.GetHashCode() ?? 0)
            .Aggregate((a, b) => a ^ b);

    public static bool operator ==(ValueObject? left, ValueObject? right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(ValueObject? left, ValueObject? right)
        => !(left == right);
}
