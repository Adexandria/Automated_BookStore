using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Authentication.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace Authentication.Infrastructure.Service
{
    public class EmailService
    {
		public async Task<bool> SendSimpleMessage(Mail newMail)
		{
            try
            {
                RestClient client = new RestClient("https://api.mailgun.net/v3")
                {
                    Authenticator = new HttpBasicAuthenticator("api", "f2bb647f002947dfe20bae6e655a2f73-d2cc48bc-70d8f34a")
                };
                RestRequest request = new RestRequest();
				request.AddParameter("domain", "paprika.software", ParameterType.UrlSegment);
				request.Resource = "paprika.software/messages";
				request.AddParameter("from", "Automated Online BookStore System <mailgun@paprika.software>");
				request.AddParameter("to", newMail.To);
				request.AddParameter("subject", newMail.Subject);
				request.AddParameter("text", newMail.Text);
				request.Method = Method.Post;
				var response = await client.ExecuteAsync(request);
				if (response.IsSuccessful)
				{
					return true;
				}
				return false;
			}
            catch (Exception e)
            {

				throw e;
            }
			
		}
	}
}
