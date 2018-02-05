using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManagerScript : MonoBehaviour {

    public Text liveText;
    public int totalLives;
    string liveCharacter = "֍";
    // Use this for initialization
    void Start () {
        var liveString = "";
        var lives = new byte[totalLives].Select((l, i) => i);        
        lives.ToList().ForEach((number) => liveString += liveCharacter);
        liveText.text = liveString;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
