using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ManageRanking : MonoBehaviour
{
    public GameObject PlayerInfo;
    public GameObject Panel;


    // Use this for initialization
    void Start()
    {
        Ranking ranking = new Ranking();
        ranking.Deserialize();
        Debug.Log(Ranking.ranking.Count);
        Vector3 vetor = Panel.GetComponent<RectTransform>().position;
        
        foreach (var item in Ranking.ranking)
        {
            Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, Panel.GetComponent<RectTransform>().rect.height + 20f);
            vetor.y -= 10f;
            Panel.GetComponent<RectTransform>().position = vetor;

            GameObject go = (GameObject)Instantiate(PlayerInfo);
            go.transform.SetParent(this.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.Find("Name").GetComponent<Text>().text = item.name;
            go.transform.Find("Name").GetComponent<Text>().color = Color.white;
            go.transform.Find("Name").GetComponent<Text>().fontSize = 13;
            go.transform.Find("Name").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

            go.transform.Find("Score").GetComponent<Text>().text = item.score.ToString();
            go.transform.Find("Score").GetComponent<Text>().color = Color.white;
            go.transform.Find("Score").GetComponent<Text>().fontSize = 13;
            go.transform.Find("Score").GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        }
    }

    public void CickButton()
    {
        SceneManager.LoadScene("Main");
    }
}
