using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SaveData : MonoBehaviour
{
    public string url;
    public TextMeshProUGUI textMsg;
    public string nameFile = "myfile";
    void Start()
    {
        /* SaveJSON saveJSON = new SaveJSON
         {
             nameHotspot = "aa",
             position = new Vector3(0, 0, 0),
             rotation = new Vector3(0, 0, 0)
         };
         string json = JsonUtility.ToJson(saveJSON);
         Debug.Log(json);*/

      //  SaveHotspot();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
                SaveHotspot();
    }

    public void SaveHotspot()
    {
        if(HotSpotController.instance.dataModels.Count > 0)
        {
            string json = "";
            var keyChild = "";
            for (int i = 0; i < HotSpotController.instance.dataModels.Count; i++)
            {
                if (HotSpotController.instance.dataModels[i].hotspot)
                {
                    if (HotSpotController.instance.dataModels[i].hotspot.GetComponent<ModelHotSpotManager>().model)
                    {
                        keyChild = HotSpotController.instance.dataModels[i].hotspot.GetComponent<ModelHotSpotManager>().model.name;
                    }
                    else
                    {
                        keyChild = "";
                    }
                }
                if (HotSpotController.instance.dataModels.Count == 1)
                    json += JsonUtility.ToJson(new SaveJSON(
                        HotSpotController.instance.dataModels[i].nameHotspot,
                        HotSpotController.instance.dataModels[i].posHotspot,
                        HotSpotController.instance.dataModels[i].rotationHotspot,
                        keyChild));//.Replace(ReplaceText, "");
                else if (HotSpotController.instance.dataModels.Count > 1) 
                    json += JsonUtility.ToJson(new SaveJSON(
                    HotSpotController.instance.dataModels[i].nameHotspot,
                    HotSpotController.instance.dataModels[i].posHotspot,
                    HotSpotController.instance.dataModels[i].rotationHotspot,
                    keyChild)) + " ,";
            }

            File.WriteAllText(Application.dataPath + "/save.txt", json);
            StartCoroutine(Upload(json));
            Debug.Log(json);
        }

        /*SaveJSON saveJSON = new SaveJSON
        {
            nameHotspot = "aa",
            position = new Vector3(0, 0, 0),
            rotation = new Vector3(0, 0, 0)
        };*/
    }

    IEnumerator Upload(string data)
    {
        /*List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection(data));
        formData.Add(new MultipartFormFileSection("nameData", "myfile.txt"));*/

        var formData = new WWWForm();
        string a = "[" + data + "]";
        formData.AddField("data", a.Replace(",]", "]"));
        formData.AddField("nameData", nameFile + ".json");
        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!" + www.downloadHandler.text);
            textMsg.text = "SALVO COM SUCESSO!";
            yield return new WaitForSeconds(1.5f);
            textMsg.text = "";
        }
    }
}

public class SaveJSON
{
    public string nameHotspot;
    public Vector3 position;
    public Vector3 rotation;
    public string keyChild;
    public SaveJSON(string _nameHotspot, Vector3 pos, Vector3 rot, string _keyChild)
    {
        nameHotspot = _nameHotspot;
        position = pos;
        rotation = rot;
        keyChild = _keyChild;
    }
}
