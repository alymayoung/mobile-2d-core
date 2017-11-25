using UnityEngine;
using System.Collections;

public class Skill_BubbleGum : Skills
{
    public GameObject bubbleGumObject;
    
    public override void Efecto()
    {
        if (Activo && Input.GetMouseButton(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 2f;
            Instantiate(bubbleGumObject, position, Quaternion.identity);
            Activo = false;
        }
    }	
    //TODO: IMPLEMENTAR DURATION
}
