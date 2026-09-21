using Librarian.models;
using Librarian.core;
using Librarian.interfaces;

namespace Librarian.Tests;

public class TransferTests
{
    [Fact]
    public void GetData()
    {
        IDataTransfer delivery = new TransferXML();
        List<BookModel> books = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks.xml"));
        Assert.True(books.Count == 42);
    }

    [Fact]
    public void GetDataWrongWay()
    {
        IDataTransfer delivery = new TransferXML();
        var exception = Assert.Throws<Exception>(() => delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "randomFolder", "randomFile.xml")));
        Assert.Equal("File not found", exception.Message);
    }

    [Fact]
    public void SaveData()
    {
        IDataTransfer delivery = new TransferXML();
        List<BookModel> books = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks.xml"));
        delivery.SaveData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"), books);
        List<BookModel> savedBooks = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"));
        Assert.True(savedBooks.Count == 42);
    }

    [Fact]
    public void SaveEmptyData()
    {
        IDataTransfer delivery = new TransferXML();
        delivery.SaveData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"), new List<BookModel>());
        List<BookModel> savedBooks = delivery.GetData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"));
        Assert.True(savedBooks.Count == 0);
    }

    [Fact]
    public void SaveNullData()
    {
        IDataTransfer delivery = new TransferXML();
        var exception = Assert.Throws<ArgumentNullException>(() => delivery.SaveData(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsData", "TestListOfBooks(saved).xml"), null));
        Assert.Equal("The books collection cannot be null. (Parameter 'books')", exception.Message);
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
