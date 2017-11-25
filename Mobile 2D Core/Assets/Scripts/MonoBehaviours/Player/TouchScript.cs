using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class TouchScript : MonoBehaviour
{
    // Update is called once per frame
    [Header("Opciones en Tutorial")]
    public bool EnTutorial;
    [Tooltip("Esto solo se utiliza en el tutorial")]
    public TutorialScript_GenerarHormigas TutorialScript;
    public int HitConsecutivos = 0;

    [Header("Opciones normales")]
    [Tooltip("Por defecto es true, en modo infinito es false porque uno resive skill y no puntos")]
    public bool ObtenerComboPrize = true;

    [Header("Combo Prize")]
    public GameObject ObjComboTexto;
    public GameObject ObjComboPrize;

    [Header("ComboSkill")]
    public Image ImgComboSkill;
    public Sprite[] ImgSkills;

    [Header("Coins en Multiplayer")]
    public bool Multiplayer;
    public Text txtCoins;

    void Start()
    {
        HitConsecutivos = 0;
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(t.position), Vector2.zero);
                    ColicionRaycastHit2D(hit);
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                ColicionRaycastHit2D(hit);
            }
        }
    }

    void ColicionRaycastHit2D(RaycastHit2D hit)
    {
        if (hit != null && hit.collider != null)
        {
            if (hit.collider.name.IndexOf("Hormiga") != -1)
            {
                if (EnTutorial == false)
                {
                    if (Multiplayer)
                        CalcularPuntosxHormiga(hit.collider.name);

                    HitConsecutivos++;
                    if (HitConsecutivos > 1)
                    {
                        var objcombotexto_ = Instantiate(ObjComboTexto, hit.collider.gameObject.transform.position, Quaternion.identity) as GameObject;
//                        objcombotexto_.GetComponent<ScriptComboTexto>().ComboTexto.text = "Combo " + HitConsecutivos.ToString();
                    }

                    AntObject ant = hit.collider.gameObject.GetComponent<AntObject>();
                    ant.Hit();

                    if (!Multiplayer)//En multiplayer no se dan puntos por Perfect
                    {
                        if ((HitConsecutivos % 15) == 0)
                        {
                            if (ObtenerComboPrize)
                            {
                                Instantiate(ObjComboPrize);
                                int Perfect = PlayerPrefs.GetInt("Perfect");
                                Perfect++;
                                PlayerPrefs.SetInt("Perfect", Perfect);
                            }
                            else
                            {
                                StartCoroutine(DetenerImgComboSkill());
                            }
                        }
                    }
                }
                else
                {
                    StartCoroutine(TutorialScript.NivelCompletado());
                    Destroy(hit.collider.gameObject);
                }
            }
            else if (hit.collider.name == "Grass")
            {
                HitConsecutivos = 0;
            }
            else
            {

            }
        }
    }

    string skillAnterior = "";
    IEnumerator DetenerImgComboSkill()
    {
        if (skillAnterior != "")
            GameObject.Find(skillAnterior).transform.position = new Vector3(-100f, 0f, 0f);
        ImgComboSkill.gameObject.SetActive(true);

        for (int i = 0; i < 10; i++)
        {
            ImgComboSkill.sprite = ImgSkills[Random.Range(0, ImgSkills.Length)];
            yield return new WaitForSeconds(0.1f);
        }

        skillAnterior = "Skill_" + Random.Range(0, 3).ToString();
        var skillSelected = GameObject.Find(skillAnterior);
        if (skillSelected != null)
            skillSelected.transform.position = ImgComboSkill.transform.position;
        ImgComboSkill.gameObject.SetActive(false);
    }

    void CalcularPuntosxHormiga(string _nombreHormiga)
    {
        int Coins_ = PlayerPrefs.GetInt("CoinsMultiplayer");
        /*switch (_nombreHormiga)
        {
            case "Hormiga Soldado(Clone)":
                #region
                if (Unidades_Selecionadas.NivelHormigaSoldado == 1)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[0].CostoNivel1 * 4);
                else if (Unidades_Selecionadas.NivelHormigaSoldado == 2)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[0].CostoNivel2 * 4);
                else
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[0].CostoNivel3 * 4);
#endregion
                break;

            case "Hormiga Runner(Clone)":
                #region
                if (Unidades_Selecionadas.NivelHormigaSoldado == 1)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[1].CostoNivel1 * 4);
                else if (Unidades_Selecionadas.NivelHormigaSoldado == 2)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[1].CostoNivel2 * 4);
                else
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[1].CostoNivel3 * 4);
                #endregion
                break;

            case "Hormiga Canon(Clone)":
                #region
                if (Unidades_Selecionadas.NivelHormigaSoldado == 1)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[2].CostoNivel1 * 4);
                else if (Unidades_Selecionadas.NivelHormigaSoldado == 2)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[2].CostoNivel2 * 4);
                else
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[2].CostoNivel3 * 4);
#endregion
                break;

            case "Hormiga Tank(Clone)":
                #region
                if (Unidades_Selecionadas.NivelHormigaSoldado == 1)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[3].CostoNivel1 * 4);
                else if (Unidades_Selecionadas.NivelHormigaSoldado == 2)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[3].CostoNivel2 * 4);
                else
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[3].CostoNivel3 * 4);
#endregion
                break;

            case "Hormiga Princesa(Clone)":
                #region
                if (Unidades_Selecionadas.NivelHormigaSoldado == 1)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[4].CostoNivel1 * 4);
                else if (Unidades_Selecionadas.NivelHormigaSoldado == 2)
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[4].CostoNivel2 * 4);
                else
                    Coins_ = Coins_ + (ListaCostoHormiga.Hormigas[4].CostoNivel3 * 4);
#endregion
                break;
        }
        PlayerPrefs.SetInt("CoinsMultiplayer", Coins_);
        txtCoins.text = Coins_.ToString();
        PlayerPrefs.Save();*/
    }
}
