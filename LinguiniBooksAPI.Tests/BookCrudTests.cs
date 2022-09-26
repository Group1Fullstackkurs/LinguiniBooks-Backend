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
        public void ConnStrT()
        {
            string fakedbconnstr = "";
            fakedbconnstr = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\fakedbconnstr.txt");
            string realdbConnStr = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt");

            var result = ConnStrHelper.ReadConnStr();

            Assert.NotEqual(fakedbconnstr, result);
            Assert.Equal(result, realdbConnStr);
        }
    }
}