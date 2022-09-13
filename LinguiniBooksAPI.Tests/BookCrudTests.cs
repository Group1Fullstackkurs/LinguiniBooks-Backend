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
    }
}