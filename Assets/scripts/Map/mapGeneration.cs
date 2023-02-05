using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : MonoBehaviour
{
    [SerializeField] int DEPTH_MAX;
    [SerializeField] int WIDTH_MAX;
    [SerializeField] int SCENES_MAX = 100;
    [SerializeField] int MAX_MIDBOSS_SCENES = 5;

    [SerializeField] GameObject[] ScenesSingleUp = new GameObject[10];
    [SerializeField] GameObject[] ScenesSingleCenter = new GameObject[10];
    [SerializeField] GameObject[] ScenesSingleDown = new GameObject[10];
    [SerializeField] GameObject[] ScenesLargeDown = new GameObject[10];
    [SerializeField] GameObject[] ScenesLargeUp = new GameObject[10];
    [SerializeField] GameObject[] ScenesLargeCenter = new GameObject[10];
    [SerializeField] GameObject[] ScenesDoubleUp = new GameObject[10];
    [SerializeField] GameObject[] ScenesDoubleCenter = new GameObject[10];
    [SerializeField] GameObject[] ScenesDoubleDown = new GameObject[10];
    [SerializeField] GameObject[] boulder= new GameObject[3];
    [SerializeField] GameObject[] finalScene = new GameObject[2];

    GameObject map;


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

        map = GameObject.FindGameObjectsWithTag("mappa")[0];
        GenerateMap();
    }

    void GenerateMap()
    {
        System.Random rnd = new System.Random();
        GameObject start = Instantiate(ScenesDoubleCenter[2], new Vector3(0,0,0), map.transform.rotation);
        start.transform.parent = map.transform;
        Vector3 newCoordinatesBoulder = new(start.transform.position.x - start.GetComponent<SpriteRenderer>().bounds.size.x*0.66f, start.transform.position.y, 0);

        GameObject boulderInit = Instantiate(boulder[0], newCoordinatesBoulder, start.transform.rotation);
        boulderInit.transform.parent = map.transform;

        Queue<Tuple<string, GameObject>> formedScenes = new Queue<Tuple<string, GameObject>>();
        formedScenes.Enqueue(new Tuple<string, GameObject>("double", start));
        int remainingMidBossScenes = MAX_MIDBOSS_SCENES;
        int generatedScenes = 1;
        while(generatedScenes < SCENES_MAX && formedScenes.Count != 0)
        {
            
            Tuple<string, GameObject> oldScene = formedScenes.Dequeue();
            
            if(oldScene.Item1 == "double") {
                double firstSceneRand = rnd.NextDouble();
                double secondSceneRand = rnd.NextDouble();
                double randBoulderForNext = rnd.NextDouble();

                generatedScenes +=2;
                if((randBoulderForNext < 0.5f))
                {
                    //double room implies two new more scenes
                    float midBossThreshold = (generatedScenes + remainingMidBossScenes) / ((float)SCENES_MAX);
                    if (remainingMidBossScenes > 0 && firstSceneRand < midBossThreshold)
                    {

                        int indexScene = ((int)(ScenesLargeDown.Length * rnd.NextDouble()));
                        remainingMidBossScenes--;

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);


                        GameObject toInstantiate = ScenesLargeDown[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "single";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));

                        //generate midboss

                    }
                    else if (firstSceneRand < 0.50f)
                    {
                        //generate single
                        int indexScene = ((int)(ScenesSingleDown.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);


                        GameObject toInstantiate = ScenesSingleDown[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "single";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));

                        //GameObject toInstantiate = ;

                    }
                    else if (firstSceneRand > 0.5f)
                    {   //generate double
                        int indexScene = ((int)(ScenesDoubleDown.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);


                        GameObject toInstantiate = ScenesDoubleDown[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "double";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));
                    }


                    midBossThreshold = (generatedScenes + remainingMidBossScenes) / ((float)SCENES_MAX);
                    if (remainingMidBossScenes > 0 && firstSceneRand < midBossThreshold)
                    {
                        int indexScene = ((int)(ScenesLargeUp.Length * rnd.NextDouble()));
                        remainingMidBossScenes--;

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);


                        GameObject toInstantiate = ScenesLargeUp[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "singleboulder";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));
                    }
                    else// if (secondSceneRand < 0.50f)
                    {
                        //generate single
                        int indexScene = ((int)(ScenesSingleUp.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);


                        GameObject toInstantiate = ScenesSingleUp[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "singleboulder";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));

                    }
                    //else if (secondSceneRand > 0.50f)
                    //{   //generate double
                    //    int indexScene = ((int)(ScenesDoubleUp.Length * rnd.NextDouble()));

                    //    float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                    //    float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                    //    Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);

                    //    GameObject toInstantiate = ScenesDoubleUp[indexScene];
                    //    GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                    //    scene.transform.parent = map.transform;
                    //    string nextType = "doubleboulder";
                    //    formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));
                    //}
                }
                else
                {
                    //downSceneHasBoulder
                    float midBossThreshold = (generatedScenes + remainingMidBossScenes) / ((float)SCENES_MAX);
                    if (remainingMidBossScenes > 0 && firstSceneRand < midBossThreshold)
                    {

                        int indexScene = ((int)(ScenesLargeDown.Length * rnd.NextDouble()));
                        remainingMidBossScenes--;

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);


                        GameObject toInstantiate = ScenesLargeDown[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "singleboulder";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));

                        //generate midboss

                    }
                    else //if (firstSceneRand < 0.50f)
                    {
                        //generate single
                        int indexScene = ((int)(ScenesSingleDown.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);


                        GameObject toInstantiate = ScenesSingleDown[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "singleboulder";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));

                        //GameObject toInstantiate = ;

                    }
                    //else if (firstSceneRand > 0.5f)
                    //{   //generate double
                    //    int indexScene = ((int)(ScenesDoubleDown.Length * rnd.NextDouble()));

                    //    float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                    //    float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                    //    Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);


                    //    GameObject toInstantiate = ScenesDoubleDown[indexScene];
                    //    GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                    //    scene.transform.parent = map.transform;
                    //    string nextType = "double";
                    //    formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));
                    //}


                    midBossThreshold = (generatedScenes + remainingMidBossScenes) / ((float)SCENES_MAX);
                    if (remainingMidBossScenes > 0 && firstSceneRand < midBossThreshold)
                    {
                        int indexScene = ((int)(ScenesLargeUp.Length * rnd.NextDouble()));
                        remainingMidBossScenes--;

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);


                        GameObject toInstantiate = ScenesLargeUp[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "single";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));
                    }
                    else if (secondSceneRand < 0.50f)
                    {
                        //generate single
                        int indexScene = ((int)(ScenesSingleUp.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);


                        GameObject toInstantiate = ScenesSingleUp[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "single";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));

                    }
                    else if (secondSceneRand > 0.50f)
                    {   //generate double
                        int indexScene = ((int)(ScenesDoubleUp.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);

                        GameObject toInstantiate = ScenesDoubleUp[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        string nextType = "double";
                        formedScenes.Enqueue(new Tuple<string, GameObject>(nextType, scene));
                    }
                }

            }
            else  //single scene
            {
                generatedScenes++;
                double sceneRand = rnd.NextDouble();

                float midBossThreshold = (generatedScenes + remainingMidBossScenes) / ((float)SCENES_MAX);
                if (oldScene.Item1.Contains("boulder"))
                {
                    float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                    float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                    Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width/2, oldScene.Item2.transform.position.y, 0);

                    GameObject toInstantiate = boulder[0];
                    GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);
                    scene.transform.parent = map.transform;
                }
                else {
                    if (remainingMidBossScenes > 0 && sceneRand < midBossThreshold)
                    {
                        int indexScene = ((int)(ScenesLargeCenter.Length * rnd.NextDouble()));
                        remainingMidBossScenes--;

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y, 0);

                        GameObject toInstantiate = ScenesLargeCenter[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);


                        scene.transform.parent = map.transform;
                        formedScenes.Enqueue(new Tuple<string, GameObject>("single", scene));
                    }
                    else if (sceneRand < 0.5f)
                    {
                        //generate single
                        int indexScene = ((int)(ScenesSingleCenter.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y, 0);


                        GameObject toInstantiate = ScenesSingleCenter[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        formedScenes.Enqueue(new Tuple<string, GameObject>("single", scene));

                    }
                    else
                    {   //generate double
                        int indexScene = ((int)(ScenesDoubleCenter.Length * rnd.NextDouble()));

                        float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                        float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                        Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y, 0);


                        GameObject toInstantiate = ScenesDoubleCenter[indexScene];
                        GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                        scene.transform.parent = map.transform;
                        formedScenes.Enqueue(new Tuple<string, GameObject>("double", scene));
                    }
                }
                 
            }
        }
        if (formedScenes.Count > 0)
        {
            Tuple<string, GameObject> oldScene = formedScenes.Dequeue();
            if (oldScene.Item1.Contains("boulder"))
            {
                float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width *0.55f, oldScene.Item2.transform.position.y, 0);

                GameObject toInstantiate = boulder[0];
                GameObject scene = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);
                scene.transform.parent = map.transform;
            }
            else
            {
                if (oldScene.Item1 == "double")
                {
                    float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                    float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                    Vector3 newCoordinatesupper = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y + height / 2, 0);
                    Vector3 newCoordinateslower = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y - height / 2, 0);


                    GameObject toInstantiate = finalScene[0];
                    GameObject scene1 = Instantiate(toInstantiate, newCoordinatesupper, oldScene.Item2.transform.rotation);
                    GameObject scene2 = Instantiate(toInstantiate, newCoordinateslower, oldScene.Item2.transform.rotation);

                    scene1.transform.parent = map.transform;
                    scene2.transform.parent = map.transform;
                }
                else
                {
                    float width = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.x;
                    float height = oldScene.Item2.GetComponent<SpriteRenderer>().bounds.size.y;
                    Vector3 newCoordinates = new(oldScene.Item2.transform.position.x + width, oldScene.Item2.transform.position.y, 0);


                    GameObject toInstantiate = finalScene[0];
                    GameObject scene1 = Instantiate(toInstantiate, newCoordinates, oldScene.Item2.transform.rotation);

                    scene1.transform.parent = map.transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
