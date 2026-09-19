using Librarian.models;
namespace Librarian.interfaces
{
    public interface IDataProcessor
    {
        List<BookModel> AddBook(BookModel book);
        List<BookModel> DeleteBook(BookModel book);
        BookModel GetBook(string searchString);
        List<BookModel> SortBooks(List<BookModel> list);
        List<BookModel> ViewList();
    }
}
