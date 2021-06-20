﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class TempVolumeControl : MonoBehaviour
{
    //The target Audio Mixer
    [SerializeField] AudioMixer mixer = null;
    //Initialize volume
    [SerializeField] int _musicVolume ;
    public int MusicVolume { get { return _musicVolume; } }

    [SerializeField] int _soundVolume ;
    public int SoundVolume { get { return _soundVolume; } }
    //for incrementing the volume up and down
    [SerializeField] int _increment = 1;

    //To get the Volume to scale properly interact with the audio mixer
    [SerializeField] float _multiplier = 30.0f;

    //UI Text object to display volume
    [SerializeField] TextMeshProUGUI _musicText = null;
    [SerializeField] TextMeshProUGUI _soundText = null;

    private void Start()
    {
        SetVolumes();
    }
    // Update is called once per frame
    void Update()
    {
        ClampVolume();
        //DisplayVolume();
        
    }

    public void SetMixerDisplayVolumes(int _music, int _sound)
    {
        if (_musicVolume != _music)
        {
            _musicVolume = _music;
        }

        if (_soundVolume != _music)
        {
            _soundVolume = _sound;
        }

        DisplayVolume();
    }

    public void SetVolumes()
    {
     //SET MUSIC VOLUME
        //turn int into a decimal
        float musicvalue = (_musicVolume / 10.0f);
        float newMusicValue;

        //This just allows for audio to be muted while volume is set to 0
        if (_musicVolume != 0)
        {
            newMusicValue = Mathf.Log10(musicvalue) * _multiplier;
            mixer.SetFloat("MusicVolume", newMusicValue);
        }
        else
        {
            newMusicValue = -80.0f;
            mixer.SetFloat("MusicVolume", newMusicValue);
        }
     //SET SFX VOLUME
        //turn int into a decimal
        float sfxvalue = (_soundVolume / 10.0f);
        float newSfxValue;

        //This just allows for audio to be muted while volume is set to 0
        if (_soundVolume != 0)
        {
            newSfxValue = Mathf.Log10(sfxvalue) * _multiplier;
            mixer.SetFloat("SfxVolume", newSfxValue);
        }
        else
        {
            newSfxValue = -80.0f;
            mixer.SetFloat("SfxVolume", newSfxValue);
        }

        SoundManager.SetMasterVolumes(_musicVolume, _soundVolume);
    }

    //function for Increment the volume
    public void IncreaseVolume(string _audioGroup)
    {
        //Check if the target Audio group is the Music group
        if (_audioGroup == "Music")
        {
            //this if is for a bug fix with button allowing a click while at 10
            if (_musicVolume < 10)
            {
                //Increase music volume integer by increment
                _musicVolume += _increment;

                //turn int into a decimal
                float value = (_musicVolume / 10.0f);
                float newValue;

                //This just allows for audio to be muted while volume is set to 0
                if (_musicVolume != 0)
                {
                    newValue = Mathf.Log10(value) * _multiplier;
                    mixer.SetFloat("MusicVolume", newValue);
                }
                else
                {
                    newValue = -80.0f;
                    mixer.SetFloat("MusicVolume", newValue);
                }
            }
            
      
        }
        //Check if the target  Audio group is the Sound group
        else if (_audioGroup == "Sound")
        {
            //this if is for a bug fix with button allowing a click while at 10
            if (_soundVolume < 10)
            {
                //Increase music volume integer by increment
                _soundVolume += _increment;

                //turn int into a decimal
                float value = (_soundVolume / 10.0f);
                float newValue;

                //This just allows for audio to be muted while volume is set to 0
                if (_soundVolume != 0)
                {
                    newValue = Mathf.Log10(value) * _multiplier;
                    mixer.SetFloat("SfxVolume", newValue);
                }
                else
                {
                    newValue = -80.0f;
                    mixer.SetFloat("SfxVolume", newValue);
                }
            }
           
           
        }
        SoundManager.SetMasterVolumes(_musicVolume, _soundVolume);
        DisplayVolume();
    }

    //function for Decrement the volume
    public void DecreaseVolume(string _audioGroup)
    {
        //Check if the target Audio group is the Music group
        if (_audioGroup == "Music")
        {
            //this if is for a bug fix with button allowing a click while at 0
            if (_musicVolume > 0)
            {
                //Decrease music volume integer by increment
                _musicVolume -= _increment;
                
                //turn int into a decimal
                float value = (_musicVolume / 10.0f);
                float newValue;

                //This just allows for audio to be muted while volume is set to 0
                if (_musicVolume != 0)
                {
                    newValue = Mathf.Log10(value) * _multiplier;
                    mixer.SetFloat("MusicVolume", newValue);
                }
                else
                {
                    newValue = -80.0f;
                    mixer.SetFloat("MusicVolume", newValue);
                }
            }
            
           
        }
        //Check if the target  Audio group is the Sound group
        else if (_audioGroup == "Sound")
        {
            //this if is for a bug fix with button allowing a click while at 0
            if (_soundVolume > 0)
            {
                //Decrease music volume integer by increment
                _soundVolume -= _increment;

                //turn int into a decimal
                float value = (_soundVolume / 10.0f);
                float newValue;

                //This just allows for audio to be muted while volume is set to 0
                if (_soundVolume != 0)
                {
                    newValue = Mathf.Log10(value) * _multiplier;
                    mixer.SetFloat("SfxVolume", newValue);
                }
                else
                {
                    newValue = -80.0f;
                    mixer.SetFloat("SfxVolume", newValue);
                }
            }
           
        }
        SoundManager.SetMasterVolumes(_musicVolume, _soundVolume);
        DisplayVolume();
    }
    
    //Function for Keeping Volume Int between 0 and 10
    void ClampVolume()
    {
        if (_musicVolume >= 10)
        {
            _musicVolume = 10;
        }
        else if (_musicVolume <= 0)
        {
            _musicVolume = 0;
        }

         
        if (_soundVolume >= 10)
        {
            _soundVolume = 10;
        }
        else if (_soundVolume <= 0)
        {
            _soundVolume = 0;
        }
    }

    //Function for Displaying the volume on menu UI
    void DisplayVolume()
    {
        if (_musicText != null)
        {
            _musicText.text = ($"Music: {_musicVolume}");
        }
        if (_soundText != null)
        {
            _soundText.text = ($"SFX: {_soundVolume}");
        }
    }
}
