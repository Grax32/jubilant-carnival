using Microsoft.Extensions.Localization;

namespace MultiLingualWeb;

public class MyStringLocalizerFactory : IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource) => Create();
    public IStringLocalizer Create(string baseName, string location) => Create();

    private static IStringLocalizer Create() => new MyStringLocalizer(typeof(MyStringLocalizerFactory), null!);
}
