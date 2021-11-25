using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_server.Services
{
    public interface IFilemanager
    {

        public  Task<string> SaveFile(IBrowserFile file);

        public bool DeleteFile(string filename);
    }
}
