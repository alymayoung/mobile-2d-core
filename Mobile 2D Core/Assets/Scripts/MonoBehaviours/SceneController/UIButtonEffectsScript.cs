using UnityEngine;
using System.Collections;

public class UIButtonEffectsScript : MonoBehaviour 
{
    public AudioSource UISong;
	// Update is called once per frame
    public void OnMouseEnter()
    {
        UISong.Play();
        
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    public void OnMouseExit()
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnMouseClick()
    {
        UISong.Play();
    }
}
