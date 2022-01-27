using Model;
using System.Threading.Tasks;

namespace ProdMan_WASM.Service
{
    public interface IAuthService
    {

        public Task<AuthenticateResponseDTO> Login(AuthenticationRequestDTO req);
        public Task<UserRegistrationResponseDTO> RegisterUser(UserRequestDTO req);

        public Task Logout();
    }
}
