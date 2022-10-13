using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHotSpot : MonoBehaviour
{
    //public GameObject ModelHotSpotRef;
    public GameObject ModelHotSpotPrefab;
    public GameObject HotSpotRef;
    public GameObject HotSpotPrefab;

    public GameObject EditorCanvas;
    //public EditorManager editorManager;
    //public ModelManager modelManager;


    public GameObject LeftPanelCanvas;
    public GameObject SelectModelCanvas;
    public GameObject ConfigModelCanvas;

    public ObjectRaycastManager objectRaycastManager;
    bool MouseDown = true;

    public bool ModelHotSpot;
    public bool UIHotSpot;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                
        if (Input.GetMouseButtonDown(0))
        {
            if (hit && MouseDown && objectRaycastManager.ignore == false)
            {
                if(ModelHotSpot == true && UIHotSpot == false)
                {
                    Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);                
                    StartCoroutine(SpawnModelHotSpot(newPosition));                
                }
                else if(ModelHotSpot == false && UIHotSpot == true)
                {
                    Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);
                    StartCoroutine(SpawnUIHotSpot(newPosition));
                }
            }
            //HotSpot.transform.position = newPosition;
            //Debug.Log(hitInfo.point);
        }
        if (hit)
        {
            if(ModelHotSpot == true && UIHotSpot == false)
            {
                Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);          
                HotSpotRef.transform.position = newPosition;
                HotSpotRef.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
            else if(ModelHotSpot == false && UIHotSpot == true)
            {
                Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.1f, hitInfo.point.z);
                HotSpotRef.transform.position = newPosition;
                HotSpotRef.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }
    }
    
    public IEnumerator SpawnModelHotSpot(Vector3 position)
    {
        Debug.Log("aqui add");
        MouseDown = false;
        GameObject HotSpot = Instantiate(ModelHotSpotPrefab, position, HotSpotRef.transform.rotation);
        //HotSpot.GetComponent<ModelHotSpotManager>().SetInfo(EditorCanvas, editorManager, modelManager, LeftPanelCanvas, SelectModelCanvas, ConfigModelCanvas);
        HotSpot.name = "hotspot_" + HotSpot.GetHashCode().ToString().Replace("-", "");
        HotSpot.transform.SetParent(HotSpotController.instance.hotspotRoot);

        HotSpotController.instance.dataModels.Add(new HotSpotController.DataModels(HotSpot, HotSpot.GetComponent<RectTransform>().localPosition, HotSpot.transform.eulerAngles, HotSpot.name, ""));

        yield return new WaitForEndOfFrame();
        MouseDown = true;

        HotSpotRef.SetActive(false);
        gameObject.SetActive(false);
        ModelHotSpot = false;
    }
    public IEnumerator SpawnUIHotSpot(Vector3 position)
    {
        MouseDown = false;
        GameObject HotSpot = Instantiate(HotSpotPrefab, position, HotSpotRef.transform.rotation);
        //HotSpot.GetComponent<HotSpotManager>().SetInfo(EditorCanvas, editorManager);
        Debug.Log("spawn!");
        yield return new WaitForEndOfFrame();
        MouseDown = true;
    }

    public void SetModelHS()
    {
        ModelHotSpot = true;
        UIHotSpot = false;
    }

    public void SetUIHS()
    {
        ModelHotSpot = false;
        UIHotSpot = true;
    }
}
