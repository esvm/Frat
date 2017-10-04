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
        //for (int i = 0; i < Ranking.ranking.Count; i++)
        //{
        //    var Box = new GameObject();
        //    Box.AddComponent<TextMesh>();
        //    var texto = Box.GetComponent<TextMesh>();

        //    texto.transform.parent = panel.transform;
        //    texto.transform.position = new Vector3(-7f , 4f - i, 0f);

        //    texto.text = Ranking.ranking[i].name;
        //    texto.color = Color.white;
        //    texto.fontSize = 10;
        //    Font myFont = Resources.Load<Font>("Berlin Sans FB Demi");
        //    texto.font = myFont;

        //    var Box2 = new GameObject();
        //    Box2.AddComponent<TextMesh>();
        //    var texto2 = Box.GetComponent<TextMesh>();

        //    texto2.transform.parent = panel.transform;
        //    texto2.transform.position = new Vector3(0f, 4f - i, 0f);

        //    texto2.text = Ranking.ranking[i].name;
        //    texto2.color = Color.white;
        //    texto2.fontSize = 10;
        //    Font myFont2 = Resources.Load<Font>("Berlin Sans FB Demi");
        //    texto2.font = myFont2;

        //}
        Vector3 vetor = Panel.GetComponent<RectTransform>().position;
        foreach (var item in Ranking.ranking)
        {
            Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(500f, Panel.GetComponent<RectTransform>().rect.height + 20f);
            vetor.y -= 10f;
            Panel.GetComponent<RectTransform>().position = vetor;

            GameObject go = (GameObject)Instantiate(PlayerInfo);
            go.transform.SetParent(this.transform);
            go.transform.position = new Vector3(0f, 0f, 0f);
            setData(item.name, go.transform.Find("Name").GetComponent<Text>());
            setData(item.score.ToString(), go.transform.Find("Score").GetComponent<Text>());
        }
    }

    private void setData(string item, Text text)
    {
        text.text = item;
        text.color = Color.white;
        text.fontSize = 13;
        Font myFont2 = Resources.Load<Font>("Berlin Sans FB Demi");
        text.font = myFont2;
    }

    public void CickButton()
    {
        SceneManager.LoadScene("Main");
    }
}
