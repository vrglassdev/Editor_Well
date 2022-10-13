using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;
using static HotSpotController;

public class Writedatajsonfile : MonoBehaviour
{
    public string url;
    // List<ListSerializer> pos = new List<ListSerializer>();
    string data;
    public string aspas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            WriteJsonForLevel();
    }

    public void WriteJsonForLevel()
    {
        string path = Application.dataPath + "/text.json";
        data = "";
        //List<ListSerializer> pos = new List<ListSerializer>();
        for (int i = 0; i < HotSpotController.instance.dataModels.Count; i++)
        {
            Vector3 pos = HotSpotController.instance.dataModels[i].posHotspot;
            Vector3 rot = HotSpotController.instance.dataModels[i].rotationHotspot;

            string posX = pos.x.ToString().Replace(",", ".");
            string posY = pos.y.ToString().Replace(",", ".");
            string posZ = pos.z.ToString().Replace(",", ".");

            string rotX = rot.x.ToString().Replace(",", ".");
            string rotY = rot.y.ToString().Replace(",", ".");
            string rotZ = rot.z.ToString().Replace(",", ".");

            if (HotSpotController.instance.dataModels.Count == 1)
            {
                data += "{" + aspas + "nameHotspot" + aspas + ":" +
                    aspas + HotSpotController.instance.dataModels[i].nameHotspot + aspas + "," +
                    aspas + "position" + aspas + ":{" +
                    aspas + "x" + aspas + ":" + posX + "," +
                    aspas + "y" + aspas + ":" + posY + "," +
                    aspas + "z" + aspas + ":" + posZ + "}," +
                    aspas + "rotation" + aspas + ":{" +
                    aspas + "x" + aspas + ":" + rotX + "," +
                    aspas + "y" + aspas + ":" + rotY + "," +
                    aspas + "z" + aspas + ":" + rotZ + "}}";
            }
            else
            {
                data += "{" + aspas + "nameHotspot" + aspas + ":" +
                    aspas + HotSpotController.instance.dataModels[i].nameHotspot + aspas + "," +
                    aspas + "position" + aspas + ":{" +
                    aspas + "x" + aspas + ":" + posX + "," +
                    aspas + "y" + aspas + ":" + posY + "," +
                    aspas + "z" + aspas + ":" + posZ + "}," +
                    aspas + "rotation" + aspas + ":{" +
                    aspas + "x" + aspas + ":" + rotX + "," +
                    aspas + "y" + aspas + ":" + rotY + "," +
                    aspas + "z" + aspas + ":" + rotZ + "}} ,";
            }


           /* pos[p].nameHotspot = HotSpotController.instance.dataModels[i].nameHotspot;
            pos[p].position = HotSpotController.instance.dataModels[i].posHotspot;
            pos[p].rotation = HotSpotController.instance.dataModels[i].rotationHotspot;*/
        }
        string posOutput = JsonUtility.ToJson(data);
        File.WriteAllText(path, posOutput);
        StartCoroutine(Upload(data));
        Debug.Log("position:" + posOutput);
    }

    IEnumerator Upload(string data)
    {
        /*List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection(data));
        formData.Add(new MultipartFormFileSection("nameData", "myfile.txt"));*/

        var formData = new WWWForm();
        string a = "[" + data + "]";
        formData.AddField("data", a.Replace(",]", "]"));
        formData.AddField("nameData", "myyyyfile.json");
        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!" + www.downloadHandler.text);
        }
    }
}
[Serializable]
public class ListSerializer
{
    public string nameHotspot;
    public Vector3 position;
    public Vector3 rotation;
    public ListSerializer(string _nameHotspot, Vector3 pos, Vector3 rot)
    {
        nameHotspot = _nameHotspot;
        position = pos;
        rotation = rot;
    }
}