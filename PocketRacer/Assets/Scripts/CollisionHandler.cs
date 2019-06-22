using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject CollisionPrefab;
    public float CollisionPrefabScaleFactor = 2f;
    public AudioClip CollisionSound;

    GameObject instantiatedCollisionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.rigidbody == null || collision.transform.CompareTag("AI")) && instantiatedCollisionPrefab == null)
        {
            instantiatedCollisionPrefab = Instantiate(CollisionPrefab, collision.contacts[0].point, Quaternion.identity);
            instantiatedCollisionPrefab.transform.localScale = new Vector3(instantiatedCollisionPrefab.transform.localScale.x * CollisionPrefabScaleFactor, instantiatedCollisionPrefab.transform.localScale.y * CollisionPrefabScaleFactor, instantiatedCollisionPrefab.transform.localScale.z * CollisionPrefabScaleFactor);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position, transform.forward, out hit))
    //    {
    //        Instantiate(CollisionPrefab, hit.point,Quaternion.identity);

    //        Debug.Log("Point of contact: " + hit.point);
    //    }
    //}
}
