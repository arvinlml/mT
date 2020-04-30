<Query Kind="Program">
  <Connection>
    <ID>43000b45-7076-4600-8eca-1eb13feb557d</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\Package Sources\WU.iWatch.Adapters\bin\Debug\net472\WU.iWatch.Adapters.dll</CustomAssemblyPath>
    <CustomTypeName>WU.iWatch.Adapters.iWatchEntities</CustomTypeName>
    <CustomCxString>IWSIT04_AJ</CustomCxString>
    <AppConfigPath>C:\LINQPad5-AnyCPU\myConns.config</AppConfigPath>
    <DisplayName>SIT</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>C:\work\iWatch\Package Sources\WU.iWatch.Adapters\bin\Debug\net472\DataModel.dll</Reference>
  <Namespace>iWatch</Namespace>
</Query>

void Main()
{
	SfGddFormV02.Add(new SfGddFormV02 { AgentId = "SOMETHING IS NOT GOOD" });
	try
	{
		this.SaveChanges();
	}
	catch (DbEntityValidationException ex)
	{
		foreach (var element in ex.EntityValidationErrors)
		{
			foreach (var fex in element.ValidationErrors)
			{
				Console.WriteLine($"Property: {fex.PropertyName}, error: {fex.ErrorMessage}");
			}
		}
	}
	catch (Exception exx)
	{
		Console.WriteLine(exx);
	}
}