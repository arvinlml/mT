<Query Kind="Statements">
  <Output>DataGrids</Output>
</Query>

class Phone
{
	public string Tenant_Id { get; set; }
	public string Phone_Number { get; set; }
	public string MTCN16 { get; set; }
	public int PartyId { get; set; }
	public string Type { get; set; }
	public string ISD_Cd { get; set; }
}

class Address
{
	public string Tenant_Id { get; set; }
	public string MTCN16 { get; set; }
	public int PartyId { get; set; }
	public string AddressType { get; set; }
	public string Line1 { get; set; }
	public string Line2 { get; set; }
	public string City { get; set; }
	public string State_Province { get; set; }
	public string Postal_Cd { get; set; }
	public string Country_ISO2 { get; set; }
}