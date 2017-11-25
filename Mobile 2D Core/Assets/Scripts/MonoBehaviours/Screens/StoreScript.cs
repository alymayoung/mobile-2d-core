using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.PlayerSkills;

public class Script_Tienda : MonoBehaviour
{
    [Header("Skill Imagenes")]
    public Image ImgSkillBubbleGum;
    public Image ImgSkillThimble;
    public Image ImgSkillToad;
    public GameObject PanelBtns;

    [Header("Panel Info")]
    public GameObject PanelInfo;
    public Text txtInfoItem;
    public Text txtInfoDescripcion;
    public Image ImgInfoItem;

    [Header("Panel Upgrade")]
    public GameObject PanelUpgrade;
    public GameObject BtnUpgrade;
    public Text txtInfoItemUpgrade;
    public Image PanelUpgradeImgSkill;

    // Use this for initialization
    public Text txtPanelInfo;
    public Text txtPanelInfoSkillLevel;
    public Text txtPanelUpgrade;
    public Text txtDinero;
    public GameObject parent;
    public int CostoIten;

    public void Awake()
    {
        txtDinero.text = PlayerPrefs.GetInt("Coins").ToString();

        if (PlayerPrefs.GetInt("BubbleGum") > 0)
            ImgSkillBubbleGum.sprite = Resources.Load<Sprite>("Sprites/BubbleGum");

        if (PlayerPrefs.GetInt("Thimble") > 0)
            ImgSkillThimble.sprite = Resources.Load<Sprite>("Sprites/Thimble");

        if (PlayerPrefs.GetInt("Toad") > 0)
            ImgSkillToad.sprite = Resources.Load<Sprite>("Sprites/Toad");

    }

    public void Btn_Mostrar_Opciones(GameObject btn)
    {
        float x_ = btn.transform.position.x;
        float y_ = btn.transform.position.y;
        float h = btn.GetComponent<RectTransform>().rect.height;

        parent = btn;
        PanelBtns.SetActive(true);
        PanelBtns.transform.position = new Vector3(x_, (y_ - h / 2) - 10, 0f);
        string nameSkill = parent.name.Split('_')[1];
        int nivel = PlayerPrefs.GetInt(nameSkill);

        if (parent.GetComponent<PriceSkillScript>().PriceSKill.Length > nivel)
        {
            if (nivel > 0)
            {
                BtnUpgrade.SetActive(true);
            }
            else
            {
                BtnUpgrade.SetActive(false);
            }
        }
        else
        {
            BtnUpgrade.SetActive(false);
        }
    }

    public void BtnInfo_MostrarPanel()
    {
        //parent con para tengo todas las propiedades
        PanelInfo.gameObject.SetActive(true);
        string nameSkill = parent.name.Split('_')[1];
        txtPanelInfo.text = nameSkill;
        var _skill = SkillLevels.Skills.Where(x => x.Name == nameSkill).FirstOrDefault();
        txtInfoItem.text = _skill.Description;
        txtInfoDescripcion.text = "";
        if (_skill != null)
        {
            for (int i = 0; i < _skill.Levels.Length; i++)
            {
                txtInfoDescripcion.text += "Level " + (i + 1) + ": Cooldown " + _skill.Levels[i].Cooldown.ToString() + "s, Duraction " + _skill.Levels[i].Duration.ToString() + "s" + ((_skill.Levels[i].Uses > 0) ? ", Uses " + _skill.Levels[i].Uses.ToString() : "") + "\n";
                //Debug.Log(txtInfoDescripcion.text);
            }
            ImgInfoItem.sprite = parent.GetComponent<Image>().sprite;
        }
    }

    public void BtnUpgrade_MostrarPanel()
    {
        //parent con para tengo todas las propiedades
        PanelUpgrade.gameObject.SetActive(true);
        string nameSkill = parent.name.Split('_')[1];// obtengo el nombre del skill
        txtPanelUpgrade.text = "Upgrade " + nameSkill;

        int skill_level = PlayerPrefs.GetInt(nameSkill);//obtengo el nivel del skill
        if (parent.GetComponent<PriceSkillScript>().PriceSKill.Length > skill_level)
        {
            CostoIten = parent.GetComponent<PriceSkillScript>().PriceSKill[(skill_level - 1)];
            GameObject.Find("txtPriceUpgrade").GetComponent<Text>().text = "$" + CostoIten.ToString();
        }

        var _skill = SkillLevels.Skills.Where(x => x.Name == nameSkill).FirstOrDefault();
        txtInfoItemUpgrade.text = _skill.Description;
        txtPanelInfoSkillLevel.text = "";
        txtPanelInfoSkillLevel.text += "Cooldown " + _skill.Levels[skill_level].Cooldown.ToString() + "s -> " + _skill.Levels[skill_level + 1].Cooldown.ToString() + "s\n";
        txtPanelInfoSkillLevel.text += "Duration " + _skill.Levels[skill_level].Duration.ToString() + "s -> " + _skill.Levels[skill_level + 1].Duration.ToString() + "s\n";

        if (_skill.Levels[skill_level].Uses > 0)
            txtPanelInfoSkillLevel.text += "Uses " + _skill.Levels[skill_level].Uses.ToString() + " -> " + _skill.Levels[skill_level + 1].Duration.ToString();

        PanelUpgradeImgSkill.sprite = parent.GetComponent<Image>().sprite;
    }

    public void CerrarPanel()
    {
        if (PanelInfo.activeInHierarchy)
            PanelInfo.gameObject.SetActive(false);
        else if (PanelUpgrade.activeInHierarchy)
            PanelUpgrade.gameObject.SetActive(false);
    }

    public void Comprar()
    {
        if (PlayerPrefs.GetInt("Coins") - CostoIten >= 0)
        {
            string nameSkill = parent.name.Split('_')[1];

            var restante = PlayerPrefs.GetInt("Coins") - CostoIten;
            PlayerPrefs.SetInt("Coins", restante);
            txtDinero.text = restante.ToString();

            int nivel = PlayerPrefs.GetInt(nameSkill);
            nivel++;
            PlayerPrefs.SetInt(nameSkill, nivel);
            txtPanelInfoSkillLevel.text = "Nivel actual: " + PlayerPrefs.GetInt(nameSkill).ToString();
            //si el numero de precios es menor al nivel continuo a mostrar el siguiente precio, de lo contrario oculto el btn
            if (parent.GetComponent<PriceSkillScript>().PriceSKill.Length > nivel)
            {
                GameObject.Find("txtPriceUpgrade").GetComponent<Text>().text = parent.GetComponent<PriceSkillScript>().PriceSKill[nivel].ToString();
            }
            else
            {
                GameObject.Find("txtPriceUpgrade").transform.parent.gameObject.SetActive(false);
            }
            Obtener_PuntosParaTrofeo(nivel);
            PlayerPrefs.Save();
            PanelUpgrade.SetActive(false);
        }
    }

    private void Obtener_PuntosParaTrofeo(int nivelActual)
    {
        //AQUI SE SUBEN LOS PUNTOS PARA DESBLOQUEAR EL TROFEO SKILLLEVEL
        int SkillLevel = PlayerPrefs.GetInt("SkillLevel");
        if (nivelActual == 3 && SkillLevel < 3) 
        {
            SkillLevel = 3;
        }
        else if (nivelActual == 4 && SkillLevel < 5)
        {
            SkillLevel = 5;
        }
        else 
        {
            bool max = true;
            //SkillLevel++;
            if (SkillLevel == 5)
            {
                for (int i = 0; i < SkillLevels.Skills.Length; i++)
                {
                    if (PlayerPrefs.GetInt(SkillLevels.Skills[i].Name) < 4)
                    {
                        max = false;
                    }
                }
                if (max)
                {
                    SkillLevel = SkillLevels.Skills.Length * 5;
                }
            }
        }
        PlayerPrefs.SetInt("SkillLevel", SkillLevel);
        PlayerPrefs.Save();
    }
}
