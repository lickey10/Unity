using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

public static class DungeonHandler
{
    //private static string dungeonsFilePath = Application.persistentDataPath + "/Dungeons.xml";
    //private static string dungeonsFilePath = Path.Combine(Application.streamingAssetsPath, "Dungeons.xml");
    private static string dungeonsFilePath = "Dungeons.xml";

    public static XmlDocument CreateDoc()
    {
        //test values
        string Name = "Gryphon Games";
        bool Favorite = true;
        string Address = "";
        string Phone = "";
        string City = "Fort Collins";
        string State = "Colorado";
        string Zip = "";
        float Lat = 40.551668f;
        float Lon = -105.097807f;
        int Rating = 5;
        //test values


        XmlDocument xDocDungeons = new XmlDocument();

        //(1) the xml declaration is recommended, but not mandatory
        //XmlDeclaration xmlDeclaration = xDocDungeons.CreateXmlDeclaration("1.0", "UTF-8", null);
        //XmlElement root = xDocDungeons.DocumentElement;
        //xDocDungeons.InsertBefore(xmlDeclaration, root);

        XmlElement elementMain = xDocDungeons.CreateElement(string.Empty, "Dungeons", string.Empty);
        xDocDungeons.AppendChild(elementMain);

        //(2) string.Empty makes cleaner code
        XmlElement element1 = xDocDungeons.CreateElement(string.Empty, "Dungeon", string.Empty);
        element1.SetAttribute("id", "-1");
        elementMain.AppendChild(element1);

        XmlElement element2 = xDocDungeons.CreateElement(string.Empty, "Name", string.Empty);
        XmlText text1 = xDocDungeons.CreateTextNode(Name);
        element2.AppendChild(text1);
        element1.AppendChild(element2);

        XmlElement element3 = xDocDungeons.CreateElement(string.Empty, "Favorite", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Favorite.ToString());
        element3.AppendChild(text1);
        element1.AppendChild(element3);

        XmlElement element12 = xDocDungeons.CreateElement(string.Empty, "Phone", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Phone);
        element12.AppendChild(text1);
        element1.AppendChild(element12);

        XmlElement element10 = xDocDungeons.CreateElement(string.Empty, "Address", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Address);
        element10.AppendChild(text1);
        element1.AppendChild(element10);

        XmlElement element4 = xDocDungeons.CreateElement(string.Empty, "City", string.Empty);
        text1 = xDocDungeons.CreateTextNode(City);
        element4.AppendChild(text1);
        element1.AppendChild(element4);

        XmlElement element5 = xDocDungeons.CreateElement(string.Empty, "State", string.Empty);
        text1 = xDocDungeons.CreateTextNode(State);
        element5.AppendChild(text1);
        element1.AppendChild(element5);

        XmlElement element11 = xDocDungeons.CreateElement(string.Empty, "Zip", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Zip);
        element11.AppendChild(text1);
        element1.AppendChild(element11);

        XmlElement element6 = xDocDungeons.CreateElement(string.Empty, "Rating", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Rating.ToString());
        element6.AppendChild(text1);
        element1.AppendChild(element6);

        XmlElement element8 = xDocDungeons.CreateElement(string.Empty, "Lat", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Lat.ToString());
        element8.AppendChild(text1);
        element1.AppendChild(element8);

        XmlElement element9 = xDocDungeons.CreateElement(string.Empty, "Lon", string.Empty);
        text1 = xDocDungeons.CreateTextNode(Lon.ToString());
        element9.AppendChild(text1);
        element1.AppendChild(element9);

        //Email.Send(xDocDungeons.OuterXml);

        //byte[] bytes = Encoding.Default.GetBytes(xDocDungeons.OuterXml);

        //// Uploading a file.
        //var file = new UnityGoogleDrive.Data.File { Name = "QuestLog.txt", Content = bytes };
        //GoogleDriveFiles.Create(file).Send();

        //updateFile(xDocDungeons.OuterXml);


        return xDocDungeons;
    }

    public static XmlNode CreateNode(Dungeon dungeon, XmlDocument mainDoc)
    {
        ////test values
        //Name = "Gryphon Games";
        //Favorite = true;
        //City = "Fort Collins";
        //State = "Colorado";
        //Lat = 40.551668f;
        //Lon = -105.097807f;
        //Rating = 5;
        ////test values


        //xDocDungeons = new XmlDocument();

        //(1) the xml declaration is recommended, but not mandatory
        //XmlDeclaration xmlDeclaration = xDocDungeons.CreateXmlDeclaration("1.0", "UTF-8", null);
        //XmlElement root = xDocDungeons.DocumentElement;
        //xDocDungeons.InsertBefore(xmlDeclaration, root);

        //XmlElement elementMain = mainDoc.CreateElement(string.Empty, "Dungeons", string.Empty);
        //mainDoc.AppendChild(elementMain);

        //(2) string.Empty makes cleaner code
        XmlElement element1 = mainDoc.CreateElement(string.Empty, "Dungeon", string.Empty);
        element1.SetAttribute("id", dungeon.ID.ToString());
        mainDoc.DocumentElement.AppendChild(element1);

        XmlElement element2 = mainDoc.CreateElement(string.Empty, "Name", string.Empty);
        XmlText text1 = mainDoc.CreateTextNode(dungeon.Name);
        element2.AppendChild(text1);
        element1.AppendChild(element2);

        XmlElement element3 = mainDoc.CreateElement(string.Empty, "Favorite", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.Favorite.ToString());
        element3.AppendChild(text1);
        element1.AppendChild(element3);

        XmlElement element12 = mainDoc.CreateElement(string.Empty, "Phone", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.Phone);
        element12.AppendChild(text1);
        element1.AppendChild(element12);

        XmlElement element10 = mainDoc.CreateElement(string.Empty, "Address", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.Address);
        element10.AppendChild(text1);
        element1.AppendChild(element10);

        XmlElement element4 = mainDoc.CreateElement(string.Empty, "City", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.City);
        element4.AppendChild(text1);
        element1.AppendChild(element4);

        XmlElement element5 = mainDoc.CreateElement(string.Empty, "State", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.State);
        element5.AppendChild(text1);
        element1.AppendChild(element5);

        XmlElement element11 = mainDoc.CreateElement(string.Empty, "Zip", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.zip);
        element11.AppendChild(text1);
        element1.AppendChild(element11);

        XmlElement element6 = mainDoc.CreateElement(string.Empty, "Rating", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.Rating.ToString());
        element6.AppendChild(text1);
        element1.AppendChild(element6);

        XmlElement element8 = mainDoc.CreateElement(string.Empty, "Lat", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.Lat.ToString());
        element8.AppendChild(text1);
        element1.AppendChild(element8);

        XmlElement element9 = mainDoc.CreateElement(string.Empty, "Lon", string.Empty);
        text1 = mainDoc.CreateTextNode(dungeon.Lon.ToString());
        element9.AppendChild(text1);
        element1.AppendChild(element9);

        //Email.Send(xDocDungeons.OuterXml);

        //byte[] bytes = Encoding.Default.GetBytes(xDocDungeons.OuterXml);

        //// Uploading a file.
        //var file = new UnityGoogleDrive.Data.File { Name = "QuestLog.txt", Content = bytes };
        //GoogleDriveFiles.Create(file).Send();

        //updateFile(xDocDungeons.OuterXml);



        return mainDoc.DocumentElement.FirstChild;
    }

    private static XmlDocument GetDungeons()
    {
        XmlDocument xDungeons = new XmlDocument();

        //xDungeons.LoadXml(DungeonsList.DungeonsXML);
        //PlayerPrefs.SetString("Dungeons", xDungeons.OuterXml);

        string tempDungeons = PlayerPrefs.GetString("Dungeons");

        if (tempDungeons.Trim().Length > 0)
            xDungeons.LoadXml(tempDungeons);

        if (xDungeons.DocumentElement == null)
            xDungeons.LoadXml(DungeonsList.DungeonsXML);



        //if (System.IO.File.Exists(dungeonsFilePath))
        //    xDungeons.Load(dungeonsFilePath);
        //else
        //    xDungeons.LoadXml(DungeonHandler.CreateDoc().OuterXml);

        //-----------create text string of dungeons.xml content------------

        //if(DungeonsList.DungeonsXML.Length == 0)
        //{ 
        //string[] lines = File.ReadAllLines(dungeonsFilePath);

        //foreach(string line in lines)
        //{
        //    DungeonsList.DungeonsXML += line.Trim();
        //}

        //File.WriteAllText("theDungeonList.txt", DungeonsList.DungeonsXML);
        //}
        //-----------create text string of dungeons.xml content------------

        return xDungeons;
    }

    public static List<Dungeon> GetDungeonsList()
    {
        List<Dungeon> dungeons = new List<Dungeon>();
        XmlDocument xDungeons = GetDungeons();

        foreach(XmlElement dungeon in xDungeons.DocumentElement)
        {
            Dungeon newDungeon = new Dungeon();
            newDungeon.Name = dungeon.SelectSingleNode("Name").InnerText;
            newDungeon.Lat = double.Parse(dungeon.SelectSingleNode("Lat").InnerText);
            newDungeon.Lon = double.Parse(dungeon.SelectSingleNode("Lon").InnerText);
            newDungeon.Rating = int.Parse(dungeon.SelectSingleNode("Rating").InnerText);
            newDungeon.Favorite = bool.Parse(dungeon.SelectSingleNode("Favorite").InnerText);
            newDungeon.Address = dungeon.SelectSingleNode("Address").InnerText;
            newDungeon.City = dungeon.SelectSingleNode("City").InnerText;
            newDungeon.State = dungeon.SelectSingleNode("State").InnerText;
            newDungeon.zip = dungeon.SelectSingleNode("Zip").InnerText;
            newDungeon.Phone = dungeon.SelectSingleNode("Phone").InnerText;

            dungeons.Add(newDungeon);
        }

        SaveDungeons(dungeons);

        return dungeons;
    }

    public static Dungeon GetDungeon(double Lat, double Lon)
    {
        Dungeon newDungeon = GetDungeonsList().Where(x => x.Lat.ToString() == Lat.ToString()).Where(x => x.Lon.ToString() == Lon.ToString()).FirstOrDefault();
        
        return newDungeon;
    }

    public static XmlDocument SaveDungeons(List<Dungeon> dungeons)
    {
        XmlDocument xdocNewDungeons = new XmlDocument();

        XmlElement elementMain = xdocNewDungeons.CreateElement(string.Empty, "Dungeons", string.Empty);
        xdocNewDungeons.AppendChild(elementMain);

        int dCounter = 0;

        Dungeon[] jsonArray = new Dungeon[dungeons.Count];

        foreach (Dungeon dungeon in dungeons)
        {
            jsonArray[dCounter] = dungeon;

            dCounter++;

            dungeon.ID = dCounter;

            elementMain.AppendChild(CreateNode(dungeon, xdocNewDungeons));
        }

        PlayerPrefs.SetString("Dungeons", xdocNewDungeons.OuterXml);
        
        //cant get this to work
        string json = JsonUtility.ToJson(jsonArray);

        //xdocNewDungeons.Save(dungeonsFilePath.ToLower().Replace(".xml","_updated.xml"));

        return xdocNewDungeons;
    }

    public static void AddDungeon(Dungeon newDungeon)
    {
        XmlDocument xDungeons = GetDungeons();
        XmlNode elementToReplace = null;

        foreach (XmlElement dungeon in xDungeons.DocumentElement)
        {
            if (dungeon.SelectSingleNode("Lat").InnerText == newDungeon.Lat.ToString() && dungeon.SelectSingleNode("Lon").InnerText == newDungeon.Lon.ToString())
            {
                elementToReplace = dungeon;

                break;
            }
        }

        if (elementToReplace != null)
            xDungeons.DocumentElement.RemoveChild(elementToReplace);

        xDungeons.DocumentElement.AppendChild(DungeonHandler.CreateNode(newDungeon, xDungeons));

        PlayerPrefs.SetString("Dungeons", xDungeons.OuterXml);
        //xDungeons.Save(dungeonsFilePath);
    }

    public static void DeleteDungeon(string Lat, string Lon)
    {
        XmlDocument xDungeons = GetDungeons();
        XmlNode elementToDelete = null;
        
        foreach (XmlElement dungeon in xDungeons.DocumentElement)
        {
            if(dungeon.SelectSingleNode("Lat").InnerText == Lat && dungeon.SelectSingleNode("Lon").InnerText == Lon)
            {
                elementToDelete = dungeon;

                break;
            }
        }

        if (elementToDelete != null)
        {
            xDungeons.RemoveChild(elementToDelete);

            PlayerPrefs.SetString("Dungeons", xDungeons.OuterXml);
            //xDungeons.Save(dungeonsFilePath);
        }
    }

    /// <summary>
    /// Remove local list and refresh list from server
    /// </summary>
    public static XmlDocument ResetList()
    {
        PlayerPrefs.DeleteKey("Dungeons");

        if (System.IO.File.Exists(dungeonsFilePath))
            File.Delete(dungeonsFilePath);

        return GetDungeons();
    }
}
