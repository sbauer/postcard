using System.IO;

namespace Postcard
{
    public class Attachment
    {
        public string FileName { get; set; }
        public Stream Data { get; set; }
    }
}