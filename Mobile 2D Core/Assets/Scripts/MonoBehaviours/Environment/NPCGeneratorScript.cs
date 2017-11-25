using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneradordeHormigasScript : MonoBehaviour 
{
    public List<GameObject> HormigasActiva;
    //public List<GameObject> HormigasDesactivada;
    
    public SpawnerObject SpawnerObj_;
    float VelocidadDeSalida = 0.7f;
    System.Random rnd = new System.Random();

    public GameObject Cannon;
    public float step = 0.01f;
    public bool Moverse = false;
    public bool Bajar = false;


    void Awake()
    {
        HormigasActiva = new List<GameObject>();
        for (int i = 0; i < GameObject.Find("AquiSeGeneranLasHormigas").GetComponent<SkillMenuScript>().ListHormigas.Count; i++)
        {
            GameObject.Find("AquiSeGeneranLasHormigas").GetComponent<SkillMenuScript>().ListHormigas[i].transform.position = new Vector3(0f, -10f, 0f);
            HormigasActiva.Add(GameObject.Find("AquiSeGeneranLasHormigas").GetComponent<SkillMenuScript>().ListHormigas[i]);
        }

    }

	void Start () 
    {
        StartCoroutine(SoltarHomigas());
	}

    IEnumerator SoltarHomigas() 
    {
        while (HormigasActiva.Count > 0)
        {
            int indice_ = rnd.Next(HormigasActiva.Count);
            
            var hormiga = HormigasActiva[indice_];
            hormiga.SetActive(true);
            if (hormiga != null)
            {
                HormigasActiva.Remove(hormiga);
                hormiga.GetComponent<AntObject>().Pausado = false;
                if (hormiga.name == "Hormiga Canon(Clone)")
                {
                    GameObject.Find("Cannon").GetComponent<CannonObjectScript>().CargarCannon(hormiga);
                }
                else
                {
                    //hormiga.SetActive(true);
                    if (hormiga.name.IndexOf("Runner") != -1)
                    {
                        hormiga.GetComponent<AntObject>().Speed = 2.5f;
                    }
                    else if (hormiga.name.IndexOf("Tank") != -1)
                    {
                        hormiga.GetComponent<AntObject>().Speed = 0.75f;
                    }
                    else
                    {
                        hormiga.GetComponent<AntObject>().Speed = 1f;
                    }

                    int indece = Random.Range(0, SpawnerObj_.StartingPoint.Length);
                    hormiga.transform.position = SpawnerObj_.StartingPoint[indece].transform.position;
                }
                yield return new WaitForSeconds(VelocidadDeSalida);
            }
        }    
    }   
}
