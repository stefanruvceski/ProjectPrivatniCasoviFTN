using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class BlobHelper
    {
        #region Fields
        CloudStorageAccount storageAccount;
        CloudBlobClient blobStorage;
        CloudBlobContainer container;
        CloudBlockBlob blob;
        #endregion

        #region Kreiranje kontejnera
        //Kreiranje kontejnera
        public BlobHelper(string containerName)
        {
            try
            {
                // read account configuration settings
                storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));

                // create blob container for images
                blobStorage = storageAccount.CreateCloudBlobClient();
                container = blobStorage.GetContainerReference(containerName);
                container.CreateIfNotExists();
                // configure container for public access
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
            catch (WebException)
            {
            }
        }
        #endregion

        #region Blob i string
        // Kad zelimo staviti string u blob
        public CloudBlockBlob UploadStringToBlob(String blobName, String content)
        {
            blob = null;

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(content)))
            {
                try
                {
                    blob = container.GetBlockBlobReference(blobName);
                    blob.UploadFromStream(stream);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return blob;
        }

        //Kad zelimo skinuti string iz bloba
        public String DownloadStringFromBlob(String blobName)
        {
            String text = "";

            using (var stream = new MemoryStream())
            {
                try
                {
                    blob = container.GetBlockBlobReference(blobName);
                    blob.DownloadToStream(stream);
                    text = Encoding.Default.GetString(stream.ToArray());
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return text;
        }
        #endregion

        #region Blob i Slika
        //Na workerRole kad zelimo skinuti sliku iz bloba
        public Image DownloadImage(String blobName)
        {
            blob = container.GetBlockBlobReference(blobName);
            using (MemoryStream ms = new MemoryStream())
            {
                blob.DownloadToStream(ms);
                return new Bitmap(ms);
            }
        }
        //Na WorkerRole kad zelimo staviti sliku u blob
        public string UploadImage(String blobName, Image image)
        {

            blob = container.GetBlockBlobReference(blobName);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Bmp);
                memoryStream.Position = 0;
                blob.Properties.ContentType = "image/bmp";
                blob.UploadFromStream(memoryStream);
                return blob.Uri.ToString();
            }
        }
        #endregion

        #region Blob i File iz web forme
        //Na WbRole kad zelimo staviti file iz forme u blob
        public void UploadFileToBlob(String blobName, HttpPostedFileBase file)
        {
            blob = null;

            try
            {
                blob = container.GetBlockBlobReference(blobName);
                blob.Properties.ContentType = file.ContentType;
                blob.UploadFromStream(file.InputStream);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string DownladPhotoURLFromBlob(string blobName)
        {
            CloudBlockBlob b = container.GetBlockBlobReference(blobName);

            return b.Uri.ToString();

        }
        #endregion
    }
}
