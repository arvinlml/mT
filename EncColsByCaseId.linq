<Query Kind="Program">
  <Connection>
    <ID>1730c124-1713-46d8-a907-60ef54d6c73e</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\CPCE Services\DataModel\bin\Debug\DataModel.dll</CustomAssemblyPath>
    <CustomTypeName>iWatch.iWatchEntities</CustomTypeName>
    <CustomCxString>iWEntAj</CustomCxString>
    <AppConfigPath>C:\Users\312198\Source\Repos\WebApplication1\WebApplication1\Web.config</AppConfigPath>
    <DisplayName>DM2_SIT</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
</Query>

// Folder = ${_mQueueConfig.QNameOrTopic}
// File = ${tm.MessageId} // If already found skip writing

void Main()
{
	var p = new RepParam { CaseId = "M18091850791691" };
	p.CaseKey = Cases.Where(c => c.CaseId == p.CaseId).Select(c => c.CaseKey).FirstOrDefault();
	p.RptId = CaseReport.Where(cr => cr.CaseId==p.CaseId).Select(cr => cr.ReportId).FirstOrDefault();
	GetEncryptedValues(p);				
	Console.WriteLine(Cases.Where(c=>c.CaseId ==p.CaseId));
}

class RepParam
{
	public int CaseKey { get; set; }
	public string CaseId { get; set; }
	public int RptId { get; set; }
}

void GetEncryptedValues(RepParam x)
{
	Console.WriteLine("All encrypted columns from Bank Account : By Report Id");
	Console.WriteLine(
	CrBankAccount
	.Where(cba => cba.ReportId == x.RptId)
	.Select(cba => new { cba.BankAccountNumber })
	);

	Console.WriteLine("All encrypted columns from Disclosure Location : By Report Id");
	Console.WriteLine(
	CrDisclosureLocation
	.Where(cdl => cdl.ReportId == x.RptId)
	.Select(cdl => new {cdl.AgentEin})
	);

	Console.WriteLine("All encrypted columns from Disclosure subject : By Report Id");
	Console.WriteLine(
	CrDisclosureSubject
	.Where(cds => cds.ReportId == x.RptId)
	.Select(cds => new { cds.Ssn, cds.IdNumber })
	);

	Console.WriteLine("All encrypted columns from Disclosure Transaction Details : By Report Id");
	Console.WriteLine(
	CrDisclosureTransactionDetails
	.Where(cdtd => cdtd.ReportId == x.RptId)
	.Select(cdtd => new { cdtd.CreditCardNumber, cdtd.IdNumber, cdtd.PBankAccountNumber, cdtd.PIdOther, cdtd.PSsn, cdtd.PTaxIdNumber, cdtd.SBankAccountNumber, cdtd.Ssn, cdtd.Mtcn, cdtd.ReportId })
	);

	var cTs = CaseTransaction.Where(ct => ct.CaseKey == x.CaseKey).Select(ct => ct.Mtcn).ToList();

	Console.WriteLine("Transactions (by Case / Report ID)");
	Console.WriteLine(
	Transactions
	.Where(t => cTs.Contains(t.Mtcn))
	.Select(t => new { t.DebtorAccountNum, t.IntendedPBankAcctNum, t.PBankAccountNumber, t.PClId1Number, t.PClId2Number, t.PClId3Number, t.PIdNumber, t.PId2Number, t.PId3Number, t.P3ClId1Number, t.P3IdNumber, t.PrisonerAccountNum, t.SBankAccountNumber, t.SClId1Number, t.SClId2Number, t.SClId3Number, t.SIdNumber, t.SId2Number, t.SId3Number, t.S3ClId1Number, t.S3IdNumber, t.SendCreditCardNum })
	);

	Console.WriteLine("Case Report");
	Console.WriteLine(
	CaseReport.Where(cr => cr.ReportId==x.RptId)
	);


	//	// Get all encrypted columns from CrSarSubjectPhotoId
	//	Console.WriteLine(
	//	CrSarSubjectPhotoId.Select(csspi => new { csspi.ReportId })
	//	);
	//}
}