using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCamera : MonoBehaviour
{
    public Transform Hero;
    public Texture2D bshLogo;
    public float m_speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //if (Hero)
        //    transform.position = new Vector3(Hero.transform.position.x, Hero.transform.position.y + 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Hero)
        {
            //transform.position = Vector3.Lerp(transform.position, Hero.position, m_speed);// + new Vector3(0, 0, 20);

            transform.position = new Vector3(Mathf.Lerp(Hero.transform.localPosition.x, transform.localPosition.x, Time.deltaTime * 1), Mathf.Lerp(Hero.transform.localPosition.y, transform.localPosition.y, Time.deltaTime * 1), 30);

            //transform.position = new Vector3(Hero.transform.position.x, Hero.transform.position.y + 2);

        }
    }

    void OnGUI()
    {
        //GUI.DrawTexture(new Rect(10, 10, 80, 90), bshLogo, ScaleMode.StretchToFill, true);
    }
}
