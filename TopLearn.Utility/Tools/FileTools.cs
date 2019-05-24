using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Utility.Tools
{
    class FileTools
    {
    }

    public class ImageUploader
    {
        public static async Task<string> ImageUpload(string projectPath, string filePath, string oldFileName, string fileNewName, IFormFile file)
        {
            string imagePath = "";
            if (oldFileName != "Defult.jpg")
            {
                imagePath = projectPath + filePath + oldFileName;
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            
            imagePath = projectPath + filePath + fileNewName;
            try
            {
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
