using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using UnityEngine.UI;

public class ManageGameOver : MonoBehaviour
{
    public GameObject GameOver;
    public InputField inputField;
    public Canvas Canvas;

    void Start()
    {

        GameOver.SetActive(false);
    }

    public void ClickButton()
    {
        Ranking ranking = new Ranking();
        ranking.Deserialize();
        Debug.Log(inputField.text);
        Ranking.ranking.Add(new Ranking(inputField.text, PlayerPrefs.GetInt("Score", 0)));
        Canvas.gameObject.SetActive(false);
        GameOver.SetActive(true);
        ranking.Serialize();
        SceneManager.LoadScene("Ranking");
    }
}
