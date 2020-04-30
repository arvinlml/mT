<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System.Net</Namespace>
</Query>

public class Program
{
	static void Main(string[] args)
	{
		//creating object of program class to access methods  
		Program obj = new Program();
		obj.InvokeService();
	}
	public void InvokeService()
	{
		//Calling CreateSOAPWebRequest method  
		HttpWebRequest request = CreateSOAPWebRequest();

		XmlDocument SOAPReqBody = new XmlDocument();
		//SOAP Body Request  
		SOAPReqBody.LoadXml(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tran='http://www.westernunion.com/TransactionLookupService/' xmlns:tran1='http://www.westernunion.com/TransactionLookupService'>
   <soapenv:Header/>
   <soapenv:Body>
      <tran:TransactionLookupWSRequest>
         <tran1:header>
            <AppName Version='2.0'>?</AppName>
            <Timestamp>2019-09-03T11:17:28.6543455-04:00</Timestamp>
            <CorrelationId>830084</CorrelationId>
         </tran1:header>
         <tran1:versionNumber>2.0</tran1:versionNumber>
         <tran1:partnerId>IWATCH</tran1:partnerId>
         <tran1:partnerPwd>68j942Wk1M</tran1:partnerPwd>
         <tran1:txnMtcn>0351417900</tran1:txnMtcn>
         <tran1:txnSurKey>5000000000526978579</tran1:txnSurKey>
         <tran1:txnSide>S</tran1:txnSide>
         <tran1:txnAttemptID>3400000000003300931</tran1:txnAttemptID>
      </tran:TransactionLookupWSRequest>
   </soapenv:Body>
</soapenv:Envelope>");


		using (Stream stream = request.GetRequestStream())
		{
			SOAPReqBody.Save(stream);
		}
		//Geting response from request  
		using (WebResponse Serviceres = request.GetResponse())
		{
			using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
			{
				//reading stream  
				var ServiceResult = rd.ReadToEnd();
				//writting stream result on console  
				ServiceResult.Dump();				
			}
		}
	}

	public HttpWebRequest CreateSOAPWebRequest()
	{
		//Making Web Request  
		HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://10.45.235.137:8180/TransactionLookupService/TransactionLookupService");
		//SOAPAction  
		Req.Headers.Add(@"SOAPAction:");
		//Content_type  
		Req.ContentType = "text/xml;charset=\"utf-8\"";
		Req.Accept = "text/xml";
		//HTTP method  
		Req.Method = "POST";
		//return HttpWebRequest  
		return Req;
	}
}