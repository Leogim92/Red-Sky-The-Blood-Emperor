using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //Biblioteca necessária para usar funções de audio
using UnityEngine.UI;
//Tirado daqui https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
//Poderia ter colocado dentro de MenuManager mas achei melhor assim
//IMPORTANTE: Expor o valor para script no audio mixer. Selecionar o grupo e com o direito do mouse expor o valor para a alteração de volume
public class SetVolume : MonoBehaviour {

    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider fxSlider;

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        fxSlider.value = PlayerPrefs.GetFloat("FxVolume", 1f);
    }

    public void SetLevelMaster(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20); //Porque o valor do volume é dado em decibéis
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetLevelFx(float sliderValue)
    {
        mixer.SetFloat("FXVolume", Mathf.Log10(sliderValue) * 20); //Porque o valor do volume é dado em decibéis
        PlayerPrefs.SetFloat("FxVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetLevelMusic(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20); //Porque o valor do volume é dado em decibéis
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.Save();
    }

}
