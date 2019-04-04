using acs.Example;
using Xunit;

namespace acs.tests.Example.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var a = new Class1();
            
            Assert.Equal("WORKS", a.ToString());
        }
    }
}