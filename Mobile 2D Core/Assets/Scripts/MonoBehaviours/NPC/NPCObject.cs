using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AntObject : MonoBehaviour
{
    public bool Pausado = false;
    public float Speed = 1f;// velocidad de la hormiga
    public float velocidadDisparo = 3f; //
    public float tiempoVuelo = 1.5f; //Tiempo que pasa en el aire la hormiga
    public int Hp = 1;
    public List<AntObject> Children;//cuando la reina vaya a tener hijos
    public int EvasionAllowed;

    public bool PuedeMoverse; // indica si la hormiga esta en movimiento
    public GameObject Objetivo;
    public float totalTiempoVuelo;
    public Vector3 Escala;
    public float Probabilidad;
    public GameObject DefensaAerea;
    public AudioClip scoreSound;
    public Vector3 PosicionObjetivo;
    public Vector3 PosicionTemporal;
    public bool PuedeEsquivar;
    public Text txtAntHP;
    public GameObject Sombra;
    // Use this for initialization
    void Start()
    {
        //PosicionObjetivo = Objetivo.transform.position;
        //totalTiempoVuelo = tiempoVuelo;
        //if (tiempoVuelo != 0)
        //    ((GameObject)transform.GetChild(0).gameObject).SetActive(false);
        //else
        //    ((GameObject)transform.GetChild(0).gameObject).SetActive(true);
        Escala_Hormiga();
    }

    //Cambia el tamaño
    public void Escala_Hormiga()
    {
        totalTiempoVuelo = tiempoVuelo;
        transform.localScale = new Vector3(1f, 1f, 1f);
        Escala = transform.localScale;

    }
    // Update is called once per frame
    void Update()
    {
        if (Pausado == false)
        {
            if (PuedeMoverse)//si esta instanciada y el el suelo
            {
                ///<summry> si la posicion de la hormiga es inferior a la de su objetivo se llama a la funcion Moverse()</summry>

                //if (this.transform.position.y < PosicionObjetivo.y)
                //{
                Moverse();
                //}
            }
            else //cuando no esta en el suelo se llama a la funcion de vuelo de la hormiga
            {

                Volar();
            }

            if (txtAntHP != null)
                txtAntHP.text = Hp.ToString();
        }
    }

    public void Volar()
    {
        //la velocidad de dispara por los segundos va a ser la velocidad de desplazamiendo de la hormiga
        float step = velocidadDisparo * Time.deltaTime * GameManager.GlobalSpeed;

        ///<summary>mover hacia su objetivo, con una velocidad de spet</summary>
        transform.position = Vector3.MoveTowards(transform.position, PosicionObjetivo, step);

        //va subiendo, el tiempo de vuelo iniclamente es el mismo tiempo que totaltiempovuelo
        //asi que al principo el tiempo vuelto va a ser mayor que el tiempo vuelo /2

        if (tiempoVuelo > (totalTiempoVuelo / 2))
        {

            transform.localScale += new Vector3(0.01f, 0.01f, 0f);


        }
        else
        {
            ///<summary>Decrese el tamaño de la hormiga cuando esta en el aire</summary>

            transform.localScale += new Vector3(-0.01f, -0.01f, 0f);

            // var a = transform.localScale / transform.localScale;
        }

        ///<summary>Decremento del tiempo de vuelo de la hormiga</summary>
        tiempoVuelo -= Time.deltaTime;

        ///<summary>Cuando el tiempo de vuelo sea igual o inferior a cero se camina de nuevo</summary>
        if (tiempoVuelo <= 0.0f)
        {
            GetComponent<Animator>().SetBool("Girar", false);
            PuedeMoverse = true;
            transform.localScale = new Vector3(1f, 1f, 0f);
            transform.GetComponent<BoxCollider2D>().enabled = true;
            transform.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    ///<summary>Funcion de moviemiento de la hormiga hacia su objetivo</summary>
    void Moverse()
    {
        Debug.Log("GameManager.GlobalSpeed" + GameManager.GlobalSpeed);
        Raycasting();
        float step = Speed * Time.deltaTime * GameManager.GlobalSpeed;
        var xR = transform.position.x - PosicionObjetivo.x;
        var yR = PosicionObjetivo.y - transform.position.y;
        if (xR < 0.008f && yR < 0.008f)
        {
            PosicionObjetivo = Vector3.zero;
        }

        if (PosicionObjetivo != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, PosicionObjetivo, step);
        }
        else
        {
            transform.position += Vector3.up * step;
        }
    }

    public Transform sightStartCenter;
    public Transform sightEndCenter;

    public Transform sightStartLeft;
    public Transform sightEndLeft;

    public Transform sightStartRight;
    public Transform sightEndRight;

    public bool VeoUnObstaculoCentro;
    public bool VeoUnObstaculoIzquierda;
    public bool VeoUnObstaculoDerecha;
    public bool Esquivar = false;

    void Raycasting()
    {
        Debug.DrawLine(sightStartCenter.position, sightEndCenter.position, Color.white);
        Debug.DrawLine(sightStartLeft.position, sightEndLeft.position, Color.white);
        Debug.DrawLine(sightStartRight.position, sightEndRight.position, Color.white);

        //http://forum.unity3d.com/threads/what-does-a-double-less-than-sign-do.56981/
        VeoUnObstaculoCentro = Physics2D.Linecast(sightStartCenter.position, sightEndCenter.position, 1 << LayerMask.NameToLayer("Hormiga"));
        VeoUnObstaculoIzquierda = Physics2D.Linecast(sightStartLeft.position, sightEndLeft.position, 1 << LayerMask.NameToLayer("Hormiga"));
        VeoUnObstaculoDerecha = Physics2D.Linecast(sightStartRight.position, sightEndRight.position, 1 << LayerMask.NameToLayer("Hormiga"));

        if (Esquivar == false)
        {
            if (VeoUnObstaculoCentro == true && VeoUnObstaculoIzquierda == false && VeoUnObstaculoDerecha == false)
            {
                Esquivar = true;
                if (Random.Range(0, 2) == 0) //derecha
                {
                    RaycastHit2D hit = Physics2D.Raycast(sightStartCenter.position, sightEndCenter.position);
                    float x_ = hit.collider.transform.position.x + (hit.collider.GetComponent<BoxCollider2D>().size.x / 2f) + transform.GetComponent<BoxCollider2D>().size.x;
                    float y_ = hit.collider.transform.position.y - (0.2973489f / 2f);
                    PosicionObjetivo = new Vector3(x_, y_, 0f);
                }
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(sightStartCenter.position, sightEndCenter.position);
                    float x_ = hit.collider.transform.position.x - (hit.collider.GetComponent<BoxCollider2D>().size.x / 2f) - transform.GetComponent<BoxCollider2D>().size.x;
                    float y_ = hit.collider.transform.position.y - (0.2973489f / 2f);
                    PosicionObjetivo = new Vector3(x_, y_, 0f);
                }
            }
        }
        else if (VeoUnObstaculoCentro == true && VeoUnObstaculoIzquierda == true && VeoUnObstaculoDerecha == false)
        {
            //Ir por la derecha
            Esquivar = true;
            RaycastHit2D hit = Physics2D.Raycast(sightStartCenter.position, sightEndCenter.position);
            float x_ = hit.collider.transform.position.x + (hit.collider.GetComponent<BoxCollider2D>().size.x / 2f) + transform.GetComponent<BoxCollider2D>().size.x;
            float y_ = hit.collider.transform.position.y - (0.2973489f / 2f);
            PosicionObjetivo = new Vector3(x_, y_, 0f);

        }
        else if (VeoUnObstaculoCentro == true && VeoUnObstaculoIzquierda == false && VeoUnObstaculoDerecha == true)
        {
            //ir por la izquierda
            Esquivar = true;
            RaycastHit2D hit = Physics2D.Raycast(sightStartCenter.position, sightEndCenter.position);
            float x_ = hit.collider.transform.position.x - (hit.collider.GetComponent<BoxCollider2D>().size.x / 2f) - transform.GetComponent<BoxCollider2D>().size.x;
            float y_ = hit.collider.transform.position.y - (0.2973489f / 2f);
            PosicionObjetivo = new Vector3(x_, y_, 0f);
        }
        //else if (VeoUnObstaculoCentro == false && VeoUnObstaculoIzquierda == false && VeoUnObstaculoDerecha == false) 
        //{
        //    //
        //}

        //if (VeoUnObstaculoIzquierda == true)
        //{
        //    VeoUnObstaculoDerecha = Physics2D.Linecast(sightStartRight.position, sightEndRight.position, 1 << LayerMask.NameToLayer("Hormiga"));
        //}


        //if (VeoUnObstaculoIzquierda == false && VeoUnObstaculoDerecha)
        //{
        //    transform.position = Vector3.left * Speed * Time.deltaTime;// MoveTowards(transform.position, Objetivo.transform.position, Speed);
        //    Debug.Log("Izquierda");
        //}
        //else if (VeoUnObstaculoDerecha == false && VeoUnObstaculoIzquierda)
        //{
        //    transform.position = Vector3.right * Speed * Time.deltaTime;
        //    Debug.Log("Derecha");
        //}
        //else
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, Objetivo.transform.position, Speed);
        //    Debug.Log("Centro");
        //}
    }

    IEnumerator BuscarPosicion()
    {
        //if(transform.position = Vector3.Lerp(transform.position, PosicionObjetivo, Time.deltaTime);)
        if (transform.position.sqrMagnitude != PosicionObjetivo.sqrMagnitude)
        {
            transform.position = Vector3.Lerp(transform.position, PosicionObjetivo, Time.deltaTime * 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Hit()
    {
        Debug.Log(".");
        if (PuedeMoverse) //solo puedes anotar si no esta volando
        {
            Debug.Log("HP = " + Hp.ToString() + " - Daño = " + GameManager.DanoPorTab.ToString());
            Hp = Hp - GameManager.DanoPorTab;
            if (Hp <= 0)
            {
                gameObject.GetComponents<AudioSource>()[1].volume = GameManager.FXVolume;
                gameObject.GetComponents<AudioSource>()[1].Play();
                GameManager.Anotar();
                EliminarHormiga(gameObject.name);
            }
        }
        else
        {
            StartCoroutine(MostrarDefensaAera());
            gameObject.GetComponent<AudioSource>().volume = GameManager.FXVolume;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }


    IEnumerator MostrarDefensaAera()
    {
        DefensaAerea.SetActive(true);
        yield return new WaitForSeconds(.1f);
        DefensaAerea.SetActive(false);
    }

    ///<summary>Funcion colicion de la hormiga con el objetivo y luego se destrulle</summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dulce")
        {
            GameManager.VIDA--;
            Destroy(gameObject, 0.2f);
        }
        else if (other.gameObject.tag == "BubbleGum")
        {
            StartCoroutine(AtrapadoPorBubbleGum(other.gameObject));
        }
    }

    IEnumerator AtrapadoPorBubbleGum(GameObject _BubbleGum)
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        float tempspeed = Speed;
        Speed = 0f;
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Speed = tempspeed;
        Destroy(_BubbleGum);
    }

    public float FuerzaImpulso;
    void EliminarHormiga(string NombreHormiga)
    {
        if (NombreHormiga != "Hormiga Princesa(Clone)")
        {
            Destroy(gameObject, 0.1f);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                var NuevaHormiga = Instantiate(Resources.Load("Prefabs/Hormiga Soldado", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                Rigidbody2DExtension.AddExplosionForce(NuevaHormiga.GetComponent<Rigidbody2D>(), 100, NuevaHormiga.transform.position, 30);
                NuevaHormiga.GetComponent<AntObject>().Pausado = false;
                Debug.Log("Se solto la hormiga " + NuevaHormiga.name);
            }
            Destroy(gameObject, 0.1f);
        }
    }


}

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }

    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}
