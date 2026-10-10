namespace Library.Domain.Common;

// Помилка, коли порушено бізнес-правило
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
