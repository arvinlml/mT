<Query Kind="Program">
  <Connection>
    <ID>6e2fc036-dbbd-4463-a6ed-184cacc03a35</ID>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\CPCE Services\DataModel\bin\Debug\DataModel.dll</CustomAssemblyPath>
    <CustomTypeName>iWatch.iWatchEntities</CustomTypeName>
    <CustomCxString>iWEnt</CustomCxString>
    <AppConfigPath>C:\Users\312198\Source\Repos\WebApplication1\WebApplication1\Web.config</AppConfigPath>
    <DisplayName>DM2_SIT</DisplayName>
  </Connection>
</Query>

void Main()
{
	var loginId = "lg06cwc".ToUpperInvariant();

	var ROP_that_requires_explicit_Get_permission_in_roles = GetROPFromDb(loginId);
	Console.WriteLine(ROP_that_requires_explicit_Get_permission_in_roles);

	var ROP_intrinsic_Get_permission = GetROPFromDb_IntrinsicGet(loginId);
	Console.WriteLine(ROP_intrinsic_Get_permission);
}

IQueryable<UserResourceOperation> GetROPFromDb(string loginId)
{
	var ctx = this;
	var getUP = ctx.Users.Join(ctx.UserRole, u => u.UserId, ur => ur.UserId, (u, ur) => new { u, ur })
		.Join(ctx.RolePermission, @t => @t.ur.RoleId, rp => rp.RoleId, (@t, rp) => new { @t, rp })
		.Where(@t => loginId.Equals(@t.@t.u.LoginId.ToUpper()))
		.Select(@t => new { @t.@t.u.UserId, @t.@t.u.LoginId, @t.rp.PermissionId })
	.Union(ctx.Users.Join(ctx.UserPermission, u => u.UserId, up => up.UserId, (u, up) => new { u, up })
		.Where(@t => loginId.Equals(@t.u.LoginId.ToUpper()))
		.Select(@t => new { @t.u.UserId, @t.u.LoginId, @t.up.PermissionId }));
	var uROP = getUP.Join(ctx.ResourceOperationPermission, p => p.PermissionId, rop => rop.PermissionId, (p, rop) => new UserResourceOperation { ResourceOperation = string.Concat(rop.Resource.Trim(), rop.Operation.Trim()).ToLower() });
	return uROP;
}

IQueryable<UserResourceOperation> GetROPFromDb_IntrinsicGet(string loginId)
{
	var ctx = this;

	var queryUserPermissions = ctx.Users
		.Join(ctx.UserRole, u => u.UserId, ur => ur.UserId, (u, ur) => new { u, ur })
		.Join(ctx.RolePermission, @t => @t.ur.RoleId, rp => rp.RoleId, (@t, rp) => new { @t, rp })
		.Where(@t => loginId.Equals(@t.@t.u.LoginId
#pragma warning disable S1449 // Culture should be specified for "string" operations
							.ToUpper()
#pragma warning restore S1449 // Culture should be specified for "string" operations
						))
		.Select(@t => new { @t.@t.u.UserId, @t.@t.u.LoginId, @t.rp.PermissionId })
		.Union(ctx.Users
			.Where(@t => loginId.Equals(@t.LoginId
#pragma warning disable S1449 // Culture should be specified for "string" operations
									.ToUpper()
#pragma warning restore S1449 // Culture should be specified for "string" operations
							))
			.Select(@t => new { @t.UserId, @t.LoginId, PermissionId = 181 }))
		.Union(ctx.Users
			.Join(ctx.UserPermission, u => u.UserId, up => up.UserId, (u, up) => new { u, up })
			.Where(@t => loginId.Equals(@t.u.LoginId
#pragma warning disable S1449 // Culture should be specified for "string" operations
									.ToUpper()
#pragma warning restore S1449 // Culture should be specified for "string" operations
							))
			.Select(@t => new { @t.u.UserId, @t.u.LoginId, @t.up.PermissionId }));

	IQueryable<UserResourceOperation> listResourceOperation =
		queryUserPermissions.Join(ctx.ResourceOperationPermission, p => p.PermissionId,
			rop => rop.PermissionId,
			(p, rop) => new UserResourceOperation
			{
				ResourceOperation = string.Concat(rop.Resource.Trim(), rop.Operation.Trim()).ToLower()
			});
	return listResourceOperation;
}

internal class UserResourceOperation
{
	public string ResourceOperation { get; set; }
}