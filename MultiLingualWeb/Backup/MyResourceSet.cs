using System.Resources;

namespace MultiLingualWeb.Backup;

public class MyResourceSet : ResourceSet
{
    public override string? GetString(string name, bool ignoreCase)
    {
        return base.GetString(name, ignoreCase);
    }
}
