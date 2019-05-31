using UnityEngine;
using System.Collections;

public class LightHandler : MonoBehaviour {
    GameObject[] Lights;

	// Use this for initialization
	void Start () {
        Lights = GameObject.FindGameObjectsWithTag("Light");
        StartCoroutine(lightsDemo(500));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void turnLightOff(GameObject light)
    {
        light.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void turnLightOn(GameObject light)
    {
        light.GetComponent<SpriteRenderer>().enabled = true;
    }

    private IEnumerator lightsDemo(int flashCount)
    {
        //for (int x = 0; x < flashCount; x++)
        while(0 != 1)
        {
            StartCoroutine(FlashAllLights(5));

            yield return new WaitForSeconds(2f);

            StartCoroutine(FlashInSequence(5));

            yield return new WaitForSeconds(2f);

            StartCoroutine(FlashRedLights(3));

            yield return new WaitForSeconds(2f);

            StartCoroutine(FlashBlueLights(3));
        }
    }

    public IEnumerator FlashAllLights(int flashCount)
    {
        //how to call this function
        //StartCoroutine(FlashAllLights(3));

        for (int x = 0; x < flashCount; x++)
        {
            foreach (GameObject light in Lights)
            {
                turnLightOn(light);
            }

            yield return new WaitForSeconds(.2f);

            foreach (GameObject light in Lights)
            {
                turnLightOff(light);
            }
        }
    }

    public IEnumerator FlashInSequence(int flashCount)
    {
        //how to call this function
        //StartCoroutine(FlashInSequence(3));

        for (int x = 0; x < flashCount; x++)
        {
            foreach (GameObject light in Lights)
            {
                turnLightOn(light);

                yield return new WaitForSeconds(.2f);

                turnLightOff(light);
            }
        }
    }

    public IEnumerator FlashRedLights(int flashCount)
    {
        //how to call this function
        //StartCoroutine(FlashRedLights(3));

        for (int x = 0; x < flashCount; x++)
        {
            foreach (GameObject light in Lights)
            {
                if(light.name.Contains("3") || light.name.Contains("5")  || light.name.Contains("6"))
                    turnLightOn(light);
            }

            yield return new WaitForSeconds(.2f);

            foreach (GameObject light in Lights)
            {
                if (light.name.Contains("3") || light.name.Contains("5") || light.name.Contains("6"))
                    turnLightOff(light);
            }
        }
    }

    public IEnumerator FlashBlueLights(int flashCount)
    {
        //how to call this function
        //StartCoroutine(FlashBlueLights(3));

        for (int x = 0; x < flashCount; x++)
        {
            foreach (GameObject light in Lights)
            {
                if (light.name.Contains("1") || light.name.Contains("2") || light.name.Contains("4") || light.name.Contains("7"))
                    turnLightOn(light);
            }

            yield return new WaitForSeconds(.2f);

            foreach (GameObject light in Lights)
            {
                if (light.name.Contains("1") || light.name.Contains("2") || light.name.Contains("4") || light.name.Contains("7"))
                    turnLightOff(light);
            }
        }
    }
}
