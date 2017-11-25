using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Historia_Script : MonoBehaviour 
{
    public string NombreEscena;
    public Sprite[] ArraySprite;
    public GameObject SpriteHistoria;
    public int indece = 0;
    public LevelLoader LevelLoader_;
    private int nivel_a_gargar;
    private void Awake()
    {
//        BotonHistoriaScript informacionDeHistoria = new BotonHistoriaScript();
//        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("JsonNivel"), informacionDeHistoria);
//        nivel_a_gargar = informacionDeHistoria.Nivel;
//        ArraySprite = new Sprite[informacionDeHistoria.Historieta.Length];
/*        if (ArraySprite.Length == 0)
        {
            if (informacionDeHistoria.CargarPaginaSkill == false)
            {
                if (nivel_a_gargar != 0)
                    StartCoroutine(LevelLoader_.LoadingSceneCoroutine(NombreEscena));
                else 
                {
                    StartCoroutine(LevelLoader_.LoadingSceneCoroutine("Nivel_0" + nivel_a_gargar.ToString()));
                }
            }
        }
        else
        {
            for (int i = 0; i < ArraySprite.Length; i++)
            {
//                Debug.Log(informacionDeHistoria.Historieta[i]);
//                ArraySprite[i] = (Resources.Load("Sprites/" + informacionDeHistoria.Historieta[i], typeof(Sprite)) as Sprite);
            }
        }*/
    }

    private void Start() 
    {
        SpriteHistoria.GetComponent<SpriteRenderer>().sprite = ArraySprite[indece];
        indece++;
    }

    public void Siguiente() 
    {
        Debug.Log("AQUI  " + ArraySprite.Length);
        if (indece < ArraySprite.Length)
        {
            SpriteHistoria.GetComponent<SpriteRenderer>().sprite = ArraySprite[indece];
            indece++;
        }
        else
        {
            if (nivel_a_gargar != 0)
                StartCoroutine(LevelLoader_.LoadingSceneCoroutine(NombreEscena));
            else
            {
                StartCoroutine(LevelLoader_.LoadingSceneCoroutine("Nivel_0" + nivel_a_gargar.ToString()));
            }
        }
    }

    public void SkilpHistorieta() 
    {
        if (nivel_a_gargar != 0)
            StartCoroutine(LevelLoader_.LoadingSceneCoroutine(NombreEscena));
        else
        {
            StartCoroutine(LevelLoader_.LoadingSceneCoroutine("Nivel_0" + nivel_a_gargar.ToString()));
        }
    }
}
