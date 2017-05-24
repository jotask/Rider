using System.Collections.Generic;
using UnityEngine;

public class MusicLibrary : MonoBehaviour
{

	public enum Scene { MENU, PLAY }

	public MusicGroup[] MusicGroups ;

	private readonly Dictionary<Scene, AudioClip[]> groupDictionary = new Dictionary<Scene, AudioClip[]>();

	void Awake()
	{
		foreach (MusicGroup group in MusicGroups)
		{
			groupDictionary.Add(group.groupID, group.group);	
		}
	}

	[System.Serializable]
	public class MusicGroup
	{
		public Scene groupID;
		public AudioClip[] group;
	}

	public AudioClip GetMusic(Scene name)
	{
		if (groupDictionary.ContainsKey(name))
		{
			AudioClip[] sounds = groupDictionary[name];
			return sounds[Random.Range(0, sounds.Length)];
		}
		return null;
	}

}
