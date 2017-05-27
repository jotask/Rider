using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	
	public GameObject mainMenu;
	public GameObject optionsMenu;

	public Slider[] volumen;

	void Start()
	{

		this.MainMenu();
		
		volumen[0].value = AudioManager.instance.masterVolumen;
		volumen[1].value = AudioManager.instance.musicVolumen;
		volumen[2].value = AudioManager.instance.sfxVolumen;
		
		AudioManager.instance.PlayMusic(MusicLibrary.Scene.MENU);
	}

	public void Play() 
	{
		Loading.LoadScene(Loading.Scenes.SELECTION);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void OptionsMenu()
	{
		mainMenu.SetActive(false);
		optionsMenu.SetActive(true);
	}

	public void MainMenu()
	{
		mainMenu.SetActive(true);
		optionsMenu.SetActive(false);
	}

	public void GoToShop()
	{
		Loading.LoadScene(Loading.Scenes.SHOP);
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
