using DBDataAccess.DBAccess;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;

namespace LinguiniBooksAPI.Tests
{
    public class BookTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void BuyBook_Test() // TDD approach, so this test is in progress until BuyBook functionality is implemented.
        {
            BookModel book = new BookModel();
            book.Stock = 10;
            int newStock = book.Stock - 1;

            Assert.NotEqual(book.Stock, newStock);
            Assert.True(newStock == 9);
        }
    }
}