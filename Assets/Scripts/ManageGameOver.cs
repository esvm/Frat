using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using UnityEngine.UI;

public class ManageGameOver : MonoBehaviour
{
    public GameObject GameOver;
    public InputField InputField;
    public Canvas Canvas;

    void Start()
    {
        GameOver.SetActive(false);
    }

    public void ClickButton()
    {
        Ranking ranking = new Ranking();
        ranking.Deserialize();
        Ranking.ranking.Add(new Ranking(InputField.text, PlayerPrefs.GetInt("Score", 0)));
        Canvas.gameObject.SetActive(false);
        GameOver.SetActive(true);
        ranking.Serialize();
        SceneManager.LoadScene("Ranking");
    }
}
