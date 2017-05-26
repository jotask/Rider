using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

	public GameObject inGame;
	public GameObject options;
	public GameObject gameover;
	
	public GameObject gameInput;

	public Slider master;
	public Slider music;
	public Slider sfx;

	// Use this for initialization
	void Start () {
		inGame.SetActive(true);
		options.SetActive(false);
		gameover.SetActive(false);
		
		#if UNITY_ANDROID
			gameInput.SetActive(true);
		#endif

		master.value = AudioManager.instance.masterVolumen;
		music.value = AudioManager.instance.musicVolumen;
		sfx.value = AudioManager.instance.sfxVolumen;
		
	}

	public void showOptions()
	{
		Time.timeScale = 0f;
		inGame.SetActive(false);
		options.SetActive(true);
		gameover.SetActive(false);
		
		#if UNITY_ANDROID
				gameInput.SetActive(false);
		#endif
	}

	public void GameOver()
	{
		Time.timeScale = 0f;
		inGame.SetActive(false);
		options.SetActive(false);
		gameover.SetActive(true);
		
		#if UNITY_ANDROID
				gameInput.SetActive(false);
		#endif
	}

	public void continuePlaying()
	{
		inGame.SetActive(true);
		options.SetActive(false);
		gameover.SetActive(false);
		
		#if UNITY_ANDROID
				gameInput.SetActive(true);
		#endif
		
		Time.timeScale = 1f;
	}
	
	public void setMasterVolumen(float value)
	{
		AudioManager.instance.SetVolumen(value, AudioManager.AudioChannel.Master);
	}
	
	public void setMusicVolumen(float value)
	{
		AudioManager.instance.SetVolumen(value, AudioManager.AudioChannel.Music);
	}
	
	public void setSfxVolumen(float value)
	{
		AudioManager.instance.SetVolumen(value, AudioManager.AudioChannel.Sfx);
	}

	public void reload()
	{
		SceneManager.LoadScene("game");
	}

	public void goToMenu()
	{
		SceneManager.LoadScene("menu");
	}

}
