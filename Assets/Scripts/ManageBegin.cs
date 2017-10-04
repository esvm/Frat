using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class ManageBegin : MonoBehaviour
{
    void Start()
    {
        Ranking rank = new Ranking();
        rank.Deserialize();
    }
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit ();
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
				if (c.CompareTag ("BtStart"))
					SceneManager.LoadScene ("Game");
                else if (c.CompareTag("BtAbout"))
					SceneManager.LoadScene ("About");
                else if (c.CompareTag("BtHelp"))
					SceneManager.LoadScene ("Help");
            }
    }
}
