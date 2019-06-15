using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Linq;

[InitializeOnLoad]
public class DependencyChecker : EditorWindow
{
	public const bool dotweenRequired = true;
	public static bool isDoTweenSetupDone = false;
	public static bool isDoTweenInstalled = false;

	public static string HBDOTween = "HBDOTween";

	static DependencyChecker()
	{
		EditorApplication.update += RunOnce;
	}

	static void RunOnce() 
	{
		EditorApplication.update -= RunOnce;
		RunDependencyCheckOnLaunch();
	}

	public static void RunDependencyCheckOnLaunch()
	{
		if(dotweenRequired)
		{
			EditorApplication.update += CheckItNow;
		}
	}

	public static void CheckItNow()
	{
		if (Directory.Exists ("Assets/Demigiant"))
		{
			isDoTweenInstalled = true;
		}
		else
		{
			RemoveSymbolForAllPlatform(HBDOTween);
			//OpenWelcomeWindow();
			return;
		}

		if(File.Exists("Assets/Resources/DOTweenSettings.asset"))
		{
			isDoTweenSetupDone = true;
		}
		else
		{
			RemoveSymbolForAllPlatform(HBDOTween);
			//OpenWelcomeWindow();
			return;
		}

		if(isDoTweenInstalled && isDoTweenSetupDone)
		{
			AddSymbolForAllPlatform(HBDOTween);
		}
		else
		{
			OpenWelcomeWindow();
		}
		EditorApplication.update -= CheckItNow;
	}

	public static void OpenWelcomeWindow()
	{
		GetWindow<DependencyChecker>(true);
	}
	
	public static void AddSymbolForAllPlatform(string symbol){
		AddSymbolForPlatform (BuildTargetGroup.Standalone,symbol);
		AddSymbolForPlatform (BuildTargetGroup.Android, symbol);
		AddSymbolForPlatform (BuildTargetGroup.iOS, symbol); 
		AddSymbolForPlatform (BuildTargetGroup.WSA, symbol);		
		AddSymbolForPlatform (BuildTargetGroup.WebGL, symbol);
	}

	public static void RemoveSymbolForAllPlatform(string symbol)
	{
		RemoveSymbolForPlatform (BuildTargetGroup.Standalone,symbol);
		RemoveSymbolForPlatform (BuildTargetGroup.Android, symbol);
		RemoveSymbolForPlatform (BuildTargetGroup.iOS, symbol); 
		RemoveSymbolForPlatform (BuildTargetGroup.WSA, symbol);		
		RemoveSymbolForPlatform (BuildTargetGroup.WebGL, symbol);
	}

	static void AddSymbolForPlatform(BuildTargetGroup platform, string newSymbol)
	{
		string existingSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(platform);

		if(!existingSymbols.Contains(newSymbol))
		{
			existingSymbols = existingSymbols.Replace(newSymbol + ";","");
			existingSymbols = existingSymbols.Replace(newSymbol,"");  
			existingSymbols = newSymbol + ";" + existingSymbols;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(platform,existingSymbols);
		}
	}

	static void RemoveSymbolForPlatform(BuildTargetGroup platform, string removingSymbol)
	{
		string existingSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(platform);
		
		List<string> allSymbols = existingSymbols.Split(';').ToList();

		if(allSymbols.Contains(removingSymbol))
		{
			allSymbols.Remove(removingSymbol);
		}

		existingSymbols = "";
		foreach(string symbol in allSymbols) {
			existingSymbols = symbol +";";
		}
		PlayerSettings.SetScriptingDefineSymbolsForGroup(platform,existingSymbols);
	}

	void OnEnable()
	{
		titleContent = new GUIContent("Please Fix Plugin Dependencies."); 
	}	

	public void OnGUI()
	{
		GUILayoutUtility.GetRect(300, 10);
		GUILayout.Space(1);
		GUILayout.BeginVertical();	
		GUIStyle myStyle = new GUIStyle();
  	  	myStyle.fontStyle = FontStyle.Bold;
	
		if(!isDoTweenInstalled)
		{
			GUILayout.Label(" DOTWEEN REQUIRED.",myStyle);
			GUILayout.Label("It's completely FREE and available on Unity assetstore.");
			if(GUILayout.Button("Get DOTWEEN(HOTween v2) It's Free!",GUILayout.Height(50)))
			{
				Application.OpenURL("http://u3d.as/aZ1");
			}
		}

		if(!isDoTweenSetupDone)
		{
			GUILayout.Label("Seeme like DoTween is installed but setup is remaining.");
			GUILayout.Label("Please go to Tools -> Demigiant -> DOTween Utility Panel and Press Setup DOTween...");
		}

		GUILayout.Space(20);

		if(isDoTweenInstalled && isDoTweenSetupDone)
		{
			GUILayout.Label(" CONGRATULATIONS.",myStyle);
			GUILayout.Label("Your plugin dependencies seems correct.");
		}
	}
}
         