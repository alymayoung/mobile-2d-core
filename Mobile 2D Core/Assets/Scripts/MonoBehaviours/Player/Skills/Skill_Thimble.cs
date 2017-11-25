using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill_Thimble : Skills 
{
    public override void Efecto()
    {
        if (Activo)
        {
            //TODO: IMPLEMENTAR CUANTAS VECES SE PUEDE USAR
            GameManager.DanoPorTab = 2;
            StartCoroutine(focus(duration));
            Activo = false;
        }
    }

    private IEnumerator focus(float p)
    {
        float pauseEndTime = Time.realtimeSinceStartup + p;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {

            yield return 0;
        }
        GameManager.DanoPorTab = 1;
    }
	
}
