<Query Kind="Statements">
  <Connection>
    <ID>1730c124-1713-46d8-a907-60ef54d6c73e</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\CPCE Services\DataModel\bin\Debug\DataModel.dll</CustomAssemblyPath>
    <CustomTypeName>iWatch.iWatchEntities</CustomTypeName>
    <CustomCxString>IWSIT04_AJ</CustomCxString>
    <AppConfigPath>C:\LINQPad5\myConns.config</AppConfigPath>
    <DisplayName>DM2_SIT</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
</Query>

var ctx = this;
ctx.Users.Where(u => u.LoginStatus == "1").Select(u => new { u.UserId, u.LoginId, u.FirstName, u.MiddleName, u.LastName, u.Email }).Dump();
ctx.Roles.Select(r => new { r.RoleId, r.BusinessGroupId, r.RoleName, r.Description }).Dump();
ctx.Permissions.Select(p => new{	p.PermissionId,    p.BusinessGroupId, p.PermissionName, p.Category }) .Dump();
var effectivePermissions=ctx.Users.Join(ctx.UserRole, u => u.UserId, ur => ur.UserId, (u, ur) => new { u, ur })
	.Join(ctx.RolePermission, @t => @t.ur.RoleId, rp => rp.RoleId, (@t, rp) => new { @t, rp })
	.Where(@t => @t.@t.u.LoginStatus == "1")
	.Select(@t => new { @t.@t.u.UserId, @t.@t.u.LoginId, @t.rp.PermissionId })
.Union(ctx.Users.Join(ctx.UserPermission, u => u.UserId, up => up.UserId, (u, up) => new { u, up })
	.Where(@t => @t.u.LoginStatus == "1")
	.Select(@t => new { @t.u.UserId, @t.u.LoginId, @t.up.PermissionId }));
effectivePermissions.Dump();

ctx.Users.Join(ctx.UserRole, u => u.UserId, ur => ur.UserId, (u, ur) => new { u, ur })
	.Select(@t => new { @t.u.UserId, @t.u.LoginId, @t.ur.RoleId }).Dump();

RolePermission.Where(rp => rp.PermissionId > 1).Dump();
