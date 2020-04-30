<Query Kind="Expression">
  <Connection>
    <ID>efe422d9-cdf3-4e8b-aa68-22d3430af8d5</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\work\iWatch\Package Sources\WU.iWatch.Adapters\bin\Debug\net472\WU.iWatch.Adapters.dll</CustomAssemblyPath>
    <CustomTypeName>WU.iWatch.Adapters.iWatchEntities</CustomTypeName>
    <CustomCxString>CAP_SF_RV</CustomCxString>
    <AppConfigPath>C:\LINQPad5-AnyCPU\myConns.config</AppConfigPath>
    <DisplayName>Snowflake WH</DisplayName>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>C:\work\iWatch\Package Sources\WU.iWatch.Adapters\bin\Debug\net472\DataModel.dll</Reference>
  <NuGetReference>CData.Snowflake</NuGetReference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Snowflake.Data</NuGetReference>
  <Namespace>Snowflake.Data.Client</Namespace>
  <Namespace>Snowflake.Data.Configuration</Namespace>
  <Namespace>Snowflake.Data.Core</Namespace>
</Query>

// C:\Program Files\CData\CData ADO.NET Provider for Snowflake 2019\help\help.htm

CaseDetails.Take (() => 10)