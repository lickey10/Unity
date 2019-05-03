using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveActiveStateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(PlayerPrefs.GetInt(this.gameObject.name + "_ActiveState", 1) == 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt(this.gameObject.name + "_ActiveState", this.gameObject.activeSelf.ToString() == "false" ? 0 : 1);
        PlayerPrefs.Save();
    }
}
