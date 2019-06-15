using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (Input.GetMouseButtonDown(0) && touch.position.x < Screen.width / 2)
            {
                //slide menu out

            }
        }
        
    }
}
