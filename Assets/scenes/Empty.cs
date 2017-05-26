using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Empty : MonoBehaviour
{

	public Slider slider;

	void Start () {
		Loading.LoadScene(Loading.Scenes.MENU);
	}

}
