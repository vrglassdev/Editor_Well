using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpotController : MonoBehaviour
{
    [System.Serializable]
    public class DataModels
    {
        public GameObject hotspot;
        public Vector3 posHotspot;
        public Vector3 rotationHotspot;
        public string nameHotspot;
        public string keyChild;

        public DataModels (GameObject _hotspot, Vector3 _posHotspot, Vector3 _rotationHotspot, string _nameHotspot, string _keyChild)
        {
            hotspot = _hotspot;
            posHotspot = _posHotspot;
            rotationHotspot = _rotationHotspot;
            nameHotspot = _nameHotspot;
            keyChild = _keyChild;
        }
    }

    public static HotSpotController instance;

    public GameObject LeftPanelCanvas;
    [HideInInspector]public Transform hotspotLocation;
    public Transform hotspotRoot;

    public List<DataModels> dataModels = new List<DataModels>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LeftPanelCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void AddModel(int num)
    {
        GameObject go = Instantiate(ModuleController.instance.modules[num].prefabModel, hotspotLocation.transform.position, hotspotLocation.rotation);
        go.name = ModuleController.instance.modules[num].name;
        go.transform.SetParent(hotspotLocation);
        if (hotspotLocation.GetComponent<ModelHotSpotManager>().model)
        {
            Destroy(hotspotLocation.GetComponent<ModelHotSpotManager>().model);
            hotspotLocation.GetComponent<ModelHotSpotManager>().model = go;
        }
        else
        {
            hotspotLocation.GetComponent<ModelHotSpotManager>().model = go;
        }
    }
}
