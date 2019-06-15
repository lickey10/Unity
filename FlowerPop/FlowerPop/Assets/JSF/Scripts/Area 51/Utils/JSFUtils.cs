
/// <summary>
/// JMF utils. use as a helper class for various static function calls
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class JSFUtils {

	public const string gmPanelName = "GameManagerPanel";
	public const string panelPoolName = "Panels";
	public const string piecePoolName = "Pieces";
	public const string particlePoolName = "Particles";
	public const string miscPoolName = "Misc";
	public static JSFGameManager gm; // updated by JSFGameManager -> Awake()
	public static JSFVisualManager vm; // updated by JSFGameManager -> Awake()
	public static JSFWinningConditions wc;  // updated by JSFGameManager -> Awake()
	public static bool isPooling {get{return gm.usingPoolManager;}}

	// -----------------------------------------------------------------------------------------
	
	/// <summary>
	/// 
	/// the codes below are mine ... 
	/// COPYRIGHT kurayami88
	/// 
	/// </summary>


	// look for an object bounds
	public static Bounds findObjectBounds(GameObject obj){
        
		// includes all mesh types (filter; renderer; skinnedRenderer)
		Renderer ren = obj.GetComponent<Renderer>();
		if(ren == null){
            //ren = obj.GetComponentInChildren<Renderer>();
            Bounds bounds = GetChildRendererBounds(obj);

            if (bounds != null)
                return bounds;
		}
		if(ren != null){
			return ren.bounds;
		}
		Debug.LogError("Your prefab" + obj.ToString() + "needs a mesh to scale!!!");
		return new Bounds(Vector3.zero,Vector3.zero); // fail safe
	}

    public static Bounds GetChildRendererBounds(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;
            for (int i = 1, ni = renderers.Length; i < ni; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
            return bounds;
        }
        else
        {
            return new Bounds();
        }
    }

    // auto scale objects to fit into a board box size
    public static void autoScale(GameObject obj){
		autoScaleRatio(obj,1f); // default ratio of 1
	}
	
	// auto scale objects to fit into a board box size - with padding!
	public static void autoScalePadded(GameObject obj){
		autoScaleRatio(obj,gm.boxPadding); // default ratio of 1
	}

	public static void autoScaleHexagon(GameObject obj){
		autoScaleRatio(obj,1.156f); // 1.156f is the hexagon's scale
	}

	// auto scale objects to fit into a board box size with ratio
	public static void autoScaleRatio(GameObject obj, float ratio){
		obj.transform.localScale = Vector3.one; // resets the scale first

		// auto scaling feature
		Bounds bounds = findObjectBounds(obj);
		float val = (gm.size* (1-(gm.spacingPercentage/100.0f)) * ratio) / // get the bigger size to keep ratio
			Mathf.Clamp( Mathf.Max(bounds.size.x,bounds.size.y),0.0000001f,float.MaxValue);
		obj.transform.localScale = new Vector3 (val, val, val ); // the final scale value
		
		// adjust the box collider if present...
		BoxCollider bc = obj.GetComponent<BoxCollider>();
		if ( bc != null){
			float maxSize = Mathf.Max( new float[] {bounds.size.x,bounds.size.y,bounds.size.z} );
			bc.size = new Vector3(maxSize, maxSize, bounds.size.z + 0.01f);
			bc.center = Vector3.zero;
		}
	}

	public static void creatSwipeLine(GameObject obj, Vector3 a, Vector3 b, float zValue){
		obj.transform.localScale = new Vector3 (obj.transform.localScale.x, 1, 1 ); // resets the scale first

		// auto scaling feature
		Bounds bounds = findObjectBounds(obj);
		float val = (Vector3.Distance(a,b)) / // get the bigger size to keep ratio
			Mathf.Clamp( Mathf.Max(bounds.size.x,bounds.size.y),0.0000001f,float.MaxValue);
		obj.transform.localScale = new Vector3 (obj.transform.localScale.x, val, 1 ); // the final scale value
		obj.transform.position = Vector3.Lerp(a,b,0.5f) + new Vector3(0,0,zValue); // reposition to the middle
		obj.transform.LookAt(b,Vector3.forward); // auto adjust the rotation to look at the target
		
		// adjust the box collider if present...
		BoxCollider bc = obj.GetComponent<BoxCollider>();
		if ( bc != null){
			float maxSize = Mathf.Max( new float[] {bounds.size.x,bounds.size.y,bounds.size.z} );
			bc.size = new Vector3(maxSize, maxSize, bounds.size.z + 0.01f);
			bc.center = Vector3.zero;
		}
	}
}
