using UnityEngine;
using System.Collections;

public class DragBasketLv4 : MonoBehaviour {
	
	// Use this for initialization
	public GameObject[] food;
	public GameObject bread;

	int endn = 0;
	void Start () {
		StartCoroutine("tick");
		endn = (int)Random.Range (5, 12);
	}
	bool istick =true;
	IEnumerator tick(){
		while(istick){
			yield return new WaitForSeconds(2);
			if(endn <= 0){
				bread.GetComponent<Rigidbody>().isKinematic = false;
				bread.GetComponent<Rigidbody>().AddForce(Vector3.down);
				istick = false;
			}else{
				
				int trnd = (int)Random.Range(0,5);
				float trndx = Random.Range(-1.5f,1.5f);
				GameObject tfood = Instantiate(food[trnd],new Vector3(trndx,3,0),Quaternion.identity) as GameObject;
				tfood.GetComponent<Rigidbody>().isKinematic =false;
				Destroy(tfood,3);
				endn--;
			}
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameData.getInstance ().isFail || GameData.getInstance ().isWin)
						return;
		if (bread.transform.position.y < -5) {
			GameData.getInstance().main.gameFailed();		
		}
	}
	
	void OnTriggerEnter(Collider collider){
		
		if (collider.tag == "game") {
			Destroy (collider.gameObject);
			GameData.getInstance ().main.gameFailed ();
		} else if (collider.name == "bread") {
			bread.SetActive (false);
			GameData.getInstance ().main.gameWin ();
			
			
		}
		
	}
}
