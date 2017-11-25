using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.PlayerSkills;
public class Skills : MonoBehaviour
{
    // Update is called once per frame   
    public int id;
    public string Nombre;
    public string Descripcion;
    public Image coolDownImage;
    public bool Activo = false;
    public virtual void Efecto() { }
    public float coolDownImageTime { get; set; }
    public float duration { get; set; }
    public int uses;
    //public virtual float coolDownImagefillAmount { get; set; }
    public bool SeLlamo = false;

    void Awake()
    {
        Debug.Log("Nombre " + Nombre);
        for (var i = 0; i < SkillLevels.Skills.Length; i++)
        {
            if (Nombre == SkillLevels.Skills[i].Name)
            {
                int nivelActual = PlayerPrefs.GetInt(Nombre);
                Debug.Log("Nombre " + Nombre + " SkillLevels.Skills[" + i + "].Name" + SkillLevels.Skills[i].Description + ", Nivel actual " + nivelActual);
                if (nivelActual > 1)
                    nivelActual = nivelActual - 1;
                coolDownImageTime = SkillLevels.Skills[i].Levels[nivelActual].Cooldown;
                duration = SkillLevels.Skills[i].Levels[nivelActual].Duration;
                uses = SkillLevels.Skills[i].Levels[nivelActual].Uses;
                break;
            }
        }
    }

    void Update()
    {
        if (Activo)
        {
            Efecto();
            gameObject.GetComponent<Button>().enabled = false;
            if (SeLlamo == false)
            {
                StartCoroutine(cooldown_timer(coolDownImageTime));
                SeLlamo = true;
            }
        }
    }

    public void Usar()
    {
        Activo = true;
        coolDownImage.fillAmount = 1f;
        coolDownImage.gameObject.SetActive(true);
    }

    private IEnumerator cooldown_timer(float p)
    {
        Debug.Log(" IEnumerator cooldown_timer");
        float pauseEndTime = Time.realtimeSinceStartup + p;
        float coolDownImagefillAmount2 = 1f / p;
        float segundosTranscurridos = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            var f = (Time.realtimeSinceStartup - segundosTranscurridos) * coolDownImagefillAmount2;

            coolDownImage.fillAmount -= f;// coolDownImagefillAmount2;
            segundosTranscurridos = Time.realtimeSinceStartup;
            
            yield return 0;
        }
        coolDownImage.fillAmount = 0f;
        SeLlamo = false;
        coolDownImage.gameObject.transform.parent.GetComponent<Button>().enabled = true;
        coolDownImage.gameObject.SetActive(false);
    }
}