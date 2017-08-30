using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderSQL;
using System.Data;

namespace ThunderSQL
{
    class Car
    {
        public int car_id { get; set; }
        public string car_name { get; set; }
        public string car_num { get; set; }
    }

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

            //----------------------------------------------------------------------

            //2、写数据库
            //var context = new DataContext();  
            //var insertParams = new { ID = 14, Name = "BMWaaaa" };  //匿名类 
            //int n = context.Execute("INSERT INTO Cars(car_id,car_name) VALUES (@0, @1)", insertParams.ID, insertParams.Name); 
            //Console.WriteLine("End:{0}", n);            //返回更新的行数

            //----------------------------------------------------------------------

            //3、读取DataTable
            //var context = new DataContext();
            //string sql = "select * from cars";
            //DataTable dt = context.GetDataTable(sql);
            //foreach(DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["car_id"].ToString());
            //}

            //----------------------------------------------------------------------

            //4、读取List
            //var context = new DataContext();
            //string sql = "select car_name from cars";
            //List<string> list = context.GetList(sql);
            //foreach(string item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //----------------------------------------------------------------------

            //5、读取List<T>，T的属性值与数据库字段需要对应

            //var context = new DataContext();
            //string sql = "select * from cars";
            //List<Car> list = context.GetList<Car>(sql);
            //foreach (Car item in list)
            //{
            //    Console.WriteLine("{0},{1}", item.car_id, item.car_name);
            //}

            /* 6、遍历 */
            var context = new DataContext();
            string sql = "select * from cars";
            IDataReader reader = context.Query(sql);
            Console.WriteLine("字段数量：{0}", reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write("{0}({1})", reader.GetName(i), reader.GetDataTypeName(i));
                if (i < reader.FieldCount - 1)
                    Console.Write("\t");
                else
                    Console.Write("\n");

            }
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write("{0}", reader[i].ToString());
                    if (i < reader.FieldCount - 1)
                        Console.Write("\t\t");
                    else
                        Console.Write("\n");
                }
            }
        } 
    }
}
