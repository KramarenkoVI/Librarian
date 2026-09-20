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
                return new List<BookModel>();
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

                if (!CurrentBooks.Remove(delBook))
                    throw new Exception("Something's wrong! The item wasn't deleted.");

                return CurrentBooks;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error deleting data: ({ex.Message})");
                return new List<BookModel>();
            }
        }

        public List<BookModel> DeleteBook(int id)
        {
            try
            {
                if (id < 0 || id > CurrentBooks.Count)
                    throw new Exception("invalid id");

                CurrentBooks.RemoveAt(id);
                return CurrentBooks;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error deleting data: ({ex.Message})");
                return new List<BookModel>();
            }
        }

        public List<BookModel> SerachBooks(string searchString)
        {
            try
            {
                if (string.IsNullOrEmpty(searchString))
                    throw new Exception("Search field is empty");

                searchString = searchString.ToLower();
                List<BookModel> searchResult = CurrentBooks.Where(books => books.Title.ToLower().Contains(searchString)).ToList();
                if (searchResult.Count == 0)
                    throw new ArgumentNullException();

                return searchResult;
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine($"book not found: ({searchString})");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data search error: ({ex.Message})");
                return null;
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
                return new List<BookModel>();
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
                return new List<BookModel>();
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
                return new List<BookModel>();
            }
        }
    }
}
