using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Reponseobjekt som skickas efter lyckad eller misslyckad registreringsprocess
    /// </summary>
    public class UserRegistrationResponseDTO
    {
        public bool IsRegistrationSuccessful { get; set; }
        public  List<string> ErrorMessages { get; set; }
    }
}
