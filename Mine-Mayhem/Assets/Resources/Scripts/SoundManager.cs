using UnityEngine;

public static class SoundManager
{
    /// Death, Win 1: +5, Win 2: +4, Win 3: +3, Boom Jump, General UI Button Click, Gem Pickup, TNT Explosion, Spike Death: -5, Boom Death: -6, 
    /// Main Menu Transition: QuickTransition4, 
    /// 

    private static AudioSource MusicSource { get; set; }
    private static AudioSource SFXSource { get; set; }

    public static AudioClip Win1 { get; private set; } // Positive 5
    public static AudioClip Win2 { get; private set; } // Positive 4
    public static AudioClip Win3 { get; private set; } // Positive 3
    public static AudioClip SpikeDeath { get; private set; } // Negative 5
    public static AudioClip BoomDeath { get; private set; } // Negative 6
    public static AudioClip BoomJump { get; private set; } // RoundHitFire
    public static AudioClip TNT_Explosion { get; private set; } // NukeMissile_CartoonyExplosion
    public static AudioClip GemPickup { get; private set; } // Coins (24)
    public static AudioClip ButtonClick { get; private set; } // PrizeWheelSpin2Tick
    public static AudioClip MainMenuTransition { get; private set; } // Quick Transition 4

    static SoundManager()
    {

        Win1 = GetClip("SFX/Victory/Positive 5");
        Win2 = GetClip("SFX/Victory/Positive 4");
        Win3 = GetClip("SFX/Victory/Positive 3");
        SpikeDeath = GetClip("SFX/Defeat/Negative 5");
        BoomDeath = GetClip("SFX/Defeat/Negative 6");
        BoomJump = GetClip("SFX/Booms/Jump/RoundHitFire");
        //TNT_Explosion = GetClip("SFX/Booms/TNT/NukeMissle_CartoonyExplosion");
        GemPickup = GetClip("SFX/Pickup/Coins (24)");
        ButtonClick = GetClip("SFX/UI/Prize Wheel Spin 2 Tick");
        MainMenuTransition = GetClip("SFX/UI/Quick Transition 4");
    }

    public static void PlaySound(AudioClip clip)
    {
        Debug.Log($"Clip being played: {clip}.");
        //SFXSource.PlayOneShot(clip);

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
