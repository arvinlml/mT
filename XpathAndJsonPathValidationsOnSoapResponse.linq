<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var xmlStr = @"
<SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/'>
    <SOAP-ENV:Body>
        <ns0:response xmlns:ns0='http://www.westernunion.com/eai/xsd/custom/Payout.xsd'>
            <ns1:header xmlns:ns0='www.tibco.com/be/ontology/PayoutServices/HTTPService/Events/callPayoutResponse' xmlns:ns1='http://www.westernunion.com/eai/xsd/custom/Payout.xsd' xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/'>
                <ns1:applicationID>TANDEM001</ns1:applicationID>
                <ns1:hostname>localhost</ns1:hostname>
                <ns1:timestamp>2020-01-29T05:10:55.04-05:00[</ns1:timestamp>
                <ns1:requestID>WUNS1$QD2BV$QD2BV:Thread-2086388992805833</ns1:requestID>
                <ns1:correlationID>8992805833</ns1:correlationID>
            </ns1:header>
            <ns0:status>
                <ns0:status>SYS_ERROR</ns0:status>
                <ns0:code>7000</ns0:code>
                <ns0:subStatus>
                    <ns0:subCode>
                        <ns0:code>7409</ns0:code>
                        <ns0:message>System Exception</ns0:message>
                    </ns0:subCode>
                </ns0:subStatus>
                <ns0:message>SYSTEM ERROR</ns0:message>
            </ns0:status>
		</ns0:response>
	</SOAP-ENV:Body>
</SOAP-ENV:Envelope>	
";
XmlDocument doc = new XmlDocument();
doc.LoadXml(xmlStr);
Console.WriteLine(JsonConvert.SerializeObject(doc));
JObject obj = JObject.Parse(JsonConvert.SerializeObject(doc));
// Console.WriteLine(obj);
Console.WriteLine(obj.SelectTokens("SOAP-ENV:Envelope.SOAP-ENV:Body.ns0:response.ns1:header.ns1:correlationID"));
Console.WriteLine(obj.SelectTokens("SOAP-ENV:Envelope.SOAP-ENV:Body.ns0:response.ns0:status.ns0:code"));
Console.WriteLine(obj.SelectTokens("SOAP-ENV:Envelope.SOAP-ENV:Body.ns0:response.ns0:status.ns0:code"));
Console.WriteLine(obj.SelectTokens("SOAP-ENV:Envelope.SOAP-ENV:Body.ns0:response.ns0:status.ns0:subStatus.ns0.subCode.ns0.code"));
Console.WriteLine(obj.SelectTokens("ns1:correlationID"));
var nsmgr = new XmlNamespaceManager(doc.NameTable);
nsmgr.AddNamespace("SOAP-ENV","http://schemas.xmlsoap.org/soap/envelope/");
nsmgr.AddNamespace("ns0", "http://www.westernunion.com/eai/xsd/custom/Payout.xsd");
nsmgr.AddNamespace("ns1","http://www.westernunion.com/eai/xsd/custom/Payout.xsd");
Console.WriteLine(doc.SelectNodes( "//ns0:status/ns0:code",nsmgr));
Console.WriteLine(doc.SelectNodes("//ns1:correlationID", nsmgr));
Console.WriteLine(doc.SelectNodes("//SOAP-ENV:Envelope",nsmgr));