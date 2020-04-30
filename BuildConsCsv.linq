<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>ChoETL.JSON</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>ChoETL</Namespace>
</Query>

void Main()
{
	string tsStr = DateTime.Now.ToString("yyyyMMdd_HHmmss");
	string consFile = @"C:\Users\312198\Desktop\apn\Consolidated" + tsStr + ".json";
	string consCsvFile = @"C:\Users\312198\Desktop\apn\Consolidated" + tsStr + ".csv";
	List<Dictionary<string, object>> allJson = new List<System.Collections.Generic.Dictionary<string, object>>();
	foreach (string fPath in Directory.GetFiles(@"C:\Users\312198\Desktop\apn", "*.xml"))
	{
		Console.WriteLine($"Processing file {fPath}");
		allJson.Add(BuildJson(fPath));
	}
	File.WriteAllText(consFile, JsonConvert.SerializeObject(allJson));
	using (var r = new ChoJSONReader(consFile))
	{
		using (var w = new ChoCSVWriter(consCsvFile).WithFirstLineHeader())
		{
			w.Write(r);
		}
		if (File.Exists(consCsvFile))
			Console.WriteLine($"Processed CSV file {consCsvFile} sucessfully");
	}
}

Dictionary<string, object> BuildJson(string path)
{
	var document = new XmlDocument();
	document.Load(path);
	var nsmgr = new XmlNamespaceManager(document.NameTable);
	nsmgr.AddNamespace("con", "http://eviware.com/soapui/config");
	var names = document.SelectNodes("//con:properties/con:property/con:name", nsmgr);
	Dictionary<string, object> kv = new Dictionary<string, object>();
	kv.Add("TCFile", Path.GetFileName(path));
	foreach (XmlNode xn in names)
	{
		var k = xn.FirstChild.Value;
		var v = xn.ParentNode.LastChild.InnerText;
		if (!k.ToUpper().EndsWith("XML")) kv.Add(k, v);
	}
	return kv;
}