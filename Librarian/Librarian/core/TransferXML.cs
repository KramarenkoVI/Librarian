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
                return new List<BookModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data:({ex.Message})");
                return new List<BookModel>();
            }
        }

        public void SaveData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data:({ex.Message})");
            }
        }
    }
}
