using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebRole1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public static Trie trie;
        public List<string> names= new List<string>(){"hello","hello world","hi","home","hope","happy!","happiness","hunger","hungry_every_day","hello_everyone","zoo","hello_to_me","hellish"};
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public void PopulateTrie()
        {
            trie = new Trie();
            Regex rgx = new Regex(@"[^a-zA-Z_]$");
                /*using (StreamReader sr = new StreamReader("sample.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine();*/
                        foreach(string line in names){
                            bool onlyEnglishChar = true;
                        
                            foreach (char c in line)
                            {
                                string s = c.ToString();
                                if (rgx.IsMatch(s))
                                {
                                    onlyEnglishChar = false;
                                }
                            }
                            if (onlyEnglishChar)
                            {
                                trie.InsertPhrase(line);
                            }
                    }
                }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> searchPrefixForSuggestions(string phrase)
        {
            PopulateTrie();     
            List<string> output = trie.SearchPrefix(phrase);
            return output;
        }
       
        [WebMethod]
        public string DownloadBlob()
        {
            //DownloadBlob
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("wikinamescontainer");

            // Retrieve reference to a blob named "wikinamesblob.txt".
            //CloudBlockBlob blockBlob = container.GetBlockBlobReference("wikinamesblob");


            if (container.Exists())
            {
                foreach (IListBlobItem item in container.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        // Save blob contents to a file.
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\alice\Downloads\myblob1"))
                        {
                            blob.DownloadToStream(fileStream);
                        }
                    }
                }
                return "downloaded";
            }
            return null;
        }

    }
}
