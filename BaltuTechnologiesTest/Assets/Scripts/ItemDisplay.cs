using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class ItemDisplay : MonoBehaviour
{
    // Variables
    public GameManager.Item item;

    // UI
    public RawImage rawImage;

    public void UpdateDisplay()
    {
        StartCoroutine(GetTexture(item.imageURL));
    }

    public void GoToWebsite()
    {
        Application.OpenURL(item.link);
    }

    IEnumerator GetTexture(string imageURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

        rawImage.SetNativeSize();
    }
}
