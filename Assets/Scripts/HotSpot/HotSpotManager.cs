using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.Networking;

public class HotSpotManager : MonoBehaviour
{

    public Button HotSpotButton;
    public Button HotSpotButtonInvisible;

    public int TypeOfHotspot; //0 = mp4, 1 = image, 2 = youtube, 3 = streaming

    
    public float OpacityValue = 1, ScaleValue = 1, PerspectiveXValue = 0, PerspectiveYValue = 0, PerspectiveZValue = 0;

    public void SetInfo(GameObject _EditorCanvas)
    {
               
    }

    public void OnClick()
    {
        
        //SetMP4On("Gorillaz.mp4");
    }
}
