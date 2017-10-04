using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnObjetos : MonoBehaviour
{
    // Use this for initialization
    public float Altura;
    public float Altura2;
    public static float TempoParaObjetoAparecerEmCena = 3;
    public GameObject Gabinete;
    public GameObject Coxinha;
    public GameObject Bananas;
    public GameObject Barra;
    public GameObject Cafe;
    //public GameObject ObGame;
    public GameObject GO_Usado;
    //public List<GameObject> ListaObjeto;
	//private bool paused = false;
	public static int speed = 1;
    void Start()
    {
        Invoke("Launcher", 1);
    }

	void Update()
	{
		DestroyObjetos.speed = speed;

		/*if (Input.GetKeyDown(KeyCode.Escape)) {
			Time.timeScale = 0;
			DestroyObjetos.speed = 0;
			paused = !paused;

			if (!paused) {
				Time.timeScale = 1;
				DestroyObjetos.speed = 1;
			}
		}*/


	}

    void Launcher()
    {
        int RdNUm = Random.Range(0, 50);
        if (RdNUm % 5 == 0)
        {
            GO_Usado = Instantiate(Gabinete) as GameObject;
        }
        else if (RdNUm % 5 == 1)
        {
            GO_Usado = Instantiate(Bananas) as GameObject;
        }
        else if (RdNUm % 5 == 2)
        {
            GO_Usado = Instantiate(Cafe) as GameObject;
        }
        else if (RdNUm % 5 == 3)
        {
            GO_Usado = Instantiate(Barra) as GameObject;
        }
        else if (RdNUm % 5 == 4)
        {
            GO_Usado = Instantiate(Coxinha) as GameObject;
        }
        //switch (RdNUm)
        //{
        //    case 0:
        //        GO_Usado = Instantiate(Gabinete) as GameObject;
        //        break;
        //    case 1:
        //        GO_Usado = Instantiate(Bananas) as GameObject;
        //        break;
        //    case 2:
        //        GO_Usado = Instantiate(Cafe) as GameObject;
        //        break;
        //    case 3:
        //        GO_Usado = Instantiate(Barra) as GameObject;
        //        break;
        //    case 4:
        //        GO_Usado = Instantiate(Coxinha) as GameObject;
        //        break;
        //}

        if (!GO_Usado.CompareTag("obstaculo"))
        {
			GO_Usado.transform.position = new Vector3(transform.position.x, Altura2, transform.position.z);
        }
        else
        {
			GO_Usado.transform.position = new Vector3(transform.position.x, Altura, transform.position.z);
        }

        //GO_Usado.SetActive(true);

        Invoke("Launcher", TempoParaObjetoAparecerEmCena);
    }
}
