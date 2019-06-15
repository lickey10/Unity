using UnityEditor;
using UnityEngine;



/// <summary> ##################################
/// 
/// NOTICE :
/// This script is just an editor extension to call and open the board layout custom
/// inspector onto a gui window. This makes it easier to see the custom inspector as
/// it is naturally going to be quite big.
/// 
/// DO NOT TOUCH UNLESS REQUIRED
/// 
/// </summary> ##################################


public class JSFBoardLayoutWindow : EditorWindow
{

    [MenuItem ("Window/Swipe Framework/Board Layout")]
    static void Init () 
    {
        // Get existing open window or if none, make a new one:
		EditorWindow.GetWindow (typeof (JSFBoardLayoutWindow),false, "Board Setup");
    }

    void OnGUI () {

        GameObject sel = Selection.activeGameObject;
		
		if( sel != null){
			JSFBoardLayout layout = sel.GetComponent<JSFBoardLayout>();

	        if (layout != null)
	        {
	            Editor editor = Editor.CreateEditor(layout);
	            editor.OnInspectorGUI();
	        } else { 
				showErrorMsg(); // tells user to select the GameManger object
			}
		} else {
			showErrorMsg(); // tells user to select the GameManger object
		}
    }
	
	void showErrorMsg(){
		EditorGUILayout.LabelField("\n* Please select the object that contains the " +
					"\"GameManager\" script.\nThen check back here again.", GUILayout.Height(45));
	}
}