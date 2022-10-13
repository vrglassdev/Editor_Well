using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelPreview : MonoBehaviour
{
    public Image image;
    public GameObject obj;
    void Start()
    {
#if UNITY_EDITOR
        Texture2D texture = UnityEditor.AssetPreview.GetAssetPreview(obj);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
#endif
    }
}
