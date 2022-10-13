using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectRaycastManager : MonoBehaviour
{
    public Renderer objectRenderer;
   // public GameObject EditorCanvas;
   // public EditorManager editorManager;
    public GameObject ChangeTextureCanvas;
    public GameObject LeftPanelCanvas;
    public Dropdown MaterialsDropdown;

    public bool ignore;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                //Debug.Log(hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Construction" /*&& EditorCanvas.activeSelf != true*/ && ignore == false)
                {
                    Debug.Log(hitInfo.transform.gameObject.name);
                    objectRenderer = hitInfo.transform.gameObject.GetComponent<Renderer>();
                 //   editorManager.ResetObjectRenderer();
                 //   EditorCanvas.SetActive(true);
                    ChangeTextureCanvas.SetActive(true);
                    LeftPanelCanvas.SetActive(false);

                    List<Dropdown.OptionData> Options = new List<Dropdown.OptionData>();
                    for(int i = 0; i < objectRenderer.materials.Length; i++)
                    {
                        int materialNumber = i + 1;
                        Dropdown.OptionData option = new Dropdown.OptionData("Material " + materialNumber.ToString());
                        Options.Add(option);
                    }

                    MaterialsDropdown.AddOptions(Options);
                   // editorManager.SetObjectRenderer(objectRenderer);
                    
                }
            }
        }
    }
}
