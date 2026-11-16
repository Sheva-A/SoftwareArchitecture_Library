namespace Library.Domain.Common;

// Абстракція поточного часу, реалізація в Infrastructure
public interface IClock
{
    DateTime Now { get; }
}
