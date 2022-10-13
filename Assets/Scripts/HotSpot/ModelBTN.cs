using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelBTN : MonoBehaviour
{
    public Image image;
    public Texture2D texture;
    public int num;
    public void AddModel()
    {
        HotSpotController.instance.AddModel(num);
    }

    private void Start()
    {
        if (texture)
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
