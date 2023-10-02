using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 
public class GetMethod : MonoBehaviour
{
    InputField outputArea;
    public ProjectData data;
    string JsonDwonloaded;

    public class ProjectData
    {
        //public bool auto_close;
        //public bool auto_open;
        //public bool auto_start;
        //public int drawing_grid_size;
        //public string filename;
        //public int grid_size;
        //public string name;
        //public string path;
        //public string project_id;
        //public int scene_height;
        //public int scene_width;
        //public bool show_grid;
        //public bool show_interface_labels;
        //public bool show_layers;
        //public bool snap_to_grid;
        //public string status;
        //public string supplier;
        //public string variables;
        //public int zoom;
        public int drawings;
        public int links;
        public int nodes;
        public int snapshots;


    }

    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
    }
 
    void GetData() => StartCoroutine(GetData_Coroutine());
 
    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
    //string uri = "https://my-json-server.typicode.com/typicode/demo/posts";
     
        string uri = "http://192.168.56.101/v2/projects/7362784f-4375-474e-9445-f8d9d79cefa9/stats";
        using(UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
                outputArea.text = "Erro de conexão:" + request.error;
            else
            {
                string JsonDownloaded = request.downloadHandler.text;
                data = JsonUtility.FromJson<ProjectData>(JsonDownloaded);
                outputArea.text = data.nodes.ToString();
            }

        }
    }
}