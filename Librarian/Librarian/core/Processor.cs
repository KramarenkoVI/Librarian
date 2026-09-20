using Librarian.models;
using Librarian.interfaces;
using System.Reflection.Metadata.Ecma335;
namespace Librarian.core
{
    public class Processor : IDataProcessor
    {
        List<BookModel> CurrentBooks { get; set; }

        public Processor(List<BookModel> books)
        {
            CurrentBooks = books;
        }

        public List<BookModel> AddBook(BookModel book)
        {
            try
            {
                CurrentBooks.Add(book);
                return CurrentBooks;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error adding data: ({ex.Message})");
                throw;
            }
        }

        public List<BookModel> DeleteBook(BookModel book)
        {
            try
            {
                if (book is null)
                    throw new Exception("invalid book object");

                BookModel? delBook = CurrentBooks.FirstOrDefault(x => x.Title == book.Title && x.Author == book.Author);

                if (delBook is null)
                    return CurrentBooks;

                CurrentBooks.Remove(delBook);

                return CurrentBooks;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error deleting data: ({ex.Message})");
                throw;
            }
        }

        public List<BookModel> DeleteBook(int id)
        {
            try
            {
                CurrentBooks.RemoveAt(id);
                return CurrentBooks;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error deleting data: ({ex.Message})");
                throw;
            }
        }

        public List<BookModel> SerachBooks(string searchString)
        {
            try
            {
                if (string.IsNullOrEmpty(searchString))
                    return new List<BookModel>();

                searchString = searchString.ToLower();
                List<BookModel> searchResult = CurrentBooks.Where(books => books.Title.ToLower().Contains(searchString)).ToList();
                return searchResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data search error: ({ex.Message})");
                throw;
            }
        }

        public List<BookModel> SortBooks(List<BookModel> list)
        {
            try
            {
                List<BookModel> sortedData = list.OrderBy(books => books.Author).ThenBy(books => books.Title).ToList();
                return sortedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data sorting error: ({ex.Message})");
                throw;
            }
        }

        public List<BookModel> SortBooks()
        {
            try
            {
                CurrentBooks = CurrentBooks.OrderBy(books => books.Author).ThenBy(books => books.Title).ToList();
                return CurrentBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data sorting error: ({ex.Message})");
                throw;
            }
        }

        public List<BookModel> ViewList()
        {
            try
            {
                return CurrentBooks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data display error: ({ex.Message})");
                throw;
            }
        }
    }
}
