using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BloodConnector.Shared.Constants;
using BloodConnector.WebAPI.Utilities;

namespace BloodConnector.WebAPI.Helper
{
    public class UploadConfig
    {
        public HttpPostedFile FileBase { get; set; }

        public string FileNameWithPath { get; set; }
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileguId { get; set; }
    }

    public class FileHelper
    {
        public static UploadConfig Upload(HttpPostedFile fileBase, Enums.FileType fileType)
        {
            Guid fileguId = Guid.NewGuid();
            var fileName = String.Format("{0}{1}", fileguId, Path.GetExtension(fileBase.FileName));
            
            var filePath = string.Empty;

            switch (fileType)
            {
                case Enums.FileType.Avatar:
                    filePath = Path.Combine(Const.UPLOAD,Const.AVATER);
                    break;
                case Enums.FileType.Document:
                    filePath = Path.Combine(Const.UPLOAD, Const.DOCUMENT);
                    break;
            }

            var uploadConfig = new UploadConfig
            {
                FileBase = fileBase,
                FileName = fileName,
                FileNameWithPath = Path.Combine(filePath,fileName),
                FilePath = filePath,
                FileguId = fileguId.ToString()
            };

            try
            {
                SaveAs(uploadConfig);
            }
            catch (Exception)
            {
                return new UploadConfig();
            }

            return uploadConfig;
        }

        public static void Delete(string relativePath)
        {
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(relativePath)))
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(relativePath));
            }
        }

        private static void SaveAs(UploadConfig config)
        {
            if (config.FileBase == null || config.FileBase.ContentLength <= 0)
            {
                throw new FileNotFoundException("File not found.");
            }

            if (string.IsNullOrEmpty(config.FileName))
            {
                config.FileName = config.FileBase.FileName;
            }

            if (string.IsNullOrEmpty(config.FilePath))
            {
                config.FilePath = Const.UPLOAD;
            }

            var fullPath = HttpContext.Current.Server.MapPath(config.FileNameWithPath);

            var dir = Path.GetDirectoryName(fullPath) ?? string.Empty;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            config.FileBase.SaveAs(fullPath);
        }
    }
}