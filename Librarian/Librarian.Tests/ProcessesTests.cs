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
        public void SortBooks()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            IDataTransfer delivery = new TransferXML();
            List<BookModel> sortedExample = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(sorted).xml"));
            books = processor.SortBooks();
            var expectedTitles = books.Select(book => book.Title);
            var sortedTitles = sortedExample.Select(book => book.Title);
            Assert.Equal(expectedTitles, sortedTitles);
        }

        [Fact]
        public void SearchSingleBook()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SerachBooks("ride and");
            BookModel result = Assert.Single<BookModel>(books);
            Assert.True(result.Title == "Pride and Prejudice" && result.Author == "Jane Austen" && result.Pages == 62);
        }

        [Fact]
        public void DifferentLetterCase()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            var searchByLower = processor.SerachBooks("b").Select(book => book.Title);
            var searchByUpper = processor.SerachBooks("B").Select(book => book.Title);
            Assert.Equal(searchByLower, searchByUpper);
        }

        [Fact]
        public void EmptySearch()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SerachBooks(string.Empty);
            Assert.Empty(books);
        }

        [Fact]
        public void SearchNotExist()
        {
            List<BookModel> books = GetTestData();
            IDataProcessor processor = new Processor(books);
            books = processor.SerachBooks("Murder in the Orient Express");
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

        private void SaveTestData(List<BookModel> data)
        {
            IDataTransfer delivery = new TransferXML();
            delivery.SaveData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"), data);
        }
        #endregion
    }
}
