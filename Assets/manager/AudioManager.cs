using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public enum AudioChannel { Master, Sfx, Music }

    public float masterVolumen { get; private set; }
    public float sfxVolumen { get; private set; }
    public float musicVolumen { get; private set; }

    private AudioSource sfxSource;
    private AudioSource[] musicSource;
    
    private int activeMusicSource;

    public static AudioManager instance;

    private SoundLibrary soundsLibrary;
    private MusicLibrary musicLibrary;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            soundsLibrary = GetComponent<SoundLibrary>();
            musicLibrary = GetComponent<MusicLibrary>();

            musicSource = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("MusicSource" + (i + 1));
                musicSource[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }

            GameObject newSfxSource = new GameObject("2D sfx source");
            sfxSource = newSfxSource.AddComponent<AudioSource>();
            newSfxSource.transform.parent = transform;

            masterVolumen = PlayerPrefs.GetFloat(AudioChannel.Master.ToString(), 1f);
            sfxVolumen = PlayerPrefs.GetFloat(AudioChannel.Sfx.ToString(), 1);
            musicVolumen = PlayerPrefs.GetFloat(AudioChannel.Music.ToString(), 1f);

        }
    }

    public void SetVolumen(float value, AudioChannel channel){
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumen = value;
                break;
            case AudioChannel.Music:
                musicVolumen = value;
                break;
            case AudioChannel.Sfx:
                sfxVolumen = value;
                break;
        }
        
        for (int i = 0; i < musicSource.Length; i++)
        {
            musicSource[i].volume = musicVolumen * masterVolumen;
        }
        
        PlayerPrefs.SetFloat(AudioChannel.Master.ToString(), masterVolumen);
        PlayerPrefs.SetFloat(AudioChannel.Music.ToString(), musicVolumen);
        PlayerPrefs.SetFloat(AudioChannel.Sfx.ToString(), sfxVolumen);
        PlayerPrefs.Save();
        
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if(clip != null)
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumen * masterVolumen);
    }

    public void PlaySound(string name, Vector3 pos)
    {
        PlaySound(soundsLibrary.GetClipFromName(name), pos);
    }

    public void PlayMusic(MusicLibrary.Scene name, float fade = 1f)
    {
        PlayMusic(musicLibrary.GetMusic(name), fade);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f)
    {
        
        if (clip == null)
            return;
        
        activeMusicSource = 1 - activeMusicSource;
        musicSource[activeMusicSource].clip = clip;
        musicSource[activeMusicSource].Play();
		
        StartCoroutine(AnimateMusicCrossFade(fadeDuration));
		
    }

    IEnumerator AnimateMusicCrossFade(float duration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSource[activeMusicSource].volume = Mathf.Lerp(0, musicVolumen * masterVolumen, percent);
            musicSource[1-activeMusicSource].volume = Mathf.Lerp(musicVolumen * masterVolumen, 0, percent);
            yield return null;
        }
    }

    public void PlaySound2D(string name)
    {
        sfxSource.PlayOneShot(soundsLibrary.GetClipFromName(name), sfxVolumen * masterVolumen);
    }

}