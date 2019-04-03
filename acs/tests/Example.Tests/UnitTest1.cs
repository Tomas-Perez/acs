using System;
using acs.Example;
using Xunit;

namespace acs
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