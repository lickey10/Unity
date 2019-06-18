using UnityEngine;
using System.Collections;



public class DisplayPoints : MonoBehaviour {
    float Points = 0;
    float Point = 0;
    private float GetHitEffect = 0;
    private float targY = 0;
    private Vector3 PointPosition;
    private bool displayPoints = false;

    GUISkin PointSkin;
    GUISkin PointSkinShadow;

	// Use this for initialization
	void Start () {
        Point = Mathf.Round(Random.Range(Point / 2, Point * 2));
        PointPosition = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        targY = Screen.height / 2;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (displayPoints)
        {
            Vector3 screenPos2 = Camera.main.GetComponent<Camera>().WorldToScreenPoint(PointPosition);
            GetHitEffect += Time.deltaTime * 30;
            GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (GetHitEffect - 50) / 7);
            GUI.skin = PointSkinShadow;
            GUI.Label(new Rect(screenPos2.x + 8, targY - 2, 80, 70), "+" + Point.ToString());
            GUI.skin = PointSkin;
            GUI.Label(new Rect(screenPos2.x + 10, targY, 120, 120), "+" + Point.ToString());

            if (PointPosition.y <= Screen.height)
                displayPoints = false;
        }
    }

    void Die()
    {
        displayPoints = true;
    }
}
