<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	EncryptToString(new ToEncrypt { valToEncrypt="abc", dbContextTableName="CPCE.SOMETHING", encryptionContextType= ContextType.PasswordExpire, encryptionContextValue ="[TST]177", fieldCategory= FieldCategory.InActive});
}

public enum ContextType { Active = 1, InActive = 2, Suspended = 3, PasswordExpire = 4, FiveWrongAttempts = 5 };
public enum FieldCategory { Active = 1, InActive = 2, Suspended = 3, PasswordExpire = 4, FiveWrongAttempts = 5 };

public class ToEncrypt
{
	public string valToEncrypt; public ContextType encryptionContextType;
	public string encryptionContextValue;
	public string dbContextTableName;
	public FieldCategory fieldCategory;
}

public static class Cryptography
{
	public static string EncryptToString(string val, string cat)
	{ return "abc";}
}

public string EncryptToString(ToEncrypt theValue)
{
	if (string.IsNullOrWhiteSpace(theValue.valToEncrypt))
	{
		return string.Empty;
	}
	var encVal = string.Empty;
	try
	{
		encVal = Cryptography.EncryptToString(theValue.valToEncrypt, theValue.fieldCategory.ToString());
		if (encVal == theValue.valToEncrypt)
			Console.WriteLine($"{JsonConvert.SerializeObject(theValue)}");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"{JsonConvert.SerializeObject(theValue)}");
		Console.WriteLine("Exception: RRUtils - EncryptToString()" + ex);
	}
	return encVal;
}