using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Payroc.MSPWareApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        string apiurl = "http://localapi.mspware.com:3000";
        string appid = "yourAppIdHere";
        string appkey = "yourAppKeyHere";
        string merchantApplicationNo = "95649";

        HttpClient httpClient;

        public UnitTest1()
        {
            httpClient = new HttpClient(new HttpLoggingHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri($"{apiurl}/{appid}/v2/")
            };

            //add auth headers
            httpClient.DefaultRequestHeaders.Add("x-api-key", appkey);
            httpClient.DefaultRequestHeaders.Add("x-app-id", appid);
        }

        [TestMethod]
        public async Task GetApplicationTypes()
        {
            //send and get response
            var response = await httpClient.GetAsync($"applications/types");
            var responseContent = await response.Content.ReadAsStringAsync();
            Trace.WriteLine("response :" + responseContent);
        }

        [TestMethod]
        public async Task PutSubmitApp()
        {
            //init content
            string json = "{\"documentlist\": \"180184, 180206\",\"mpadocument\":\"180293\",\"mpadocumentpage\":\"7\",\"override\":false}";
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            //PUT to submit
            var response = await httpClient.PutAsync($"applications/{merchantApplicationNo}/submit", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Trace.WriteLine("response :" + responseContent);
        }

        [TestMethod]
        public async Task PostUploadDocument()
        {
            //load the file and stuff it into multipart form data
            string filePath = Environment.CurrentDirectory + @"\\dummy.pdf";
            using var form = new MultipartFormDataContent();
            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(fileContent, "file", Path.GetFileName(filePath)); //this important

            //send and get response
            var response = await httpClient.PostAsync($"applications/{merchantApplicationNo}/documents", form);
            var responseContent = await response.Content.ReadAsStringAsync();
            Trace.WriteLine("response :" + responseContent);
        }
    }
}