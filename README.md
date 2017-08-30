# ThunderSQL
 This project is from Thunderstruck：https://github.com/19WAS85/Thunderstruck
看到一个很好的.net数据库操作类库，不过貌似作者已经很久未更新了。使用过程中也发现一些还需要完善的地方，复制过来，持续更新修订一下，顺便写点文档，希望能不断完善成一个功能强大的数据库操作类库。

## 修改日志： ##
v1.0：

1、默认不使用事务，简化常用连接；

2、增加测试项目代码

## 基本操作: ##
### 1、数据库配置： ###

源码包默认使用数据库： Sql Server 2008 ，数据库名：Test。可自行修改app.config中的数据库连接字符串。

当然也可以配置其余数据库，配置如下：

```
 <connectionStrings>
        <add name="Default" providerName="System.Data.SqlClient" connectionString="..." />
        <add name="Other" providerName="System.Data.OracleClient" connectionString="..." />
        <add name="MySqlCon" providerName="MySql.Data.MySqlClient" connectionString="..." />
  </connectionStrings>
``` 

 

### 2、简单操作： ###


- 读数据库：GetValue()
           var context = new DataContext(); 
           string sql = "select car_name from cars where car_id=14"; 
           var value = context.GetValue(sql);//返回第一行第一列数据;	
 

	
- 写数据库：Execute()

           var context = new DataContext();  
           var insertParams = new { ID = 14, Name = "BMWaaaa" };  //匿名类 
           int n = context.Execute("INSERT INTO Cars(car_id,car_name) VALUES (@0, @1)", insertParams.ID, insertParams.Name); 
           Console.WriteLine("End:{0}", n);            //返回更新的行数
