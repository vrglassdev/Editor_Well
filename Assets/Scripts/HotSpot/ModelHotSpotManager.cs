using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelHotSpotManager : MonoBehaviour
{
   // public GameObject EditorCanvas;
   // public EditorManager editorManager;
  //  public ModelManager modelManager;
    public GameObject Spawner;

    public GameObject ModelHotSpotButton;
    public GameObject ModelHotSpotButtonInvisible;

    public GameObject LeftPanelCanvas;
    public GameObject SelectModelCanvas;
    public GameObject ConfigModelCanvas;

    public GameObject model;

    //public ConfigModel ConfigModel;

    public float ScaleValue = 1, PerspectiveXValue = 0, PerspectiveYValue = 0, PerspectiveZValue = 0;

    public void SetInfo(GameObject _LeftPanelCanvas, GameObject _SelectModelCanvas, GameObject _ConfigModelCanvas)
    {
       // EditorCanvas = _EditorCanvas;
      //  editorManager = _editorManager;
     //   modelManager = _modelManager;
        LeftPanelCanvas = _LeftPanelCanvas;
        SelectModelCanvas = _SelectModelCanvas;
        ConfigModelCanvas = _ConfigModelCanvas;
    }
    public void OnClick()
    {
        LeftPanelCanvas = HotSpotController.instance.LeftPanelCanvas;
        SetModelsInfo();
        LeftPanelCanvas.SetActive(true);
        HotSpotController.instance.hotspotLocation = transform;

        /*SelectModelCanvas.SetActive(true);
        ConfigModelCanvas.SetActive(false);*/
        //  EditorCanvas.SetActive(true);
        //   editorManager.SetModelHotSpot(this, Spawner);
    }
    public void OnClickInvisible()
    {
        LeftPanelCanvas.SetActive(false);
        SelectModelCanvas.SetActive(false);
        ConfigModelCanvas.SetActive(true);
      //  EditorCanvas.SetActive(true);
      //  editorManager.SetModelHotSpot(this, Spawner);
      //  modelManager.SetModel(ConfigModel);
    }
    public void SetSlidersValue()
    {
      //  modelManager.SetSlidersValue(ScaleValue, PerspectiveXValue, PerspectiveYValue, PerspectiveZValue);
    }
    public void SetModelOn()
    {
        ModelHotSpotButton.SetActive(false);
        ModelHotSpotButtonInvisible.SetActive(true);
    }

    public void SetModelsInfo()
    {
       /* foreach(ModelButton modelButton in modelManager.ModelButtons)
        {
            modelButton.SetModel(Spawner, modelManager, EditorCanvas, editorManager, LeftPanelCanvas, SelectModelCanvas, ConfigModelCanvas);
        }*/
    }
}
