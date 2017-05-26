
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

	public enum Scenes { MENU, GAME }

	private static Loading instance;

	public Canvas canvas;
	public Slider loading;

	// Use this for initialization
	void Awake () {
		if (instance)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;

		canvas.enabled = false;
		
		DontDestroyOnLoad(this);
		
	}

	public static void LoadScene(Scenes scene)
	{
		if (!IsInstance()) return;	
		instance.LoadSceneHere(scene);
	}

	private void LoadSceneHere(Scenes scene)
	{
		canvas.enabled = true;
		StartCoroutine(LoadNewScene(scene));
	}

	static bool IsInstance()
	{
		if (!instance)
		{
			Debug.LogError("Loader haven't been loaded: Is Not An Instance Error");
		}
		return instance;
	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene(Scenes scene)
	{
		
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(scene.ToString().ToLower());
		
		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone)
		{
			loading.value = async.progress;
			Debug.Log(async.progress);
			yield return null;
		}
		canvas.enabled = false;
	}
	
}
