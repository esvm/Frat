using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManageHelp : MonoBehaviour
{
    public GameObject botao;
    public GameObject Fundo;
    public GameObject Frat;
    public Sprite sprite2;
    private SpriteRenderer TexturaDoFundo;
    private float Tempo = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SceneManager.LoadScene ("Main");
			//Application.LoadLevel("Main");
        }

        if (Input.touchCount > 0)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(
               Input.GetTouch(0).position);
            Opcoes(pos);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(
               Input.mousePosition);
            Opcoes(pos);
        }
    }
    void Opcoes(Vector2 pos)
    {
        Collider2D[] col = Physics2D.OverlapPointAll(pos);

        if (col.Length > 0)
            foreach (Collider2D c in col)
            {
				if (c.CompareTag ("BtHelp"))
					ProximaPagina ();
				else if (c.CompareTag ("BtHelp2"))
					SceneManager.LoadScene ("Main");
                    //Application.LoadLevel("Main");

            }
    }
    void ProximaPagina()
    {
        TexturaDoFundo = GetComponent<SpriteRenderer>();
        if (TexturaDoFundo != null)
            TexturaDoFundo.sprite = sprite2;
        Destroy(Frat);
        if (Tempo > 0)
        {
            Tempo -= Time.deltaTime;
        }
        else
        {
            if (botao != null)
                botao.tag = "BtHelp2";
        }
    }
}
