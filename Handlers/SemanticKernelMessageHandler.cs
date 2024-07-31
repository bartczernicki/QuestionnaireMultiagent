using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireMultiagent.Handlers
{
    // https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/http-message-handlers  

    public class SemanticKernelMessageHandler : DelegatingHandler
    {
        private readonly ILogger logger;

        public SemanticKernelMessageHandler(ILogger logger) : base(new HttpClientHandler())
        {
            this.logger = logger;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Start Request At: {Now}", DateTime.Now);


            // Show request body
            if (request.Content != null)
            {
                var requestContent = await request.Content.ReadAsStringAsync();
                this.logger.LogInformation("BODY: {requestContent}", requestContent);
            }

            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);
            this.logger.LogInformation("End Request at: {Now}", DateTime.Now);

            return response;
        }
    }
}
