using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// Används som request för authensiering med användarnamn och lösen
    /// </summary>
    public class AuthenticationRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
