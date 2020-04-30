<Query Kind="Program">
  <Connection>
    <ID>6e2fc036-dbbd-4463-a6ed-184cacc03a35</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\CPCE Services\DataModel2\bin\Debug\DataModel2.dll</CustomAssemblyPath>
    <CustomTypeName>iWatch.iWatchEntities</CustomTypeName>
    <CustomCxString>iWEnt</CustomCxString>
    <AppConfigPath>C:\Users\312198\Source\Repos\WebApplication1\WebApplication1\Web.config</AppConfigPath>
  </Connection>
</Query>

void Main()
{
	var param = new  UserQuery.EfileWorkflowParameter	{ reportGroupId = 1000000206, batchId = 124};
	GeneratedEFile(param); 
}

void GeneratedEFile(UserQuery.EfileWorkflowParameter param)
{
	var rgsd = GetRGSD(param);
	GetCRDisclosure(param);
	CreateTable(param);
	GetRptSeqNum(param);
}

void CreateTable(EfileWorkflowParameter param)
{
	
}

void GetRptSeqNum(EfileWorkflowParameter param)
{
	int year = DateTime.Now.Year;
	int? seqNumber = 1;
	int maxBatchId = EfileBatch.
	Where(x => x.RptGrpId ==  param.reportGroupId  
	&& x.BatchStatus == 23 
	&& x.CreateTimestamp.Value.Year == year).
	OrderByDescending(x => x.BatchId).
	Select(x => x.BatchId).FirstOrDefault();
	Console.WriteLine(maxBatchId);
	var sMax = maxBatchId.ToString();
	var maxRptSeqNum = CaseReport.
	Where(x => x.EfileBatchId == sMax).
	OrderByDescending(x => x.SeqNum).
	Select(x => x.SeqNum).FirstOrDefault().ToString();
	seqNumber = string.IsNullOrWhiteSpace(maxRptSeqNum) ? 1 : Convert.ToInt32(maxRptSeqNum) + 1;
	Console.WriteLine(seqNumber);
}

void GetCRDisclosure(EfileWorkflowParameter param)
{
	param.lstDisclosure = EfileBatchElement.
		Where(x => x.BatchId.Equals(param.batchId) && x.StatusTimestamp == null).
		Join(CrDisclosure, c => c.ReportId, p => p.ReportId, (c, p) => p).
		OrderBy(x=>x.ReportId).ToList();
	Console.WriteLine(param.lstDisclosure);

	param.lstDisclosureSubjectAll = EfileBatchElement.
		Where(x => x.BatchId.Equals(param.batchId) && x.StatusTimestamp == null).
		Join(CrDisclosureTransactionDetails, c => c.ReportId, p => p.ReportId, (c, p) => p).
		Distinct().
		OrderBy(x => x.ReportId).ToList();
	Console.WriteLine(param.lstDisclosureSubjectAll);

	param.lstSubjectPhotoIdAll= EfileBatchElement.
		Where(x => x.BatchId.Equals(param.batchId) && x.StatusTimestamp == null).
		Join(CrSarSubjectPhotoId, c => c.ReportId, p => p.ReportId, (c, p) => p).
		Distinct().
		OrderBy(x => x.ReportId).ToList();
	Console.WriteLine(param.lstSubjectPhotoIdAll);
}

object GetRGSD(UserQuery.EfileWorkflowParameter param)
{
	var rgsd = ReportGroupStaticData.
	Where(x => x.RptGrpId == param.reportGroupId).
	Select(x => new { x.CultureInfo, x.AttributeName, x.AttributeValue }).OrderBy(x => x.AttributeName).GroupBy(x => x.CultureInfo).
		ToDictionary(x => x.Key, x => x.ToDictionary(y => y.AttributeName, y => y.AttributeValue));
	Console.WriteLine(rgsd);
	return rgsd;
}
public class EfileWorkflowParameter
{
	public string sessionId;
	public int loggedOnUserId;
	public int reportGroupId;
	public string reportGroupName;
	public int batchId;
	public string enableReEfile;
	public string reEfileComments;
	public string fileName;
	public string zipFileName;
	public int outputFileNumber;
	public byte[] fullFileBuffer;
	public string localDate;
	public string localDateTime;
	public string goAMLValidationError;
	public string isAutoSubmission;
	public string xlsFileName;
	public string xlsSheetName;
	public string xsdSchemaPath;
	public bool isXSDValidation;
	public string targetNameSpace;
	public List<CrDisclosure> lstDisclosure;
	public List<CrDisclosureTransactionDetails> lstDisclosureSubjectAll;
	public List<CrSarSubjectPhotoId> lstSubjectPhotoIdAll;
	public List<AgentProfile> lstAgentProfile;
	public List<ReportGroupCountryAlias> lstReportGroupCountryAlias;
	public List<ReportGroupStateAlias> lstReportGroupStateAlias;
	public List<ReportGroupCityAlias> lstReportGroupCityAlias;
	public List<CountryReportGroup> lstCountryReportGroup;
	public List<CrDisclosureIdTypeMapping> lstCrDisclosureIdTypeMapping;
	public List<CaseReport> lstCaseReport;
	public string CrDisclosureEFileName;
	public string CrDisclosureFileType;
	public string CrDisclosureIsCompressed;
	public string CrDisclosureDataFormat;
	public string xmlFileName;
	public string docFileName;
	public string datFileName;
	public string pdfFileName;
	public DataSet dsResult;
	public bool isExistingRecords;
	public int reportId;
	public string appVersionId;
	public string isNewEfile;
	public string BatchActionCode = null;
	public Dictionary<int, int> ReportNumberList = null;

	#region Common DataMembers for Export the Data into Excel and coverpagecollection 
	public List<Dictionary<string, string>> CoverPageDataCollection = null;
	public List<Dictionary<string, string>> PdfPageDataCollection = null;
	public Dictionary<string, Dictionary<string, string>> ReportGroupStaticDataWithCulture = null;
	public bool ishypen = false;
	public DataSet dsOutput = null;
	public DataTable dtOutput = null;
	public string directoryPath;
	public string isoCountryCode;
	public string suspiciousNarrative;
	public short xlsHeaderFontColour;
	public bool isXlsHeaderLoweCase;
	public string invGrpCPCCode;
	#endregion Common DataMembers for Export the Data into Excel and coverpagecollection	
	public DMSHeader Header;
		
//	public EfileWorkflowParameter(string pSessionId, int pLoggedOnUserId, int pReportGroupId, string pReportGroupName, int pBatchId, string pEnableReEfile, string pReEfileComments, string pLocalDate, string pLocalDateTime)
//	{
//		sessionId = pSessionId;
//		loggedOnUserId = pLoggedOnUserId;
//		reportGroupId = pReportGroupId;
//		reportGroupName = pReportGroupName;
//		batchId = pBatchId;
//		enableReEfile = pEnableReEfile;
//		reEfileComments = pReEfileComments;
//		localDate = pLocalDate;
//		localDateTime = pLocalDateTime;
//	}

}

public class DMSHeader
{
	public string IP { get; set; }
	public string HostName { get; set; }
	public string AppName { get; set; }
	public string AppVersion { get; set; }
	public string source { get; set; }
	public string CorelationID { get; set; }
}