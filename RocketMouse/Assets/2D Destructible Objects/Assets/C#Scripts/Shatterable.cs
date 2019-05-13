using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatterable : MonoBehaviour, IHittable
{
    public List<Spawner> spawnPoints;
    public GameObject PointsDisplay;
    public int PointsValue = 10;

    private SpriteRenderer render;

    // Use this for initialization
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void HitReceived()
    {
        Die();
    }

    public void Die()
    {
        //display points
        PointsDisplay.GetComponentInChildren<UILabel>().text = "+" + PointsValue.ToString();
        PointsDisplay.transform.Find("pointsEnd").position = new Vector3(transform.position.x - 2, transform.position.y + 15, transform.position.z);

        Instantiate(PointsDisplay, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        render.enabled = false;

        foreach (Spawner spawn in spawnPoints)
        {
            spawn.Spawn();
        }

        Destroy(gameObject);
    }
}
