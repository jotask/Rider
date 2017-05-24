using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

	public GameObject inGame;
	public GameObject options;

	public Slider master;
	public Slider music;
	public Slider sfx;

	// Use this for initialization
	void Start () {
		inGame.SetActive(true);
		options.SetActive(false);

		master.value = AudioManager.instance.masterVolumen;
		music.value = AudioManager.instance.musicVolumen;
		sfx.value = AudioManager.instance.sfxVolumen;
		
	}

	public void showOptions()
	{
		Time.timeScale = 0f;
		inGame.SetActive(false);
		options.SetActive(true);
	}

	public void continuePlaying()
	{
		inGame.SetActive(true);
		options.SetActive(false);
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

}
