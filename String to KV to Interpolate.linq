<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

var x="AtNum,AtWeight,BoilPoint,channel,CorrelationID,ElName,Expected Response,fundsDelayInMinutes,middleNameLength,middleNameLength_RUAB,MTCN,Ordinal,ordinalNumber,OverallFX,partnerAccountNumber,partnerBankCode,partnerBankName,partnerBeneficiaryName,partnerbicOrSwiftBic,partnerBranchCode,paternalNameLength,Purpose,receiveAmount,receiveCountryCode,receiveCountryName,receiveCurrencyCode,receiverAccountType,receiveraddressLine1,receiveraddressLine2,receiverAddressType,receiverCity,receiverDateOfBirth,receiverEmail,receiverFirstName,receiverLastName,receiverMiddleName,receiverMobilePhone,receiverMobilePhonePrefix,receiverNationalId,receiverPrimaryPhone,receiverPrimaryPhonePrefix,receiverUAB,RestPartnerCall_Final_Status,RestPartnerCall_PAY_ACK,sendAmount,sendCountryCode,sendCurrencyCode,senderAddressLine1,senderAddressLine2,senderAgentAccountNo,senderAgentID,senderCity,senderCountryCode,senderFirstName,senderLastName,senderMiddleName,senderStateCode,senderUAB,senderUCB,senderZipCode,Symbol,TCName,testCaseDescription,testCaseName,testStep0Name,testStep0Results,testStep1Name,testStep1Results,testStep2Name,testStep2Results,testStep3Name,testStep3Results,testStepCount,usdFXRate,usdSendAmount,senderAgentAcountNumber,senderMobilePhone,PartnerID,PartnerName,senderPrimaryPhone,senderMobilePhonePrefix,receiverState,senderfxRate,receiverfxRate,senderPrimaryPhonePrefix,fundsAvailableDate,receiverZipCode,accountNumberPrefix,bicOrSwiftBic,receiverStateCode,deliveryServiceCode";
string[] xStrs=x.Split(',');
Dictionary<string,string> xDict = new Dictionary<string,string>();
foreach (var element in xStrs)
{
	xDict.Add(element, $"<{element}>");
}
Console.WriteLine(JsonConvert.SerializeObject(xDict));