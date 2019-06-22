using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public GameObject PowerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AI" || other.tag == "Player")
        {
            if (PowerUpPrefab != null)
            {
                other.transform.root.GetComponentInChildren<Car>().Landmines.Add(PowerUpPrefab);

                Destroy(gameObject);
            }
        }
    }
}
