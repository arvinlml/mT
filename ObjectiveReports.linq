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

// public readonly string cSqlRptSchedules = @"SELECT OCM.RPT_GRP_ID as RptGrpId, 	RG.RPT_GRP_NAME AS RptGrpName, 	PERIOD_TYPE as PeriodTypeId, 	DDF.DROP_DOWN_FIELD_DESCRIPTION AS PeriodType, 	SCHEDULER_START_DAY as StartDay, 	SCHEDULER_START_TIME as StartTime,	NULL AS TxnStartDate, 	NULL as TxnEndDate, 	NULL as ScheduleStatus, 	RG.IS_OBJECTIVE_RPT as IsObjRpt 	FROM CPCE.OBJECTIVE_REPORT_SCHEDULE OCM JOIN CPCE.DROP_DOWN_FIELD_VALUE DDF ON OCM.PERIOD_TYPE = DDF.DROP_DOWN_FIELD_VALUE_ID AND CATEGORY = 'OBJECTIVE_REPORT_PERIOD_TYPE' JOIN CPCE.REPORT_GROUP RG ON OCM.RPT_GRP_ID = RG.RPT_GRP_ID ORDER BY SCHEDULER_START_TIME ASC";
// This query is converted to an object returned from LinQ query

void Main()
{
	// EfileBatch, EfileBatchOutput, CaseReport, Cases, Transactions, ObjectiveReportTransaction

	// EfileBatchOutput
	// CaseReport

	// Generate
	// Batch -> Reports -> Case -> Transaction -> Attempts
	// Batch -> Reports -> Case -> Transaction
	var oRgs = ReportGroup.Where(rg => rg.IsObjectiveRpt == "Y").ToList();
	Console.WriteLine(oRgs);
	var org = oRgs.Select(rg => rg.RptGrpId).ToArray();
	Console.WriteLine(org);
	Console.WriteLine(EfileBatch.Where(eb => org.Contains(eb.RptGrpId)));
	Console.WriteLine(EfileBatchOutput.Where(eb => org.Contains(eb.RptGrpId)));
}
Dictionary<string, Dictionary<string, string>> getRGSD(string rptGrpName)
{
	var rptGrpId = ReportGroup.FirstOrDefault(x => x.RptGrpName == rptGrpName).RptGrpId;
	return ReportGroupStaticData.Where(rgsd => rgsd.RptGrpId == rptGrpId)
	.Select(x => new { x.CultureInfo, x.AttributeName, x.AttributeValue }).OrderBy(x => x.AttributeName).GroupBy(x => x.CultureInfo).
	ToDictionary(x => x.Key, x => x.ToDictionary(y => y.AttributeName, y => y.AttributeValue));
}

#region Commented out
// Define other methods and classes here

//class ObjRptSchedules
//{
//	public int RptGrpId { get; set; }
//	public string RptGrpName { get; set; }
//	public int PeriodTypeId { get; set; }
//	public string PeriodType { get; set; }
//	public int StartDay { get; set; }
//	public TimeSpan StartTime { get; set; }
//	public DateTime? TxnStartDate { get; set; }
//	public DateTime? TxnEndDate { get; set; }
//	public string ScheduleStatus { get; set; }
//	public string IsObjRpt { get; set; }
//}
#endregion Commented out

#region CreateObjBatchScheduler part
//	// var xD = this.Database.SqlQuery <ObjRptSchedules>(cSqlRptSchedules);
//	var xxd = ObjectiveReportSchedule.Join(DropDownFieldValue.Where(x => x.Category == "OBJECTIVE_REPORT_PERIOD_TYPE"), ors => ors.PeriodType, ddfv => ddfv.DropDownFieldValueId, (ors, ddfv) => new { ors, ddfv }).Join(ReportGroup, @t => t.ors.RptGrpId, rg => rg.RptGrpId, (@t, rg) => new { @t, rg }).Select(@t => new
//	{
//		@t.t.ors.RptGrpId,
//		@t.rg.RptGrpName,
//		PeriodTypeId = @t.t.ors.PeriodType,
//		PeriodType = @t.t.ddfv.DropDownFieldDescription,
//		Frequency =
//		(
//			@t.t.ddfv.DropDownFieldDescription == "Daily" ? 1 :
//			@t.t.ddfv.DropDownFieldDescription == "WeeklyFromMonday" ? 2 :
//			@t.t.ddfv.DropDownFieldDescription == "WeeklyOnMonday" ? 2 :
//			@t.t.ddfv.DropDownFieldDescription == "Weekly" ? 2 :
//			@t.t.ddfv.DropDownFieldDescription == "CustomFiveDays" ? 3 :
//			@t.t.ddfv.DropDownFieldDescription == "Bimonthly" ? 4 :
//			@t.t.ddfv.DropDownFieldDescription == "MonthlyAggregationWeekly" ? 5 :
//			@t.t.ddfv.DropDownFieldDescription == "Monthly" ? 6 :
//			@t.t.ddfv.DropDownFieldDescription == "Quarterly" ? 7 :
//			@t.t.ddfv.DropDownFieldDescription == "LookBackDays" ? 8 : 99
//		),
//		@t.t.ors.SchedulerStartDay,
//		@t.t.ors.SchedulerStartTime,
//		TxnStartDate = new DateTime(),
//		TxnEndDate = new DateTime(),
//		ScheduleStatus = "",
//		@t.rg.IsObjectiveRpt
//	}).Where(x => x.IsObjectiveRpt == "Y").OrderBy(@t => new { @t.Frequency, @t.SchedulerStartTime }).ToList();
//	xxd.Dump();
//
//	// This should be only for Romania Objective Report
//	var roRgsd = getRGSD("ROMANIA OBJECTIVE");
//
//	// This should be only for NewZealand Annual Report
//	var nzRgsd = getRGSD("NZ ANNUAL REPORT");
//	nzRgsd.Dump();
//	// (nzRgsd["en-US"])["START_MONTH"].Dump();
#endregion CreateObjBatchScheduler part
