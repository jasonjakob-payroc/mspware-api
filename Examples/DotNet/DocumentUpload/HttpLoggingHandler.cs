/*
 * A C# Example on how to upload a supporting document using the fidano/payroc/pulsepoint api
 * Using this source code you accept there are no guarantees
 * jason.jakob@payroc.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroc.MSPWareApi.Examples.DotNet.DocumentUpload
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        public HttpLoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        /// <summary>
        /// Provides a mechanism to log the raw HTTP request send and response
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            //if (request.Content != null)
            //{
            //    Console.WriteLine(await request.Content.ReadAsStringAsync());
            //}
            //Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            return response;
        }
    }
}
