using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProdMan_server.Services
{
    public class Filemanager : IFilemanager
    {
        private readonly IWebHostEnvironment env;
        public string RootPhysicalPath { get; set; }
        public string RootRelativePath { get; set; } = "/ProdImages/";
        public Filemanager(IWebHostEnvironment env)
        {
            this.env = env;
            RootPhysicalPath = this.env.WebRootPath + "\\ProdImages\\";
            if (!Directory.Exists(RootPhysicalPath))
            {
                Directory.CreateDirectory(RootPhysicalPath);
            }
        }

        public bool DeleteFile(string filename)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(IBrowserFile file)
        {
            FileInfo newfileinfo = new FileInfo(file.Name);
            string newfilename = Guid.NewGuid().ToString() + newfileinfo.Extension;

            bool success = false;
            using (FileStream fs=new FileStream(RootPhysicalPath+newfilename,FileMode.Create))
            {
                try { 
                    await file.OpenReadStream().CopyToAsync(fs);
                    success = true;
                    
                }catch(Exception ex)
                {

                }
            }
            if(success)
            {
                return  RootRelativePath + newfilename;
            }else
            {
                return null;
            }


          
        }
    }
}
