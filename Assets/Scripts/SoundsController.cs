using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;
    private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private AudioSource audioSource, levelMusic;
    private bool soundsEnabled;
    private float levelMusicVolume = 0.5f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LoadData();
        SoundsPreporation();
    }
    private void LoadData()
    {
        GameObject gameDataController = GameObject.Find("GameDataController");
        if (gameDataController is not null)
        {
            SaveData saveData = gameDataController.GetComponent<GameDataController>().LoadData();
            soundsEnabled = saveData._soundsEnable;
        }
    }
    public void ToogleSoundsEnabled(bool value)
    {
        soundsEnabled = value;
    }
    public void PlaySound(string name)
    {
        audioSource.volume = soundsEnabled ? 1 : 0;
        audioSource.clip = sounds[name];
        audioSource.Play();
    }  
    public void PlaySound(string name, float volume)
    {
        if (soundsEnabled)
        {
            audioSource.volume = volume;
            audioSource.clip = sounds[name];
            audioSource.Play();
            Debug.Log($"{audioSource.clip} played");
        }
    }
    public void MuteLevelMusic()
    {
        levelMusic.volume = 0;
    }
    private void SoundsPreporation()
    {
        if(_sounds is not null && _sounds.Length > 0)
        {
            if (_sounds[0] != null) sounds.Add("BearTrap", _sounds[0]);
            if (_sounds[1] != null) sounds.Add("Poison", _sounds[1]);
            if (_sounds[2] != null) sounds.Add("Spike", _sounds[2]);
            if (_sounds[3] != null) sounds.Add("Coin", _sounds[3]);
            if (_sounds[4] != null) sounds.Add("Potion", _sounds[4]);
            if (_sounds[5] != null) sounds.Add("Jump", _sounds[5]);
            if (_sounds[6] != null) sounds.Add("Hit", _sounds[6]);
            if (_sounds[7] != null) sounds.Add("Attack", _sounds[7]);
            if (_sounds[8] != null) sounds.Add("Lose", _sounds[8]);
            if (_sounds[9] != null) sounds.Add("Win", _sounds[9]);
            if (_sounds[10] != null) sounds.Add("Click", _sounds[10]);
            if (_sounds[11] != null) sounds.Add("GameOver", _sounds[11]);
        }
        levelMusic = transform.GetChild(0).GetComponentInChildren<AudioSource>();
        if (soundsEnabled) levelMusic.volume = levelMusicVolume;
        else MuteLevelMusic();
    }
}
