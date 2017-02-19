
# Collector Identity Provider (IdP)

## Table of Content
 1. [Overview](#markdown-heading-overview)  
 2. [Prerequisites](#markdown-heading-prerequisites)  
    2.1 [Authentication methods](#markdown-heading-authentication-methods)  
    2.2 [OAuth OpenID Connect implict flow](markdown-heading-oauth-openid-connect-implict-flow)  
    2.3 [WSFederation](markdown-heading-wsfederation)  
 3. [Customization](#markdown-heading-customization)  
    3.1 [OAuth OpenID Connect implict flow custom parameter values](#markdown-heading-oauth-openid-connect-implict-flow-custom-parameter-values)  
    3.2 [WSFederation custom parameters](#markdown-heading-wsfederation-custom-parameters)  
    3.3 [Authentication methods](#markdown-heading-authentication-methods)  
    3.4 [UI Locales and authentication methods](#markdown-heading-ui-locales-and-authentication-methods)  
 4. [Try It Out](#markdown-heading-try-it-out)  
    4.1 [To test OAuth OpenID Connect implict flow](#markdown-heading-to-test-oauth-openid-connect-implict-flow)  
    4.2 [To test WSFederation](#markdown-heading-to-test-wsfederation)  
    4.3 [Test User](#markdown-heading-test-user)  
 5. [Samples](#markdown-heading-samples)  

---
## Overview
The Collector IdP supports OAuth OpenID Connect implict flow and the WS-Federation protocol.

There are three environments

 * Test **https://web-idpserver-auth-test.azurewebsites.net**
 * UAT **https://web-idpserver-auth-uat.azurewebsites.net**
 * PROD **https://idp.collectorbank.se**

The metadata address for each environments are located at

 * OAuth OpenID Connect Discovery:  /.well-known/openid-configuration  
   [Test](https://web-idpserver-auth-test.azurewebsites.net/.well-known/openid-configuration)  
   [UAT](https://web-idpserver-auth-uat.azurewebsites.net/.well-known/openid-configuration)  
   [PROD](https://idp.collectorbank.se/.well-known/openid-configuration)  
 * WS-Federation Metadata: /2007-06/FederationMetadata.xml  
   [Test](https://web-idpserver-auth-test.azurewebsites.net/2007-06/FederationMetadata.xml)  
   [UAT](https://web-idpserver-auth-uat.azurewebsites.net/2007-06/FederationMetadata.xml)  
   [PROD](https://idp.collectorbank.se/2007-06/FederationMetadata.xml)  

Authentication methods supported:

 * Swedish BankID
 * Norwegain BankID
 * Finnish Tupas

---
## Prerequisites
In order to use Collector IdP you need to preregister by contacting Collector ?? [TODO INSERT CONTACT INFO].

### Authentication methods
Specify which authentication methods you want to use Swedish BankID, Norwegain BankID or Finnish Tupas.

If you specify more than one specify which one you want as the default, i.e. it will be used as standard if you won't specify a specific one when making authentication request.

### OAuth OpenID Connect implict flow
For OAuth OpenID Connect implict flow you need to specify the redirect URIs that you want to use.

The redirect URIs must have https schema. When you are registered you will get a Client Id.

After that you can call the authorize endpoint using OAuth OpenID Connect implict flow (i.e. response\_type=id\_token and scope=openid)

### WSFederation
For WSFederation you need to specify the Realm you want to use and optional any redirect URIs you want to use with the realm.  
If you won't specify any redirect URIs then the Realm will also function as redirect URI.

---
## Customization
Collector IdP specific parameter values that can be set when making authentication request.

### OAuth OpenID Connect implict flow custom parameter values
OAuth Parameter | Values | Desription
--------------- | ------ | ----------
login\_hint | sbid, nbid or tupas | Specifies the authentication method to use. Note that the Client must be allowed to use the authentication method specified.
ui\_locales | sv, nb, fi, en | One or more ui locales seperated by space. See [UI Locales and authentication](#UI Locales and authentication methods) methods for more info.

For other parameters see the specification:

 * [OpenID Connect](https://openid.net/connect/)
 * [OpenID Connect Core specification](http://openid.net/specs/openid-connect-core-1_0.html)

### WSFederation custom parameters
Custom WSFederation Pamater | Values | Desription
--------------------------- | ------ | ----------
coauth | sbid, nbid or tupas | Specifies the authentication method to use. Note that the Client must be allowed to use the authentication method specified.
colocales | sv, nb, fi, en | One or more ui locales seperated by space. See [UI Locales and authentication](#UI Locales and authentication methods) methods for more info.

For other parameters see the specification:

 * [WS-Federation Version 1.2](http://docs.oasis-open.org/wsfed/federation/v1.2/os/ws-federation-1.2-spec-os.html)

### Authentication methods
The Authentication methods supported

Authentication method | Value
--------------------- | -----
Swedish BankI | sbid
Norwegain BankID | nbid
Finnish Tupas | tupas

### UI Locales and authentication methods
The UI local that will be used will be based on the UI Locales specified in authentication request and the UI Locales that the authentication method to use supports.

If UI Locales is specified in the authentication request then the first local in the list that are supported by the authentication method will be used.

If no UI Locales is supported then the end user will see a screen where they can choose the local they want to use.

If UI Locales is not specified in the authentication then the default UI locales for the authentication request will be used.

Authentication method | Supporeted UI Locales | Default UI Locales
--------------------- | --------------------- | ------------------
Swedish BankID (sbid) | sv, en | sv
Norwegain BankID (nbid) | nb, en | nb
Finnish Tupas (tupas) | fi, sv, en | fi

---
## Try It Out

### To test OAuth OpenID Connect implict flow
You can use the following settings to try out OAuth OpenID Connect implict flow in the test environment.  
Server **https://web-idpserver-auth-test.azurewebsites.net/**  
client\_id: **btwzQWmTSKeKfubsTzdYvw**  
response\_type: **id\_token**  
Redirect Uris that you can use are:  
 **https://localhost:45000/signin-oidc**  
 **https://localhost:45000/signin**  
 **https://localhost:45100/signin-oidc**  
 **https://localhost:45100/signin**  

Additional you can specify sbid, nbid, or tupas as login\_hint.  
If you don't specify any loing\_hint the sbid (Swedish BankID) will be used by default.  
You can also specify ui_locales see for more information [UI Locales and authentication methods](#ui-locales-and-authentication-methods)

### To test WSFederation
Wtrealm: **https://localhost/wsfed**  
Wreply: **https://localhost:45200/signin-wsfed**  
MetadataAddress: **https://web-idpserver-auth-test.azurewebsites.net/2007-06/FederationMetadata.xml**

### Test User
For PROD you will need to have real account when authenticating.

For Test and UAT you need to have test user when authenticating.  

 * Swedish BankID follow the instructions at [https://demo.bankid.com/](https://demo.bankid.com/)
 * Norwegain BankID you can use national identifier: **21048349827** and for OTP (One Time Password) type **otp** and the password for the user is **qwer1234**
 * Finnish Tupas you only need to select Nordea then click continue on each step (the forms should be auto filled).

---
## Samples
All samples are written in C# using ASP.NET.

Sample | Protocol | Description
------ | -------  | -----------
OpenIDConnectWebClientCore | OpenID Connect implict flow | ASP.NET Core using .NETCore 1.1.0 framework
OpenIDConnectWebClientOwin | OpenID Connect implict flow | ASP.NET using .NET Framework with OWIN
WsFederationOwin | WSFederation | ASP.NET using .NET Framework with OWIN
