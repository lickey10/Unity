using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustDimensions : MonoBehaviour
{
    public UILabel WidthDisplay;
    public UILabel HeightDisplay;
    public UILabel CompletionDisplay;

    int minWidth = 3;
    int maxWidth = 20;
    int minHeight = 3;
    int maxHeight = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDimensions()
    {
        AddWidth();
        AddHeight();

        var intWidth = int.Parse(WidthDisplay.text);

        var intHeight = int.Parse(HeightDisplay.text);

        int tempCompletions = SaveSystem.GetInt(intWidth.ToString() + "X" + intHeight.ToString() + "completions", 0);

        CompletionDisplay.text = tempCompletions.ToString();
    }

    public void SubtractDimensions()
    {
        SubtractWidth();
        SubtractHeight();

        var intWidth = int.Parse(WidthDisplay.text);

        var intHeight = int.Parse(HeightDisplay.text);

        int tempCompletions = SaveSystem.GetInt(intWidth.ToString() + "X" + intHeight.ToString() + "completions", 0);

        CompletionDisplay.text = tempCompletions.ToString();
    }

    public void AddWidth()
    {
        var intWidth = int.Parse(WidthDisplay.text);

        intWidth++;

        if(intWidth <= maxWidth)
            WidthDisplay.text = intWidth.ToString();
    }

    public void SubtractWidth()
    {
        var intWidth = int.Parse(WidthDisplay.text);

        intWidth--;

        if(intWidth >= minWidth)
            WidthDisplay.text = intWidth.ToString();
    }

    public void AddHeight()
    {
        var intHeight = int.Parse(HeightDisplay.text);

        intHeight++;

        if(intHeight <= maxHeight)
            HeightDisplay.text = intHeight.ToString();
    }

    public void SubtractHeight()
    {
        var intHeight = int.Parse(HeightDisplay.text);

        intHeight--;

        if(intHeight >= minHeight)
            HeightDisplay.text = intHeight.ToString();
    }
}
