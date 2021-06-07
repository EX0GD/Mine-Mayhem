using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class TempVolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] int _musicVolume  = 10;
    [SerializeField] int _soundVolume  = 10;
    [SerializeField] int _increment = 1;
    [SerializeField] float _multiplier = 30.0f;

    [SerializeField] TextMeshProUGUI _musicText;
    [SerializeField] TextMeshProUGUI _soundText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClampVolume();
        DisplayVolume();
    }

    public void IncreaseVolume(string _audioGroup)
    {
        // check whihc group to adjust

        //take volume float 0 - 1

        //Increase volume by x

        //Assign volume to audiomixer 
        if (_audioGroup == "Music")
        {
            if (_musicVolume < 10)
            {
                _musicVolume += _increment;

                // float value = 0.0f;
                float value = (_musicVolume / 10.0f);
                if (_soundVolume != 0)
                {
                    mixer.SetFloat("MusicVolume", Mathf.Log10(value) * _multiplier);
                }
                else
                {
                    mixer.SetFloat("MusicVolume", -80.0f);
                }
            }
        }
        else if (_audioGroup == "Sound")
        {
            if (_soundVolume < 10)
            {
                _soundVolume += _increment;

                //float value = 0.0f;
                float value = (_soundVolume / 10.0f);
                if (_soundVolume != 0)
                {
                    mixer.SetFloat("SfxVolume", Mathf.Log10(value) * _multiplier);
                }
                else
                {
                    mixer.SetFloat("SfxVolume", -80.0f);
                }
            }
        }
     
       
    }

    public void DecreaseVolume(string _audioGroup)
    {
        // Check which group to adjust

        //take volume float 0 - 1

        //Increase volume by x

        //Assign volume to audiomixer

        if (_audioGroup == "Music")
        {
            if (_musicVolume > 0)
            {
                _musicVolume -= _increment;

                //float value = 0.0f;
                float value = (_musicVolume / 10.0f);
                if (_musicVolume != 0)
                {
                    mixer.SetFloat("MusicVolume", Mathf.Log10(value) * _multiplier);
                }
                else
                {
                    mixer.SetFloat("MusicVolume", -80.0f);
                }
            }
        }
        else if (_audioGroup == "Sound")
        {
            if (_soundVolume > 0)
            {
                _soundVolume -= _increment;
                //float value = 0.0f;
                float value = (_soundVolume / 10.0f);
                if (_soundVolume != 0)
                {
                    mixer.SetFloat("SfxVolume", Mathf.Log10(value) * _multiplier);
                }
                else
                {
                    mixer.SetFloat("SfxVolume", -80.0f);
                }
            }
        }
    }
    
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
