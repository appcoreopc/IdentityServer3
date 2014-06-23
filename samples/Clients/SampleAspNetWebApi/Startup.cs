﻿using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using System.Linq;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityServer.v3.AccessTokenValidation;

[assembly: OwinStartup(typeof(SampleAspNetWebApi.Startup))]

namespace SampleAspNetWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = ClaimMappings.None;

            // for self contained tokens
            app.UseJsonWebToken(
                issuer:    "https://idsrv3.com",
                audience:  "https://idsrv3.com/resources",
                signingKey: X509.LocalMachine.TrustedPeople.SubjectDistinguishedName.Find("CN=idsrv3test", false).First());

            // for reference tokens
            app.UseIdentitiyServerReferenceTokens(new ReferenceTokenValidationOptions
                {
                    TokenValidationEndpoint = "http://localhost:3333/core/connect/accessTokenValidation"
                });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}