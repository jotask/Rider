using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleSortLayerScript : MonoBehaviour {
	
	void Awake () {
		GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = "Background";	
	}

}
