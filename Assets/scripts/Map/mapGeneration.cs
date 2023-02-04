using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : MonoBehaviour
{
    [SerializeField] int DEPTH_MAX;
    [SerializeField] int WIDTH_MAX;

    [SerializeField] GameObject[] ScenesSingleUp = new GameObject[10];
    [SerializeField] GameObject[] ScenesSingleCenter = new GameObject[10];
    [SerializeField] GameObject[] ScenesSingleDown = new GameObject[10];
    [SerializeField] GameObject[] ScenesLargeDown = new GameObject[10];
    [SerializeField] GameObject[] ScenesLargeUp = new GameObject[10];
    [SerializeField] GameObject[] ScenesLargeCenter = new GameObject[10];
    [SerializeField] GameObject[] ScenesDoubleUp = new GameObject[10];
    [SerializeField] GameObject[] ScenesDoubleCenter = new GameObject[10];
    [SerializeField] GameObject[] ScenesDoubleDown = new GameObject[10];


    private static mapGeneration _instance;
    public static mapGeneration Instance
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
        //spawnPoint = GameObject.Find("spawnpoints").transform.Cast<Transform>().ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for(int i = 0; i < DEPTH_MAX; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
