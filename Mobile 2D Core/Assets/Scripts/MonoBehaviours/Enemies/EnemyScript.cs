using UnityEngine;
using System.Collections;

public class ShooterScript : MonoBehaviour
{

    public float step = 0.01f;

    public bool Moverse = false;

    public bool Bajar = false;
    // Use this for initialization

    public float Direccion = 12f;
    // Use this for initialization
    public AntObject Hormiga;
    public AntObject HormigaCanon;
    public AntObject HormingaRunner;
    public AntObject HormingaTank;

    public GameObject Objetivo;
    public float TotalTime = 0f;
    public AudioClip scoreSound;
    public float TiempoSoltarHormiga = 3f;
    public float TiempoActual = 0f;
    public SpawnerObject SpawnerObject_;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ///<summary>Cuando se cumpla la condicion mover es true y el cañon va a subir</summary>
        if (Random.Range(0, 100) < 1f && Moverse == false && transform.position.y < -4.64f && Bajar == false)
        {
            Moverse = true;
        }
        Mover(); ///<summary>La funcion mover siempre se llama</summary>
                 ///
        if (TiempoActual >= TiempoSoltarHormiga) 
        {
            Soltar_Hormiga();
            TiempoActual = 0f;
            
        }
        else 
        {
            TiempoActual += Time.deltaTime;
        }
    }

    void Mover()
    {
        ///<summary>Posicion maxima del cañon -4.64 arriba y baja -5.43 abajo
        if (Moverse && transform.position.y < -4.64f && Bajar == false)
        {
            ///<summary>si moverse es si y la posicion del cañon es inferior de -4.64f y bajar es falso el cañon  sube</summary>
            transform.Translate(new Vector2(0f, step) * Time.deltaTime);            
        }
        else if (Moverse && transform.position.y >= -4.64f && Bajar == false)
        {
            ///<summary>si moverse es si y la posicion del cañon es superior a -4.64f y bajar se no, se llama a la animacio de explocion</summary>
            Animator animacion = gameObject.GetComponentInChildren<Animator>();
            animacion.Play("explosjon3_");            
            Moverse = false;
            if(HormigaCanon != null)
                Disparar();
            
        }
        else if (Moverse == false && transform.position.y >= -5.43f)
        {
            ///<summary>si moverse es no y la posicion del cañon es superior a -5.43f, el cañon va a bajar</summary>
            Bajar = true;
            transform.Translate(new Vector2(0f, -step) * Time.deltaTime);
            
        }
        else
        {
            ///<summary>Esto reinica el bajar y subir del cañon
            ///como bajar es falso caeria en la primera condicion de nuevo y iniciaria el ciclo otra ves</summary>
            Bajar = false;
        }
    }

    void Disparar()
    {
        ///<summary>se instancia la clase AntObject para aceder a las propiedades de hormiga
        ///le paso los valores a las propiedades de la hormiga</summary>
        AntObject hormiga_ = (AntObject)Instantiate(HormigaCanon, this.transform.position, transform.rotation);
        hormiga_.Objetivo = Objetivo;
        hormiga_.PuedeMoverse = false;
        var velocidadInicial = Random.Range(1f, 2.5f);
        hormiga_.velocidadDisparo = velocidadInicial;
        ((GameObject)hormiga_.transform.GetChild(0).gameObject).SetActive(false);
        //Debug.Log("CAÑON DISPARO");
        AudioSource.PlayClipAtPoint(scoreSound, transform.position);
    }

    void Soltar_Hormiga() 
    {
        //random de 1, 10 si el valor es mayor a 7 sueltas la hormigarunner;
        int randomValue = Random.Range(1, 10);

        int index = Random.Range(0, SpawnerObject_.StartingPoint.Length);
        AntObject hormiga_;
        
        if (randomValue < 6 && HormingaRunner != null)
             hormiga_ = (AntObject)Instantiate(Hormiga, SpawnerObject_.StartingPoint[index].transform.position, transform.rotation);
        else if (randomValue > 8 && HormingaTank != null)
        {
            Debug.Log(randomValue);
            hormiga_ = (AntObject)Instantiate(HormingaTank, SpawnerObject_.StartingPoint[index].transform.position, transform.rotation);
        }
        else if (HormingaRunner != null)
            hormiga_ = (AntObject)Instantiate(HormingaRunner, SpawnerObject_.StartingPoint[index].transform.position, transform.rotation);
        else
            hormiga_ = (AntObject)Instantiate(Hormiga, SpawnerObject_.StartingPoint[index].transform.position, transform.rotation);
        hormiga_.Objetivo = Objetivo;
        hormiga_.PuedeMoverse = true;
        //((GameObject)hormiga_.transform.GetChild(0).gameObject).SetActive(false);
    }
}
