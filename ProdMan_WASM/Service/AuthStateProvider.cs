using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ProdMan_WASM.Helper;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProdMan_WASM.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient client;
        private string tokenName = "jwttoken";
        public AuthStateProvider(ILocalStorageService localStorage,HttpClient client)
        {
            this.localStorage = localStorage;
            this.client = client;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>(tokenName);

            //return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name,"kalle@home.se")}, "jwtAuthType")));


            if (token == null)
            {
                return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType")));

            return authState;
            
        }
    }
}
