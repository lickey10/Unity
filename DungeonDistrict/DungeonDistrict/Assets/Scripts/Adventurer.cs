//using Google.Apis.Drive.v3;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Drive.v3.Data;
//using Google.Apis.Services;
//using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    //google drive reference
    //https://github.com/Elringus/UnityGoogleDrive


    public string FirstName = "";
    public string LastName = "";
    public DMExperienceLevels DMExperience;
    public string FavoriteDungeon = "";//where they like to play
    public string Race = "";
    public string Class = "";
    public float lat = -1;
    public float lon = -1;
    public enum DMExperienceLevels
    {
        Beginner,
        Intermediate,
        Advanced,
        Expert
    }

    XmlDocument xDocAdventurer;

    // Start is called before the first frame update
    void Start()
    {
        //test values
        FirstName = "Chad";
        LastName = "Lickey";
        DMExperience = DMExperienceLevels.Intermediate;
        FavoriteDungeon = "Gryphon Games";
        Race = "Dwarf";
        Class = "Warrior";
        lat = 235432;
        lon = 98704;

        //CreateDoc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDoc()
    {
        xDocAdventurer = new XmlDocument();

        //(1) the xml declaration is recommended, but not mandatory
        //XmlDeclaration xmlDeclaration = xDocAdventurer.CreateXmlDeclaration("1.0", "UTF-8", null);
        //XmlElement root = xDocAdventurer.DocumentElement;
        //xDocAdventurer.InsertBefore(xmlDeclaration, root);

        //XmlElement elementMain = xDocAdventurer.CreateElement(string.Empty, "Adventurers", string.Empty);
        //xDocAdventurer.AppendChild(elementMain);

        //(2) string.Empty makes cleaner code
        XmlElement element1 = xDocAdventurer.CreateElement(string.Empty, "Adventurer", string.Empty);
        element1.SetAttribute("id", "-1");
        xDocAdventurer.AppendChild(element1);

        XmlElement element2 = xDocAdventurer.CreateElement(string.Empty, "FirstName", string.Empty);
        XmlText text1 = xDocAdventurer.CreateTextNode(FirstName);
        element2.AppendChild(text1);
        element1.AppendChild(element2);

        XmlElement element3 = xDocAdventurer.CreateElement(string.Empty, "LastName", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(LastName);
        element3.AppendChild(text1);
        element1.AppendChild(element3);

        XmlElement element4 = xDocAdventurer.CreateElement(string.Empty, "DMExperience", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(DMExperience.ToString());
        element4.AppendChild(text1);
        element1.AppendChild(element4);

        XmlElement element5 = xDocAdventurer.CreateElement(string.Empty, "FavoriteDungeon", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(FavoriteDungeon);
        element5.AppendChild(text1);
        element1.AppendChild(element5);

        XmlElement element6 = xDocAdventurer.CreateElement(string.Empty, "Race", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(Race);
        element6.AppendChild(text1);
        element1.AppendChild(element6);

        XmlElement element7 = xDocAdventurer.CreateElement(string.Empty, "Class", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(Class);
        element7.AppendChild(text1);
        element1.AppendChild(element7);

        XmlElement element8 = xDocAdventurer.CreateElement(string.Empty, "Lat", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(lat.ToString());
        element8.AppendChild(text1);
        element1.AppendChild(element8);

        XmlElement element9 = xDocAdventurer.CreateElement(string.Empty, "Lon", string.Empty);
        text1 = xDocAdventurer.CreateTextNode(lon.ToString());
        element9.AppendChild(text1);
        element1.AppendChild(element9);
        
        Email.Send(xDocAdventurer.OuterXml);

        //byte[] bytes = Encoding.Default.GetBytes(xDocAdventurer.OuterXml);

        //// Uploading a file.
        //var file = new UnityGoogleDrive.Data.File { Name = "QuestLog.txt", Content = bytes };
        //GoogleDriveFiles.Create(file).Send();

        //updateFile(xDocAdventurer.OuterXml);
    }

    //private Google.Apis.Drive.v3.Data.File updateFile(string fileContent)
    //{
    //    UserCredential credential;
    //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
    //              new ClientSecrets { ClientId = "88892991233-041jr0eoiqo81en4nfg0941vemjtfc2u.apps.googleusercontent.com", ClientSecret = "k4thzrn4oCPK7eetH08NHnzR" },
    //              new[] { DriveService.Scope.Drive,
    //        DriveService.Scope.DriveFile },
    //              "user",
    //              CancellationToken.None,
    //              new FileDataStore("Drive.Auth.Store")).Result;


    //    // Create Drive API service.
    //    var service = new DriveService(new BaseClientService.Initializer()
    //    {
    //        HttpClientInitializer = credential,
    //        ApplicationName = "QuestLog",
    //    });


    //    Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
    //    //body.Title = System.IO.Path.GetFileName(_uploadFile);
    //    body.Description = "File updated by Chad";
    //    body.MimeType = "text/xml";// GetMimeType(fileContent);
    //    //body.Parents = new List() { new ParentReference() { Id = _parent } };

    //    // File's content.
    //    byte[] byteArray = System.IO.File.ReadAllBytes(fileContent);
    //    System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
    //    try
    //    {
    //        FilesResource.UpdateMediaUpload request = service.Files.Update(body, "16__jYYkmq7SfDNzuHvaeY6hUDBg5Ys0b", stream, body.MimeType);
    //        request.Upload();
    //        return request.ResponseBody;
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine("An error occurred: " + e.Message);
    //        return null;
    //    }

    //    //service.Files.Update(body, "16__jYYkmq7SfDNzuHvaeY6hUDBg5Ys0b");
    //}

    // tries to figure out the mime type of the file.
    //private static string GetMimeType(string fileName)
    //{
    //    string mimeType = "application/unknown";
    //    string ext = System.IO.Path.GetExtension(fileName).ToLower();
    //    Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
    //    if (regKey != null && regKey.GetValue("Content Type") != null)
    //        mimeType = regKey.GetValue("Content Type").ToString();
    //    return mimeType;
    //}

    //public class FilesUpdateOptionalParms
    //{
    //    /// A comma-separated list of parent IDs to add.
    //    public string AddParents { get; set; }
    //    /// Whether to set the 'keepForever' field in the new head revision. This is only applicable to files with binary content in Drive.
    //    public bool KeepRevisionForever { get; set; }
    //    /// A language hint for OCR processing during image import (ISO 639-1 code).
    //    public string OcrLanguage { get; set; }
    //    /// A comma-separated list of parent IDs to remove.
    //    public string RemoveParents { get; set; }
    //    /// Whether to use the uploaded content as indexable text.
    //    public bool UseContentAsIndexableText { get; set; }

    //}

    /// <summary>
    /// Updates a file's metadata and/or content with patch semantics. 
    /// Documentation https://developers.google.com/drive/v3/reference/files/update
    /// Generation Note: This does not always build corectly.  Google needs to standardise things I need to figuer out which ones are wrong.
    /// </summary>
    /// <param name="service">Authenticated drive service.</param>  
    /// <param name="fileId">The ID of the file.</param>
    /// <param name="body">A valid drive v3 body.</param>
    /// <param name="optional">Optional paramaters.</param>        /// <returns>FileResponse</returns>
    //public static Google.Apis.Drive.v3.Data.File Update(DriveService service, string fileId, Google.Apis.Drive.v3.Data.File body, FilesUpdateOptionalParms optional = null)
    //{
    //    try
    //    {
    //        // Initial validation.
    //        if (service == null)
    //            throw new ArgumentNullException("service");
    //        if (body == null)
    //            throw new ArgumentNullException("body");
    //        if (fileId == null)
    //            throw new ArgumentNullException(fileId);

    //        // Building the initial request.
    //        var request = service.Files.Update(body, fileId);

    //        // Applying optional parameters to the request.                
    //        //request = (FilesResource.UpdateRequest)SampleHelpers.ApplyOptionalParms(request, optional);

    //        // Requesting data.
    //        return request.Execute();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("Request Files.Update failed.", ex);
    //    }
    //}

    private void saveAdventurerInfo()
    {
        SaveSystem.SetString("FirstName", FirstName);
        SaveSystem.SetString("LastName", LastName);
        SaveSystem.SetString("DMExperience", DMExperience.ToString());
        SaveSystem.SetString("FavoriteDungeon", FavoriteDungeon);
        SaveSystem.SetString("Race", Race);
        SaveSystem.SetString("Class", Class);
        SaveSystem.SetFloat("Lat", lat);
        SaveSystem.SetFloat("Lon", lon);
    }

    private void getAdventurerInfo()
    {
        FirstName = SaveSystem.GetString("FirstName");
        LastName = SaveSystem.GetString("LastName");
        SaveSystem.GetString("DMExperience", DMExperience.ToString());
        FavoriteDungeon = SaveSystem.GetString("FavoriteDungeon");
        Race = SaveSystem.GetString("Race");
        Class = SaveSystem.GetString("Class");
        lat = SaveSystem.GetFloat("Lat");
        lon = SaveSystem.GetFloat("Lon");
    }
}
