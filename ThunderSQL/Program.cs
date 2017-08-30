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

            //1、写数据库
            /*
                        var context = new DataContext(); 
                        string sql = "select car_name from cars where car_id=15"; 
                        var value = context.GetValue(sql);//返回第一行第一列数据;
                       if (value == null)
                           Console.WriteLine("yes");
                       else
                            Console.WriteLine(value);
           */

            //2、写数据库
            var context = new DataContext();  
            var insertParams = new { ID = 14, Name = "BMWaaaa" };  //匿名类 
            int n = context.Execute("INSERT INTO Cars(car_id,car_name) VALUES (@0, @1)", insertParams.ID, insertParams.Name); 
            Console.WriteLine("End:{0}", n);            //返回更新的行数
             

        }
    }
}
