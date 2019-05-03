using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public delegate void EventHandler(object sender, EventArgs args);
    public event EventHandler LightIsLit = delegate { };
    public Color OnColor = Color.cyan;
    public Color OffColor = Color.gray;
    public int GroupOrder = 0;
    public bool On = false;
    private SpriteRenderer sprite;
    private LightGroupController groupController;

    // Use this for initialization
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        groupController = GetComponentInParent<LightGroupController>();

        if (On)
            turnLightOn();
        else
            turnLightOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnLightOn()
    {
        StopAllCoroutines();
        turnLightOn();
    }

    private void turnLightOn()
    {
        sprite.color = OnColor;

        On = true;

        LightIsLit(this, new EventArgs());

        if (groupController != null)
            groupController.CheckIfGroupIsLit();
    }

    public void TurnLightOff()
    {
        StopAllCoroutines();
        turnLightOff();
    }

    private void turnLightOff()
    {
        sprite.color = OffColor;

        On = false;
    }

    public void FlashStop()
    {
        StopAllCoroutines();
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(flashLight(.5f));
    }

    public void Flash(float delay)
    {
        StopAllCoroutines();
        StartCoroutine(flashLight(delay));
    }

    public void Flash(int timesToFlash, float delay)
    {
        StopAllCoroutines();
        StartCoroutine(flashLight(timesToFlash,delay));
    }

    IEnumerator flashLight(int timesToFlash, float delay)
    {
        for (int x = 0; x < timesToFlash; x++)
        {
            turnLightOn();

            yield return new WaitForSeconds(delay);

            turnLightOff();

            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// flashes light until FlashStop is called
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator flashLight(float delay)
    {
        while (true)
        {
            turnLightOn();

            yield return new WaitForSeconds(delay);

            turnLightOff();

            yield return new WaitForSeconds(delay);
        }
    }
}
