using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTravelOverTime : MonoBehaviour
{
    Vector3 travelDirection;		// 0,0,1 = forward etc
    bool local	= false;
    float speed	= 1.0f;
    float delay	= 0.0f;
    float delayCount;

    // Start is called before the first frame update
    void Start()
    {
        delayCount = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayCount <= 0)
        {
            if (local)
                transform.localPosition += travelDirection * Time.deltaTime * speed;
            else if (!local)
                transform.position += travelDirection * Time.deltaTime * speed;
        }
        else
            delayCount -= Time.deltaTime;
    }
}
