using Librarian.models;
namespace Librarian.interfaces
{
    public interface IDataTransfer
    {
        List<BookModel> GetData(string filePath);
        void SaveData();
    }
}
