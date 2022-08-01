using System.Collections;
using System.Resources;

namespace MultiLingualWeb.Backup;

public class MyResourceReader : Dictionary<string, object>, IResourceReader
{
    private bool _isDisposed;

    public void Close()
    {
    }

    IDictionaryEnumerator IResourceReader.GetEnumerator() => GetEnumerator();

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
