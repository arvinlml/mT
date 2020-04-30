<Query Kind="Program">
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
</Query>

void Main()
{
	// The schedule is expected to be maintained outside of this framework.
	GenerateObjectiveReport("SCOTIA BANK");
}

void GenerateObjectiveReport(string rptGrp)
{
	// Get rptgrpid
	var iRptGrpId = ReportGroup.FirstOrDefault(rg => rg.RptGrpName == rptGrp).RptGrpId
	// Extract information
	GetTransFromORT(iRptGrpId);

	// Transform data (this should be the source of all forms of output)..

	// Generate output (can be multiple, should accept a list of objects)
	// (XLS|PDF|TXT|RPT|DOC), (single or multiple), if multiple what is the criteria
	// Should all be zipped
}

List<ObjectiveReportTransaction> GetTransFromORT(int rptGrp)
{
	return ObjectiveReportTransaction.Where(ort => ort.RptGrpId==rptGrp).ToList();
}
 
 
//ReportGroupStaticData.Where(rgsd => rgsd.AttributeName == "START_DAY")
//InvestigativeGroup.Where(ig => ig.BusinessGroupId == "R&R").Dump();
//ReportGroup.Take(1).Dump();
//Queues.Where(q => q.BusinessGroupId == "R&R").Dump();