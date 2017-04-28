
# Collector Identity Provider (IdP)

 1. [Overview](#overview)
 1. [Prerequisites](#prerequisites)
    1. [Authentication methods](#authentication-methods)
    1. [OAuth OpenID Connect implict flow](#oauth-openid-connect-implict-flow)
    1. [WSFederation](#wsfederation)
 1. [Customization](#customization)
    1. [OAuth OpenID Connect implict flow custom parameter values](#oauth-openid-connect-implict-flow-custom-parameter-values)
    1. [WSFederation custom parameters](#wsfederation-custom-parameters)
    1. [Authentication methods](#authentication-methods)
    1. [UI Locales and authentication methods](#ui-locales-and-authentication-methods)
 1. [Try It Out](#try-it-out)
    1. [To test OAuth OpenID Connect implict flow](#to-test-oauth-openid-connect-implict-flow)
    1. [To test WSFederation](#to-test-wsfederation)
    1. [Test User](#test-user)
 1. [Samples](#samples)
    
    
---
## Overview
The Collector IdP supports OAuth OpenID Connect implict flow and the WS-Federation protocol.

There are two environments

 * UAT **https://idp-uat.collectorbank.se**
 * PROD **https://idp.collectorbank.se**

The metadata address for each environments are located at

 * OAuth OpenID Connect Discovery (**/.well-known/openid-configuration**)
   * UAT: [https://idp-uat.collectorbank.se/.well-known/openid-configuration](https://idp-uat.collectorbank.se/.well-known/openid-configuration)
   * PROD: [https://idp.collectorbank.se/.well-known/openid-configuration](https://idp.collectorbank.se/.well-known/openid-configuration)
 * WS-Federation Metadata (**/2007-06/FederationMetadata.xml**)
   * UAT: [https://idp-uat.collectorbank.se/2007-06/FederationMetadata.xml](https://idp-uat.collectorbank.se/2007-06/FederationMetadata.xml)
   * PROD: [https://idp.collectorbank.se/2007-06/FederationMetadata.xml](https://idp.collectorbank.se/2007-06/FederationMetadata.xml)  

Authentication methods supported:

 * Swedish BankID
 * Norwegain BankID
 * Finnish Tupas

---
## Prerequisites
In order to use Collector IdP you need to preregister by contacting [Collector](mailto:merchant@collectorbank.se).

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
OAuth Parameter | Values | Desription |
--------------- | ------ | -----------|
login\_hint | AuthCode or AuthCode_AuthHint | Specifies the code of the authentication method to use and optional information about the end user that is expected to be authenticated. Note that the Client must be allowed to use the authentication method specified.
ui\_locales | sv, nb, fi, en | One or more ui locales seperated by space. See [UI Locales and authentication](#ui-locales-and-authentication-methods) methods for more info.

Example login\_hint can be set to nbid_21048349827 then the first step in Norwegian BankID, where the end user enters the national identifier, will be skipped. If the value
is set to nbid then the end user must enter there national identifier themself.
**WARNING** Using AuthHint in login\_hint paramater will lead to an information disclosure since the national identifier will be sent in a front channel reveling information 
about the expected end user that the system expect to authenticate.


For other parameters see the specification:

 * [OpenID Connect](https://openid.net/connect/)
 * [OpenID Connect Core specification](http://openid.net/specs/openid-connect-core-1_0.html)

### WSFederation custom parameters
Custom WSFederation Pamater | Values | Desription
--------------------------- | ------ | ----------
coauth | sbid, nbid or tupas | Specifies the authentication method to use. Note that the Client must be allowed to use the authentication method specified.
cohint | Dependent on the coauth value | Specifies optional information about the end user that is expected to be authenticated.
colocales | sv, nb, fi, en | One or more ui locales seperated by space. See [UI Locales and authentication](#UI Locales and authentication methods) methods for more info.

**WARNING** Using cohint parameter will lead to an information disclosure since the national identifier will be sent in a front channel reveling information 
about the expected end user that the system expect to authenticate.

For other parameters see the specification:

 * [WS-Federation Version 1.2](http://docs.oasis-open.org/wsfed/federation/v1.2/os/ws-federation-1.2-spec-os.html)

### Authentication methods 

Authentication method | Code | Hint | Hint Description |
--------------------- | -----|------|------------------|
Swedish BankI | sbid | yyyyMMddNNNC | where dd = day, MM = month, yyyy = year, NNN = serial number, C = control digits |
Norwegain BankID | nbid | ddMMyyZZZQQ | where dd = day, MM = month, yy = year, ZZZ = serial number, QQ = control digits |
Finnish Tupas | tupas | ddMMyyCzzzQ | where dd = day, MM = month, yy = year, C = Century sign can have value +, - or A, zzz = serial number, Q = control digit |

**WARNING** Using Hint will lead to an information disclosure since the national identifier will be sent in a front channel reveling information 
about the expected end user that the system expect to authenticate.

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
Server **https://idp-uat.collectorbank.se/**  
client\_id: **MZxDS_9hY64cva_-V9eV**  
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
MetadataAddress: **https://idp-uat.collectorbank.se//2007-06/FederationMetadata.xml**

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
