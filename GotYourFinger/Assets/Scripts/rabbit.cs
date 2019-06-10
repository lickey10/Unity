using UnityEngine;
using System.Collections;

public class rabbit : MonoBehaviour {

    bool followMouse = false;
    Vector3 clickedPosition;
    int speed = 2;
    public GameObject hand;//one hand so we know where to hang from
    float handOffset = 0;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        //clickedPosition = Camera.main.ScreenToWorldPosition(Input.mousePosition);
        //transform.position = Vector3.Lerp(startPoint, clickedPosition, (Time.time - startTime) / 1.0f);

        var attacking = animator.GetBool("Attacking");

        if (followMouse)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);
            //transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
            pos.y = pos.y - 3;
            transform.position = pos;
        }
    }

    void OnMouseDown()
    {
        //jump to mouse
        followMouse = true;
        handOffset = (hand.transform.position.y);
        animator.SetBool("jump", true);
    }

    void OnMouseUp()
    {
        followMouse = false;
        animator.SetBool("jump", false);
    }
}
