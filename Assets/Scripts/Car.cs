using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{


    [SerializeField]
    private float speed=0;
    
                 
    // Start is called before the first frame update
    void Start()
    {
        speed = 2;//Optimize the car speed

        if (GameManager.MyInstance.currentCarNum >= 0)
        {
            GameManager.MyInstance.StartThisTurn();
        }

        Time.timeScale = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position += transform.up * Time.deltaTime * speed;
        if (GameManager.MyInstance.turningRight && GameManager.MyInstance.isReady)
        {
            Time.timeScale = 1.0f;

            var lSpeed = 1.0f; // Set this to a value you like
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z - 100), Time.deltaTime * lSpeed);
            
        }

        if (GameManager.MyInstance.turningLeft && GameManager.MyInstance.isReady)
        {
            Time.timeScale = 1.0f;

            var lSpeed = 1.0f; // Set this to a value you like
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + 100), Time.deltaTime * lSpeed);     
        }


    }

    private void FixedUpdate()
    {
            GameManager.MyInstance.ghosts[GameManager.MyInstance.currentCarNum].positions.Add(transform.position);
            GameManager.MyInstance.ghosts[GameManager.MyInstance.currentCarNum].rotations.Add(transform.rotation);
    }

    public void TurnLeft()
    {
        GameManager.MyInstance.turningLeft = true;
       
    }

    public void TurnRight()
    {
        GameManager.MyInstance.turningRight = true;
    }

    public void StopTurningLeft()
    {
        GameManager.MyInstance.turningLeft = false;
    }

    public void StopTurningRight()
    {
        GameManager.MyInstance.turningRight = false;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Car reaches the target
        if (collision.name==GameManager.MyInstance.cars[GameManager.MyInstance.currentCarNum].target.name)
        {
            Debug.Log("GREAT");
            
            Destroy(this.gameObject);

            GameManager.MyInstance.isThereACar = false;

            GameManager.MyInstance.currentCarNum++;
                     
            Time.timeScale = 0.0f;
        }
        else // When the car go out of the field we need to destroy it.
        {
            Destroy(this.gameObject);
            GameManager.MyInstance.isThereACar = false;
            //Reset the positions and rotations
            GameManager.MyInstance.ghosts[GameManager.MyInstance.currentCarNum].positions.Clear();
            GameManager.MyInstance.ghosts[GameManager.MyInstance.currentCarNum].rotations.Clear();

            GameManager.MyInstance.continueScreen.SetActive(true);
            GameManager.MyInstance.isReady = false;
            Time.timeScale = 0.0f;
        }
        
    }

    
}
