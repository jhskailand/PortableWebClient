# PortableWebClient
WebClient replacement compatible with Portable Class Library (PCL) based .NET/Mono/Xamarin projects. Minimal dependencies.

Useful when accessing REST based services using C#.

# Example for HTTP GET
Here is an example of how to GET data from a server. Replace login and URL with your own details, and adjust Accept as necessary.
```
PortableWebClient webClient = new PortableWebClient();
webClient.Accept = "application/json";
webClient.Username = "<my login>";
webClient.Password = "<my password>";
string url = "<my http post address>";
string result = webClient.DownloadString(url);
if (webClient.StatusCode == System.Net.HttpStatusCode.OK)
{
  Console.WriteLine(result);
  return true;
}
else
{
  Console.WriteLine("Error: " + webClient.StatusDescription);
}
```

# Example for HTTP POST
Here is an example of how to POST data to a server resource that does not reply with any response. Replace login and URL with your own details, and adjust ContentType/Accept as necessary.
```
PortableWebClient webClient = new PortableWebClient();
webClient.ContentType = contentType;
webClient.Accept = "application/json";
webClient.Username = "<my login>";
webClient.Password = "<my password>";
string url = "<my http post address>";
webClient.UploadString(url, body);
if (webClient.StatusCode == System.Net.HttpStatusCode.OK)
{
  Console.WriteLine("Success");
  return true;
}
else
{
  Console.WriteLine("Error: " + webClient.StatusDescription);
  return false;
}
```

# Example for HTTP POST with response data
Here is an example of how to POST data to a server and retrieve result. Replace login and URL with your own details, and adjust ContentType/Accept as necessary.
```
PortableWebClient webClient = new PortableWebClient();
webClient.ContentType = "application/json";
webClient.Accept = "application/json";
webClient.Username = "<my login>";
webClient.Password = "<my password>";
string url = "<my http post address>";
string result = webClient.UploadString(url, body);
if (webClient.StatusCode == System.Net.HttpStatusCode.OK)
{
  Console.WriteLine("Result: " + result);
}
else
{
  Console.WriteLine("Error: " + webClient.StatusDescription);
}
```
