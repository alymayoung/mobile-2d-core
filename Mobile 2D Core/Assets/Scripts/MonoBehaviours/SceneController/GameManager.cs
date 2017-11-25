using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Referencias a otros Script")]
    public LevelLoader LevelLoaderScript;

    [Header("Tiempo de la partida")]
    public float tiempoTranscurrido = 0f;
    public static int Puntos;

    [Header("Label")]
    public Text TextoPuntaje;
    public Text TexTPuntosEnMenu;
    public Text VidaTXT;
    public Text Estado;

    [Header("Panels & Spriters")]
    public GameObject CanvasMenuPausa;
    public Image ImageEstadoPartida;
    public Sprite SpriteLevelClear;
    public Sprite SpriteFailed;

    [Header("Opcines")]
    [Tooltip("Por defecto tiene que ir en false")]
    public bool isCamaraLenta = false;
    public String BgSoundName = "Rune Arena - Battle Song";
    public Slider SliderFXVolume;
    public static float FXVolume = 0.5f;
    public Slider SliderBGVolume;
    public static float BGVolume = 0.5f;
    public static int VIDA = 61;
    public static bool GameOver = false;
    public static bool PartidaReiniciada = false;

    [Tooltip("Nombre de la escena nueva a cargar")]
    public string SiguienteEscena;
    [Tooltip("Numero de nivel actual")]
    public string NoDeNivel;
    public static bool SiguienteNivel = false;
    public static float GlobalSpeed;
    [Tooltip("Nombre de la escena actual")]
    public Text txtNombreEscena;
    [Tooltip("Indica el daño que causa el jugador")]
    public static int DanoPorTab = 1;
    private int PuntosDePartida = 0;

    void Start()
    {
        GlobalSpeed = 1f;
        GameManager.FXVolume = PlayerPrefs.GetFloat("FXVolume");
        GameManager.BGVolume = PlayerPrefs.GetFloat("BGVolume");
        AjustarSliderDeSonido();
    }

    public void BGVolumeCtrl(float v)
    {
        BGVolume = v;
        var BGSounds = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < BGSounds.Length; i++)
        {
            if (BGSounds[i].name == BgSoundName)
            {
                BGSounds[i].volume = GameManager.BGVolume;
            }
        }
    }

    public void FXVolumeCtrl(float v)
    {
        FXVolume = v;
    }

    void AjustarSliderDeSonido()
    {
        SliderBGVolume.value = GameManager.BGVolume;
        SliderFXVolume.value = GameManager.FXVolume;
        var BGSounds = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < BGSounds.Length; i++)
        {
            if (BGSounds[i].name == BgSoundName)
            {
                BGSounds[i].volume = GameManager.BGVolume;
            }
            else
            {
                BGSounds[i].volume = GameManager.FXVolume;
            }
        }
    }

    public void GuardarConfiguracionAudio()
    {
        PlayerPrefs.SetFloat("FXVolume", GameManager.FXVolume);
        PlayerPrefs.SetFloat("BGVolume", GameManager.BGVolume);
        PlayerPrefs.Save();
    }

    void CalcularVida()
    {
        string txt = "";
        for (var i = 0; i < GameManager.VIDA; i++)
        {
            txt += "❤";
        }
        VidaTXT.text = txt;
    }

    // Update is called once per frame
    void Update()
    {
        CalcularVida();
        TextoPuntaje.text = GameManager.Puntos.ToString();
        PuntosDePartida = GameManager.Puntos;
        if (isCamaraLenta)
        {
            tiempoTranscurrido += Time.fixedDeltaTime;
        }
    }

    /// <summary>funcion estatico que suma los puntos obtenidos</summary>
    public static void Anotar()
    {
        GameManager.Puntos++;
    }

    ///<summary>Funcion para detener el juego</summary>
    public void Pausar()
    {
        Time.timeScale = 0.0f; // el tiempo de la esena se detiene igualando a 0
        CanvasMenuPausa.SetActive(true); // se llama al canvas de menu
        txtNombreEscena.text = SceneManager.GetActiveScene().name.Replace("_", " ");
        gameObject.GetComponent<TouchScript>().enabled = false;
        TexTPuntosEnMenu.text = "Puntos: " + GameManager.Puntos;
    }

    ///<summary>Funcion para reanudar el juego</summary>
    public void Resumir()
    {
        Time.timeScale = 1.0f; // el tiempo de la esena regresa a su forma normal
        if (SiguienteNivel && SiguienteEscena != String.Empty)
        {
            //SceneManager.LoadScene(SiguienteEscena, LoadSceneMode.Single);
            StartCoroutine(LevelLoaderScript.LoadingSceneCoroutine(SiguienteEscena));
        }
        else
        {
            CanvasMenuPausa.SetActive(false); // se oculta el canvas de menu
            gameObject.GetComponent<TouchScript>().enabled = true;
        }
    }

    ///<summary>Funcion para cerrar la partida</summary>
    public void CerrarPartida()
    {
        //PartidaReiniciada = true;
        //GameManager.Puntos = 0;
        //GameManager.VIDA = 5;
        //Limpiar_Hormigas();
        //Time.timeScale = 1.0f;
        //gameObject.GetComponent<TouchScript>().enabled = true;
        PartidaReiniciada = true;
        GameManager.VIDA = 5;
        GameManager.Puntos = 0;
        gameObject.GetComponent<TouchScript>().enabled = true;
        Limpiar_Hormigas();
        Time.timeScale = 1f;
        LevelLoaderScript.LoadingScene.SetActive(true);
        SceneManager.LoadScene(0);
        //StartCoroutine(LevelLoaderScript.LoadingSceneCoroutine("MenuInicial"));
    }

    ///<summary>Funcion para reiniciar la partida</summary>
    public void ReiniciarPartida()
    {
        Debug.Log("ReiniciarPartida");
        PartidaReiniciada = true;
        GameManager.VIDA = 5;
        GameManager.Puntos = 0;
        gameObject.GetComponent<TouchScript>().enabled = true;
        Limpiar_Hormigas();
        Time.timeScale = 1f;
        StartCoroutine(LevelLoaderScript.LoadingSceneCoroutine("SkillMenu"));
    }

    /// <summary>
    /// Fin de partida muestra los spriter de faild o level clear, 
    /// llama a una corutina para mostrar el canvas
    /// </summary>
    /// <param name="PartidaGanada"></param>
    public void FinPartida()
    {
        TexTPuntosEnMenu.text = "Puntos: " + PuntosDePartida.ToString();
        ImageEstadoPartida.gameObject.SetActive(true);
        if (GameManager.VIDA > 0)
        {
            ImageEstadoPartida.sprite = SpriteLevelClear;
        }
        else
        {
            ImageEstadoPartida.sprite = SpriteFailed;
        }
        StartCoroutine(MostrarCanvasFinPartida());
    }

    IEnumerator MostrarCanvasFinPartida()
    {
        yield return new WaitForSeconds(1f);
        CanvasMenuPausa.SetActive(true);
        Limpiar_Hormigas();
    }

    public void Limpiar_Hormigas()
    {
        Time.timeScale = 0f;
        Destroy(GameObject.Find("AquiSeGeneranLasHormigas"));
        var Hormigas_ = GameObject.FindGameObjectsWithTag("Hormiga");
        for (int i = 0; i < Hormigas_.Length; i++)
        {
            Destroy(Hormigas_[i]);
        }
    }

    public static void EnMultijugador() 
    {
        GameManager.GlobalSpeed = 1f;
    }
}
