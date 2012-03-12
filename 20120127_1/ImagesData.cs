using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace _20120127_1
{
    sealed public class ImagesData : DatasData
    {
        private static int ColumnNumber_ImageID = 0;
        private static int ColumnNumber_ImageName = 1;
        private static int ColumnNumber_ImageType = 2;
        private static int ColumnNumber_UserID = 3;

        public ImagesData(string currentPath)
            : base(currentPath)
        {
            DatasFileName = "images.csv";
        }

        public string ImagesDir
        {
            get
            {
                return CurrentPath + "\\images\\";
            }
        }

        protected override Dictionary<string, string> GenerateDictionary(string[] data)
        {
            return new Dictionary<string, string>
            {
                {"imageID", data[ColumnNumber_ImageID]},
                {"imageName", data[ColumnNumber_ImageName]},
                {"imageType", data[ColumnNumber_ImageType]},
                {"userID", data[ColumnNumber_UserID]}
            };
        }

        public Dictionary<string, string> FindImageId(string imageID)
        {
            return FindRecord(imageID, ColumnNumber_ImageID);
        }

        public string AddImage(string imageName, string userID, System.Web.HttpPostedFile image)
        {
            string imageID = System.Guid.NewGuid().ToString();
            string imageType;
            switch (image.ContentType)
            {
                case "image/png":
                    imageType = "png";
                    break;
                case "image/jpeg":
                case "image/pjpeg":
                    imageType = "jpg";
                    break;
                case "image/webp":
                    imageType = "webp";
                    break;
                default:
                    throw new InvalidOperationException("Unknown image content type: " + image.ContentType);
            }
            image.SaveAs(ImagesDir + imageID + "." + imageType);
            AddRecord(new string[]
            {
                imageID,
                EscapeForCsv(imageName),
                imageType,
                userID
            });
            return imageID;
        }

        public void RemoveImage(string imageID)
        {
            var image = FindImageId(imageID);
            System.IO.File.Delete(ImagesDir + image["imageID"] + "." + image["imageType"]);
            RemoveRecord(imageID, ColumnNumber_ImageID);
        }

        public void RenameImage(string imageID, string imageName)
        {
            var image = FindImageId(imageID);
            RemoveRecord(imageID, ColumnNumber_ImageID);
            AddRecord(new string[]
            {
                imageID,
                EscapeForCsv(imageName),
                image["imageType"],
                image["userID"]
            });
        }
    }
}
