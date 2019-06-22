using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKill : MonoBehaviour
{
    public GameObject Explosion;
    public float CollisionPrefabScaleFactor = 2f;
    public AudioClip CollisionSound;

    GameObject instantiatedCollisionPrefab;
    //RaceManager raceManager = null;

    // Start is called before the first frame update
    void Start()
    {
        //raceManager = GameObject.FindGameObjectWithTag("RaceManager").GetComponent<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("AI") || collision.transform.CompareTag("Player"))
        { 
            instantiatedCollisionPrefab = Instantiate(Explosion, collision.contacts[0].point, Quaternion.identity);
            instantiatedCollisionPrefab.transform.localScale = new Vector3(instantiatedCollisionPrefab.transform.localScale.x * CollisionPrefabScaleFactor, instantiatedCollisionPrefab.transform.localScale.y * CollisionPrefabScaleFactor, instantiatedCollisionPrefab.transform.localScale.z * CollisionPrefabScaleFactor);

            Destroy(collision.transform.root.gameObject);
        }        
    }
}
