<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.AccountManagement.dll</Reference>
  <Namespace>System.DirectoryServices.AccountManagement</Namespace>
</Query>

void Main()
{
	List<string> gMem = GetGroupMembers("HQINTL1", "App-TFS-iWatch-EngDevelopers");
	gMem.Dump();
}

List<string> GetGroupMembers(string domain, string group)
{
	Console.WriteLine(domain);
	Console.WriteLine(group);
	Stopwatch stopwatch = new Stopwatch();
	stopwatch.Start();
	List<string> gMem = new List<string>();
	using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain))
	{
		if (ctx is null)
		{
			($"Domain '{domain}' doesn't exist (or isn't responding)").Dump();
			return null;
		}
		using (GroupPrincipal gp = GroupPrincipal.FindByIdentity(ctx, group))
		{
			if (gp is null)
			{
				($"Group '{group}' doesn't exist in the domain {domain}").Dump();
				stopwatch.Stop();
				Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
				return null;
			}
			foreach (var element in gp.GetMembers())
			{
				gMem.Add($"{element.SamAccountName}-{element.Name}");
			}
		}
	}
	stopwatch.Stop();
	Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
	return gMem;
}
