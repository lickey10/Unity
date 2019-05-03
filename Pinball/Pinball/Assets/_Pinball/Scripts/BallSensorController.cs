using UnityEngine;
using System.Collections;
using SgLib;
using System.Linq;

public class BallSensorController : MonoBehaviour
{
    public int Points = 0;
    public GameObject[] CorrespondingLights;
    public GameObject[] CorrespondingObjectsToTrigger;

    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private bool isChecked;

    // Use this for initialization
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CorrespondingLights.ToList().ForEach(x => x.GetComponent<LightController>().TurnLightOn());
        CorrespondingObjectsToTrigger.ToList().ForEach(x => x.SendMessageUpwards("FoundBall", this, SendMessageOptions.DontRequireReceiver));

        ScoreManager.Instance.AddScore(Points);
    }
}
