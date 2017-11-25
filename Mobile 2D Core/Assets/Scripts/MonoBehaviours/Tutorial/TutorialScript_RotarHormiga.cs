using UnityEngine;
using System.Collections;

public class TutorialScript_RotarHormiga : MonoBehaviour 
{
    public bool Giran180Grados;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Giran180Grados == true)
            other.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 180f);
        else
            other.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
}
