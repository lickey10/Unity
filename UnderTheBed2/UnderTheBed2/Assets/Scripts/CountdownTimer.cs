using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    int time, a;
    float x;
    public bool count;
    public string timeDisp;
    public Text CountDownText;
    public Text CountDownTextShadow;
    //public GameObject CurrentCharacter;
    private int originalFontSize = 0;
    private string currentNumber = "3";

    void Start()
    {
        time = 3;
        count = true;
        originalFontSize = CountDownText.fontSize;

        //gamestate.Instance.CurrentCharacter = CurrentCharacter;

        //make sure we are using the correct character
        if (gamestate.Instance.CurrentCharacter != null)
            ChangeCurrentCharacter();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (count)
        {
            CountDownText.enabled = true;
            CountDownTextShadow.enabled = true;
            timeDisp = time.ToString();
            //GameObject.Find("StartCounter").GetComponent<Text>().text = timeDisp;

            CountDownTextShadow.fontSize += 3;
            CountDownTextShadow.text = timeDisp;
            CountDownText.fontSize += 3;
            CountDownText.color = Color.red;
            CountDownText.text = timeDisp;
            x += Time.deltaTime;
            a = (int)x;
            print(a);
            switch (a)
            {
                case 0: //GameObject.Find("StartCounter").GetComponent<Text>().text = "3"; break;
                    if (currentNumber != "3")
                    {
                        CountDownText.fontSize = originalFontSize;
                        CountDownTextShadow.fontSize = originalFontSize;
                    }

                    currentNumber = "3";
                    CountDownText.text = "3";
                    CountDownTextShadow.text = "3";
                    break;
                case 1: //GameObject.Find("StartCounter").GetComponent<Text>().text = "2"; break;
                    if (currentNumber != "2")
                    {
                        CountDownText.fontSize = originalFontSize;
                        CountDownTextShadow.fontSize = originalFontSize;
                    }

                    currentNumber = "2";
                    CountDownText.text = "2";
                    CountDownTextShadow.text = "2";
                    break;
                case 2: //GameObject.Find("StartCounter").GetComponent<Text>().text = "1"; break;
                    if (currentNumber != "1")
                    {
                        CountDownText.fontSize = originalFontSize;
                        CountDownTextShadow.fontSize = originalFontSize;
                    }

                    currentNumber = "1";
                    CountDownText.text = "1";
                    CountDownTextShadow.text = "1";
                    break;
                case 3:
                    //GameObject.Find("StartCounter").GetComponent<Text>().enabled = false;
                    CountDownText.fontSize = originalFontSize;
                    CountDownTextShadow.fontSize = originalFontSize;

                    startGame();

                    break;
            }
        }
        
    }

    private void startGame()
    {
        CountDownText.enabled = false;
        CountDownTextShadow.enabled = false;

        count = false;
        x = 0;
        //GameObject.FindGameObjectWithTag("Player").BroadcastMessage("MoveCharacter");
    }

    public void ChangeCurrentCharacter()
    {
        GameObject currentCharacterPrefab = GameObject.FindGameObjectWithTag("Player");
        Vector3 characterPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        Quaternion characterRotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        Destroy(currentCharacterPrefab);

        Instantiate(gamestate.Instance.CurrentCharacter, characterPosition, characterRotation);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformCamera>().Hero = gamestate.Instance.CurrentCharacter.transform;
    }
}
