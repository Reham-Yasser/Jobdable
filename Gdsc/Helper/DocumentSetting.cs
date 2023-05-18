using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Gdsc.Helper
{
    public class DocumentSetting
    {
        public static string UploadeFile(IFormFile file,string FolderName)
        {

            

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files",FolderName);
            var FileName = $"{Guid.NewGuid()}{file.FileName}";

            var filePath = Path.Combine(FolderPath,FileName);
            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return $"https://localhost:7086/files/{FolderName}{FileName}";
   
        }





        public static void DeleteFile(string FolderName, string FileName)
        {
            string[] words = FileName.Split($"https://localhost:7086/files/{FolderName}/");
            FileName = words[1];

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName, FileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
