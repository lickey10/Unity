using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLightController : MonoBehaviour
{

    public Color OnColor = Color.cyan;
    public Color OffColor = Color.gray;
    public Color TextColor = Color.white;
    public int GroupOrder = 0;
    public bool On = false;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();

        if (On)
            TurnLightOn();
        else
            TurnLightOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnLightOn()
    {
        sprite.color = OnColor;

        On = true;
    }

    public void TurnLightOff()
    {
        sprite.color = OffColor;

        On = false;
    }

    public void Flash(int timesToFlash, float delay)
    {
        StartCoroutine(flashLight(timesToFlash,delay));
    }

    IEnumerator flashLight(int timesToFlash, float delay)
    {
        for (int x = 0; x < timesToFlash; x++)
        {
            TurnLightOn();

            yield return new WaitForSeconds(delay);

            TurnLightOff();

            yield return new WaitForSeconds(delay);
        }
    }
}
