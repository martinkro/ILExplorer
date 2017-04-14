using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnitDemo.Services
{
    public class RectangleService
    {
        public double Area(double width, double height)
        {
            return width * height;
        }

        public double Primeter(double width, double height)
        {
            return 2 * (width + height);
        }
    }
}
