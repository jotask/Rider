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

	void Start () {
		inGame.SetActive(true);
		options.SetActive(false);
		gameover.SetActive(false);
		
		gameInput.SetActive(true);

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
		
		gameInput.SetActive(false);

	}

	public void GameOver()
	{
		Time.timeScale = 0f;
		inGame.SetActive(false);
		options.SetActive(false);
		gameover.SetActive(true);
		
		gameInput.SetActive(false);
		
	}

	public void continuePlaying()
	{
		inGame.SetActive(true);
		options.SetActive(false);
		gameover.SetActive(false);
		
		gameInput.SetActive(true);
		
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
		Loading.LoadScene(Loading.Scenes.GAME);
	}

	public void goToMenu()
	{
		Loading.LoadScene(Loading.Scenes.MENU);
	}

}
