using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleUnitDemo.Services;
using Xunit;

namespace SimpleUnitDemo.Tests
{
    public class RectangleServiceTest
    {
        [Fact]
        public void AreaTest()
        {
            RectangleService instance = new RectangleService();

            var area = instance.Area(2, 3.5);

            Assert.Equal(7.0, area);
        }

        [Fact]
        public void OperationTest()
        {
            int a = 1;
            int b = 2;
            int c = a + b;
            Assert.Equal(3, c);
        }
    }
}
