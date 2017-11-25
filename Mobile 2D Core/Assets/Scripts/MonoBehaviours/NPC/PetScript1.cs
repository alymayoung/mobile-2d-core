using UnityEngine;
using System.Collections;

public class ScriptRanaLengua : MonoBehaviour 
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.parent.GetComponent<RanaScript>().SoltarLengua = false;
        gameObject.transform.parent.GetComponent<RanaScript>().IniciarConteo = 0;
        gameObject.transform.parent.GetComponent<RanaScript>().tiempoParaRegresar = 0f;
        gameObject.transform.parent.GetComponent<RanaScript>().NumeroDeAtaques++;

        collision.gameObject.GetComponent<AntObject>().PuedeMoverse = false;
        collision.gameObject.GetComponent<AntObject>().PuedeEsquivar = false;
        Destroy(collision.gameObject);
    }
}
