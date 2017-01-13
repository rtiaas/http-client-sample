# http-client-sample
RTIaaS HTTP Client Sample

An example client showing how to obtain a token from the RTIaaS authorization service and POST data to RTIaaS http endpoint.

    mkdir c:\rtiaas
    cd c:\rtiaas
    git clone https://github.com/rtiaas/http-client-sample

Edit the `src\appsettings.json` file and add the clientId and clientSecret values to the configuration. 

    cd src
    dotnet restore
    dotnet run

You will see the following output:

    Calling authorization service at https://dev-shell-rtiaas-01-wsvc-auth.azurewebsites.net/connect/token
    Authorization token received.  Token expires in 3600 seconds.  Sending test request
    Finished call to test service.  Response...

    This is the test service confirming receipt of 17 bytes in request at 2017-01-13T14:21:31
    
    Press enter to quit


