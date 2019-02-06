
# Collector Identity Provider (IdP)

 1. [Overview](#overview)
 1. [Prerequisites](#prerequisites)
    1. [Authentication methods](#authentication-methods)
    1. [OAuth OpenID Connect code flow](#oauth-openid-connect-code-flow)
 1. [Customization](#customization)
    1. [OAuth OpenID Connect code flow custom parameter values](#oauth-openid-connect-code-flow-custom-parameter-values)
    1. [Authentication methods](#authentication-methods)
    1. [UI Locales and authentication methods](#ui-locales-and-authentication-methods)
 1. [Try It Out](#try-it-out)
    1. [To test OAuth OpenID Connect code flow](#to-test-oauth-openid-connect-code-flow)
    1. [Test User](#test-user)
 1. [Samples](#samples)
    
    
---
## Overview
The Collector IdP supports OAuth OpenID Connect code flow and the WS-Federation protocol.

There are two environments

 * UAT **https://idp-uat.collectorbank.se**
 * PROD **https://idp.collectorbank.se**

The metadata address for each environments are located at

 * OAuth OpenID Connect Discovery (**/.well-known/openid-configuration**)
   * UAT: [https://idp-uat.collectorbank.se/.well-known/openid-configuration](https://idp-uat.collectorbank.se/.well-known/openid-configuration)
   * PROD: [https://idp.collectorbank.se/.well-known/openid-configuration](https://idp.collectorbank.se/.well-known/openid-configuration)

Authentication methods supported:

 * Swedish BankID
 * Norwegain BankID
 * Norwegain Mobile BankID
 * Finnsh Trust Network
 * Danish NemID
---
## Prerequisites
In order to use Collector IdP you need to preregister by contacting [Collector](mailto:merchant@collectorbank.se).

### Authentication methods
Specify which authentication methods you want to use Swedish BankID, Norwegain BankID or Finnsh Trust Network.

If you specify more than one specify which one you want as the default, i.e. it will be used as standard if you won't specify a specific one when making authentication request.

### OAuth OpenID Connect code flow
For OAuth OpenID Connect code flow you need to specify the redirect URIs that you want to use.

The redirect URIs must have https schema. When you are registered you will get a Client Id.

After that you can call the authorize endpoint using OAuth OpenID Connect code flow (i.e. response\_type=code and scope=openid)

---
## Customization
Collector IdP specific parameter values that can be set when making authentication request.

### OAuth OpenID Connect code flow custom parameter values
OAuth Parameter | Values | Desription |
--------------- | ------ | -----------|
ui\_locales | sv, nb, fi, en | One or more ui locales seperated by space. See [UI Locales and authentication](#ui-locales-and-authentication-methods) methods for more info.

For other parameters see the specification:

 * [OpenID Connect](https://openid.net/connect/)
 * [OpenID Connect Core specification](http://openid.net/specs/openid-connect-core-1_0.html)

### Authentication methods 

Authentication method | Code | SSN | SSN Description |
--------------------- | -----|------|------------------|
Swedish BankID | urn:collectorbank:ac:method:sbid | yyyyMMddNNNC | where dd = day, MM = month, yyyy = year, NNN = serial number, C = control digits |
Norwegain BankID | urn:collectorbank:ac:method:nbid | ddMMyyZZZQQ | where dd = day, MM = month, yy = year, ZZZ = serial number, QQ = control digits |
Norwegain Mobile BankID | urn:collectorbank:ac:method:nbid-mobile | ddMMyyZZZQQ | where dd = day, MM = month, yy = year, ZZZ = serial number, QQ = control digits |
Finnish Trust Network | urn:collectorbank:ac:method:ftn | ddMMyyCzzzQ | where dd = day, MM = month, yy = year, C = Century sign can have value +, - or A, zzz = serial number, Q = control digit |
Danish NemID | urn:collectorbank:ac:method:nemid | ddMMyy-ssss | where dd = day, MM = month, yy = year, ssss = Sequence number |

### UI Locales and authentication methods
The UI local that will be used will be based on the UI Locales specified in authentication request and the UI Locales that the authentication method to use supports.

If UI Locales is specified in the authentication request then the first local in the list that are supported by the authentication method will be used.

If no UI Locales is supported then the end user will see a screen where they can choose the local they want to use.

If UI Locales is not specified in the authentication then the default UI locales for the authentication request will be used.

Authentication method (acr value) | Supporeted UI Locales | Default UI Locales
--------------------- | --------------------- | ------------------
Swedish BankID (urn:collectorbank:ac:method:sbid) | sv, en | sv
Norwegain BankID (urn:collectorbank:ac:method:nbid) | nb, en | nb
Norwegain Mobile BankID (urn:collectorbank:ac:method:nbid-mobil) | nb, en | nb
Finnish Trust Network (urn:collectorbank:ac:method:ftn) | fi, sv, en | fi
Danish NemID (urn:collectorbank:ac:method:nemid) | da, en | da

---
## Try It Out

### To test OAuth OpenID Connect code flow
You can use the following settings to try out OAuth OpenID Connect code flow in the test environment.  
Server **https://idp-uat.collectorbank.se/**  
client\_id: **MZxDS_9hY64cva_-V9eV**  
response\_type: **code**  
Redirect Uris that you can use are:  
 **https://localhost:45000/signin-oidc**  
 **https://localhost:45000/signin**  
 **https://localhost:45100/signin-oidc**  
 **https://localhost:45100/signin**  
 **https://localhost:44300/signin-oidc**

You can also specify ui_locales see for more information [UI Locales and authentication methods](#ui-locales-and-authentication-methods)

### Test User
For PROD you will need to have real account when authenticating.

For Test and UAT you need to have test user when authenticating.  

 * Swedish BankID follow the instructions at [https://demo.bankid.com/](https://demo.bankid.com/)
 * Norwegain BankID you can use national identifier: **21048349827** and for OTP (One Time Password) type **otp** and the password for the user is **qwer1234**
 * Finnish Trust Network you only need to select Nordea then click continue on each step (the forms should be auto filled).
 * Danish NemID follow the instructions at [https://www.nets.eu/dk-da/l%C3%B8sninger/nemid/nemid-tjenesteudbyder/Pages/bestil.aspx](https://www.nets.eu/dk-da/l%C3%B8sninger/nemid/nemid-tjenesteudbyder/Pages/bestil.aspx)
---
## Samples
All samples are written in C# using ASP.NET.

Sample | Protocol | Description
------ | -------  | -----------
------ | -------  | -----------
OpenIDConnectWebClientCore | OpenID Connect code flow | ASP.NET Core using .NETCore 2.2 framework
