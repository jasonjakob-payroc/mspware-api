# mspware-api
See the C# test project for examples in making calls to payroc/fidano mspware api for merchant onboarding

# curl on windows fails because of a bug that doesn't add headers to the request. Sooooo this call will not work on windows instead use curl on linux or WLS2 linux distro on windows.
curl -H 'x-app-id: MyAppIdGoesHere' -H 'x-api-key: MyApiKeyGoesHere' -H 'Accept-Encoding: gzip, deflate, br' -F file=@/mnt/c/Users/jason.jakob/Documents/dummy.pdf -k -v http://localapi.mspware.com:3000/jjakob/v2/applications/95394/documents
