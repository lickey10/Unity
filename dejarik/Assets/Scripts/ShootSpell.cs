using UnityEngine;
using System.Collections;

public class ShootSpell : MonoBehaviour
{

    public Transform target;
    public GameObject Explosion;

    float timeTravel = .5f;
    float time = 0f;

    Vector3 origin;
    Vector3 destination;
    float distance = 0f;

    void Start()
    {
        //shootAt(target);
    }

    public void ShootAt(Transform newTarget)
    {
        target = newTarget;

        time = timeTravel; // in seconds

        origin = transform.position;
        destination = target.position;
    }

    void Update()
    {
        if (target == null) return;

        time -= Time.deltaTime;

        destination = target.position;
        float progress = Mathf.InverseLerp(timeTravel, 0f, time);

        //set projectile position
        Vector3 newPosition = Vector3.Lerp(origin, destination, progress);

        //set projectile height
        newPosition.y = Mathf.Cos(Mathf.Lerp(-Mathf.PI * 1f, Mathf.PI * 1f, progress));

        transform.position = newPosition;
        if (transform.position == destination || (transform.position.x == destination.x && transform.position.z == destination.z))
        {
            //create small explosion
            Instantiate(Explosion, destination, Quaternion.Euler(0, 90, 0));
            
            target = null;

            //call destroy on this fireball
            Destroy(transform.gameObject);
        }
    }

}