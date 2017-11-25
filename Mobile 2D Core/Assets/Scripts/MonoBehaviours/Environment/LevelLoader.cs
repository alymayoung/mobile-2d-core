using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
	public GameObject LoadingScene;
    public IEnumerator LoadingSceneCoroutine(string NameLevel)
	{
		LoadingScene.SetActive (true);
        AsyncOperation async = SceneManager.LoadSceneAsync(NameLevel, LoadSceneMode.Single);

		while (!async.isDone) 
        {
			//LoadingBar.fillAmount = async.progress / 0.9f;
			yield return null;
		}
	}
}