using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill_Toad : Skills 
{
    public GameObject ObjToad;
    //(5.81f,-1.23f,0f)
    public override void Efecto()
    {
        if (Activo)
        {
            ObjToad.SetActive(true);
            Activo = false;
            Instantiate(ObjToad);
            ObjToad.GetComponent<RanaScript>().SoltarRana();
        }
    }

}
