<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var TxnDateTime = DateTime.Now.AddMinutes(-new Random().Next(300, 600));
	var CaseCreateTime = DateTime.Now.AddMinutes(-new Random().Next(1, 300));
	// New case.. Just created
	Console.WriteLine("\n\nNew case json document: ");
	Console.WriteLine(JsonConvert.SerializeObject(new Case
	{
		TenantId = 1,
		CaseId = 123456,
		Order = new Order
		{
			OrderId = "MTCN123456",
			TxnInfo = new TxnInfo
			{
				TotGross = Math.Round(new Random().Next() / 100000.0m, 2),
				RecordDtTm = TxnDateTime
			}
		},
		Status = "New",
		TransactionSide = 'S',
		Entities = new List<UserQuery.Entity> {
		new Entity{ Id = 111 },
		new Entity{ Id=222}
		},
		CaseHistory = new UserQuery.CaseHistory[] { new CaseHistory { Status = "New", DateTime = CaseCreateTime } }
	}, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore	}));

	// Assigned / pushed to a specific user
	Console.WriteLine("\n\nCase assigned to an L1 user: ");
	Console.WriteLine(JsonConvert.SerializeObject(new Case
	{
		TenantId = 2,
		CaseId = 123456,
		Order = new Order
		{
			OrderId = "MTCN123456",
			TxnInfo = new TxnInfo
			{
				TotGross = Math.Round(new Random().Next() / 100000.0m, 2),
				RecordDtTm = TxnDateTime
			}
		},
		Status = "Assigned",
		TransactionSide = 'R',
		Assignee = new User { UserId = 12, FirstName = "Ramesh Kumar", LastName = "Venkataraman" },
		Entities = new List<UserQuery.Entity> {
		new Entity{ Id = 111},
		new Entity{ Id=222}
		},
		CaseHistory = new UserQuery.CaseHistory[] { new CaseHistory { Status = "New", DateTime = CaseCreateTime }, new CaseHistory { Status = "Assigned", DateTime = DateTime.Now, Assignee = 12, Level = "L1", Role = "Analyst" } }
	}, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
}

/// <Summary>
/// The case that iWatch will create (automatically when different sources send case creation message or when a user manually creates a case)
/// </Summary>
public class Case
{

	/// <Summary>
	/// This will set in iWatch when a case gets created based on the App Id (validated against the App Secret of the tenant)
	/// </Summary>
	public int TenantId { get; set; }

	/// <Summary>
	/// This will be auto generated within iWatch when a case gets created
	/// </Summary>
	public int CaseId { get; set; }

	/// <Summary>
	/// This is not part of a transaction. Instead it is determined based on the Rule hit or something else, and probably set by the source.
	/// </Summary>
	public char TransactionSide { get; set; }

	/// <Summary>
	/// The transaction object for which this GSI case is being created .. Read only.. 
	/// 	How do we know whether the transaction status has changed. 
	/// 	Do we really bother about that while dealing with the case?
	/// 	MTCN, Date, Amount, Galactic ID ..
	/// 	What vital details of transaction would an analyst need to aid in disposition / reasoning ?
	/// </Summary>
	// Order
	public Order Order {get;set;}

	public string Country_ISO_Code3 { get; set; }
	public string Country_ISO_Code2 { get; set; }
	public string AttempID { get; set; }
	public string TranSurKey { get; set; }
	public string State_Province_code { get; set; }
	public string WuCardNum { get; set; }
	public string SubjectName { get; set; }
	public string TypeOfTrasnaction { get; set; }

	/// <Summary>
	/// This is probaly the same as the value received in RTRA.. However it has to match the iWatch enumeration / value BusinessGroup
	/// </Summary>
	public string InvestigativeGroupType { get; set; }

	/// <Summary>
	/// This will be auto generated within iWatch when a case gets created
	/// </Summary>
	public string SourceOfTransaction { get; set; }

	/// <Summary>
	/// This property/sub object/document section is the tricky part. 
	///		We will always be calling RTRA, reconciling the list every time against entities that we have dispositioned etc, until we 'Complete' the case.
	/// </Summary>
	public ICollection<Entity> Entities { get; set; }

	/// <Summary>
	/// The status of the case 'New', 'Assigned', 'Pending', 'Completed', 'Closed'.. Should comply to the 'CaseStatus' enumeration 
	/// </Summary>
	public string Status { get; set; }

	/// <Summary>
	/// The current assignee. Whenever a case transitions from One level to another or one status to another. Should the whole diff of the case be stored ? Or only the status changes. We can call CaseLog or CaseHistory based on the information.. What is needed is unkonwn at this moment.
	/// </Summary>
	public User Assignee { get; set; }

	/// <Summary>
	/// The decision on the Case as derived by each level (L1 (analyst), L2, QA, Manager or whomsoever responsible) 
	/// </Summary>
	public string Decision { get; set; }

	/// <Summary>
	/// This is a derived based on unique reasons from the various reasons of different entities in the case
	/// </Summary>
	public Reason[] Reasons { get; set; }

	/// <Summary>
	/// Should the whole diff of the case be stored ? Or only the status changes. 
	///	We can call CaseLog or CaseHistory based on the information.. What is needed is unkonwn at this moment
	/// </Summary>
	public CaseHistory[] CaseHistory { get; set; }
}

public class Entity
{ 
	public int Id { get; set; }
	public string Disposition { get; set; }
	public Reason[] Reasons { get; set; }
}

public class Order
{
	public string OrderId { get; set; }
	public ItemInfo ItemInfo { get; set; }
	public TxnInfo TxnInfo {get;set;}
	public Customer Sender { get; set; }
	public Customer Receiver {get;set;}
}

public class Customer { 
	public string GalacticId  { get; set; } 
}
public class TxnInfo
{
	public DateTime RecordDtTm { get; set; }
	public decimal TotGross { get; set; }
}
public class ItemInfo
{
	public string ItemId { get; set; }
}
public class User
{
	public int UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
}

public enum Reason
{
   Reason_One,
   Reason_Two
}

public class CaseHistory
{
	public int Assignee { get; set; }
	public DateTime DateTime { get; set; }
	public string Status { get; set; }
	public string Role { get; set; }
	public string Level { get; set; }
}