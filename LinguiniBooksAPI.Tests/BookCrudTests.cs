using DBDataAccess;
using DBDataAccess.DBAccess;
using DBDataAccess.Interfaces;
using DBDataAccess.Models;
using LinguiniBooksAPI.Helpers;
using MongoDB.Driver;
using Moq;

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
        public void MedMoq()
        { 
            var mockBookClient = new Mock<IBookRepository>();
            // ...
        }
    }
}