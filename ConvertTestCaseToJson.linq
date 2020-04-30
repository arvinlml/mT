<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>ChoETL.JSON</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>ChoETL</Namespace>
</Query>

void Main()
{
	List<string> ProcessedFiles = new List<string>();
	foreach (string fPath in Directory.GetFiles(@"C:\Users\312198\apn", "*.xml"))
	{
		Console.WriteLine($"Processing file {fPath}");
		if (GenerateJson(fPath)) ProcessedFiles.Add(fPath);
	}
	foreach (string fPath in ProcessedFiles)
	{
		var pFile = fPath.TrimEnd("xml".ToCharArray()) + "json";
		var cFile = fPath.TrimEnd("xml".ToCharArray()) + "csv";
		if (File.Exists(pFile))
		{
			Console.WriteLine($"Processed output file {pFile} sucessfully");
			Console.WriteLine($"Creating CSV file for {fPath}");
			using (var r = new ChoJSONReader(pFile))
			{
				using (var w = new ChoCSVWriter(cFile).WithFirstLineHeader())
				{
					w.Write(r);
				}
				if (File.Exists(cFile))
					Console.WriteLine($"Processed CSV file {cFile} sucessfully");
			}
		}
	}
}

bool GenerateJson(string path)
{
	var document = new XmlDocument();
	document.Load(path);
	var nsmgr = new XmlNamespaceManager(document.NameTable);
	nsmgr.AddNamespace("con", "http://eviware.com/soapui/config");
	var names = document.SelectNodes("//con:properties/con:property/con:name", nsmgr);
	Dictionary<string, string> kv = new Dictionary<string, string>();
	foreach (XmlNode xn in names)
	{
		var k = xn.FirstChild.Value;
		var v = xn.ParentNode.LastChild.InnerText;
		if (!k.ToUpper().EndsWith("XML")) kv.Add(k, v);
	}
	var kvJson = JsonConvert.SerializeObject(kv);
	File.WriteAllText(path.TrimEnd("xml".ToCharArray()) + "json", kvJson);
	return kvJson != "{}";
}