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
    public static AudioClip LevelMusic1to5 { get { return GetClip("Music/Casual Game Music 11"); } } //CGM 11
    public static AudioClip LevelMusic6to10 { get { return GetClip("Music/Casual Game Music 08"); } } //CGM 8
    public static AudioClip LevelMusic11to15 { get { return GetClip("Music/Casual Game Music 12"); } } //CGM 12
    public static AudioClip LevelMusic16to20 { get { return GetClip("Music/Casual Game Music 06"); } } //CGM 6
    public static AudioClip LevelMusic21to25 { get { return GetClip("Music/Casual Game Music 03"); } } //CGM 3
    public static AudioClip LevelMusic26to30 { get { return GetClip("Music/Casual Game Music 01"); } } //CGM 1

    //public static AudioClip SpikeDeath { get; private set; } // Negative 5
    //public static AudioClip BoomDeath { get; private set; } // Negative 6
    //public static AudioClip BoomJump { get; private set; } // RoundHitFire
    //public static AudioClip TNT_Explosion { get; private set; } // NukeMissile_CartoonyExplosion
    //public static AudioClip GemPickup { get; private set; } // Coins (24)
    //public static AudioClip ButtonClick { get; private set; } // PrizeWheelSpin2Tick
   //public static AudioClip MainMenuTransition { get; private set; } // Quick Transition 4

    static SoundManager()
    {
        MasterMusicVolume = 5;
        MasterSoundVolume = 5;
        //SpikeDeath = GetClip("SFX/Defeat/Negative 5");
        //BoomDeath = GetClip("SFX/Defeat/Negative 6");
        //BoomJump = GetClip("SFX/Booms/Jump/RoundHitFire");
        //TNT_Explosion = GetClip("SFX/Booms/TNT/NukeMissle_CartoonyExplosion");
        //GemPickup = GetClip("SFX/Pickup/Coins (24)");
        //ButtonClick = GetClip("SFX/UI/Prize Wheel Spin 2 Tick");
        //MainMenuTransition = GetClip("SFX/UI/Quick Transition 4");
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
