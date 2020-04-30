<Query Kind="Statements">
  <Connection>
    <ID>f6a9e815-2242-43a2-8307-8dbc4a986f94</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\CPCE Services\DataModel\bin\Debug\DataModel.dll</CustomAssemblyPath>
    <CustomTypeName>iWatch.iWatchEntities</CustomTypeName>
    <CustomCxString>IWSIT04_AJ</CustomCxString>
    <AppConfigPath>C:\LINQPad5\myConns.config</AppConfigPath>
    <DisplayName>IWSIT04_AJ</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
</Query>

this.Database.Log = s => Console.WriteLine(s);
var uToDel=Users.Where(u => u.LoginId=="312198" || u.LoginId=="310843"
).Select(u => u.UserId).ToList();
var inList = Newtonsoft.Json.JsonConvert.SerializeObject(uToDel).Replace("[", "(").Replace("]", ")");
Console.WriteLine(inList);
if (inList=="()") return;
this.Database.ExecuteSqlCommand($"UPDATE CPCE.USER SET INSTITUTION_ID = 1 WHERE USER_ID IN {inList}");
this.Database.ExecuteSqlCommand($"DELETE CPCE.USER WHERE USER_ID IN {inList}");
this.Database.ExecuteSqlCommand($"DELETE CPCE.USER_ROLE WHERE USER_ID IN {inList}");
this.Database.ExecuteSqlCommand($"DELETE CPCE.USER_PERMISSION WHERE USER_ID IN {inList}");