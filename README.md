# ThunderSQL
 This project is from Thunderstruck：https://github.com/19WAS85/Thunderstruck
看到一个很好的.net数据库操作类库，不过貌似作者已经很久未更新了。使用过程中也发现一些还需要完善的地方，复制过来，持续更新修订一下，顺便写点文档，希望能不断完善成一个功能强大的数据库操作类库。

## 修改日志： ##
v1.0：

1、默认不使用事务，简化常用连接；

2、增加测试项目代码

3、提供更丰富的查询功能

## 基本操作: ##
### 1、数据库配置： ###

源码包默认使用数据库： Sql Server 2008 ，数据库名：Test。

参考数据如下：
```
car_id(int)     car_name(nvarchar)      car_num(int)
12              hello           		10001
11              11              		10003
14              BMWaaaa         		10004
11              hello           		10005
```


连接数据库时，可自行修改app.config中的数据库连接字符串。

当然也可以配置其余数据库，配置如下：

```xml
 <connectionStrings>
        <add name="Default" providerName="System.Data.SqlClient" connectionString="..." />
        <add name="Other" providerName="System.Data.OracleClient" connectionString="..." />
        <add name="MySqlCon" providerName="MySql.Data.MySqlClient" connectionString="..." />
  </connectionStrings>
``` 

 

### 2、简单操作： ###

 

**一般读取：GetValue()**
用于获取查询结果中第一行第一列数据，为数据库查询中常用操作之一。

```c#
            var context = new DataContext(); 
            string sql = "select car_name from cars where car_id=14"; 
            var value = context.GetValue(sql);	
```


**遍历读取：Query()**
获取查询结果的IDataReader对象，可获取相关查询结果的临时表所有数据。
 
```c#
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
```
结果：
```
字段数量：3
car_id(int)     car_name(nvarchar)      car_num(int)
12              hello           		10001
11              11              		10003
14              BMWaaaa         		10004
11              hello           		10005
请按任意键继续. . .
```
	
**DataTable格式读取:GetDataTable()**
```c#
            var context = new DataContext();
            string sql = "select * from cars";
            DataTable dt = context.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("{0}:{1}",dr["car_id"].ToString(),dr["car_name"].ToString());
            }
```
结果：
```
12:hello
11:11
14:BMWaaaa
11:hello
请按任意键继续. . .
```

**List格式读取：GetList()**
```c#
            var context = new DataContext();
            string sql = "select car_name from cars";
            List<string> list = context.GetList(sql);
            foreach (string item in list)
            {
                Console.WriteLine(item);
            }
```
结果：
```
hello
11
BMWaaaa
hello
请按任意键继续. . .
```

**List<T> 格式读取：GetList()**
自定义类，字段与数据库表结构对应
```c#
            class Car
            {
            	public int car_id { get; set; }
            	public string car_name { get; set; }
            	public string car_num { get; set; }
            }
```
主函数内代码：
```c#
            var context = new DataContext();
            string sql = "select * from cars";
            List<Car> list = context.GetList<Car>(sql);
            foreach (Car item in list)
            {
                Console.WriteLine("{0},{1}", item.car_id, item.car_name);
            }       

```
结果：
```
12,hello
11,11
14,BMWaaaa
11,hello
请按任意键继续. . .
```

**  写数据库：Execute() **

```c#
 var context = new DataContext();  
 var test = new { ID = 14, Name = "BMWaaaa" };  //匿名类 
 int n = context.Execute("INSERT INTO Cars(car_id,car_name) VALUES (@0, @1)", test.ID, test.Name); 
 Console.WriteLine("End:{0}", n);            //返回更新的行数
```

<br/>

----------


## 后续持续更新中... 欢迎关注个人微信公众号：long_lab  ##