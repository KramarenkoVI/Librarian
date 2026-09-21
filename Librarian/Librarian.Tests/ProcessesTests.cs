using Librarian.models;
using Librarian.core;
using Librarian.interfaces;

namespace Librarian.Tests
{
    public class ProcessesTests
    {
        [Fact]
        public void AddBook()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            BookModel book = new BookModel() { Author = "TestedAuthor", Title = "TestedTitle", Pages = 80 };
            processor.AddBook(book);
            Assert.Contains<BookModel>(book, books);
        }

        [Fact]
        public void AddBookNull()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            var exception = Assert.Throws<ArgumentNullException>(() => processor.AddBook(null));
            Assert.Contains("invalid book object", exception.Message);
        }

        [Fact]
        public void SortBooks()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            IDataTransfer delivery = new TransferXML();
            List<BookModel> expectedExample = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(sorted).xml"));
            books = processor.SortBooks();
            var actualTitles = books.Select(book => book.Title);
            var expectedTitles = expectedExample.Select(book => book.Title);
            Assert.Equal(expectedTitles, actualTitles);
        }

        [Fact]
        public void SortCustomBooks()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            IDataTransfer delivery = new TransferXML();
            List<BookModel> expectedExample = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(sorted).xml"));
            books = processor.SortBooks(books);
            var actualTitles = books.Select(book => book.Title);
            var expectedTitles = expectedExample.Select(book => book.Title);
            Assert.Equal(expectedTitles, actualTitles);
        }

        [Fact]
        public void SearchSingleBook()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SearchBooks("ride and");
            BookModel result = Assert.Single<BookModel>(books);
            Assert.Equal("Pride and Prejudice", result.Title);
            Assert.Equal("Jane Austen", result.Author);
            Assert.Equal(62, result.Pages);
        }

        [Fact]
        public void SearchBooks()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SearchBooks("and");
            Assert.Equal(8, books.Count);
        }

        [Fact]
        public void DifferentLetterCase()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            var searchByLower = processor.SearchBooks("b").Select(book => book.Title);
            var searchByUpper = processor.SearchBooks("B").Select(book => book.Title);
            Assert.Equal(searchByLower, searchByUpper);
        }

        [Fact]
        public void EmptySearch()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SearchBooks(string.Empty);
            Assert.Empty(books);
        }

        [Fact]
        public void SearchNotExist()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SearchBooks("Murder in the Orient Express");
            Assert.Empty(books);
        }

        [Fact]
        public void DeleteBookByObj()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            BookModel delBook = books[books.Count - 1];
            books = processor.DeleteBook(delBook);
            Assert.DoesNotContain<BookModel>(delBook, books);
        }

        [Fact]
        public void DeleteBookNull()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            var exception = Assert.Throws<ArgumentNullException>(() => processor.DeleteBook(null));
            Assert.Contains("invalid book object", exception.Message);
        }

        [Fact]
        public void DeleteNonExistentBook()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            BookModel delBook = new BookModel(){ Author = "Unknown Author", Title = "Unknown Title", Pages = 90 };
            int currentCount = books.Count;
            books = processor.DeleteBook(delBook);
            Assert.Equal(currentCount, books.Count);
        }

        [Fact]
        public void DeleteBookByIndex()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            BookModel delBook = books[books.Count - 1];
            books = processor.DeleteBook(books.Count - 1);
            Assert.DoesNotContain<BookModel>(delBook, books);
        }

        [Theory]
        [InlineData (176)]
        [InlineData (-1)]
        public void DeleteBookByWrongIndex(int index)
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(()=> processor.DeleteBook(index));
            Assert.Contains("The specified book index is outside the valid range of the collection.", exception.Message);
        }

        #region Helpers
        private List<BookModel> GetTestData()
        {
            IDataTransfer delivery = new TransferXML();
            return delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks.xml"));
        }
        #endregion
    }
}
