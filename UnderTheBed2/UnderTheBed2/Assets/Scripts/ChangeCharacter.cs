using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour {

    public GameObject locked;
    public GameObject stars;
    GameObject currentCharacterPrefab;
    public GameObject Character;

    // Use this for initialization
    void Start()
    {
        if(locked != null)//determine if this character is locked or not
        {
            string strStars = stars.GetComponent<UILabel>().text;
            int tempStars = int.Parse(stars.GetComponent<UILabel>().text.Replace(" Stars",""));
            if (gamestate.Instance.StarCount >= tempStars)//this charcter is unlocked
                locked.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeCurrentCharacter(GameObject NewCharacterPrefab)
    {
        //PlayerWeapons pw = GameObject.FindWithTag("WeaponCamera").GetComponent<PlayerWeapons>();
        //Destroy(pw);

        currentCharacterPrefab = GameObject.FindGameObjectWithTag("Player");
        Vector3 characterPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
        Quaternion characterRotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        Destroy(currentCharacterPrefab);
        
        Instantiate(NewCharacterPrefab, characterPosition, characterRotation);

        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        //GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>().enabled = false;
        //GameObject.FindGameObjectWithTag("MenuCameraMain").GetComponent<Camera>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlatformCamera>().Hero = NewCharacterPrefab.transform;

        gamestate.Instance.CurrentCharacter = NewCharacterPrefab;

        Time.timeScale = 1;

        Destroy(GameObject.FindGameObjectWithTag("ScrollingMenu"));
    }

    void OnClick()
    {
        if (enabled)
        {
            //figure out if locked or not
            if(locked == null || !locked.activeSelf)//character is unlocked
                ChangeCurrentCharacter(Character);
        }
    }
}
