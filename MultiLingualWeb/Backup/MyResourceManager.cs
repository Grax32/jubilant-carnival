using System.Globalization;
using System.Resources;

namespace MultiLingualWeb.Backup;

public class MyResourceManager : ResourceManager
{
    protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
        var reader = new MyResourceReader();
        return new ResourceSet(reader);
    }
}
