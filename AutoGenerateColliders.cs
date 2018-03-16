using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateColliders : MonoBehaviour {

	public Transform ColliderCharacter;
	public Transform ColliderObject;

	// Use this for initialization
	void Start () {

		List<string> ColliderParts = new List<string>();

		ColliderParts.Add("lHand");
		ColliderParts.Add("rHand");
		ColliderParts.Add("lToe");
		ColliderParts.Add("rToe");
		ColliderParts.Add("head");

		Component[] children = ColliderCharacter.GetComponentsInChildren<Transform>();
		foreach(Transform transform in children)
		{
			if (ColliderParts.Contains (transform.name)) {
				Transform coll = Instantiate (ColliderObject, transform.position, transform.rotation, transform);
				coll.name = "BuildingCollider_" + transform.name;

				if (transform.name.Contains("Hand")) {
					string endJoint = transform.name [0] + "Mid3";
					GameObject endJointObject = GameObject.Find (endJoint);
					float handLength = Vector3.Distance (transform.position, endJointObject.transform.position);
					coll.localScale = new Vector3 (0.2f, 0.1f, 0.2f);
				} else if (transform.name.EndsWith("Toe")) {
					string endJoint = transform.name [0] + "BigToe";
					GameObject endJointObject = GameObject.Find (endJoint);
					float footLength = Vector3.Distance (transform.position, endJointObject.transform.position);
					coll.localScale = new Vector3 (0.2f, 0.1f, 0.4f);
				} else {
					coll.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
				}
			}
		}
	}
}
