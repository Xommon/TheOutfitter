using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RestAPI : MonoBehaviour
{
    /// URL Categories
    // Masculine: 0 = torso, 1 = pants, 2 = shoes
    private string[] mascURLs = new string[]
    {
        "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=1045630&page=1",
        "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=2476498011",
        "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=679312011"
    };

    // Feminine: 0 = torso, 1 = pants, 2 = shoes
    private string[] femURLs = new string[]
    {
        "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=2368343011",
        "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=1048184",
        "https://api.rainforestapi.com/request?api_key=26203EF15AF64CA5BDC04FE4BBCC5CC1&type=search&amazon_domain=amazon.com&search_term=*&category_id=679337011"
    };

    bool test = false;
    public float time;

    // UI
    public GameObject loadingScreen;
    public GameManager gameManager;
    public string previousGenderExpression;

    public GameManager.Item[] tempArray;

    public void Search()
    {
        test = true;

        loadingScreen.SetActive(true);

        if (gameManager.currentGenderExpression == "Masculine")
        {
            StartCoroutine(GetData(mascURLs));
        }
        else if (gameManager.currentGenderExpression == "Feminine")
        {
            StartCoroutine(GetData(femURLs));
        }
    }

    private void Update()
    {
        if (test)
        {
            time += Time.deltaTime;
        }
    }

    IEnumerator GetData(string[] urlSet)
    {
        int i = 0, x = 0; // i = Index of itemsArrays | x = Index of Item fields (title, image, link)
        time = 0;

        if (previousGenderExpression != gameManager.currentGenderExpression)
        for (i = x; i < 3; i++)
        {
            x = 0;
            using (UnityWebRequest request = UnityWebRequest.Get(urlSet[i]))
            {
                yield return request.SendWebRequest();

                // Show error
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError(request.error);
                    loadingScreen.SetActive(false);
                    break;
                }

                // Loading successful
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                // Build new clothing arrays
                while (stats[3][x] != null)
                {
                    gameManager.itemsArrays[i][x].title = stats[3][x]["title"];
                    gameManager.itemsArrays[i][x].price = stats[3][x]["prices"][0]["value"];
                    if (gameManager.itemsArrays[i][x].price == null)
                    {
                        gameManager.itemsArrays[i][x].price = stats[3][x]["prices"][0]["raw"].ToString().Substring(1, stats[3][x]["prices"][0]["raw"].Count);
                    }
                    gameManager.itemsArrays[i][x].link = stats[3][x]["link"];
                    gameManager.itemsArrays[i][x].imageURL = stats[3][x]["image"];
                    x++;
                }
            }
        }

        loadingScreen.SetActive(false);
        gameManager.CreateOutfit();
        previousGenderExpression = gameManager.currentGenderExpression;

        Debug.Log("Completed after " + time + " seconds");
        test = false;
    }
}
