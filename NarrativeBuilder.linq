<Query Kind="Program">
  <Output>DataGrids</Output>
</Query>

void Main()
{
	// Console.WriteLine(buildNarrative(new CaseWithTransactions()));
	foreach (var element in GetCaseTxn_TestCases())
	{
		Console.WriteLine(buildNarrative(element));
	}
}


public List<CaseWithTransactions> GetCaseTxn_TestCases()
{
	List<CaseWithTransactions> cwt = new List<CaseWithTransactions>();
	for (int i = 0; i < 5; i++)
	{
		CaseWithTransactions thisCWT = new CaseWithTransactions { theCase = new Case { CaseId = "M" + i }, transactionList = new List<UserQuery.Transactions>() };
		for (int j = 0; j < new Random().Next(1, i + 1);)
		{
			thisCWT.transactionList.Add(new Transactions
			{
				MTCN = j++.ToString(),
				SendDate = DateTime.Today.AddDays(- new Random(i).Next()),
				PayeeName = RandomNames.names[new Random(i).Next(0, RandomNames.names.Length)]
			});
		}
		cwt.Add(thisCWT);
	}
	return cwt;
}

string buildNarrative(CaseWithTransactions caseWithTransactions)
{
	string retVal = "Transactions are required";
	if (caseWithTransactions.transactionList.Count == 1)
	{
		Transactions singleTran = caseWithTransactions.transactionList[0];
		retVal = $@"Western Union (WU) is filing this SAR to report 1 transaction conducted on {singleTran.SendDate.ToString("MM/DD/YYYY")} totaling $<send amount> (<transaction status> transactions) relating to a potential fraud reported to us.
	We have reason to believe that Subject '{singleTran.PayeeName}' <action type> a transaction from sender <sender full name> through WU's digital services involving the use of a compromised <p.in method> information.

	<Transaction_decription>
	Transactions occurring on WU's digital platform may contain additional information such as IP and email address.
	<Attempted transaction description>
	<Sensitive data narrative>";
	}
	/*
2.  <send date> - S.Date, format MM/DD/YYYY
3. <receiver full name> - P.Name
4. <sender full name> - S.Name
5. <send amount> - S. US Prin in format 9,999,99
6. <p.in method> - a) if p.in channel=AC or BA or IB or IA or EW or DI or EB pull value "bank account" b)  if p.in channel=DC or CR or DA pull value "credit card" 
7. <transaction status> a)  if txn_status=3, than pull value "completed" b) if txn_status ≠3, than pull value "attempted"
8. <Attempted transaction description> if txn_status ≠3, than pull value "Attempted but uncompleted transactions may have been blocked for various compliance reasons. These are generally returned to the sender. Recent attempted but uncompleted transactions may also be in progress, awaiting pickup by the receiver."
9. <Transaction_decription> a) if txn_status=3, than pull value Subject <receiver full name> received 1 transaction in principal amounts equaling $<Pay amount>, as listed below. b) if txn_status≠3, than pull value Subject <sender full name> attempted to send 1 transaction in principal amount equaling $<send amount>, as listed below.  
<pay amount>- P. US Prin in format 9,999,99
10. <action type> a)  if txn_status=3, than pull value "received" b) if txn_status ≠3, than pull value "attempted to receive"
<Sensitive data narrative>
 a) if p.in channel=DC or CR or DA is available in at least one of  txns and other p.in channel is not equal to AC or BA or IB or IA or EW or DI or EB include value "We identified potentially compromised credit card information. For credit card numbers please refer to below attached transaction details."
b) if p.in channel=AC or BA or IB or IA or EW or DI or EB  is available in at least one of  txns include value  and other p.in channel is not equal to DC or CR or DA "We identified potentially compromised bank account information. For bank account numbers please refer to below attached transaction details."
c) In case  p.in channel=(DC or CR or DA) & p.in channel=(AC or BA or IB or IA or EW or DI or EB)  include value "We identified potentially compromised credit card, bank account information. For account numbers, credit card numbers please refer to below attached transaction details.".
	*/
	else
		retVal = $"Transactions are {caseWithTransactions.transactionList.Count()} transactions in the case {caseWithTransactions.theCase.CaseId}";
	return retVal;
}

public class RandomNames
{
	public static string[] names = {"Aravindh J", "Ramesh V", "Dmitry G", "Gunasekar K", "Ganapathi S", "Rajyalakshmi V", "Ponnuraj S"};
}


public class Case
{
	public string CaseId { get; set; }
}

public class Transactions
{
	public string MTCN { get; set; }
	public string PayeeName {get;set;}
	public DateTime SendDate { get; set; }
}

public class CaseWithTransactions
{
	public Case theCase { get; set; }
	public List<Transactions> transactionList { get; set; }
}
