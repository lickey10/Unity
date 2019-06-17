using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float PointCurrent;
    public bool RandomizePointValue = false;
    private float GetHitEffect;
    private float targY;
    private Vector3 PointPosition;

    public GUISkin PointSkin;
    public GUISkin PointSkinShadow;

    // Start is called before the first frame update
    void Start()
    {
        if (RandomizePointValue)
            PointCurrent = Mathf.Round(Random.Range(PointCurrent / 2, PointCurrent * 2));

        PointPosition = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        targY = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        targY -= Time.deltaTime * 200;
    }

    void OnGUI()
    {
        Vector3 screenPos2 = Camera.main.GetComponent<Camera>().WorldToScreenPoint(PointPosition);
        GetHitEffect += Time.deltaTime * 30;
        GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (GetHitEffect - 50) / 7);
        GUI.skin = PointSkinShadow;
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 50;
        myStyle.normal.textColor = Color.black;
        GUI.Label(new Rect(screenPos2.x + 8, targY - 2, 160, 140), "+" + PointCurrent.ToString(), myStyle);
        GUI.skin = PointSkin;
        myStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(screenPos2.x + 10, targY, 240, 240), "+" + PointCurrent.ToString(), myStyle);
    }
}
