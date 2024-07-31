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
    /*
    https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/http-message-handlers
    https://stackoverflow.com/questions/42130393/does-net-core-httpclient-have-the-concept-of-interceptors 
    */

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
            var startMessage = $"""

                --------------------------------
                Start Request At: {DateTime.Now}
                """;
            this.logger.LogInformation(startMessage);


            // Show request body
            if (request.Content != null)
            {
                var requestContent = await request.Content.ReadAsStringAsync();

                var bodyMessage = $"""
                    BODY:
                    {requestContent}
                    """;

                this.logger.LogInformation(bodyMessage);
            }

            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);

            var endMessage = $"""
                End Request At: {DateTime.Now}
                ------------------------------

                """;
            this.logger.LogInformation(endMessage);

            return response;
        }
    }
}
