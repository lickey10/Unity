using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public AudioClip explore;
	public AudioClip buildup;
	public AudioClip crystalmusic;
	public Transform crystalarea;
	public Transform explorearea;
	public Transform explorearea2;
	public Transform buildarea;
	public Transform objecttopositionon;
	public Transform currentmusicarea;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(objecttopositionon){
			transform.position=objecttopositionon.transform.position;
		}
		
		
	}
	void OnTriggerEnter(Collider other){
	if(other.transform==explorearea){
			currentmusicarea=other.transform;
		if(GetComponent<AudioSource>().clip==explore){}
			else{
			GetComponent<AudioSource>().clip=explore;	
			GetComponent<AudioSource>().Play();
			}
		}
		
		if(other.transform==explorearea2){
			currentmusicarea=other.transform;
		if(GetComponent<AudioSource>().clip==explore){}
			else{
			GetComponent<AudioSource>().clip=explore;	
			GetComponent<AudioSource>().Play();
			}
		}
		
		
		if(other.transform==buildarea){
			currentmusicarea=other.transform;
		if(GetComponent<AudioSource>().clip==buildup){}
			else{
			GetComponent<AudioSource>().clip=buildup;	
			GetComponent<AudioSource>().Play();
			}
		}
		
		if(other.transform==crystalarea){
			currentmusicarea=other.transform;
		if(GetComponent<AudioSource>().clip==crystalmusic){}
			else{
			GetComponent<AudioSource>().clip=crystalmusic;	
			GetComponent<AudioSource>().Play();
			}
		}
		
		
	}
}
