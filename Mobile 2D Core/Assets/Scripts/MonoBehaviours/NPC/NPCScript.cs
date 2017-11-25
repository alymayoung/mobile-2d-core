using UnityEngine;
using System.Collections;

public class AntScript : MonoBehaviour {

    float velocidad = 0.1f;
    public bool Iniciar = false;
    public Transform Objetivo;
    public float speed;
    

    /// <summary>
    /// Funcion de moviento de la hormiga
    /// </summary>
    void Avanzar()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Objetivo.position, step);
    }

    /// <summary>
    /// Funcion que detecta el click sobre la hormiga y la destruye y aumenta los puntos
    /// </summary>
    void OnMouseDown()
    {
            GameManager.Anotar();
            Destroy(gameObject, 0.1f);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Iniciar)
        {
            if (this.transform.position.y < Objetivo.position.y)
                Avanzar();
        }
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dulce")
        {
            Destroy(gameObject, 0.2f);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("ouch 2");
        //Destroy(gameObject, 1);
    }
    
}
