using UnityEngine;
using System.Collections;
using Mr1;

public class RanaScript : MonoBehaviour
{
    [Header("Rango de vision")]
    public Transform sightStart;
    public Transform sightEnd;

    [Header("Prop de la lengua")]
    public GameObject ObjLengua;
    public bool SoltarLengua;
    public float VelocidadLengua = 2f;
    public float VelocidadVision = 2f;
    public float tiempoParaRegresar = 0;
    public float tiempoMaximoLengua = 2f;
    public int IniciarConteo = 0;
    public int NumeroDeAtaques = 0;
    public int MaxNumeroDeAtaques = 3;
    public int PorcentajeDeAcierto = 10;
    public bool ViendoHormiga;

    [Header("Opciones de tiempo")]
    public float TimerActiveToad = 0f;
    public float CountDownTimerToad = 15f;

    void Awake() 
    {
        SoltarRana();
    }

    public void SoltarRana()
    { 
        if (Random.Range(0, 10) > 5)
        {
            transform.position = new Vector3(-5.81f, -1.23f, 0f);
            sightEnd.transform.FollowPath("VisionDeLaRanaIzquierda", VelocidadVision, FollowType.PingPong);
            transform.localEulerAngles = new Vector3(0f,0f,180f);
        }
        else
        {
            transform.position = new Vector3(5.81f, -1.23f, 0f);
            sightEnd.transform.FollowPath("VisionDeLaRana", VelocidadVision, FollowType.PingPong);
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    void FixedUpdate()
    {
        Raycasting();
        Mover_Lengua();
        regresar_lengua();
    }

    void Raycasting()
    {
        if (TimerActiveToad < CountDownTimerToad)
        {
            TimerActiveToad += Time.deltaTime;
            if (ViendoHormiga == false)//si esta en true esta en espera de regresar con la corrutina
            {
                if (SoltarLengua == false && NumeroDeAtaques < MaxNumeroDeAtaques)//llegue a mi numero maximo de ataques
                {
                    Debug.DrawLine(sightStart.position, sightEnd.position, Color.white);
                    var veoUnaHormiga = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Hormiga"));
                    if (veoUnaHormiga)//veo la hormiga y voy a decir que hacer
                    {
                        int caturar = Random.Range(0, 100);
                        if (caturar < PorcentajeDeAcierto)
                        {
                            SoltarLengua = true;
                        }
                        else
                        {
                            StartCoroutine(ViendoPasarHormiga());
                        }
                    }
                }
            }
        }

        if (TimerActiveToad > CountDownTimerToad)
        {
            TimerActiveToad = 0f;
            gameObject.SetActive(false);
        }
    }

    IEnumerator ViendoPasarHormiga()
    {
        ViendoHormiga = true;
        Debug.Log("VIENDO PASAR A LAS HORMIGA");
        yield return new WaitForSeconds(2f);
        ViendoHormiga = false;
        Debug.Log("VIENDO PARA CAPTURAR");
    }

    public void Mover_Lengua()
    {
        if (SoltarLengua)
        {
            ObjLengua.transform.position = Vector3.MoveTowards(ObjLengua.transform.position, sightEnd.position, VelocidadLengua * Time.deltaTime);
            IniciarConteo = 1;
        }
        else
        {
            ObjLengua.transform.position = Vector3.MoveTowards(ObjLengua.transform.position, sightStart.position, VelocidadLengua * Time.deltaTime);
        }
    }

    public Texture2D point;

    public void regresar_lengua()
    {
        if (IniciarConteo == 1)
        {
            if (tiempoParaRegresar < tiempoMaximoLengua)
            {
                tiempoParaRegresar++;
            }
            else
            {
                SoltarLengua = false;
                tiempoParaRegresar = 0f;
                IniciarConteo = 0;
            }
        }
    }
}
