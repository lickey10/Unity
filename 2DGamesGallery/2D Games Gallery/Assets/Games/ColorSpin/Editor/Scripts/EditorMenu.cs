using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class EditorMenu_colorSpin : MonoBehaviour {

	[MenuItem("HyperByte/Plugin Setup/Check Setup",false,1)]
	private static void SetUp()
	{
		DependencyChecker.OpenWelcomeWindow();
	}

	[MenuItem("HyperByte/Clear PlayerPrefs")]
	private static void ClearPrefs()
	{
		PlayerPrefs.DeleteAll();
	}

	[MenuItem("HyperByte/Persistent Data Path/Remove All Files")]
	private static void DeleteAllFilesFromPersistentDataPath()
	{
		FileUtil.DeleteFileOrDirectory (Application.persistentDataPath);
	}

	[MenuItem("HyperByte/Capture Screenshot/1X")]
	private static void Capture1XScreenshot()
	{
		string imgName = "IMG-"+ DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") +".png";
		ScreenCapture.CaptureScreenshot ((Application.dataPath + "/" + imgName),1);
	}

	[MenuItem("HyperByte/Capture Screenshot/2X")]
	private static void Capture2XScreenshot()
	{
		string imgName = "IMG-"+ DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") +".png";
		ScreenCapture.CaptureScreenshot ((Application.dataPath + "/" + imgName),2);
	}
}
