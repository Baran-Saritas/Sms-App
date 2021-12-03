using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLayer
{
    public static class WebServiceTransaction
    {

        public static SendSmsResult SendGetOriginator(string UserName,string Password,string UserCode, string AccountId)
        {
            var smsRequestStr = new StringBuilder();
            var soapRequestStr = new StringBuilder();

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);




            smsRequestStr.AppendLine($@"<![CDATA[
                                        <GetOriginator>
                                                       <Username>{UserName}</Username>
                                                       <Password>{Password}</Password>
                                                       <UserCode>{UserCode}</UserCode>
                                                       <AccountId>{AccountId}</AccountId>
                                        </GetOriginator>
                                       ]]>");



            soapRequestStr.Append($@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns =""https://webservice.asistiletisim.com.tr/SmsProxy"">
                                        <soapenv:Header/>
                                           <soapenv:Body>
                                             <getOriginator>
                                                 <requestXml>
						                            {smsRequestStr}
						                        </requestXml>
                                              </getOriginator>
                                           </soapenv:Body>
                                        </soapenv:Envelope>");




            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(soapRequestStr.ToString());

            var webRequest = (HttpWebRequest)WebRequest.Create("https://webservice.asistiletisim.com.tr/SmsProxy.asmx");
            webRequest.Headers.Add("SOAPAction", "https://webservice.asistiletisim.com.tr/SmsProxy/getOriginator");
            webRequest.ContentType = "text/xml; charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";



            using (var stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }



            var asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            var soapResult = string.Empty;


            using (var webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (var rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                var responseXml = new XmlDocument();
                responseXml.LoadXml(soapResult);

                var errorCode = responseXml.GetElementsByTagName("ErrorCode");
                var Originator = responseXml.GetElementsByTagName("Originator");
                
            }


            SendSmsResult response = new SendSmsResult();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(soapResult);
                var code = xmlDoc.GetElementsByTagName("ErrorCode").Item(0)?.InnerText;
                var Originator = xmlDoc.GetElementsByTagName("Originator").Item(0)?.InnerText;
                response.Originator = Originator;
                response.ResultCode = (code.ToString() == "0") ? 1 : 0;
            }
            catch (Exception ex)
            {
                response.ResultCode = 0;
                response.ResultDescription = "Exception : " + ex.Message + Environment.NewLine + "Response : " + soapResult;
            }
            return response;

        }

    
        public static SendSmsResult SendSms(string UserName, string Password, string UserCode, string AccountId, string Originator, string Title, string TemplateText, List<string> GsmNumbers)
        {
            var soapRequestStr = new StringBuilder();
            var smsRequestStr = new StringBuilder();


            var gsmListStr = new StringBuilder();
            gsmListStr.AppendLine("<ReceiverList>");

            for (var i = 0; i < GsmNumbers.Count; i++)
            {
                gsmListStr.AppendLine($"<Receiver>  {GsmNumbers[i].Trim()}  </Receiver>");
            }

            gsmListStr.AppendLine("</ReceiverList>");

            smsRequestStr.AppendLine($@"<![CDATA[<SendSms>
                                                       <Username>{UserName}</Username>
                                                       <Password>{Password}</Password>
                                                       <UserCode>{UserCode}</UserCode>
                                                       <AccountId>{AccountId}</AccountId>
                                                       <Originator>{Originator}</Originator>
                                                       <ValidityPeriod>3360</ValidityPeriod>
                                                       <SendDate/>
                                                       <MessageText>{TemplateText}</MessageText>                                                
                                                        {gsmListStr} 
                                                    </SendSms>]]>");



            soapRequestStr.Append($@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns=""https://webservice.asistiletisim.com.tr/SmsProxy"">
                                           <soapenv:Header/>
                                           <soapenv:Body>
                                              <sendSms>
                                                 <requestXml>
						                            {smsRequestStr}
						                        </requestXml>
                                              </sendSms>
                                           </soapenv:Body>
                                        </soapenv:Envelope>");

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(soapRequestStr.ToString());




            var webRequest = (HttpWebRequest)WebRequest.Create("https://webservice.asistiletisim.com.tr/SmsProxy.asmx");
            webRequest.Headers.Add("SOAPAction", "https://webservice.asistiletisim.com.tr/SmsProxy/sendSms");
            webRequest.ContentType = "text/xml; charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";


            using (var stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }



            var asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            var soapResult = string.Empty;


            using (var webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (var rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                var responseXml = new XmlDocument();
                responseXml.LoadXml(soapResult);

                var errorCode = responseXml.GetElementsByTagName("ErrorCode");
                var Originator1 = responseXml.GetElementsByTagName("Originator");

            }


            SendSmsResult response = new SendSmsResult();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(soapResult);
                var code = xmlDoc.GetElementsByTagName("ErrorCode").Item(0)?.InnerText;
                var Originator1 = xmlDoc.GetElementsByTagName("Originator").Item(0)?.InnerText;
                response.Originator = Originator;
                response.ResultCode = (code.ToString() == "0") ? 1 : 0;
            }
            catch (Exception ex)
            {
                response.ResultCode = 0;
                response.ResultDescription = "Exception : " + ex.Message + Environment.NewLine + "Response : " + soapResult;
            }
            return response;
        }


    }
}
