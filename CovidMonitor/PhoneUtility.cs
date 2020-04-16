using System;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace CovidMonitor
{
    class PhoneUtility
    {		
		public string countOfToday { get; set; }		
		const string accountSid = "AXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX8";
		const string authToken = "3XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXb";
		public void SendMessage()
		{
			try
			{
				string messageBody = "The count of covid-19 Confirmed Case in NC:"+countOfToday;
				TwilioClient.Init(accountSid, authToken);
				var to = new PhoneNumber("+1832xxx1782");
				var from = new PhoneNumber("+17344283320");
				var message = MessageResource.Create(to, from: from, body: messageBody);
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Exception in message sending - {ex.Message}");
			}
		}
	}
}
