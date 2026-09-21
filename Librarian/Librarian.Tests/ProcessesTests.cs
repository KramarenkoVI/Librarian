using Librarian.models;
using Librarian.core;
using Librarian.interfaces;

namespace Librarian.Tests
{
    public class ProcessesTests
    {

        public void Debug()
        {
            IDataTransfer delivery = new TransferXML();
            List<BookModel> books = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks.xml"));
            //books.Add(new BookModel()
            //{
            //    Title = "New Record",
            //    Author = "Debug",
            //    Pages = 50
            //}
            //);
            //delivery.SaveData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"), books);

            IDataProcessor processor = new Processor(books);
            processor.AddBook(new BookModel() { Author = "AAA", Title = "DELETE ME", Pages = 80 });
            processor.ViewList();
            processor.SortBooks();
            processor.SortBooks(books);
            processor.SerachBooks("ride and"); //true
            processor.SerachBooks("b");
            processor.SerachBooks("B");
            processor.SerachBooks(" ");
            processor.SerachBooks("");
            processor.SerachBooks(string.Empty);
            processor.SerachBooks("Murder in the Orient Express"); //false
            processor.DeleteBook(new BookModel() { Author = "AAA", Title = "DELETE ME", Pages = 80 });
            processor.DeleteBook(0);
            processor.DeleteBook(172);
            processor.DeleteBook(-17);
            processor.DeleteBook(new BookModel() { Author = "XXX", Title = "DELETE ME!", Pages = 80 });
            Assert.True(true);
        }
    }
}
