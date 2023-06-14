using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMusicInteractable
{
    void SetMusic(GameEvent.MusicType musicType);
    AudioClip GetRandomStylyzedClip();
    void HandleSongChange(AudioClip song);
}

public interface IAmbienceInteractable
{
    void SetAmbience(AudioClip clip);
}

public class AudioController : MonoBehaviour, IMusicInteractable, IAmbienceInteractable
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource ambiencePlayer;

    [SerializeField] private AudioClip[] defaultStyleSongs;
    [SerializeField] private AudioClip[] adventureStyleSongs;
    [SerializeField] private AudioClip[] dramaticStyleSongs;

    private GameEvent.MusicType _currentMusicType = GameEvent.MusicType.Default;
    private Dictionary<GameEvent.MusicType, AudioClip[]> _musicTypeToSongs;

    private void Awake()
    {
        _musicTypeToSongs = new Dictionary<GameEvent.MusicType, AudioClip[]>
        {
            { GameEvent.MusicType.Default, defaultStyleSongs },
            { GameEvent.MusicType.Adventure, adventureStyleSongs },
            { GameEvent.MusicType.Dramatic, dramaticStyleSongs },
        };

        StartCoroutine(SwitchTrack());
    }

    public void SetMusic(GameEvent.MusicType musicType)
    {
        if (_currentMusicType != musicType)
        {
            _currentMusicType = musicType;

            AudioClip clip = GetRandomStylyzedClip();
            HandleSongChange(clip);
        }
    }

    public AudioClip GetRandomStylyzedClip()
    {
        if (_musicTypeToSongs.ContainsKey(_currentMusicType))
        {
            AudioClip[] songs = _musicTypeToSongs[_currentMusicType];
            int songsAmount = songs.Length;
            int randomClipID = Random.Range(0, songsAmount);
            return songs[randomClipID];
        }

        return null;
    }

    public void HandleSongChange(AudioClip song)
    {
        StartCoroutine(SmoothClipChange(musicPlayer, song));
    }

    public void SetAmbience(AudioClip clip)
    {
        StartCoroutine(SmoothClipChange(ambiencePlayer, clip));
    }

    private IEnumerator SwitchTrack()
    {
        while (true)
        {
            if (!musicPlayer.isPlaying)
            {
                AudioClip newClip = GetRandomStylyzedClip();
                HandleSongChange(newClip);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SmoothClipChange(AudioSource source, AudioClip clip)
    {
        float initialVolume = source.volume;
        float elapsedTime = 0f;
        float smoothness = .5f;

        while (elapsedTime < smoothness)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / smoothness);
            yield return null;
        }

        source.volume = 0f;
        source.clip = clip;

        elapsedTime = 0f;

        while (elapsedTime < smoothness)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, initialVolume, elapsedTime / smoothness);
            yield return null;
        }

        source.volume = initialVolume;

        if (source.clip != null)
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }

        yield break;
    }
}
