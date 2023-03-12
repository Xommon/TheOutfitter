using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RestAPI : MonoBehaviour
{
    private string URL = "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=1045630&page=1";
    public GameObject loadingScreen;
    public GameManager gameManager;

    public void Search()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
                loadingScreen.SetActive(false);
            }
            else
            {
                loadingScreen.SetActive(false);
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                int i = 0;
                while (stats[3][i] != null && i <= gameManager.items.Length)
                {
                    gameManager.items[i].title = stats[3][i]["title"];
                    gameManager.items[i].link = stats[3][i]["link"];
                    gameManager.items[i].imageURL = stats[3][i]["image"];
                    i++;
                }

                int newIndex = Random.Range(0, i - 1);
                gameManager.itemDisplay.item = gameManager.items[newIndex];
                gameManager.itemDisplay.UpdateDisplay();
            }
        }
    }
}
