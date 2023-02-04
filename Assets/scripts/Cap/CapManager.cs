using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapManager : MonoBehaviour
{
    [SerializeField] GameObject[] Caps = new GameObject[36];


    private static CapManager _instance;
    public static CapManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);   //to keep the singleton between scenes
    }

   
}
