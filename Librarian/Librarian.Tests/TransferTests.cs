using Librarian.models;
using Librarian.core;
using Librarian.interfaces;
namespace Librarian.Tests;

public class TransferTests
{
    [Fact]
    public void Debug()
    {
        IDataTransfer delivery = new TransferXML();
        List<BookModel> books = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks.xml"));
        books.Add(new BookModel()
        {Title = "New Record",
        Author = "Debug",
        Pages = 50}
        );
        delivery.SaveData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"), books);
        Assert.True(true);
    }
}
