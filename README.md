#一、简介
该组件主要实现数据库的基本操作，包括insert/update/delete以及存储过程等基本操作。目前支持Mssql、Oracle、MySql、Sqlite、Access等数据库，
只需要修改web.config或app.config文件中的配置即可。
#二、数据库访问
##1、操作Oracle
###1)全局程序集缓存（GAC）
若全局程序集缓存中没有注册就不会正常使用该组件，可以通过gacutil命令查看或注册程序集
- 查看GAC：c:\windows\system32\gacutil.exe /l Oracle.DataAccess
- 注册GAC：c:\windows\system32\gacutil.exe /i Oracle.DataAccess.dll

###2）配置程序集 

~~~C#
<system.data>
   <DbProviderFactories>
     <add name="Oracle Data Provider for .NET" invariant="Oracle.DataAccess.Client" description="Oracle Data Provider for .NET" 
     type="Oracle.DataAccess.Client.OracleClientFactory, Oracle.DataAccess, Version=2.111.6.20, Culture=neutral, PublicKeyToken=89b483f429c47342"/>
   </DbProviderFactories>
 </system.data>
 
~~~

### 3）调用方法  
~~~C#

string conectionString = "Data Source=orcl;User ID=test;Password=test;";
Vic.Data.DataAccess dbAccess = new Vic.Data.DataAccess(connectionString, Vic.Data.DbProviderType.OracleClient);
dbAccess.Query("select * from users ");

~~~

#三、支持数据库类型 
- **SqlServer** <br>
DbProviderType.SqlServer
- **MySql** <br>
DbProviderType.MySql
- **Oracle** <br>
DbProviderType.Oracle        // 微软公司驱动 <br>
DbProviderType.OracleClient  // Oracle公司Oracle.DataAccess.Client驱动<br>
DbProviderType.OracleManaged // Oracle公司Oracle.ManagedDataAccess.Client驱动<br>
- **Sqlite** <br>
DbProviderType.SQLite
DbProviderType.SQLiteEF6
DbProviderType.SQLiteLinq
- **Access** <br>
DbProviderType.Access
- **Excel**<br>
DbProviderType.Excel
- **SqlserverCe** <br>
DbProviderType.SqlServerCe_3_5
- **Odbc** <br>
DbProviderType.Odbc
