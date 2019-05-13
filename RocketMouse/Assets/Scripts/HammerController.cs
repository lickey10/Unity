using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    public enum Directions { Left,Right };
    //private Animator animator;
    //private bool moveLeft = false;
    //private bool moveRight = false;
    //private bool isMoving = false;
    public Directions Direction = Directions.Right;
    TweenRotation tweenRotation;


    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        tweenRotation = GetComponent<TweenRotation>();

        if (Direction == Directions.Left)
            tweenRotation.to.z = 90;
        else
            tweenRotation.to.z = -90;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //moveRight = false;
            //moveLeft = false;
            //isMoving = false;

            

            //animator.SetTrigger("Attack");
        }
    }
}
