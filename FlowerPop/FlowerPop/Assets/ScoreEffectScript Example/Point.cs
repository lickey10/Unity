using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float Score;
private float GetHitEffect;
private float targY;
private Vector3 PointPosition;

    GUISkin PointSkin;
    GUISkin PointSkinShadow;

    // Start is called before the first frame update
    void Start()
    {
        Score = Mathf.Round(Random.Range(Score / 2, Score * 2));
        PointPosition = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        targY = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        targY -= Time.deltaTime * 200;
    }

    private void OnGUI()
    {
        Vector3 screenPos2 =  Camera.main.WorldToScreenPoint(PointPosition);
        GetHitEffect += Time.deltaTime * 30;
        GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - (GetHitEffect - 50) / 7);
        GUI.skin = PointSkinShadow;
        GUI.Label(new Rect(screenPos2.x + 8, targY - 2, 260, 250), "+" + Score.ToString());
        GUI.skin = PointSkin;
        GUI.Label(new Rect(screenPos2.x + 10, targY, 300, 300), "+" + Score.ToString());
    }
}
