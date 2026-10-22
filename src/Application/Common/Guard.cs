namespace Application.Common;

internal static class Guard
{
    public static T Found<T>(T? entity, string name) where T : class
    {
        return entity ?? throw new KeyNotFoundException($"{name} not found");
    }
}
