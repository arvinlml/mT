<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>iWatch.TIBCO.EMS</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>TIBCO.EMS</Namespace>
</Query>

static int counter = 0;

Dictionary<string, int> myQueueStat = new Dictionary<string, int> { };

void Main(string[] args)
{
	var env = "QA";
	// if (args.Length>0)	env=args[0];
	myQueueStat[$"WU.QA.AN.SENDNOTIF.ICS.EMAIL.QUEUE"] = 0;
	Parallel.For(1, 2, (i, state) => SendRandomMessage(env));
	Console.WriteLine(myQueueStat);
}

void SendRandomMessage(string env)
{
	Destination destination;
	ConnectionFactory factory;
	Connection connection;
	Session session;
	try
	{
		destination = null;
		string uri = "tcp://10.17.193.24:7200";
		// uri = "tcp://10.45.234.100:7200";
		// uri = "tcp://10.45.234.240:7200"; // ,tcp://10.45.234:241:7200";
		uri="10.45.234.240:7200";
		string qNameOrTopic;
		qNameOrTopic = GetRandomQName(env);
		TCreds creds = GetCredsByTopic(qNameOrTopic);
		factory = new TIBCO.EMS.ConnectionFactory(uri);
		connection = factory.CreateConnection(creds.uName, creds.Pwd);
		session = connection.CreateSession(false, Session.EXPLICIT_CLIENT_ACKNOWLEDGE);

		// All are old queues...

		// Not enough permissions to add a message to DEV Queues.. For testing purposes, we should have so that we can play around in the DEV environment.
		if (qNameOrTopic.EndsWith("QUEUE"))
			destination = session.CreateQueue(qNameOrTopic);
		else
			destination = session.CreateTopic(qNameOrTopic);

		// create the producer
		MessageProducer msgProducer = session.CreateProducer(null);

		connection.Start();
		TextMessage msg = session.CreateTextMessage();
		msg.Text = GetXmlByTopic(qNameOrTopic);

		// Console.WriteLine($"Before sending to queue!! '{msg.MessageID}'. If the code is clean should be empty ;)" );
		msgProducer.Send(destination, msg);
		// Console.WriteLine($"Sent to queue!! {msg.MessageID}");

		Console.WriteLine($"{++counter} Message sent to queue {qNameOrTopic}");
		myQueueStat[qNameOrTopic] = myQueueStat[qNameOrTopic] + 1;
		// session.CreateSender(destination).Send(msg);
	}
	catch (System.OutOfMemoryException omex)
	{
		GC.Collect();
		Console.WriteLine(omex);
	}
	catch (Exception ex)
	{
		// EMSException - Failed to connect to the server at {uri}
		// InvalidDestinationException - If the queue / topic name is wrong (or) trying to connect to queue for a topic and vice versa.
		// SecurityException - If User / Pwd to connect is incorrect
		// ArgumentException - If the deliver/acknowledgement mode is not set correctly		
		Console.WriteLine(ex.Message);
		Console.WriteLine(ex);
		Environment.ExitCode = ex.HResult;
		return;
	}
	finally
	{
		session = null;
		connection = null;
		factory = null;
		destination = null;
	}
}

string GetRandomQName(string env)
{
	string[] sQueues = {"WU.QA.AN.SENDNOTIF.ICS.EMAIL.QUEUE"};
	return sQueues.Skip(new Random().Next(0, sQueues.Length)).FirstOrDefault();
}

TCreds GetCredsByTopic(string qNameOrTopic)
{
	TCreds retval = new TCreds();
	if (qNameOrTopic.Contains("TRANSFEED"))
	{
		// TransFeed creation creds
		retval.uName = retval.Pwd = "RTRACRTCASE";
	}
	else if (qNameOrTopic.Contains("GSI"))
	{
		if (qNameOrTopic.Contains(".DEV."))
			retval.uName = retval.Pwd = "IWATCHCASESVC";
		else
			retval.uName = retval.Pwd = "RTRA2IWATCHCASE";
	}
	else if (qNameOrTopic.Contains("CASE"))
	{   // Case creation creds
		retval.uName = retval.Pwd = "IWATCHCASESVC";
	}
	else if (qNameOrTopic.Contains("NOTIF"))
	{   // Case creation creds
		retval.uName = retval.Pwd = "IWATCHSENDNOTIF";
	}
	else
	{
		retval.uName = retval.Pwd = "";
	}
	return retval;
}

string GetXmlByTopic(string topic)
{
	topic = string.Join(".", topic.Split('.').Skip(2));
	var myMtcn = getRandomMTCN();
	switch (topic)
	{
		#region case "IWATCH.COMPLIANCE.CREATECASE.QUEUE":
		case "":
			return $@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
			< ns0:NotificationRequest xmlns:ns0='http://www.wu.com/schemas/message/NotificationRequest'>
  <ns1:Header xmlns:ns1='http://www.wu.com/schemas/common/Header'>
    <ns1:Source>IWATCH</ns1:Source>
    <ns1:AppName>Auto Notification</ns1:AppName>
    <ns1:HostName></ns1:HostName>
    <ns1:Timestamp>2020-03-27T13:10:52.5735335-04:00</ns1:Timestamp>
    <ns1:CorrelationId>3000000000009821645</ns1:CorrelationId>
    <ns1:TransactionId></ns1:TransactionId>
  </ns1:Header>
  <ns1:Customer xmlns:ns1='http://www.wu.com/schemas/Customer'>
    <ns2:Name xmlns:ns2='http://www.wu.com/schemas/common/Name'>
      <ns2:FirstName>SAKSHI</ns2:FirstName>
      <ns2:LastName ns2:Paternal='' ns2:Maternal=''>JHA</ns2:LastName>
      <ns2:NameType>D</ns2:NameType>
    </ns2:Name>
    <ns2:CustomerId xmlns:ns2='http://www.wu.com/schemas/customer/CustomerId'>
      <ns2:Key>GALACTICID</ns2:Key>
      <ns2:Value>3000000000009821645</ns2:Value>
    </ns2:CustomerId>
    <ns2:Address xmlns:ns2='http://www.wu.com/schemas/common/Address'>
      <ns2:AddressType>PRIMARY</ns2:AddressType>
      <ns2:Line1>LANE 250</ns2:Line1>
      <ns2:City>PARIS</ns2:City>
      <ns2:StateProvince></ns2:StateProvince>
      <ns2:PostalCd>75008</ns2:PostalCd>
      <ns2:Country>FRA</ns2:Country>
    </ns2:Address>
    <ns2:Email xmlns:ns2='http://www.wu.com/schemas/common/Email'>
      <ns2:EmailType ns2:Desc='RCPEMAIL'>PRIMARY</ns2:EmailType>
      <ns2:EmailValue>Aravindh.Janakiraman@wu.com</ns2:EmailValue>
    </ns2:Email>
    <ns2:Preference xmlns:ns2='http://www.wu.com/schemas/customer/Preference'>
      <ns2:PrefLanguageCode ns2:Desc=''>FR</ns2:PrefLanguageCode>
    </ns2:Preference>
  </ns1:Customer>
  <ns0:MessageType>FSS_SERVICE_FOR_PERSONAL_REMITTANCE</ns0:MessageType>
  <ns0:MessageChannelPreference>EMAIL</ns0:MessageChannelPreference>
  <ns0:OriginCountry>IND</ns0:OriginCountry>
  <ns0:MessageValues>
    <ns0:MessageValue>
      <ns0:Name>MTCN</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>ReceiverName</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>ReceiverCountry</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>ReceiverLanguage</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>MessageCategory</ns0:Name>
      <ns0:Value>S</ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>SendType</ns0:Name>
      <ns0:Value>M</ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>Currency</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>PaidAmount</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>AgentID</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
    <ns0:MessageValue>
      <ns0:Name>NetworkID</ns0:Name>
      <ns0:Value></ns0:Value>
    </ns0:MessageValue>
  </ns0:MessageValues>
</ns0:NotificationRequest>";
		case "IWATCH.COMPLIANCE.CREATECASE.QUEUE":
			return $@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
	<CreateCase xmlns:ns2='http://www.wu.com/schemas/common/Header'>
    <GalacticId>3000000000077982684</GalacticId>
    <IsIDDoc>N</IsIDDoc>
    <MTCN>{myMtcn}</MTCN>
    <TransactionDate>2018-11-14-05:00</TransactionDate>
    <InvestigativeGroupType>CCB</InvestigativeGroupType>
    <TransactionSide>S</TransactionSide>
    <Country_ISO_Code3>AUS</Country_ISO_Code3>
    <Country_ISO_Code2>AU</Country_ISO_Code2>
    <AttempID>3000000001880416693</AttempID>
    <TranSurKey>7000000000554496005</TranSurKey>
    <TxnAmount>424929.56399999995</TxnAmount>
    <State_Province_code>VIC</State_Province_code>
    <ReasonCode></ReasonCode>
    <SourceOfReferral></SourceOfReferral>
    <Comments></Comments>
    <WuCardNum>133D37513</WuCardNum>
    <SourceOfTransaction>RTRA</SourceOfTransaction>
    <RequiredDocuments>
        <UCB></UCB>
    </RequiredDocuments>
    <SubjectName>TREVOR GEORGE JENSEN</SubjectName>
    <TypeOfTrasnaction>R</TypeOfTrasnaction>
    <AggregationOrSingle>S</AggregationOrSingle>
    <TransactionSDQs>
        <TemplateID>UNI_01</TemplateID>
        <TransactionSDQ>
            <UCBID>21</UCBID>
            <Answer>Rent/Mortgage</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>44</UCBID>
            <Answer>Savings</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>70</UCBID>
            <Answer>Friend</Answer>
        </TransactionSDQ>
    </TransactionSDQs>
    <InvGrps>
        <InvGrp>
            <InvGrpType>CCB</InvGrpType>
            <Rule>
                <RuleId>113085</RuleId>
                <RuleDesc>14.113085:Australia to Cambodia</RuleDesc>
                <SubRule>
                    <SubRuleId></SubRuleId>
                    <SubRuleDesc>Australia to Cambodia</SubRuleDesc>
                </SubRule>
            </Rule>
        </InvGrp>
    </InvGrps>
    <ns2:Header>
        <ns2:Source>RTRA</ns2:Source>
        <ns2:AppName></ns2:AppName>
        <ns2:HostName>10.45.16.80</ns2:HostName>
        <ns2:Timestamp>{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFzzz")}</ns2:Timestamp>
        <ns2:TransactionId>{myMtcn}</ns2:TransactionId>
    </ns2:Header>
</CreateCase>";
		#endregion

		#region case "RTRA.IWATCH.AUICASE.CREATION.QUEUE":
		case "RTRA.IWATCH.AUICASE.CREATION.QUEUE":
			return $@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
<CreateCase xmlns:ns2='http://www.wu.com/schemas/common/Header'>
    <GalacticId>1000000000052498627</GalacticId>
    <IsIDDoc>N</IsIDDoc>
    <MTCN>{myMtcn}</MTCN>
    <TransactionDate>2018-10-11-04:00</TransactionDate>
    <InvestigativeGroupType>DDCU</InvestigativeGroupType>
    <TransactionSide>S</TransactionSide>
    <Country_ISO_Code3>ITA</Country_ISO_Code3>
    <Country_ISO_Code2>IT</Country_ISO_Code2>
    <AttempID>5000000000002114379</AttempID>
    <TranSurKey>3000000000052280883</TranSurKey>
    <TxnAmount>311.52</TxnAmount>
    <WuCardNum>854A99100</WuCardNum>
    <ConsumerKey>I0qj7+EL4StTHcWcG0qz6lpHHh6PUEm3zLxOG0Bm6mk=</ConsumerKey>
    <FiscalCode>MZZCRL84S28H501H</FiscalCode>
    <IDType>C</IDType>
    <IDNumber>U1G708467J</IDNumber>
    <IDIssueCountry>ITALY</IDIssueCountry>
    <IDIssueDate>22122014</IDIssueDate>
    <SourceOfTransaction>RTRA</SourceOfTransaction>
    <RequiredDocuments>
        <UCB></UCB>
    </RequiredDocuments>
    <SubjectName>CARLO MAZZOLI</SubjectName>
    <TypeOfTrasnaction>R</TypeOfTrasnaction>
    <TransactionSDQs>
        <TemplateID>UNI_01</TemplateID>
        <TransactionSDQ>
            <UCBID>21</UCBID>
            <Answer>FAMILY SUPPORT/LIVING EXPENSES</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>44</UCBID>
            <Answer>SALARY/INCOME</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>70</UCBID>
            <Answer>FAMILY</Answer>
        </TransactionSDQ>
    </TransactionSDQs>
  <ParticipatingTxns>
     <MTCN>{myMtcn.ToString().Substring(0, 10)}</MTCN>
     <TxnDate>2018-10-11 03:31:38.71</TxnDate>
     <surrogateKey>3000000000052280842</surrogateKey>
     <ConsumerKey>mDUAoINJZKRIKsFmwRrnBGvI8jGcAmQR/byNhC7LmYg=</ConsumerKey>
     <FiscalCode>MZZCRL84S28H501H</FiscalCode>
     <IDType>C</IDType>
     <IDNumber>U1G708467J</IDNumber>
     <IDIssueCountry>ITALY</IDIssueCountry>
     <IDIssueDate>22122014</IDIssueDate>
     <TransactionSide>S</TransactionSide>
     <AttempID>5000000000002114286</AttempID>
    </ParticipatingTxns>    <InvGrps>
        <InvGrp>
            <InvGrpType>DDCU</InvGrpType>
            <Rule>
                <RuleId>9936</RuleId>
            </Rule>
            <Rule>
                <RuleId>9933</RuleId>
            </Rule>
        </InvGrp>
    </InvGrps>
    <ns2:Header>
        <ns2:Source>RTRA</ns2:Source>
        <ns2:AppName></ns2:AppName>
        <ns2:HostName>10.45.16.85</ns2:HostName>
        <ns2:Timestamp>{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFzzz")}</ns2:Timestamp>
        <ns2:TransactionId>1828481672942024</ns2:TransactionId>
    </ns2:Header>
</CreateCase>";
		#endregion

		#region case "KYC.RTRA.IWATCH.TRANSFEED.QUEUE":
		case "KYC.RTRA.IWATCH.TRANSFEED.QUEUE":
			return $@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
<CreateCase xmlns:ns2='http://www.wu.com/schemas/common/Header'>
    <GalacticId>1000000000007746373</GalacticId>
    <IsIDDoc>N</IsIDDoc>
    <MTCN>{myMtcn}</MTCN>
    <TransactionDate>2018-11-12-05:00</TransactionDate>
    <InvestigativeGroupType>LAW94</InvestigativeGroupType>
    <TransactionSide>S</TransactionSide>
    <Country_ISO_Code3>ITA</Country_ISO_Code3>
    <Country_ISO_Code2>IT</Country_ISO_Code2>
    <AttempID>4000000001635481764</AttempID>
    <TranSurKey>8000000000478409816</TranSurKey>
    <TxnAmount>35175.9709713</TxnAmount>
    <WuCardNum>265934826</WuCardNum>
    <SourceOfTransaction>RTRA</SourceOfTransaction>
    <RequiredDocuments>
        <UCB></UCB>
    </RequiredDocuments>
    <SubjectName>MARIA MAGDALENA TUTASIG CAISAGUANO</SubjectName>
    <TypeOfTrasnaction>R</TypeOfTrasnaction>
    <TransactionSDQs>
        <TemplateID>ITE_05</TemplateID>
        <TransactionSDQ>
            <UCBID>21</UCBID>
            <Answer>Remittance to family members</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>44</UCBID>
            <Answer>From employment</Answer>
        </TransactionSDQ>
    </TransactionSDQs>
    <InvGrps>
        <InvGrp>
            <InvGrpType>LAW94</InvGrpType>
        </InvGrp>
    </InvGrps>
    <TransactionDetails>
        <ExternalTxnKey>8000000000478409816</ExternalTxnKey>
        <MTCN10>{myMtcn.ToString().Substring(0, 10)}</MTCN10>
        <MTCN16>{myMtcn}</MTCN16>
        <SendDate>2018-11-12-05:00</SendDate>
        <SendTime>14:14:23</SendTime>
        <SendLocalDateTime>1504-08-17-05:00</SendLocalDateTime>
        <SendUSPrincipal>35175.9709713</SendUSPrincipal>
        <SendUSCharges>490.0</SendUSCharges>
        <SendLocalPrincipal>31287.0</SendLocalPrincipal>
        <SendLocalCharges>490.0</SendLocalCharges>
        <SendCurrency>EUR</SendCurrency>
        <SendAgentId>AJW824624</SendAgentId>
        <SendAgentNetworkId>IT112</SendAgentNetworkId>
        <SendAgentSubNetworkId>15032017</SendAgentSubNetworkId>
        <SendAgentCountry>IT</SendAgentCountry>
        <SendOperatorId>100</SendOperatorId>
        <SendTerminalId>AQ3U</SendTerminalId>
        <SLastName>TUTASIG CAISAGUANO</SLastName>
        <SFirstName>MARIA MAGDALENA</SFirstName>
        <SMiddleName></SMiddleName>
        <SGender>F</SGender>
        <SDateOfBirth>23041969</SDateOfBirth>
        <SCountryOfBirth>ECUADOR</SCountryOfBirth>
        <SCountryOfResidence>ITA</SCountryOfResidence>
        <SPlaceOfBirth></SPlaceOfBirth>
        <SCity>ROMA</SCity>
        <SAddressLine1>VIA G FERRARI 12</SAddressLine1>
        <SAddressLine2></SAddressLine2>
        <SZIPCode>002</SZIPCode>
        <SFiscalCode>TTSMMG69D63Z605W</SFiscalCode>
        <SIDType>B</SIDType>
        <SIDNumber>o2lc6PQe20oJc9fx2VTzcA==</SIDNumber>
        <SIDIssueDate>15032017</SIDIssueDate>
        <SIDExpiryDate>23042027</SIDExpiryDate>
        <SIDIssueCountry>Italia</SIDIssueCountry>
        <PayAgentId>15032017</PayAgentId>
        <PayAgentNetworkId>15032017</PayAgentNetworkId>
        <PayAgentSubNetworkId>15032017</PayAgentSubNetworkId>
        <PayAgentCountry>EC</PayAgentCountry>
        <PLastName>TUTASIG SILVA</PLastName>
        <PFirstName>NANCY ELIZABETH</PFirstName>
        <PMiddleName></PMiddleName>
        <TxnStatus>1</TxnStatus>
        <Brand>WU</Brand>
        <SendTemplate>ITE_05</SendTemplate>
        <PayTemplate>15032017</PayTemplate>
    </TransactionDetails>
    <ns2:Header>
        <ns2:Source>RTRA</ns2:Source>
        <ns2:AppName></ns2:AppName>
        <ns2:HostName>10.47.16.163</ns2:HostName>
        <ns2:Timestamp>{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFzzz")}</ns2:Timestamp>
        <ns2:TransactionId>{myMtcn}</ns2:TransactionId>
    </ns2:Header>
</CreateCase>";
		#endregion

		#region 		case "KYC.IWATCH.CREATECASE.QUEUE":
		case "KYC.IWATCH.CREATECASE.QUEUE":
		#endregion

		#region 		case "KYC.RTRA.IWATCH.GSICASE.QUEUE":
		case "KYC.RTRA.IWATCH.GSICASE.QUEUE":
		#endregion

		default:
			return $@"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
<CreateCase xmlns:ns2='http://www.wu.com/schemas/common/Header'>
 <GalacticId>3000000000009467278</GalacticId>
    <IsIDDoc>N</IsIDDoc>
    <MTCN>{myMtcn}</MTCN>
    <TransactionDate>2019-08-23-04:00</TransactionDate>
    <InvestigativeGroupType>CCB</InvestigativeGroupType>
    <TransactionSide>S</TransactionSide>
    <Country_ISO_Code3>GBR</Country_ISO_Code3>
    <Country_ISO_Code2>GB</Country_ISO_Code2>
    <AttempID>3400000000003257239</AttempID>
    <TranSurKey>5000000000526965181</TranSurKey>
    <TxnAmount>193775.984</TxnAmount>
    <ReasonCode></ReasonCode>
    <SourceOfReferral></SourceOfReferral>
    <Comments></Comments>
    <WuCardNum>907A31125</WuCardNum>
    <SourceOfTransaction>RTRA</SourceOfTransaction>
    <RequiredDocuments>
        <UCB></UCB>
    </RequiredDocuments>
    <SubjectName>RAMESH BABU</SubjectName>
    <TypeOfTrasnaction>R</TypeOfTrasnaction>
    <AggregationOrSingle>S</AggregationOrSingle>
    <TransactionSDQs>
        <TemplateID>UNI_01</TemplateID>
        <TransactionSDQ>
            <UCBID>21</UCBID>
            <Answer>FAMILY SUPPORT/LIVING EXPENSES</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>44</UCBID>
            <Answer>SALARY</Answer>
        </TransactionSDQ>
        <TransactionSDQ>
            <UCBID>70</UCBID>
            <Answer>FAMILY</Answer>
        </TransactionSDQ>
    </TransactionSDQs>
    <InvGrps>
        <InvGrp>
            <InvGrpType>CCB</InvGrpType>
            <Rule>
                <RuleId>110105</RuleId>
                <RuleDesc>14.110105:UK to US</RuleDesc>
                <SubRule>
                    <SubRuleId></SubRuleId>
                    <SubRuleDesc>UK to US</SubRuleDesc>
                </SubRule>
            </Rule>
        </InvGrp>
    </InvGrps>
    <OtherChecks>
        <CheckPoint>EMAIL</CheckPoint>
        <CheckDescription>RAMESHBABU.KUMARKRISHNA@ERMAIL.COM</CheckDescription>
    </OtherChecks>
    <OtherChecks>
        <CheckPoint>Product</CheckPoint>
        <CheckDescription>CC</CheckDescription>
    </OtherChecks>
    <OtherChecks>
        <CheckPoint>Brand</CheckPoint>
        <CheckDescription>WU</CheckDescription>
    </OtherChecks>
    <OtherChecks>
        <CheckPoint>PayIn</CheckPoint>
        <CheckDescription>CA</CheckDescription>
    </OtherChecks>
    <OtherChecks>
        <CheckPoint>RecordingChannel</CheckPoint>
        <CheckDescription>AG</CheckDescription>
    </OtherChecks>
    <ns2:Header>
        <ns2:Source>RTRA</ns2:Source>
        <ns2:AppName></ns2:AppName>
        <ns2:HostName>10.45.235.137</ns2:HostName>
		<ns2:Timestamp>{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFzzz")}</ns2:Timestamp>
        <ns2:TransactionId>{myMtcn}</ns2:TransactionId>
    </ns2:Header>
</CreateCase>";
	}
}

object getRandomMTCN() => $"{DateTime.Now.ToString("yyMMddHH")}{new Random().Next(10000000, 99999999)}";

public class TCreds
{
	public string uName { get; set; }
	public string Pwd { get; set; }
}