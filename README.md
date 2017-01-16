# Vue.js Demo

Vue.js template based on [JavaScriptServices](https://github.com/aspnet/JavaScriptServices)

## What is this?

`JavaScriptServices` is a set of technologies for ASP.NET Core developers. It provides infrastructure that you'll find useful if you use Angular 2 / React / Knockout / etc. on the client, or if you build your client-side resources using Webpack, or otherwise want to execute JavaScript on the server at runtime.

This project adds a [Vue.js](https://vuejs.org) template to the mix.

This repo contains:

 * Server:
   * ASP.NET Core 1.1.0 Mvc and Web Api
   * Entity Framework Core 1.0.1
   * Swashbuckle 6.0.* for Swagger API Documentation and UI
   * Webpack dev middleware ([docs](https://github.com/aspnet/JavaScriptServices/tree/dev/src/Microsoft.AspNetCore.SpaServices#webpack-dev-middleware))
   * Hot module replacement (HMR) ([docs](https://github.com/aspnet/JavaScriptServices/tree/dev/src/Microsoft.AspNetCore.SpaServices#webpack-hot-module-replacement))
   * Web Api Secured by Azure AD
 * Client:
   * vuejs 2.0
   * vue-router 2.1.*
   * adal.js for Azure AD authentication

Everything here is cross-platform, and works with .NET Core 1.0.1 or later on Windows, Linux, or OS X.
