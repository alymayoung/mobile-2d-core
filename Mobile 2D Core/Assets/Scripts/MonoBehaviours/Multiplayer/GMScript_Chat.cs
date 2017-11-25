using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GMScript_Chat : MonoBehaviour 
{
    public GameObject PanelRetar;
    public GameObject PanelAceptarReto;
    public static string Oponente;
    public void Navegar(string NombreEscena) 
    { 
        SceneManager.LoadScene(NombreEscena, LoadSceneMode.Single); 
    }

    public void CancelarReto(GameObject Panel)//El plater cancelando el reto
    {
        Panel.SetActive(false);
    }

    public void ContinuarConElReto() //de Player a Retador
    {
        GameObject.Find("Scripts").GetComponent<ChatGui>().inputLine = PlayerPrefs.GetString(NamePickGui.UserNamePlayerPref) + " [[reta]] a " + GMScript_Chat.Oponente;
        GameObject.Find("Scripts").GetComponent<ChatGui>().GuiSendsMsg();
        MultiplayerManagerScript.NombreCuarto = PlayerPrefs.GetString(NamePickGui.UserNamePlayerPref) + "vs" + GMScript_Chat.Oponente;        
        PanelRetar.SetActive(false);
        StartCoroutine("CargarEscena");
    }
    IEnumerator CargarEscena()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }


    public void AcptarReto()//Retador aceptando a player 
    {
        PanelAceptarReto.SetActive(false);

        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
