using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDemoControl : MonoBehaviour
{
    int particleNumber				= 0;        // The current selected particle from particles[]
    GameObject[] particles;                 // The particles Availalbe for the demo
    GameObject[] targets;					// Targets for shooting particles
    int targetNumber				= 0;        // The current target
    GameObject currentParticle	;					// Holds the current in-game object
    string currentParticleName;

    // Start is called before the first frame update
    void Start()
    {
        currentParticleName = "Current: " + particles[particleNumber].name + " (" + (particleNumber + 1) + " of " + particles.Length + ")";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            ShowNewParticle();
        if (Input.GetKeyDown("left"))
            SwitchParticle(-1);
        if (Input.GetKeyDown("right"))
            SwitchParticle(1);
    }

    private void ShowNewParticle()
    {
        if (currentParticle)
            Destroy(currentParticle);
        currentParticle = Instantiate(particles[particleNumber], new Vector3(0, 0, 0), Quaternion.identity);
        currentParticle.transform.position = currentParticle.GetComponent<demoParticleControl>().startPosition;
        currentParticle.transform.eulerAngles = currentParticle.GetComponent<demoParticleControl>().startRotation;
        if (currentParticle.GetComponent<demoParticleControl>().shootsTarget)
        {
            currentParticle.transform.LookAt(targets[targetNumber].transform);
            targetNumber += 1;
            if (targetNumber < 0)
                targetNumber = targets.Length - 1;
            else if (targetNumber == targets.Length)
                targetNumber = 0;
        }
    }

    private void SwitchParticle(int value)
    {
        particleNumber += value;
        if (particleNumber < 0)
            particleNumber = particles.Length - 1;
        else if (particleNumber == particles.Length)
            particleNumber = 0;
        currentParticleName = "Current: " + particles[particleNumber].name + " (" + (particleNumber + 1) + " of " + particles.Length + ")";
    }
}
