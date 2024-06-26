
# Collector Identity Provider (IdP)

 1. [Overview](#overview)
 1. [Prerequisites](#prerequisites)
    1. [Authentication methods](#authentication-methods)
    1. [Redirect URIs](#Redirect-URIs)
 1. [Customization](#customization)
    1. [OAuth OpenID Connect code flow custom parameter values](#oauth-openid-connect-code-flow-custom-parameter-values)
    1. [Authentication method details](#authentication-method-details)
    1. [UI locales and authentication methods](#ui-locales-and-authentication-methods)
 1. [Try It Out](#try-it-out)
    1. [To test OAuth OpenID Connect code flow](#to-test-oauth-openid-connect-code-flow)
    1. [Test User](#test-user)
 1. [Samples](#samples)
        
---

## Overview
The Collector IdP supports OAuth OpenID Connect code flow.
Details can be found at [connect2id](https://connect2id.com/learn/openid-connect).


There are two environments

 * UAT **https://idp-uat.collectorbank.se**
 * PROD **https://idp.collectorbank.se**

The metadata address for each environments are located at

 * OAuth OpenID Connect Discovery (**/.well-known/openid-configuration**)
   * UAT: [https://idp-uat.collectorbank.se/.well-known/openid-configuration](https://idp-uat.collectorbank.se/.well-known/openid-configuration)
   * PROD: [https://idp.collectorbank.se/.well-known/openid-configuration](https://idp.collectorbank.se/.well-known/openid-configuration)

Authentication methods supported:

 * Swedish BankID
 * Swedish Mobile BankID
 * Swedish Mobile BankID QR
 * Norwegian BankID
 * Norwegian Mobile BankID
 * Norwegian BankID HIGH
 * Norwegian BankID Biometric
 * Finnish Trust Network
 * Danish MitID

---

## Prerequisites
In order to use Collector IdP you need to preregister by contacting [Collector](mailto:merchant@collectorbank.se).
Provide information regarding
* Authentication methods
* Redirect URIs

### Authentication methods
Specify which authentication methods you want to use, supported methods are listed above.


### Redirect URIs
For OAuth OpenID Connect code flow you need to specify redirect URIs that should be available to use in an authentication request.

The redirect URIs must have https schema. When you are registered you will get a Client Id.


When the client is setup, you are able to call the authorize endpoint using OAuth OpenID Connect code flow (i.e. response\_type=code and scope=openid)

---

## Customization
Collector IdP specific parameter values that can be set when making authentication request.

### OAuth OpenID Connect code flow custom parameter values
OAuth Parameter | Values | Description |
--------------- | ------ | -----------|
ui\_locales | sv, nb, fi, en | One or more ui locales separated by space. See [Authentication method details](#authentication-method-details) for more info.

For other parameters see the specification:

 * [OpenID Connect](https://openid.net/connect/)
 * [OpenID Connect Core specification](http://openid.net/specs/openid-connect-core-1_0.html)

### Authentication method details

Authentication method | Code | SSN  |  UI locale |  Default UI locale |
--------------------- | -----|------|------------------|---------|
Swedish BankID & Swedish BankID Mobile | urn:collectorbank:ac:method:sbid<br />urn:collectorbank:ac:method:sbid-v4 | yyyyMMddNNNC | sv, en | sv |
Swedish BankID Mobile | urn:collectorbank:ac:method:sbid-mobil<br />urn:collectorbank:ac:method:sbid-mobil-v4 | yyyyMMddNNNC | sv, en | sv |
Swedish BankID Mobile QR | urn:collectorbank:ac:method:sbid-qr<br />urn:collectorbank:ac:method:sbid-qr-v4 | yyyyMMddNNNC | sv, en | sv |
Norwegian BankID | urn:collectorbank:ac:method:nbid | ddMMyyZZZQQ | nb, en | nb |
Norwegian Mobile BankID | urn:collectorbank:ac:method:nbid-mobil | ddMMyyZZZQQ | nb, en | nb |
Norwegian BankID HIGH | urn:collectorbank:ac:method:nbid-high | ddMMyyZZZQQ | nb, en | nb |
Norwegian BankID Biometric | urn:collectorbank:ac:method:nbid-biometric | ddMMyyZZZQQ | nb, en | nb |
Finnish Trust Network | urn:collectorbank:ac:method:ftn | ddMMyyCzzzQ | fi, sv, en | fi |
Danish MidID | urn:collectorbank:ac:method:mitid-cpr | ddMMyy-ssss | da, en | da |

 where d = day, M = month, y = year, C = century sign (+ or - or A), z = serial number, Q = control digit, s = sequence number

Authentication method to use for authentication is selected by parameter **acr_values**.

**acr_values** is a space-separated string that specifies the acr values that the Authorization Server is being requested to use for processing the Authentication Requested. The format is  urn:collectorbank:ac:method:method:<name-of-method>. 

Multiple Authentication methods (for the same country) is allowed.

The *-v4 methods for Swedish BankID are methods that has the secure start behaviour, before it is enforced by the Swedish BankID backend service on 1 May 2024. For more info on this, see [Secure start will be mandatory from 1 May 2024](https://www.bankid.com/en/tekniska-uppdateringar/secure-start-will-be-mandatory-from-1-may-2024).

### UI locales and authentication methods
The UI locale used is based on the UI locales specified in authentication request and the UI locales that the authentication method supports.

If UI locales is specified in the authentication request then the first locale in the list that are supported by the authentication method will be used.

If no UI locales is supported, the end user will see a screen where they can choose the locale they want to use.

If UI locales is not specified in the authentication, the default UI locale for the authentication request will be used.

Authentication method (acr value) | Supporeted UI locales | Default UI locales

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

You can also specify ui_locales, [Authentication method details](#authentication-method-details)

### :warning: **IMPORTANT**
Please note that you MUST validate that the id token contains the subject's national_id. It is not enough to assume that the id token is issued for the subject noted in the login_hint of the request.

### Test User

For PROD you will need to have real account when authenticating.

For Test and UAT you need to have test user when authenticating.  

 * Swedish BankID follow the instructions at [https://demo.bankid.com/](https://demo.bankid.com/)
 * Norwegian BankID you can use national identifier: **21048349827** and for OTP (One Time Password) type **otp** and the password for the user is **qwer1234**
 * Finnish Trust Network you only need to select Nordea then click continue on each step (the forms should be auto filled).
 * Danish NemID follow the instructions at [https://www.nets.eu/dk-da/l%C3%B8sninger/nemid/nemid-tjenesteudbyder/Pages/bestil.aspx](https://www.nets.eu/dk-da/l%C3%B8sninger/nemid/nemid-tjenesteudbyder/Pages/bestil.aspx)

---

## Samples

All samples are written in C# using ASP.NET.

Sample | Protocol | Description
------ | -------  | -----------
------ | -------  | -----------
OpenIDConnectWebClientCore | OpenID Connect code flow | ASP.NET Core using .NETCore 2.2 framework
