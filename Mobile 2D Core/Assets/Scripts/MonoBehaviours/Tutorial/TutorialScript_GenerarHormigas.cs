using UnityEngine;
using System.Collections;

public class TutorialScript_GenerarHormigas : MonoBehaviour
{
    [Header("Ant")]
    public GameObject Ant;
    private GameObject Hormiga_;

    [Header("Canvas and Img")]
    public GameObject TapOnAntText;
    public GameObject TapOnAntTapGesture;
    public GameObject LevelClear;
    public Canvas CanvasPausa;
    public Canvas CanvasVictoria;
    public GameObject btnPausa;

    [Header("Scripts")]
    public LevelLoader LevelLoaderScript;

    void Start()
    {
        StartCoroutine(Indicaciones());
        Hormiga_ = Instantiate(Ant, new Vector3(0f, -4f, 0f), Quaternion.identity) as GameObject;
    }

    void Update()
    {

    }

    public void Pausa()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0f;
            CanvasPausa.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            CanvasPausa.gameObject.SetActive(false);
        }
    }

    IEnumerator Indicaciones()
    {
        btnPausa.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            TapOnAntText.SetActive(true);
            TapOnAntTapGesture.SetActive(true);
            yield return new WaitForSeconds(.5f);
            TapOnAntTapGesture.SetActive(false);
            yield return new WaitForSeconds(.5f);
            TapOnAntTapGesture.SetActive(true);
        }
        yield return new WaitForSeconds(0.75f);
        btnPausa.SetActive(true);
        TapOnAntText.SetActive(false);
        TapOnAntTapGesture.SetActive(false);
        Hormiga_.transform.FollowPath("HormigaTutorial", 1.3f, Mr1.FollowType.Loop);
    }

    public IEnumerator NivelCompletado()
    {
        LevelClear.SetActive(true);
        int TutorialCompleto = PlayerPrefs.GetInt("TutorialCompleto");
        if (TutorialCompleto == 0)
        {
            PlayerPrefs.SetInt("TutorialCompleto", 1);
            int totalcoins = PlayerPrefs.GetInt("Coins");
            totalcoins = totalcoins + 25;
            PlayerPrefs.SetInt("Coins", totalcoins);
            PlayerPrefs.Save();
        }
        yield return new WaitForSeconds(1f);
        CanvasVictoria.gameObject.SetActive(true);
    }

    

    public void Navegar(string nombreEscena)
    {
        StartCoroutine(LevelLoaderScript.LoadingSceneCoroutine(nombreEscena));
    }
}
