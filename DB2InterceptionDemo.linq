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
  <NuGetReference>NLog</NuGetReference>
  <Namespace>NLog</Namespace>
</Query>

void Main()
{
	this.Database.Log = Utils.WriteToLog;
	Cases.FirstOrDefault();
}

// Define other methods and classes here
static class Utils
{
	static ILogger logr= NLog.LogManager.GetLogger("iWatch.DB2");
	internal static void WriteToLog(string logString)
	{
		logr.Info(logString);		
	}
}