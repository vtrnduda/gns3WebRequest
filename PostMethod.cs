using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 
public class PostMethod : MonoBehaviour
{
    InputField outputArea;
 
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(PostData);
    }
 
    void PostData() => StartCoroutine(PostData_Coroutine());
 
    IEnumerator PostData_Coroutine()
    {
        string url = "http://192.168.56.102:80/v2/projects"; // URL da sua API
        string jsonData = "{\"name\": \"teste data2\"}"; // JSON de exemplo para enviar

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(postData);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Enviar a requisição e aguardar a resposta
            yield return webRequest.SendWebRequest();

            // Verificar se ocorreu algum erro
            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erro na requisição: " + webRequest.error);
                outputArea.text = webRequest.error;
            }
            else
            {
                // A resposta está disponível em webRequest.downloadHandler.text
                string responseText = webRequest.downloadHandler.text;
                outputArea.text = webRequest.downloadHandler.text;
                Debug.Log("Resposta da requisição: " + responseText);
            }
        }
    }
}