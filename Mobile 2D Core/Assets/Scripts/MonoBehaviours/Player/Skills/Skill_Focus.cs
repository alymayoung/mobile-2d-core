using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill_Focus :Skills 
{
    public override void Efecto()
    {
        if (Activo)
        {                       
            GameManager.GlobalSpeed = 0.5f;
            Debug.Log("Focus duration " + duration);
            StartCoroutine(focus(duration));
            Activo = false;
        }       
    }

    private IEnumerator focus(float p)
    {
        Debug.Log("Focus " + p.ToString());
        float pauseEndTime = Time.realtimeSinceStartup + p;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
           
            yield return 0;
        }
        GameManager.GlobalSpeed = 1f;
    }
}
