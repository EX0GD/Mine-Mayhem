using UnityEngine;

public static class SoundManager
{
    /// Death, Win 1: +5, Win 2: +4, Win 3: +3, Boom Jump, General UI Button Click, Gem Pickup, TNT Explosion, Spike Death: -5, Boom Death: -6, 
    /// Main Menu Transition: QuickTransition4, 
    /// 

    public static AudioSource MusicSource { get; private set; }
    public static AudioSource SFXSource { get; private set; }

    public static int MasterMusicVolume { get; private set; }
    public static int MasterSoundVolume { get; private set; }

    public static AudioClip Win1 { get { return GetClip("SFX/Victory/Positive 5"); } } // Positive 5
    public static AudioClip Win2 { get { return GetClip("SFX/Victory/Positive 4"); } } // Positive 4
    public static AudioClip Win3 { get { return GetClip("SFX/Victory/Positive 3"); } } // Positive 3
    public static AudioClip LevelMusic1to9 { get { return GetClip("Music/Casual Game Music 11"); } } //CGM 11
    public static AudioClip LevelMusic10to19 { get { return GetClip("Music/Casual Game Music 08"); } } //CGM 8
    public static AudioClip LevelMusic20to29 { get { return GetClip("Music/Casual Game Music 12"); } } //CGM 12
    public static AudioClip LevelMusic30to39 { get { return GetClip("Music/Casual Game Music 06"); } } //CGM 6
    public static AudioClip LevelMusic40to49 { get { return GetClip("Music/Casual Game Music 03"); } } //CGM 3
    public static AudioClip LevelMusic50to60 { get { return GetClip("Music/Casual Game Music 01"); } } //CGM 1

    static SoundManager()
    {
        //MasterMusicVolume = 5;
        //MasterSoundVolume = 5;

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            MasterMusicVolume = PlayerPrefs.GetInt("MusicVolume");
            Debug.Log($"Master Music Volume: {MasterMusicVolume}. Player Music Preference: {PlayerPrefs.GetInt("MusicVolume")}.");
        }
        else
        {
            MasterMusicVolume = 5;
            PlayerPrefs.SetInt("MusicVolume", MasterMusicVolume);
            Debug.Log($"Music Volume Key not found - now setting. --- Player Music Preference: {PlayerPrefs.GetInt("MusicVolume")}. ");
        }

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            MasterSoundVolume = PlayerPrefs.GetInt("SoundVolume");
            Debug.Log($"Master Sound Volume: {MasterSoundVolume}. Player Sound Preference: {PlayerPrefs.GetInt("SoundVolume")}.");
        }
        else
        {
            MasterSoundVolume = 5;
            PlayerPrefs.SetInt("SoundVolume", MasterSoundVolume);
            Debug.Log($"Sound Volume Key not found - now setting. --- Player Sound Preference: {PlayerPrefs.GetInt("SoundVolume")}.");
        }
    }

    public static void SetMasterVolumes(int _music, int _sound)
    {
        if(MasterMusicVolume != _music)
        {
            MasterMusicVolume = _music;
        }

        if(MasterSoundVolume != _sound)
        {
            MasterSoundVolume = _sound;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            if(PlayerPrefs.GetInt("MusicVolume") != _music)
            {
                PlayerPrefs.SetInt("MusicVolume", _music);
            }
        }
        else
        {
            PlayerPrefs.SetInt("MusicVolume", _music);
        }

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            if(PlayerPrefs.GetInt("SoundVolume") != _sound)
            {
                PlayerPrefs.SetInt("SoundVolume", _sound);
            }
        }
        else
        {
            PlayerPrefs.SetInt("SoundVolume", _sound);
        }
    }

    public static void PlaySound(AudioClip clip)
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public static void PlaySound(AudioClip clip, AudioSource source)
    {
        Debug.Log($"Clip being played: {clip}.");
        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);

        }
    }

    public static void PlayMusic(AudioClip clip)
    {
        Debug.Log($"Music being played: {clip}.");
        //PlayOneShot(clip);
    }

    private static AudioClip GetClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }

    public static void SetAudioSources(AudioSource soundSource, AudioSource musicSource)
    {
        if(MusicSource != null)
        {
            if(MusicSource != musicSource)
            {
                MusicSource = musicSource;
            }
            else
            {
                Debug.Log($"MusicSource is already set and has value: {MusicSource}.");
            }
        }
        else
        {
            MusicSource = musicSource;
        }

        if(SFXSource != null)
        {
            if(SFXSource != soundSource)
            {
                SFXSource = soundSource;
            }
            else
            {
                Debug.Log($"SFXSource is already set and has value: {SFXSource}.");
            }
        }
        else
        {
            SFXSource = soundSource;
        }
    }
}
