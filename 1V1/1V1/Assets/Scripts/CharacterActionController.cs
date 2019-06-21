using UnityEngine;
using System.Collections;

public class CharacterActionController : MonoBehaviour {

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("j"))
            anim.SetBool("Jab", true);
        if (Input.GetKeyDown("u"))
            anim.SetBool("Hikick", true);
        if (Input.GetKeyDown("i"))
            anim.SetBool("Spinkick", true);
        if (Input.GetKeyDown("o"))
            anim.SetBool("Rising_P", true);
        //if (Input.GetKeyDown("j"))
        //    anim.SetBool("Headspring", true);
        //if (GUI.Button(new Rect(Screen.width - 190, 210, 100, 20), "Land"))
        //    anim.SetBool("Land", true);
        if (Input.GetKeyDown("k"))
            anim.SetBool("ScrewK", true);
        //if (GUI.Button(new Rect(Screen.width - 190, 270, 100, 20), "DamageDown"))
        //    anim.SetBool("DamageDown", true);
        if (Input.GetKeyDown("l"))
            anim.SetBool("SAMK", true);
        //if (GUI.Button(new Rect(Screen.width - 190, 330, 100, 20), "Run"))
        //    anim.SetBool("Run", true);
        //if (GUI.Button(new Rect(Screen.width - 190, 360, 100, 20), "Run_end"))
        //    anim.SetBool("Run", false);
    }
}
