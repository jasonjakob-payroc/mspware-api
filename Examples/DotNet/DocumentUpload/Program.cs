/*
 * A C# Example on how to upload a supporting document using the fidano/payroc/pulsepoint api
 * Using this source code you accept there are no guarantees
 * jason.jakob@payroc.com
 */
using Payroc.MSPWareApi.Examples.DotNet.DocumentUpload;
using System.Net.Http.Headers;

string apiurl = "http://localapi.mspware.com:3000";
string appid = "your app id";
string appkey = "your app key";
string merchantApplicationNo = "95649";

//load the file and stuff it into multipart form data
string filePath = Environment.CurrentDirectory + @"\\dummy.pdf";
using var form = new MultipartFormDataContent();
using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
form.Add(fileContent, "file", Path.GetFileName(filePath)); //this important

//send
var httpClient = new HttpClient(new HttpLoggingHandler(new HttpClientHandler()))
{
    BaseAddress = new Uri($"{apiurl}/{appid}/v2/")
};
httpClient.DefaultRequestHeaders.Add("X-Api-Key", appkey);
var response = await httpClient.PostAsync($"applications/{merchantApplicationNo}/documents", form);
var responseContent = await response.Content.ReadAsStringAsync();
Console.WriteLine("response :" + responseContent);
