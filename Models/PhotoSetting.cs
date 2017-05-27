using System.IO;
using System.Linq;

namespace vega_demo.Models
{
    public class PhotoSetting
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupportedFileType(string fileName)
        {
            return AcceptedFileTypes.Any(x => x == Path.GetExtension(fileName).ToLower());
        }
    }
}