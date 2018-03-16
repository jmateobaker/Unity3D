using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollapse : MonoBehaviour {

	private float eforce;
	private float rads;

	void OnTriggerEnter (Collider col)
	{
		eforce = 100.0f;
		rads = 100.0f;
		print (col.gameObject.GetComponent<FracturedChunk>());
		col.gameObject.GetComponent<FracturedChunk> ().Impact (col.transform.position, eforce, rads, true);
	}

}
