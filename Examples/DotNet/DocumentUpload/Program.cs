/*
 * A C# Example on how to upload a supporting document using the fidano/payroc/pulsepoint api
 * Using this source code you accept there are no guarantees
 * jason.jakob@payroc.com
 */
using Payroc.MSPWareApi.Examples.DotNet.DocumentUpload;
using System.Net.Http.Headers;

/* dev testing */
string apiurl = "http://localapi.mspware.com:3000";
string appid = "jjakob";
string appkey = "849E46F2BE600A62082D5261A7E3044663A446E2284568A645FA4777FC7FDED6";
string merchantApplicationNo = "95649";

/* production testing
string apiurl = "http://api.mspware.com";
string appid = "fidano";
string appkey = "804ABC928C232B7D0D556A750E4308DA31148691F476116C26823110560B5BC4";
string merchantApplicationNo = "680";
 */

//load the file and stuff it into multipart form data
string filePath = Environment.CurrentDirectory + @"\\dummy.pdf";
using var form = new MultipartFormDataContent();
using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
form.Add(fileContent, "file", Path.GetFileName(filePath)); //this important

//create client and add auth headers
var httpClient = new HttpClient(new HttpLoggingHandler(new HttpClientHandler()))
{
    BaseAddress = new Uri($"{apiurl}/{appid}/v2/")
};
httpClient.DefaultRequestHeaders.Add("x-api-key", appkey);
httpClient.DefaultRequestHeaders.Add("x-app-id", appid);

//send and get response
var response = await httpClient.PostAsync($"applications/{merchantApplicationNo}/documents", form);
var responseContent = await response.Content.ReadAsStringAsync();
Console.WriteLine("response :" + responseContent);
