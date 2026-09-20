using System.Xml.Serialization;

namespace Librarian.models
{
    public class BookModel
    {
        [XmlElement("title")]
        public required string Title { get; set; }
        [XmlElement("author")]
        public required string Author { get; set; }
        [XmlElement("pages")]
        public int Pages { get; set; }
    }
}
