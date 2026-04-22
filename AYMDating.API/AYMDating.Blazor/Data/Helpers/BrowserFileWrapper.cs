using Microsoft.AspNetCore.Components.Forms;

namespace AYMDating.Blazor.Data.Helpers
{
    public class BrowserFileWrapper : IFormFile
    {
        private readonly IBrowserFile _browserFile;

        public BrowserFileWrapper(IBrowserFile browserFile)
        {
            _browserFile = browserFile;
        }

        public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{_browserFile.Name}\"";
        public string ContentType => _browserFile.ContentType;
        public IHeaderDictionary Headers => new HeaderDictionary();
        public long Length => _browserFile.Size;
        public string Name => _browserFile.Name;

        //AYM
        public string FileName => _browserFile.Name;

        public Stream OpenReadStream()
        {
            // Open the stream from the IBrowserFile
            return _browserFile.OpenReadStream();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            return _browserFile.OpenReadStream().CopyToAsync(target, cancellationToken);
        }

        public Task CopyToAsync(Stream target)
        {
            return _browserFile.OpenReadStream().CopyToAsync(target);
        }

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }
    }
}
