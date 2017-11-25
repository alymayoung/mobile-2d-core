using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptConfiguracionDeSonido : MonoBehaviour
{
    public Slider SliderFXVolumen;
    public Slider SliderBGVolumen;

	void Start () 
    {
       SliderFXVolumen.value = PlayerPrefs.GetFloat("FXVolume");
       SliderBGVolumen.value = PlayerPrefs.GetFloat("BGVolume");
	}
	
    public void GuardarConfiguracion() 
    {
        PlayerPrefs.SetFloat("FXVolume", SliderFXVolumen.value);
        PlayerPrefs.SetFloat("BGVolume", SliderBGVolumen.value);
        PlayerPrefs.Save();
    }

    void Update()
    {
        var Sonidos = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < Sonidos.Length; i++)
        {
            Sonidos[i].volume = SliderBGVolumen.value;
        }
    }
}
