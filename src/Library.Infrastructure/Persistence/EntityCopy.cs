using System.Reflection;

namespace Library.Infrastructure.Persistence;

// Робить копію об'єкта, щоб зміни не потрапляли у сховище до збереження
internal static class EntityCopy
{
    private static readonly MethodInfo Clone =
        typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public static T Of<T>(T entity) where T : class
    {
        return (T)Clone.Invoke(entity, null)!;
    }
}
