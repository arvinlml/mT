<Query Kind="Statements">
  <Connection>
    <ID>e3b3446e-d9c6-436c-b128-cf8f4aefc2e4</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\Package Sources\WU.iWatch.Adapters\bin\Debug\net472\WU.iWatch.Adapters.dll</CustomAssemblyPath>
    <CustomTypeName>WU.iWatch.Adapters.iWatchEntities</CustomTypeName>
    <CustomCxString>IWQA04_AJ</CustomCxString>
    <AppConfigPath>C:\LINQPad5-AnyCPU\myConns.config</AppConfigPath>
    <DisplayName>QA</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>C:\work\iWatch\Package Sources\WU.iWatch.Adapters\bin\Debug\net472\DataModel.dll</Reference>
</Query>

Database.Log = Console.Write;
var cKey = 50806440;
var yearsToLookBack = -1;
var theCase = Cases.Where(c => c.CaseKey == cKey).Select(c => new { c.CaseStatusId, c.CaseStatusTimestamp, c.CreateTimestamp }).FirstOrDefault();
var mxDt = theCase.CaseStatusId == 10 ? theCase.CaseStatusTimestamp : DateTime.Now;
var miDt = theCase.CreateTimestamp.Value.AddYears(yearsToLookBack);

// Get transactions
var lstTrans = Transactions.
Where(t =>
CaseTransaction.Where(ct => ct.CaseKey == cKey && ct.ActivatedDeactivated == "A").Select(ct => ct.Mtcn).Contains(t.Mtcn)
&& t.SendDate > miDt && t.SendDate < mxDt).
Select(t => new { t.SGalacticId, t.PGalacticId, t.SendDate, t.SendAgentCountry, t.PayAgentCountry, t.SName, t.PName, t.SendUsPrincipal, t.PayUsPrincipal, t.PAttemptStatus, t.TxnStatus }).ToList();

// The below construct will help you to build a simpler solution instead of looping in the client.
var query = lstTrans.GroupBy(
	sdr => new { sdr.SGalacticId, sdr.SendAgentCountry },
	(baseSdr, txns) => new
	{
		Key = baseSdr,
		// Count = txns.Count(),
		Total = txns.Sum(x => x.SendUsPrincipal)
	});

query.Select(x => new {x.Key.SGalacticId, x.Key.SendAgentCountry, 
// x.Count, 
x.Total}).Dump();

// Serial number is something that can be added to the grid dynamically.. No need to put a serial number.
// You can add additional conditions as something is null and not null in the above grouping query to match the current implementation