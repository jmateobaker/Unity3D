using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActivateChildren : MonoBehaviour {

	public void ActivateEverything () {
		foreach (Transform child in GetComponentsInChildren<Transform>(true)) {
			child.gameObject.SetActive(true);
		}
	}

	public void DeactivateEverything() {
		foreach (Transform child in GetComponentsInChildren<Transform>(true)) {
			child.gameObject.SetActive(false);
		}
	}
}
