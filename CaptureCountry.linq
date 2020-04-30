<Query Kind="Program">
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

void Main()
{
	string strCtrs = "UNITED STATES OF AMERICA,USA,US of A,India,Germanu,Germany,Slovakia,Kosova,Kosovo";
	foreach (var ctry in strCtrs.Split(','))
	{
		Console.WriteLine($"Capture country retrieved for '{ctry}' is : {CaptureCountry(ctry)}");
	}
}

class CountryLookup
{
	public string ISOCountryCode { get; set; }
	public string CountryName {get;set;}
}

private string CaptureCountry(string cntry)
{
	List<string> lstUnitedStates = new List<string>() { "UNITED STATES OF AMERICA", "UNITED STATES", "USA" };
	List<CountryLookup> lstAllCountries = Countries.Select(c => new CountryLookup { ISOCountryCode = c.IsoCountryCode2, CountryName = c.CountryName }).ToList();
	List<CountryLookup> lstTempCntry = new List<CountryLookup>();
	if (!string.IsNullOrEmpty(cntry))
	{
		string cntryValue = cntry.Trim().ToUpper();

		//1.First Condition - checking with lstUnitedStates[UnitedStatesofAmerica || USA || US]
		if (lstUnitedStates.Contains(cntryValue)) { return "US"; }

		//2.Second Condition - Checking with ISO_COUNTRY_CODE_2
		lstTempCntry = lstAllCountries.FindAll(s => s.ISOCountryCode.ToUpper().Trim().Equals(cntryValue));
		if (lstTempCntry != null && lstTempCntry.Count > 0) { return lstTempCntry[0].ISOCountryCode; }

		//3.Third Condition - Checking with Country Name
		lstTempCntry = lstAllCountries.FindAll(s => s.CountryName.ToUpper().Trim().Equals(cntryValue));
		if (lstTempCntry != null && lstTempCntry.Count > 0) { return lstTempCntry[0].ISOCountryCode; }
	}
	return string.Empty;
}