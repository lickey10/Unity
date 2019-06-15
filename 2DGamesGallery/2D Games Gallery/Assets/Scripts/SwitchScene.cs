
using UnityEngine;

public class SwitchScene: MonoBehaviour
{
    public string SceneNameToLoad = "";
    public Color FadeColor = Color.black;
    public float FadeDuration = 1f;
    
    public void LoadScene()
    {
        Initiate.Fade(SceneNameToLoad, FadeColor, FadeDuration);
        //SceneManager.LoadScene(SceneNameToLoad);
    }

    public void LoadScene(string sceneNameToLoad)
    {
        Initiate.Fade(sceneNameToLoad, FadeColor, FadeDuration);
    }

    public void LoadScene(string sceneNameToLoad, Color fadeColor)
    {
        Initiate.Fade(sceneNameToLoad, fadeColor, FadeDuration);
    }

    public void LoadScene(string sceneNameToLoad, Color fadeColor, float fadeDuration)
    {
        Initiate.Fade(sceneNameToLoad, fadeColor, fadeDuration);
    }
}
