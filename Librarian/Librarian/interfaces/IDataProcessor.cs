using Librarian.models;
namespace Librarian.interfaces
{
    public interface IDataProcessor
    {
        List<BookModel> AddBook(BookModel book);
        List<BookModel> DeleteBook(BookModel book);
        List<BookModel> DeleteBook(int id);
        List<BookModel> SerachBooks(string searchString);
        List<BookModel> SortBooks(List<BookModel> list);
        List<BookModel> SortBooks();
        List<BookModel> ViewList();
    }
}
