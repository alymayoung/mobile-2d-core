using UnityEngine;
using System.Collections;

public class BGMusic : MonoBehaviour 
{
    void Awake() 
    {
        if (GameObject.Find("Audio Source") != null)
            Destroy(GameObject.Find("Audio Source"));
    }
	// Update is called once per frame
	void Update () 
    {        
        //AudioListener.volume = GameManager.BGVolume;
        gameObject.GetComponent<AudioSource>().volume = GameManager.BGVolume;
	}
}
