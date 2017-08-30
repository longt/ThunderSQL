using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderSQL;

namespace ThunderSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext(Transaction.No);

            var insertParams = new { ID = 11, Name = "BMW" };  //匿名类

            int n = context.Execute("INSERT INTO Cars(car_id,car_name) VALUES (49, 'BMW')");

            Console.WriteLine("End:{0}", n);

            //增、删、查、改 

        }
    }
}
