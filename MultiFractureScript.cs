using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UltimateFracturing;

[ExecuteInEditMode]
public class MultiFractureScript : MonoBehaviour {

	public Transform PlaceForFractures;
	public Material InteriorMaterial;
	public FracturedObject frack;
	public int MaximumFracks;
	//public float DetachValue;
	public float WeightMultiplier;

	private FracturedObject fobj;

	void Progress(string strTitle, string strMessage, float fT)
	{
		if(EditorUtility.DisplayCancelableProgressBar(strTitle, strMessage, fT))
		{
			UltimateFracturing.Fracturer.CancelFracturing();
		}
	}

	public void ApplyFractures () {
		MeshFilter[] allChildren = GetComponentsInChildren<MeshFilter> ();
		List<float> floatlist = new List<float> ();

		foreach (MeshFilter child in allChildren) {
			float volume = child.sharedMesh.bounds.size.x * child.sharedMesh.bounds.size.y * child.sharedMesh.bounds.size.z;
			floatlist.Add (volume);
		}

		floatlist.Sort ();
		float listmin = floatlist [0];
		float listmax = floatlist [floatlist.Count - 1];
		float liststep = (listmax - listmin) / (MaximumFracks - 2);
		int liststepint = (int)liststep;
	
		foreach (MeshFilter child in allChildren) {
			
			fobj = Instantiate (frack, PlaceForFractures);
			fobj.name = child.name + "_frack";
			fobj.SourceObject = child.gameObject;
			fobj.RandomSeed = Random.Range (0, 65536);
			//fobj.EventDetachMinMass = DetachValue;
			//fobj.EventDetachMinVelocity = DetachValue;
			fobj.SplitMaterial = InteriorMaterial;

			// Split Dimensions
			float dimsum = child.sharedMesh.bounds.size.x + child.sharedMesh.bounds.size.y + child.sharedMesh.bounds.size.z;
			fobj.SplitXProbability = child.sharedMesh.bounds.size.x / dimsum;
			fobj.SplitYProbability = child.sharedMesh.bounds.size.y / dimsum;
			fobj.SplitZProbability = child.sharedMesh.bounds.size.z / dimsum;
			fobj.SplitXVariation = child.sharedMesh.bounds.size.x / dimsum;
			fobj.SplitYVariation = child.sharedMesh.bounds.size.y / dimsum;
			fobj.SplitZVariation = child.sharedMesh.bounds.size.z / dimsum;

			float volume = child.sharedMesh.bounds.size.x * child.sharedMesh.bounds.size.y * child.sharedMesh.bounds.size.z;
			fobj.TotalMass = volume * WeightMultiplier;
			//fobj.StartStatic = true;
			fobj.GenerateNumChunks = (int)(volume / liststepint) + 2;

			List<GameObject> listGameObjects;
			try
			{
				UltimateFracturing.Fracturer.FractureToChunks(fobj, true, out listGameObjects, Progress);
			}
			catch(System.Exception e)
			{
				Debug.LogError(string.Format("Exception computing chunks ({0}):\n{1}", e.Message, e.StackTrace));
			}

			EditorUtility.ClearProgressBar();
		}
	}
}
