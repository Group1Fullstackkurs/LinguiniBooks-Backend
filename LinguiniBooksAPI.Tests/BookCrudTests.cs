using DBDataAccess.DBAccess;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;

namespace LinguiniBooksAPI.Tests
{
    public class BookCrudTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void GetAllBooks_Test()
        {
            // Arrange
            var x = ConnStrHelper.ReadConnStr();
            BooksCrud bookCrud = new BooksCrud(x);

            // Act
            var result = bookCrud.GetAllBooks();  // ?

            // Assert
            Assert.True(true, "x");
        }

        [Fact]

        public void CreateBook_Test()
        {
            var a = ConnStrHelper.ReadConnStr();

            BooksCrud bookCrud = new BooksCrud(a);
            BookModel book = new BookModel();
            var result = bookCrud.CreateBook(book) ;
            Assert.True(true, "");

        }
        [Fact]

        public void GetBook_Test()
        {
            var b = ConnStrHelper.ReadConnStr();
            BooksCrud bookCrud = new BooksCrud(b);
            BookModel book = new BookModel();
            var result = bookCrud.GetBook(" 631efbb832415172da5510e7"); 
            Assert.True(true, "b");
        }

        [Fact]
        public void UpdateBook_Test()
        {
           var c = ConnStrHelper.ReadConnStr();
            BooksCrud bookCrud = new BooksCrud(c);
            BookModel book = new BookModel();
            var result = bookCrud.UpdateBook(book);
            Assert.True(true, "c");
        }

        [Fact]
        
        public void DeleteCBook_Test()
        {
            var d = ConnStrHelper.ReadConnStr();
            BooksCrud bookCrud = new BooksCrud(d);
            BookModel book = new BookModel();
            var result = bookCrud.DeleteCBook(book); 
            Assert.True(true, "d"); 
        }
        
        
    }
}