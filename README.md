# mspware-api
Examples for using mspare-api from payroc/fidano

# curl on windows fails because of a bug that doesn't add headers to the request.
curl -X POST -H 'x-app-id: jjakob' -H 'x-api-key: 849E46F2BE600A62082D5261A7E3044663A446E2284568A645FA4777FC7FDED6' -H 'Accept-Encoding: gzip, deflate, br' -F file=@C:\Users\jason.jakob\Documents\dummy.pdf -k -v http://localapi.mspware.com:3000/jjakob/v2/applications/95394/documents

# curl on WLS2 in windows on linux distro works fine.  NOTE use the ip of WSL net interface instead of localhost. put an entry in /etc/hosts file to the WSL net interface
curl -X POST -H 'x-app-id: jjakob' -H 'x-api-key: 849E46F2BE600A62082D5261A7E3044663A446E2284568A645FA4777FC7FDED6' -H 'Accept-Encoding: gzip, deflate, br' -F file=@/mnt/c/Users/jason.jakob/Documents/dummy.pdf -k -v http://localapi.mspware.com:3000/jjakob/v2/applications/95394/documents
