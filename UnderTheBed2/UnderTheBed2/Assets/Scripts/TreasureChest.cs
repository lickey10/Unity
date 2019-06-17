using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using Scripts;

public class TreasureChest : MonoBehaviour {

    public Animation OpenAnimation;
    private bool isOpen = false;
    public Transform Treasure;

	// Use this for initialization
	void Start () {
        //open treasure chest
        //GetComponent<Animation>().Play("open");

        //pop out the diamond and add  money
        //GameObject scripts = GameObject.FindWithTag("Scripts");
        //SceneManager gameManager = scripts.GetComponent<SceneManager>();

        //Instantiate(Treasure, new Vector3(this.transform.position.x+2, this.transform.position.y + 3.5f, this.transform.position.z+2), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isOpen)
        {
            //open treasure chest
            GetComponent<Animation>().Play("open");
            GameObject.FindGameObjectWithTag("Player").GetComponent<MummyMove>().Move = false;

            //pop out the diamond and add  money
            //GameObject scripts = GameObject.FindWithTag("Scripts");
            //SceneManager gameManager = scripts.GetComponent<SceneManager>();
            //gameManager.Coins = gameManager.Coins + 1000;
            //DBStoreController.singleton.balance += 1000;
            Instantiate(Treasure, new Vector3(this.transform.position.x, this.transform.position.y + 5,this.transform.position.z), Quaternion.identity);

            var PointScript = gameObject.AddComponent<Point>();
            PointScript.PointCurrent = 500;

            isOpen = true;

            gamestate.Instance.SetScore(gamestate.Instance.getScore() + 500);
            SoundEffectsHelper.Instance.MakeLevelCompleteSound();
            StartCoroutine(changeLevel(2));
        }
    }

    IEnumerator changeLevel(int secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        
        SimpleSceneFader.ChangeSceneWithFade("levelComplete");
    }
}
