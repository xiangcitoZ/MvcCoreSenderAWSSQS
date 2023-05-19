using Amazon.SQS;
using Amazon.SQS.Model;
using MvcCoreSenderAWSSQS.Models;
using Newtonsoft.Json;
using System.Net;

namespace MvcCoreSenderAWSSQS.Services
{
    public class ServiceSQS
    {

        private IAmazonSQS clientSQS;
        private string UrlQueue;

        public ServiceSQS(IAmazonSQS amazonSQS , IConfiguration configuration)
        {
            this.clientSQS = amazonSQS;
            this.UrlQueue =
                configuration.GetValue<string>("AWS:SQS:UrlQueue");
        }

        public async Task SendMessageAsync(Mensaje mensaje)
        {

            string json = JsonConvert.SerializeObject( mensaje );
            SendMessageRequest request =
                new SendMessageRequest(this.UrlQueue, json);
            SendMessageResponse response = 
                await this.clientSQS.SendMessageAsync(request);
            HttpStatusCode statusCode = response.HttpStatusCode;

        }



    }
}
