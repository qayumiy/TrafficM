using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderController : MonoBehaviour {
	int randomizer;

	public GameObject hingerSpoil;
	public GameObject hingerGood;

	public GameObject stepBad;
	public GameObject stepBolt1;
	public GameObject stepBolt2;
	public GameObject stepGood;

	// Use this for initialization
	void Start () {
		randomizer = Random.Range (1, 5);
		Debug.Log (randomizer);
		if (randomizer == 1) {
			hingerSpoil.SetActive (false);
			hingerGood.SetActive (true);


			stepBad.SetActive (false);
			stepBolt1.SetActive (true);
			stepBolt2.SetActive (true);
			stepGood.SetActive (true);
					
		}


		if (randomizer == 2) {
			hingerSpoil.SetActive (true);
			hingerGood.SetActive (false);


			stepBad.SetActive (true);
			stepBolt1.SetActive (false);
			stepBolt2.SetActive (false);
			stepGood.SetActive (false);

		}

		if (randomizer == 3) {
			hingerSpoil.SetActive (false);
			hingerGood.SetActive (true);


			stepBad.SetActive (true);
			stepBolt1.SetActive (false);
			stepBolt2.SetActive (false);
			stepGood.SetActive (false);

		}

		if (randomizer == 4) {
			hingerSpoil.SetActive (true);
			hingerGood.SetActive (false);


			stepBad.SetActive (false);
			stepBolt1.SetActive (true);
			stepBolt2.SetActive (true);
			stepGood.SetActive (true);

		}
	}

	public void ladderToggle(){
		randomizer = Random.Range (1, 5);

	
	}

	void Update(){

		if (Input.GetKeyUp (KeyCode.L)) {
			randomizer = Random.Range (1, 5);

		}


		if (randomizer == 2) {
			hingerSpoil.SetActive (true);
			hingerGood.SetActive (false);


			stepBad.SetActive (true);
			stepBolt1.SetActive (false);
			stepBolt2.SetActive (false);
			stepGood.SetActive (false);

		}

		if (randomizer == 3) {
			hingerSpoil.SetActive (false);
			hingerGood.SetActive (true);


			stepBad.SetActive (true);
			stepBolt1.SetActive (false);
			stepBolt2.SetActive (false);
			stepGood.SetActive (false);

		}

		if (randomizer == 4) {
			hingerSpoil.SetActive (true);
			hingerGood.SetActive (false);

			stepBad.SetActive (false);
			stepBolt1.SetActive (true);
			stepBolt2.SetActive (true);
			stepGood.SetActive (true);

		}


	}

}
