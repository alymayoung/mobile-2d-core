using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButtonScript : MonoBehaviour {

    public void Navigate(string sceneName)
    {
        Debug.Log(sceneName);
        SceneController sceneController = GameObject.FindObjectOfType<SceneController>();
        SceneReaction sceneReaction = ScriptableObject.CreateInstance<SceneReaction>();
        sceneReaction.sceneName = sceneName;
        sceneController.FadeAndLoadScene(sceneReaction);
    }
}