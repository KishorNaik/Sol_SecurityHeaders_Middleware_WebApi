﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
            // TODO Change the value depending of your needs
            httpContext.Response.Headers.Add("referrer-policy", new StringValues("strict-origin-when-cross-origin"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
            httpContext.Response.Headers.Add("x-content-type-options", new StringValues("nosniff"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            httpContext.Response.Headers.Add("x-frame-options", new StringValues("DENY"));

            // https://security.stackexchange.com/questions/166024/does-the-x-permitted-cross-domain-policies-header-have-any-benefit-for-my-websit
            httpContext.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", new StringValues("none"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
            httpContext.Response.Headers.Add("x-xss-protection", new StringValues("1; mode=block"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Expect-CT
            // You can use https://report-uri.com/ to get notified when a misissued certificate is detected
            httpContext.Response.Headers.Add("Expect-CT", new StringValues("max-age=0, enforce, report-uri=\"https://example.report-uri.com/r/d/ct/enforce\""));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Feature-Policy
            // https://github.com/w3c/webappsec-feature-policy/blob/master/features.md
            // https://developers.google.com/web/updates/2018/06/feature-policy
            // TODO change the value of each rule and check the documentation to see if new features are available
            httpContext.Response.Headers.Add("Feature-Policy", new StringValues(
                "accelerometer 'none';" +
                "ambient-light-sensor 'none';" +
                "autoplay 'none';" +
                "battery 'none';" +
                "camera 'none';" +
                "display-capture 'none';" +
                "document-domain 'none';" +
                "encrypted-media 'none';" +
                "execution-while-not-rendered 'none';" +
                "execution-while-out-of-viewport 'none';" +
                "gyroscope 'none';" +
                "magnetometer 'none';" +
                "microphone 'none';" +
                "midi 'none';" +
                "navigation-override 'none';" +
                "payment 'none';" +
                "picture-in-picture 'none';" +
                "publickey-credentials-get 'none';" +
                "sync-xhr 'none';" +
                "usb 'none';" +
                "wake-lock 'none';" +
                "xr-spatial-tracking 'none';"
                ));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
            // TODO change the value of each rule and check the documentation to see if new rules are available
            httpContext.Response.Headers.Add("Content-Security-Policy", new StringValues(
                "base-uri 'none';" +
                "block-all-mixed-content;" +
                "child-src 'none';" +
                "connect-src 'none';" +
                "default-src 'none';" +
                "font-src 'none';" +
                "form-action 'none';" +
                "frame-ancestors 'none';" +
                "frame-src 'none';" +
                "img-src 'none';" +
                "manifest-src 'none';" +
                "media-src 'none';" +
                "object-src 'none';" +
                "sandbox;" +
                "script-src 'none';" +
                "script-src-attr 'none';" +
                "script-src-elem 'none';" +
                "style-src 'none';" +
                "style-src-attr 'none';" +
                "style-src-elem 'none';" +
                "upgrade-insecure-requests;" +
                "worker-src 'none';"
                ));

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecurityHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}