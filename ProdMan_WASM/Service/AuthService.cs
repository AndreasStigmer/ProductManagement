using Blazored.LocalStorage;
using Model;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProdMan_WASM.Service
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService localStorage;
        private readonly HttpClient client;
        private string tokenName = "jwttoken";

        public AuthService(ILocalStorageService localStorage,HttpClient client)
        {
            this.localStorage = localStorage;
            this.client = client;
        }
        public async Task<AuthenticateResponseDTO> Login(AuthenticationRequestDTO req)
        {
            var stringcontent = new StringContent(JsonSerializer.Serialize(req), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/account/login", stringcontent);
            var stringdata =await  response.Content.ReadAsStringAsync();

            var AuthResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticateResponseDTO>(stringdata);
                        
            if(response.IsSuccessStatusCode)
            {
                await localStorage.SetItemAsync(tokenName, AuthResponse.Token);
                await localStorage.SetItemAsync("userdetails", AuthResponse.UserDTO);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", AuthResponse.Token);
                return new AuthenticateResponseDTO() { IsAuthenticationSuccess = true };

            }else {
                return new AuthenticateResponseDTO() { IsAuthenticationSuccess=false,ErrorMesssage=AuthResponse.ErrorMesssage };
            }
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync(tokenName);
            await localStorage.RemoveItemAsync("userdetails");
        }

        public Task<UserRegistrationResponseDTO> RegisterUser(UserRequestDTO req)
        {
            throw new System.NotImplementedException();
        }
    }
}
