using System.Xml.Serialization;

namespace Librarian.models
{
    public class BookModel
    {
        [XmlElement("title")]
        public string? Title { get; set; }
        [XmlElement("author")]
        public string? Author { get; set; }
        [XmlElement("pages")]
        public int Pages { get; set; }
    }
}
