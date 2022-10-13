using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModulesSO", menuName = "ModulesSO/Modules")]
public class ModuleSO : ScriptableObject
{
    public enum Types
    {
        decoration,
        humanoid
    }
    public string nameModel;
    public Types types = Types.decoration;
    public GameObject prefabModel;
    public Texture2D texture;
}
