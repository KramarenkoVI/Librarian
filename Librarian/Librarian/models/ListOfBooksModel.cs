using System.Xml.Serialization;

namespace Librarian.models
{
    [XmlRoot("library")]
    public class ListOfBooksModel
    {
        [XmlElement("book")]
        public List<BookModel> Books { get; set; } = new();
    }
}
