<Query Kind="Statements">
  <Connection>
    <ID>1730c124-1713-46d8-a907-60ef54d6c73e</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\CPCE Services\DataModel\bin\Debug\DataModel.dll</CustomAssemblyPath>
    <CustomTypeName>iWatch.iWatchEntities</CustomTypeName>
    <CustomCxString>IWQA04_AJ</CustomCxString>
    <AppConfigPath>C:\LINQPad5\myConns.config</AppConfigPath>
    <DisplayName>IWQA04_AJ</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
</Query>

string res="USER";
Console.WriteLine(
ResourceOperationPermission
		.Join(Permissions, rop => rop.PermissionId, p => p.PermissionId, (rop, p) => new { rop, p})
		.Where(@t => @t.rop.Resource.ToUpper().Contains(res.ToUpper()))
		.Select(@t => new { @t.rop.Resource, @t.rop.Operation, @t.p.PermissionName, @t.p.PermissionId })
		.OrderBy(@t => new { t.Resource, t.Operation, t.PermissionName})
		);