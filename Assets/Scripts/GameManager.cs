using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class TheCar
    {
     
        public GameObject target;
        public GameObject entrance;
    }

    public TheCar[] cars;

    //array of the cars
   
    [System.Serializable]
    public class Ghost
    {
        public List<Vector3> positions = new List<Vector3>();

        public List<Quaternion> rotations = new List<Quaternion>();

        public int k = 0;
    }


    //public CarPos[] cars;

    public Ghost[] ghosts;

    
    public int currentCarNum = 0;

    [SerializeField]
    private int carCount = 8;
    
    private static GameManager instance;
    public static GameManager MyInstance
    { 
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }
    public GameObject finishedLevelScreen;
    //When player hit an object show a window 
    public GameObject continueScreen;
    //When player hit an object we set isReady to false 
    public bool isReady=true;

    public GameObject[] ghostObjects;
    //for instantiating the ghost objects
    public GameObject prefab;

    private GameObject aCar;
    //For Activating target in Rewind
    private GameObject go;

    public bool isThereACar=false;

    public bool isRewinding = false;

    //for car's starting rotation
    public Quaternion q;

    public bool turningLeft = false;
    public bool turningRight = false;

    // Update is called once per frame
    void Update()
    {
        if (currentCarNum >= carCount)
        {
            finishedLevelScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
        if (!isThereACar && currentCarNum<carCount)
        {
            

            if (cars[currentCarNum].entrance.transform.position.y > 2)
            {
                q = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                q = Quaternion.identity;
            }
            aCar = Instantiate(Resources.Load("Car", typeof(GameObject)), cars[currentCarNum].entrance.transform.position, q) as GameObject;
            isThereACar = true;

            //Destroying the ghosts
            for (int i = 0; i <ghostObjects.Length; i++)
            {
                Destroy(ghostObjects[i]);
            }

        }
        
    }

    private void FixedUpdate()
    { 
        if(isRewinding)
        {        
            Rewind();
        }
        
    }

    void Rewind()
    {
        for (int i = 0; i < currentCarNum; i++)
        {
          
            if(ghosts[i].k<ghosts[i].positions.Count && ghostObjects[i] != null)
            {
                ghostObjects[i].transform.position = ghosts[i].positions[ghosts[i].k];
                ghostObjects[i].transform.rotation = ghosts[i].rotations[ghosts[i].k];
                ghosts[i].k++;
            }
            else //When reaches the target
            {
                //Destroy(ghostObjects[i].transform.gameObject);
            }
        }
        
     
    }
 
    public void StartThisTurn()
    {
        StartRewind();
        ActivateTarget();
        DeactivateTargets();
    }
    public void StartRewind()
    {

       
        //start
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].k = 0;
        }
 
        for (int i = 0; i < currentCarNum; i++)
        {
            //Instantiating Ghosts
            ghostObjects[i] = Instantiate(prefab, cars[i].entrance.transform.position, Quaternion.identity);
            ghostObjects[i].GetComponent<SpriteRenderer>().color = Color.blue;
   
        }
       
        isRewinding = true;

    }

    void ActivateTarget()
    {
        cars[currentCarNum].target.SetActive(true);
    }

    void DeactivateTargets()
    {
        for (int i = 0; i <currentCarNum; i++)
        {
            cars[i].target.SetActive(false);
        }
    }

    public void Continue()
    {
        isReady = true;
        //reset to preventing 
        turningLeft = false;
        turningRight = false;

        continueScreen.SetActive(false);
    }

}
