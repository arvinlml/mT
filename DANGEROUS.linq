<Query Kind="Statements">
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

// Function on a column in where clause is DANGEROUS
this.Database.Log = Console.WriteLine;
string p_val="M14021050065799   ";
Cases.FirstOrDefault(c => c.CaseId.Trim() == p_val.Trim());
Cases.FirstOrDefault(c => c.CaseId == p_val.Trim());
Cases.Select(c => new {c.CaseId, c.CaseKey}).FirstOrDefault(c => c.CaseId.Trim() == p_val.Trim());
Cases.Select(c => new {c.CaseId, c.CaseKey}).FirstOrDefault(c => c.CaseId == p_val.Trim());
Cases.Count().Dump();