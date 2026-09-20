using Librarian.models;
using Librarian.interfaces;
using System.Xml.Serialization;
namespace Librarian.core
{
    public class TransferXML : IDataTransfer
    {
        public List<BookModel> GetData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new Exception("File not found");
                List<BookModel> books = new List<BookModel>();
                var reader = new XmlSerializer(typeof(ListOfBooksModel));
                using var stream = File.OpenRead(filePath);
                var data = (ListOfBooksModel)reader.Deserialize(stream);
                foreach(BookModel book in data.Books)
                {
                    books.Add(book);
                }
                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data:({ex.Message})");
                return new List<BookModel>();
            }
        }

        public void SaveData(string filePath, List<BookModel> books)
        {
            try
            {
                ListOfBooksModel data = new ListOfBooksModel();
                foreach (BookModel book in books)
                {
                    data.Books.Add(book);
                }
                var writer = new XmlSerializer(typeof(ListOfBooksModel));
                using StreamWriter stream = new StreamWriter(filePath);
                writer.Serialize(stream, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data:({ex.Message})");
            }
        }
    }
}
