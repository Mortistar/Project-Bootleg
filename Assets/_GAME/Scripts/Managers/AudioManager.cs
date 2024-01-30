using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;
using FMOD.Studio;
using FMODUnityResonance;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private EventReference songIntro;
    [SerializeField] private EventReference songMenu;
    [SerializeField] private EventReference songTutorial;
    [SerializeField] private EventReference songDungeon;
    [SerializeField] private EventReference songResults;
    [SerializeField] private EventReference songCredits;
    public enum Song
    {
        Intro,
        MainMenu,
        Tutorial,
        Dungeon,
        Results,
        Credits
    }
    public Dictionary<Song,EventReference> songs {get; private set;}
    public static AudioManager instance;

    public FMOD.Studio.EventInstance currentSong {get; private set;}
    public EventReference currentSongRef {get; private set;}

    void Awake()
    {
        PersistenceCheck();
    }
    private void PersistenceCheck()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            INIT();
        }else
        {
            Destroy(this.gameObject);
        }
    }
    private void INIT()
    {
        //Initialisation
        InitSongs();
    }
    private void InitSongs()
    {
        songs = new Dictionary<Song, EventReference>()
        {
            {Song.Intro, songIntro},
            {Song.MainMenu, songMenu},
            {Song.Tutorial, songTutorial},
            {Song.Dungeon, songDungeon},
            {Song.Results, songResults},
            {Song.Credits, songCredits}
        };
    }
    public void QueueSong(Song songToPlay)
    {
        PlaySong(songToPlay, FMOD.Studio.STOP_MODE.IMMEDIATE);
        currentSong.setPaused(true);
    }
    public void PlaySong(Song songToPlay, FMOD.Studio.STOP_MODE stopMode)
    {
        //Convert enum to music reference
        EventReference musicQueued = songs[songToPlay];
        //Music state checks
        currentSong.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);

        //If current song paused
        currentSong.getPaused(out bool isPaused);
        if (isPaused)
        {
            //If queued music is the same as current music, do nothing
            if (musicQueued.Guid == currentSongRef.Guid)
            {
                currentSong.setPaused(false);
                return;
            }
        }

        //If current music playing
        if (currentSong.isValid() && state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            //If queued music is the same as current music, do nothing
            if (musicQueued.Guid == currentSongRef.Guid)
            {
                return;
            }
            currentSong.stop(stopMode);
        }
        currentSong = RuntimeManager.CreateInstance(musicQueued);
        currentSong.start();
        currentSongRef = musicQueued;
    }
    public void StopSong(FMOD.Studio.STOP_MODE stopMode)
    {
        currentSong.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (currentSong.isValid() && (state == FMOD.Studio.PLAYBACK_STATE.PLAYING))
        {
            currentSong.stop(stopMode);
        }
    }

    public void SetMasterVolume(float value)
    {
        RuntimeManager.StudioSystem.setParameterByName("MASTERVOLUME", 1);
    }
    public float GetMasterVolume()
    {
        RuntimeManager.StudioSystem.getParameterByName("MASTERVOLUME", out float value);
        return value;
    }
    public void SetMusicVolume(float value)
    {
        RuntimeManager.StudioSystem.setParameterByName("MUSICVOLUME", value);
    }
    public float GetMusicVolume()
    {
        RuntimeManager.StudioSystem.getParameterByName("MASTERVOLUME", out float value);
        return value;
    }
    public void SetSFXVolume(float value)
    {
        RuntimeManager.StudioSystem.setParameterByName("SFXVOLUME", value);
    }
    public float GetSFXVolume()
    {
        RuntimeManager.StudioSystem.getParameterByName("SFXVOLUME", out float value);
        return value;
    }

    //TODO
    //Audio Effects : Reverb / LPF
}
