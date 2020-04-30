<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.AccountManagement.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.Protocols.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Namespace>System.Security.Principal</Namespace>
  <Namespace>System.DirectoryServices.AccountManagement</Namespace>
</Query>

void Main()
{
	string grpToChk = "App-iWatch-Prod";
	SecurityIdentifier pgSiD = GetGroupSId("HQINTL1", grpToChk);
	Console.WriteLine($"The ID of the group '{grpToChk}' is  '{pgSiD.ToString()}'");
}

public static SecurityIdentifier GetGroupSId(string _domain, string _group)
{
	using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, _domain))
	{
		using (GroupPrincipal gp = GroupPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, _group))
		{
			if (gp is null)
				throw new Exception("Group not found");
			return gp.Sid;
		}
	}
}
