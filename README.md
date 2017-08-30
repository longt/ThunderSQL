# ThunderSQL
A smart SQL Tool for .Net!  This project is from Thunderstruck
看到一个很好的.net数据库操作类库，不过貌似作者已经很久未更新了，复制过来，持续更新，顺便写点文档。
后续再完善成一个功能强大的数据库操作类库。

## 修改日志： ##
v1.0：
	1、默认不使用事务，简化常用连接；
	2、修改

 ## Use Example: ##
1、编写配置文档：
```javascript
function fancyAlert(arg) {
  if(arg) {
    $.facebox({div:'#foo'})
  }
}
```
```c#
var context = new DataContext();
var insertParams = new { ID = 11, Name = "BMW" };  //匿名类
int n = context.Execute("INSERT INTO Cars(car_id,car_name) VALUES (@0, @1)",11,"hello");
Console.WriteLine("End:{0}", n);
``` 

 
1、读数据库
	