using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShooterObjectScript : MonoBehaviour
{
    public float step = 0.01f;
    public bool Moverse = false;
    public bool Bajar = false;
    public float TiempoSoltarHormiga = 3f;
    public float TiempoActual = 0f;
    public GameObject HormigaCanon;
    public List<GameObject> Municiones;
    public bool disparando = false;
    public AudioClip scoreSound;
    public void CargarCannon(GameObject Municion)
    {
        Municiones.Add(Municion);
        Debug.Log("Cargando");   
    }

    void FixedUpdate()
    {
        ///<summary>Cuando se cumpla la condicion mover es true y el cañon va a subir</summary>
        if (Municiones.Count > 0 && Moverse == false && transform.position.y < -4f && Bajar == false)
        {
            Moverse = true;

        }
        Mover(); ///<summary>La funcion mover siempre se llama</summary>
        ///
        //if (TiempoActual >= TiempoSoltarHormiga)
        //{
        //    Soltar_Hormiga();
        //    TiempoActual = 0f;

        //}
        //else
        //{
        //    TiempoActual += Time.deltaTime;
        //}
    }

    void Mover()
    {
        ///<summary>Posicion maxima del cañon -4 arriba y baja -4.90 abajo
        if (Moverse && transform.position.y < -4f && Bajar == false)
        {
            ///<summary>si moverse es si y la posicion del cañon es inferior de -4f y bajar es falso el cañon  sube</summary>
            transform.Translate(new Vector2(0f, step) * Time.deltaTime);
        }
        else if (Moverse && transform.position.y >= -4f && Bajar == false)
        {
            ///<summary>si moverse es si y la posicion del cañon es superior a -4f y bajar se no, se llama a la animacio de explocion</summary>
            Animator animacion = gameObject.GetComponentInChildren<Animator>();
            animacion.Play("explosjon3_");
            Moverse = false;
            //if (HormigaCanon != null)

            if (disparando == false)
                Disparar();
        }
        else if (Moverse == false && transform.position.y >= -4.90f)
        {
            ///<summary>si moverse es no y la posicion del cañon es superior a -4.90f, el cañon va a bajar</summary>
            Bajar = true;
            transform.Translate(new Vector2(0f, -step) * Time.deltaTime);

        }
        else
        {
            ///<summary>Esto reinica el bajar y subir del cañon
            ///como bajar es falso caeria en la primera condicion de nuevo y iniciaria el ciclo otra ves</summary>
            Bajar = false;
            disparando = false;
        }
    }

    void Disparar()
    {
        HormigaCanon = Municiones[0];
        if (HormigaCanon != null)
        {
            HormigaCanon.GetComponent<Animator>().SetBool("Girar", true);
            HormigaCanon.GetComponent<AntObject>().PuedeMoverse = false;
            ///<summary>se instancia la clase AntObject para aceder a las propiedades de hormiga
            ///le paso los valores a las propiedades de la hormiga</summary>
            //AntObject hormiga_ = (AntObject)Instantiate(HormigaCanon, this.transform.position, transform.rotation);
            HormigaCanon.transform.position = gameObject.transform.position;
            HormigaCanon.GetComponent<AntObject>().tiempoVuelo = 2.5f;
            //HormigaCanon.GetComponent<AntObject>().totalTiempoVuelo = 0f;
            //HormigaCanon.GetComponent<AntObject>().Escala_Hormiga();
            HormigaCanon.GetComponent<AntObject>().Speed = 1f;
            var velocidadInicial = Random.Range(1f, 2.5f);
            HormigaCanon.GetComponent<AntObject>().velocidadDisparo = velocidadInicial;
            HormigaCanon.GetComponent<BoxCollider2D>().enabled = false;
            HormigaCanon.GetComponent<CircleCollider2D>().enabled = false;
            //((GameObject)HormigaCanon.transform.GetChild(0).gameObject).SetActive(false);
            disparando = true;

            AudioSource.PlayClipAtPoint(scoreSound, transform.position);            
            Municiones.Remove(HormigaCanon);

        }
    }
}