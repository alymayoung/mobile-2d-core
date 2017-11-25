using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptObjTrofeo : MonoBehaviour 
{
    public Text txtTitulo;
    public Text txtDescripcion;
    public string NombrePlayerPrefs;
    public int Puntaje1 = 0;
    public int Puntaje2 = 0;
    public int Puntaje3 = 0;
    public Image ImgTrofeo1;
    public Image ImgTrofeo2;
    public Image ImgTrofeo3;

    void Awake() 
    {

        var valor = PlayerPrefs.GetInt(NombrePlayerPrefs);
        if (NombrePlayerPrefs == "SkillLevel")
            Debug.Log("TOTAL Puntos: >>> " + valor);
        if (valor >= Puntaje1) 
        { 
            
        }
        else 
        {
            ImgTrofeo1.color = Color.HSVToRGB(0f, 0f, 0f);
        }

        if (valor >= Puntaje2)
        {
            
        }
        else
            ImgTrofeo2.color = Color.HSVToRGB(0f, 0f, 0f);

        if (valor >= Puntaje3)
        {

        }
        else
            ImgTrofeo3.color = Color.HSVToRGB(0f, 0f, 0f);
    }
	
}
