using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleController : MonoBehaviour
{
    public static ModuleController instance;
    public int numList;
    public List<ModuleSO> modules = new List<ModuleSO>();

    private void Awake()
    {
        instance = this;
    }
}
