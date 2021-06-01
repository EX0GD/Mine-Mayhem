using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    /// Death, Win 1: +5, Win 2: +4, Win 3: +3, Boom Jump, General UI Button Click, Gem Pickup, TNT Explosion, Spike Death: -5, Boom Death: -6, 
    /// Main Menu Transition: QuickTransition4, 
    /// 

    /// Main Menu Music: ???

    public static AudioClip Win1; // Positive 5
    public static AudioClip Win2; // Positive 4
    public static AudioClip Win3; // Positive 3
    public static AudioClip BoomJump; // RoundHitFire
    public static AudioClip ButtonClick; // PrizeWheelSpin2Tick
    public static AudioClip GemPickup; // Coins (24)
    public static AudioClip TNT_Explosion; // NukeMissile_CartoonyExplosion
    public static AudioClip SpikeDeath; // Negative 5
    public static AudioClip BoomDeath; // Negative 6
    public static AudioClip MainMenuTransition; // Quick Transition 4

    static SoundManager()
    {
        Win1 = GetClip("SFX/Victory/Positive 5");
        Win2 = GetClip("SFX/Victory/Positive 4");
        Win3 = GetClip("SFX/Victory/Positive 3");
    }

    public static void PlaySound(AudioClip clip)
    {
        Debug.Log($"Clip being played: {clip}.");
    }

    private static AudioClip GetClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
