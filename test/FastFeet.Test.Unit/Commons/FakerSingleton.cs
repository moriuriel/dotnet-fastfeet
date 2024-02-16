using Bogus;

namespace FastFeet.Test.Unit.Commons;

public sealed class FakerSingleton
{
    public Faker Faker { get; } = new Faker("pt_BR");

    private static FakerSingleton? _instance;
    private static readonly object _lock = new();

    private FakerSingleton()
    {
    }

    public static FakerSingleton GetInstance()
    {
        if (_instance is null)
        {
            lock (_lock)
                _instance ??= new FakerSingleton();
        }

        return _instance;
    }
}