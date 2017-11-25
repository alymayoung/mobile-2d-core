using UnityEngine;
using System.Collections;

public class ScriptCredits : MonoBehaviour 
{
    public LevelLoader ScriptLevelLoader;
    public Camera _Camera;
    public float speed = 0.5f;

	void Update () 
    {
        if (_Camera.transform.position.y < -200f)
            _Camera.transform.position = new Vector3(0f,0f,0f);

        _Camera.transform.Translate(Vector3.down * Time.deltaTime * speed);
	}

    public void Navegar() 
    {
        StartCoroutine(ScriptLevelLoader.LoadingSceneCoroutine("MenuInicial"));
    }
}
