namespace Library.Domain.Common;

// Маркерний інтерфейс для всіх доменних подій
public interface IDomainEvent
{
    // Момент виникнення події
    DateTime OccurredAt { get; }
}
