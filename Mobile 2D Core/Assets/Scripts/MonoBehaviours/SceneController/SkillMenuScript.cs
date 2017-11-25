using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class SkillMenuScript : MonoBehaviour
{
    public Text txtidskill;
    [Header("Unidades de Hormigas")]
    public Unidad[] Unidades;
    public List<GameObject> ListHormigas;
    //public NivelScript Nivel_;
    //public GeneradordeHormigasScript GeneradordeHormigas;
    public LevelLoader LevelLoader_;
    [Header("Nivel")]
    public int SiguienteNivel;
    [SerializeField]
    private string[] IA_Ants;
    void Awake()
    {
        //Nivel_ = new NivelScript();
        //Nivel_.Numero = 1;
        //Nivel_.Historieta = new string[1];
        //Nivel_.Historieta[0] = "ABC_";
        //Nivel_.Hormiga = new HormigaScript[1];
        //Nivel_.Hormiga[0] = new HormigaScript();
        //Nivel_.Hormiga[0].Cantidad = 1;
        //Nivel_.Hormiga[0].Nombre = "ABC";
/*

        #region Se generan las unidades de hormigas

        BotonHistoriaScript informacionDeHistoria = new BotonHistoriaScript();
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("JsonNivel"), informacionDeHistoria);
        SiguienteNivel = informacionDeHistoria.Nivel;
        Unidades = new Unidad[informacionDeHistoria.InfoHormigasNivel.Length];
        for (int i = 0; i < Unidades.Length; i++)
        {
            Unidades[i] = new Unidad
            {
                Cantidad = informacionDeHistoria.InfoHormigasNivel[i].CantidadDeHormigasAGenerar,
                Tipo = (Resources.Load("Prefabs/" + informacionDeHistoria.InfoHormigasNivel[i].NombrePrefabHormiga, typeof(GameObject)) as GameObject)
            };    
        }
        
        ListHormigas = new List<GameObject>();
        GameObject obj_ = null;
        int ia_actual = 0;
        for (int i = 0; i < Unidades.Length; i++)
        {
            for (int k = 0; k < Unidades[i].Cantidad; k++)
            {
                if (k == 0)
                {
                    var hormiga_temporarl = (GameObject)Instantiate(Unidades[i].Tipo, new Vector3(-2f, -3f, 0f), Quaternion.identity);
                    hormiga_temporarl.GetComponent<Animator>().enabled = true;
                    hormiga_temporarl.transform.FollowPath(IA_Ants[ia_actual], 1.2f, Mr1.FollowType.Loop).SmoothLookForward(true, 10f);
                    if (ia_actual < IA_Ants.Length)
                        ia_actual++;
                    else
                        ia_actual = 0;

                    InstacianHormiga(Unidades[i].Tipo);
                }
                else
                {
                    InstacianHormiga(Unidades[i].Tipo);
                }
            }
        }
        #endregion

        for (int i = 0; i < ListHormigas.Count; i++)
        {
            ListHormigas[i].SetActive(false);
        }
        //Debug.Log("FUERZA!!!");
        //Esto hace que se separen las hormigas
        //ListHormigas[1].transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3f, 3f) * 10f, ForceMode2D.Force);
        //evito destruir este gameobject para usarlo en la siguiente escena
        //BajarVelocidadDeLasHormigas();
        DontDestroyOnLoad(transform.gameObject);*/
    }

    void InstacianHormiga(GameObject Unidad) 
    {
        var hormiga_ = (GameObject)Instantiate(Unidad, new Vector3(0f, -10f, 0f), Quaternion.identity);
        hormiga_.GetComponent<AntObject>().Pausado = true;
        DontDestroyOnLoad(hormiga_);
        ListHormigas.Add(hormiga_);
    }

    void Start() 
    {
        #region Se busca y activa el skill seleccionado
        var skill1seleccionado = PlayerPrefs.GetInt("Skill1");
        if (skill1seleccionado > 0)
        {
            var btns = GameObject.FindGameObjectsWithTag("btnSkillInfo");
            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i].GetComponent<SkillInfo>().id == skill1seleccionado)
                {
                    btns[i].GetComponent<SkillInfo>().Actualizar();
                }
            }
        }
        #endregion
    }

    public void Quitar_CircleCollider2D_Hormiga_Stealth(string nombreHormiga)
    {
        for (int i = 0; i < ListHormigas.Count; i++)
        {
            if (ListHormigas[i].name == nombreHormiga) 
            {
                ListHormigas[i].GetComponent<CircleCollider2D>().enabled = false;
                ListHormigas[i].GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 55);                

            }
        }
    }

    public void GuardarySiguienteEscena()
    {
        PlayerPrefs.SetInt("Skill1", int.Parse(txtidskill.text));
        PlayerPrefs.Save();

        string nivel_ = "";
        if (SiguienteNivel != -1)
        {
            nivel_ = ((SiguienteNivel < 10) ? "Nivel_0" + SiguienteNivel.ToString() : "Nivel_" + SiguienteNivel.ToString());
        }
        else 
        {
            nivel_ = "Infinito";
        }
        //SceneManager.LoadScene(nivel_);
        Quitar_CircleCollider2D_Hormiga_Stealth("Hormiga Stealth(Clone)");
        StartCoroutine(LevelLoader_.LoadingSceneCoroutine(nivel_));
    }

    public void BajarVelocidadDeLasHormigas() 
    {
        var _Hormigas = GameObject.FindGameObjectsWithTag("Hormiga");

        for (int i = 0; i < _Hormigas.Length; i++)
        {
            _Hormigas[i].GetComponent<AntObject>().Speed = 0f;
        }
    }
}

