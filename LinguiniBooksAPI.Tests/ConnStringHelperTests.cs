namespace LinguiniBooksAPI.Tests
{
    public class ConnStringHelperTests
    {
        [Fact]
        public void ReadConnStrTest_ReturnsString()
        {
            string connString = ConnStrHelper.ReadConnStr();

            Assert.NotEmpty(connString);
        }
    }
}