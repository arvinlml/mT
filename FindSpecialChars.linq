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

static string valFromSystemParam = @"[^(a-z A-Z:;,'""#$\n\.0-9é)]";

void Main()
{
	//	this.Database.Log = Console.Write;
	var txns = GetData();
	txns.Dump();
	foreach (var element in txns)
	{
		Console.WriteLine($"Output value: {OnTextChanged(element.agent_address)}");
	}
}

List<AjCsdl> GetData()
{
	// The below Sql statement is loading Transaction into a list of entities.. But using the same EF connection and executing a non tracking object list
	#region SQL statement
	var sqlStmt = $@"SELECT crdl.report_id ,crdl.agent_id , crdl.agent_address from CPCE.CR_DISCLOSURE_LOCATION AS CRDL 
	JOIN CPCE.EFILE_BATCH_ELEMENT AS EBE ON CRDL.REPORT_ID = EBE.REPORT_ID 
	join cpce.efile_batch AS EB ON EBE.batch_id = EB.batch_id
	WHERE EB.rpt_grp_id = 51 FETCH FIRST 50 ROWS ONLY";
	#endregion SQL statement
	//return this.Database.SqlQuery<iWatch.CrDisclosureLocation>(sqlStmt, new DB2Parameter("@pSendDate", dtFrom)).ToList();
	return this.Database.SqlQuery<AjCsdl>(sqlStmt).ToList();
}

static string OnTextChanged(string input)
{
	byte[] bytes = Encoding.UTF8.GetBytes(input);
	string defEnc = Encoding.Default.GetString(bytes);
	Console.WriteLine($"Input value: {defEnc}");
	RegexOptions options = RegexOptions.Multiline;
	Regex regex = new Regex(valFromSystemParam, options);
	return regex.Replace(input, string.Empty);
}

class AjCsdl
{
	public int report_id { get; set; }
	public string agent_id { get; set; }
	public string agent_address { get; set; }
}