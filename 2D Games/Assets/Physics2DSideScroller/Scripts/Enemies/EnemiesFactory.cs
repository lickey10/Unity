using UnityEngine;
using System.Collections;

#region - Script Synopsis
/*
This script's Awake() method creates a collider in the middle of the screen which acts as a trigger.
When that trigger is activated, it will fire the event tied into the OnTriggerEnter2D() method below.
That method simply instantiates random enemies (the number is determined by the current difficulty level)
within the switch() statement.
*/
#endregion

public class EnemiesFactory : MonoBehaviour
{
    //Creates a collider that triggers Enemy spawning
    void Awake()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.offset = new Vector2(0, 5);
        collider.size = new Vector2(0.5f, 9.5f);
    }


    //When the collider is triggered it spawns a new wave of Enemies determined by the current "level" multiplier
    //which increments with each triggered spawn. Also, destroys the trigger collider to avoid possible re-triggering
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(GetComponent<Collider2D>());
            WorldManager.Level++;

            for (int i = 0; i < WorldManager.Difficulty; i++)
            {
                int randomInstance = Random.Range(0, 6);
                float randomX = transform.position.x + (6 * (Random.Range(0, 2) * 2 - 1));
                float randomY = Random.Range(4, 8);

                switch (randomInstance)
                {
                    case 0:
                        Enemy2<Gigantor> giantGeorge = new Enemy2<Gigantor>("GiantGeorge");
                        giantGeorge.ScriptComponent.Initialize(speed: 1, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 1:
                        Enemy2<Tweaker> tweakyTim = new Enemy2<Tweaker>("TweakyTim");
                        tweakyTim.ScriptComponent.Initialize(speed: 4, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 2:
                        Enemy2<Lush> lushyLinda = new Enemy2<Lush>("LushyLinda");
                        lushyLinda.ScriptComponent.Initialize(speed: Random.Range(6, 18), position: new Vector3(randomX, randomY, 1));
                        break;

                    case 3:
                        Enemy2<Bouncer> bouncyBill = new Enemy2<Bouncer>("BouncyBill");
                        bouncyBill.ScriptComponent.Initialize(speed: 4, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 4:
                        Enemy2<Torque> torqyTom = new Enemy2<Torque>("TorqyTom");
                        torqyTom.ScriptComponent.Initialize(speed: 3, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;

                    case 5:
                        Enemy2<Ghost> ghostlyGayle = new Enemy2<Ghost>("GhostlyGayle");
                        ghostlyGayle.ScriptComponent.Initialize(speed: 2, position: new Vector3(randomX, randomY, 1));
                        break;
                }
            }
        }
    }
}