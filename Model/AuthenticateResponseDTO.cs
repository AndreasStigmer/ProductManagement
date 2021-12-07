using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Reponseobjekt vid Authensieringsförsök. Token kommer vara en JWT Baerer Token.
    /// </summary>
    public class AuthenticateResponseDTO
    {

        public bool IsAuthenticationSuccess { get; set; }
        public string ErrorMesssage { get; set; }
        public string Token { get; set; }

        public UserDTO UserDTO { get; set; }
    }
}
