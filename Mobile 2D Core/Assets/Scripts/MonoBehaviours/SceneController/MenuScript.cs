using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.PlayerSkills;

public class MenuScript : MonoBehaviour
{
    [Header("PlayerPrefs Niveles")]
    public int TotalNiveles = 5;
    public int UltimoNivel = 0;
    public int TutorialCompleto = 0;
    [Tooltip("Numero de monedas del player")]
    public int Coins = 0;

    [Header("Referencia a la clases")]
    public LevelLoader LevelLoader_;

    [Header("Sprite de btn activos y desactivados")]
    public Sprite SpriteUnlockedLevel;
    public Sprite SpritelockedLevel;

    [Header("MenuScript Opciones")]
    [Tooltip("Esta opciones es para usar en la escena historia, para llamar a la funcion que activa y desactiva los niveles")]
    public bool _Activarbotones;

    void DefinicionDeNivelesPorSkill()
    {
        SkillLevels.Skills[0] = new Skill();
        SkillLevels.Skills[0].Name = "Focus";
        SkillLevels.Skills[0].Description = "Hace que todo vaya en camara lenta";
        SkillLevels.Skills[0].Levels = new Level[5];
        #region Focus
        //LEVEL 1 FOCUS
        SkillLevels.Skills[0].Levels[0] = new Level();
        SkillLevels.Skills[0].Levels[0].Cooldown = 3.5f;
        SkillLevels.Skills[0].Levels[0].Duration = 1.5f;
        SkillLevels.Skills[0].Levels[0].Uses = 0;

        //LEVEL 2 FOCUS
        SkillLevels.Skills[0].Levels[1] = new Level();
        SkillLevels.Skills[0].Levels[1].Cooldown = 3.5f;
        SkillLevels.Skills[0].Levels[1].Duration = 2.0f;
        SkillLevels.Skills[0].Levels[1].Uses = 0;

        //LEVEL 3 FOCUS
        SkillLevels.Skills[0].Levels[2] = new Level();
        SkillLevels.Skills[0].Levels[2].Cooldown = 3.0f;
        SkillLevels.Skills[0].Levels[2].Duration = 2.0f;
        SkillLevels.Skills[0].Levels[2].Uses = 0;

        //LEVEL 4 FOCUS
        SkillLevels.Skills[0].Levels[3] = new Level();
        SkillLevels.Skills[0].Levels[3].Cooldown = 3.0f;
        SkillLevels.Skills[0].Levels[3].Duration = 2.5f;
        SkillLevels.Skills[0].Levels[3].Uses = 0;

        //LEVEL 5 FOCUS
        SkillLevels.Skills[0].Levels[4] = new Level();
        SkillLevels.Skills[0].Levels[4].Cooldown = 2.5f;
        SkillLevels.Skills[0].Levels[4].Duration = 2.5f;
        SkillLevels.Skills[0].Levels[4].Uses = 0;
        #endregion

        SkillLevels.Skills[1] = new Skill();
        SkillLevels.Skills[1].Name = "BubbleGum";
        SkillLevels.Skills[1].Description = "Chicle que detiene a las hormigas";
        SkillLevels.Skills[1].Levels = new Level[5];
        #region BubbleGum
        //LEVEL 1 BubbleGum
        SkillLevels.Skills[1].Levels[0] = new Level();
        SkillLevels.Skills[1].Levels[0].Cooldown = 3.5f;
        SkillLevels.Skills[1].Levels[0].Duration = 3f;
        SkillLevels.Skills[1].Levels[0].Uses = 0;

        //LEVEL 2 BubbleGum
        SkillLevels.Skills[1].Levels[1] = new Level();
        SkillLevels.Skills[1].Levels[1].Cooldown = 3f;
        SkillLevels.Skills[1].Levels[1].Duration = 3f;
        SkillLevels.Skills[1].Levels[1].Uses = 0;

        //LEVEL 3 BubbleGum
        SkillLevels.Skills[1].Levels[2] = new Level();
        SkillLevels.Skills[1].Levels[2].Cooldown = 3f;
        SkillLevels.Skills[1].Levels[2].Duration = 4f;
        SkillLevels.Skills[1].Levels[2].Uses = 0;

        //LEVEL 4 BubbleGum
        SkillLevels.Skills[1].Levels[3] = new Level();
        SkillLevels.Skills[1].Levels[3].Cooldown = 2.5f;
        SkillLevels.Skills[1].Levels[3].Duration = 4f;
        SkillLevels.Skills[1].Levels[3].Uses = 0;

        //LEVEL 5 BubbleGum
        SkillLevels.Skills[1].Levels[4] = new Level();
        SkillLevels.Skills[1].Levels[4].Cooldown = 2.5f;
        SkillLevels.Skills[1].Levels[4].Duration = 5f;
        SkillLevels.Skills[1].Levels[4].Uses = 0;
        #endregion

        SkillLevels.Skills[2] = new Skill();
        SkillLevels.Skills[2].Name = "Thimble";
        SkillLevels.Skills[2].Description = "Dedal para destruir mas rapido a las hormigas";
        SkillLevels.Skills[2].Levels = new Level[5];
        #region Thimble
        //LEVEL 1 Thimble
        SkillLevels.Skills[2].Levels[0] = new Level();
        SkillLevels.Skills[2].Levels[0].Cooldown = 3.5f;
        SkillLevels.Skills[2].Levels[0].Duration = 2f;
        SkillLevels.Skills[2].Levels[0].Uses = 3;

        //LEVEL 2 Thimble
        SkillLevels.Skills[2].Levels[1] = new Level();
        SkillLevels.Skills[2].Levels[1].Cooldown = 3.5f;
        SkillLevels.Skills[2].Levels[1].Duration = 3f;
        SkillLevels.Skills[2].Levels[1].Uses = 3;

        //LEVEL 3 Thimble
        SkillLevels.Skills[2].Levels[2] = new Level();
        SkillLevels.Skills[2].Levels[2].Cooldown = 3f;
        SkillLevels.Skills[2].Levels[2].Duration = 4f;
        SkillLevels.Skills[2].Levels[2].Uses = 4;

        //LEVEL 4 Thimble
        SkillLevels.Skills[2].Levels[3] = new Level();
        SkillLevels.Skills[2].Levels[3].Cooldown = 2.5f;
        SkillLevels.Skills[2].Levels[3].Duration = 4f;
        SkillLevels.Skills[2].Levels[3].Uses = 4;

        //LEVEL 5 Thimble
        SkillLevels.Skills[2].Levels[4] = new Level();
        SkillLevels.Skills[2].Levels[4].Cooldown = 0f;
        SkillLevels.Skills[2].Levels[4].Duration = 4f;
        SkillLevels.Skills[2].Levels[4].Uses = 6;
        #endregion

        SkillLevels.Skills[3] = new Skill();
        SkillLevels.Skills[3].Name = "Toad";
        SkillLevels.Skills[3].Description = "Rana que se come a las hormigas";
        SkillLevels.Skills[3].Levels = new Level[5];
        #region Toad
        //LEVEL 1 Toad
        SkillLevels.Skills[3].Levels[0] = new Level();
        SkillLevels.Skills[3].Levels[0].Cooldown = 15f;
        SkillLevels.Skills[3].Levels[0].Duration = 3f;
        SkillLevels.Skills[3].Levels[0].Uses = 0;

        //LEVEL 2 Toad
        SkillLevels.Skills[3].Levels[1] = new Level();
        SkillLevels.Skills[3].Levels[1].Cooldown = 15f;
        SkillLevels.Skills[3].Levels[1].Duration = 4f;
        SkillLevels.Skills[3].Levels[1].Uses = 0;

        //LEVEL 3 Toad
        SkillLevels.Skills[3].Levels[2] = new Level();
        SkillLevels.Skills[3].Levels[2].Cooldown = 15f;
        SkillLevels.Skills[3].Levels[2].Duration = 5f;
        SkillLevels.Skills[3].Levels[2].Uses = 0;

        //LEVEL 4 Toad
        SkillLevels.Skills[3].Levels[3] = new Level();
        SkillLevels.Skills[3].Levels[3].Cooldown = 10f;
        SkillLevels.Skills[3].Levels[3].Duration = 5f;
        SkillLevels.Skills[3].Levels[3].Uses = 0;

        //LEVEL 5 Toad
        SkillLevels.Skills[3].Levels[4] = new Level();
        SkillLevels.Skills[3].Levels[4].Cooldown = 10f;
        SkillLevels.Skills[3].Levels[4].Duration = 6f;
        SkillLevels.Skills[3].Levels[4].Uses = 0;
        #endregion
        Debug.Log("DefinicionDeNivelesPorSkill Cargado");
    }
   
    void Awake()
    {

        DefinicionDeNivelesPorSkill();

        DontDestroyOnLoad(GameObject.Find("Audio Source"));
        if (GameObject.FindObjectsOfType<AudioSource>().Length > 1)
        {
            bool encontre = false;
            var audios = GameObject.FindObjectsOfType<AudioSource>();
            for (var i = 0; i < GameObject.FindObjectsOfType<AudioSource>().Length; i++)
            {
                if (audios[i].name == "Audio Source" && encontre == false)
                {
                    encontre = true;
                }
                else if (audios[i].name == "Audio Source" && encontre == true)
                {
                    var parent = audios[i].gameObject;
                    //Destroy(audios[i]);
                    Destroy(parent);
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "EscenaMapa")
        {
            Destroy(GameObject.Find("Audio Source"));
        }

        PlayerPrefsConfig();

        //ActivarNiveles();
        if (_Activarbotones)
            Activar_botones();

        if (GameObject.Find("AquiSeGeneranLasHormigas") != null)
            Destroy(GameObject.Find("AquiSeGeneranLasHormigas"));
    }
    /// <summary>
    /// Aqui se configuran los valores de PlayerPrefs
    /// </summary>
    public void PlayerPrefsConfig() 
    {
        PlayerPrefs.SetInt("TotalNiveles", TotalNiveles);
        if (!PlayerPrefs.HasKey("UltimoNivel"))
        {
            PlayerPrefs.SetInt("Coins", 100);
            PlayerPrefs.SetInt("UltimoNivel", 1);
            PlayerPrefs.SetInt("TotalNiveles", 9);
            PlayerPrefs.SetInt("Focus", 1);
            PlayerPrefs.SetInt("BubbleGum", 0);
            PlayerPrefs.SetInt("Toad", 0);
            PlayerPrefs.SetInt("Thimble", 0);
            PlayerPrefs.SetInt("TutorialCompleto", 0);
            PlayerPrefs.SetInt("Perfect", 0);
            PlayerPrefs.SetInt("SkillLevel", 0);
            PlayerPrefs.SetFloat("FXVolume", 0.5f);
            PlayerPrefs.SetFloat("BGVolume", 0.5f);
        }
        else
        {
            TotalNiveles = PlayerPrefs.GetInt("TotalNiveles");
            UltimoNivel = PlayerPrefs.GetInt("UltimoNivel");
            TutorialCompleto = PlayerPrefs.GetInt("TutorialCompleto");
            Coins = PlayerPrefs.GetInt("Coins");
            GameManager.FXVolume = PlayerPrefs.GetFloat("FXVolume");
            GameManager.BGVolume = PlayerPrefs.GetFloat("BGVolume");
        }
        PlayerPrefs.Save();
    }

    void Start()
    {
        AjustarSonido();
    }

    void AjustarSonido()
    {
        var musica = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < musica.Length; i++)
        {
            musica[i].volume = GameManager.BGVolume;
        }
    }

    void ActivarNiveles()
    {
        var btnsNiveles = GameObject.FindGameObjectsWithTag("btnLeve");
        for (int i = 0; i < btnsNiveles.Length; i++)
        {
            Debug.Log(btnsNiveles[i].name);
            if (btnsNiveles[i].name.IndexOf("_") != -1)
            {
                if (int.Parse(btnsNiveles[i].name.Split('_')[1]) > UltimoNivel)
                {
                    btnsNiveles[i].GetComponent<Button>().enabled = false;
                }
            }
        }
    }

    void Activar_botones()
    {
        var btnsNiveles = GameObject.FindGameObjectsWithTag("btnLeve");
        for (int i = 0; i < btnsNiveles.Length; i++)
        {
            if (btnsNiveles[i].name.IndexOf("_") != -1)
            {
                if (int.Parse(btnsNiveles[i].name.Split('_')[3]) > UltimoNivel)
                {
                    btnsNiveles[i].GetComponentInChildren<Button>().enabled = false;
                    var obj_ = btnsNiveles[i].GetComponentInChildren<Button>().gameObject;
                    obj_.transform.FindChild("btnFondo").GetComponent<Image>().sprite = SpritelockedLevel;
                }
                else
                {
                    btnsNiveles[i].GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = SpriteUnlockedLevel;
                    var obj_ = btnsNiveles[i].GetComponentInChildren<Button>().gameObject;
                    obj_.transform.FindChild("btnFondo").GetComponent<Image>().sprite = SpriteUnlockedLevel;
                }
            }
        }
    }

    /// <summary>
    /// Funcion de navigacion entre escenas
    /// </summary>
    /// <param name="Nombre">Nombre de la esena a la que se va</param>
    public void Navegar(string Nombre)
    {
        if (Nombre == "MenuJuego" && TutorialCompleto == 0)
            StartCoroutine(LevelLoader_.LoadingSceneCoroutine("Nivel_00"));
        else
            StartCoroutine(LevelLoader_.LoadingSceneCoroutine(Nombre));
    }

    public void NavegarEscenaPorId(int EscenaId)
    {
        //Debug.Log("Ir a la escena" + Nombre);
        //Application.LoadLevel(Nombre);
        PlayerPrefs.SetInt("Escena", EscenaId);

    }
}

