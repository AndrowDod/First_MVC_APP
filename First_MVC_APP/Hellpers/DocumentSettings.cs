using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace First_MVC_APP.PL.Hellpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , String folderName)
        {
            // 1- Get folder name 
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //2- Get file Name 
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3- Get file path 
            String filePath = Path.Combine(folderPath, fileName);

            //4- save file as stream 
            var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;

        }

        public static void DeleteFile(String fileName , String folderName)
        {
            String filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
