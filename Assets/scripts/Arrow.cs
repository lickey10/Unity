using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    private Transform target;
    private GameObject scripts;
    private EMF emf;
    public float speed = 2;

    // Use this for initialization
    void Start () {
        emf = GameObject.FindGameObjectWithTag("Scripts").GetComponent<EMF>();
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 targetDir = new Vector3(float.Parse(emf.RawVectorValueX.text), float.Parse(emf.RawVectorValueY.text), float.Parse(emf.RawVectorValueZ.text)) - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
