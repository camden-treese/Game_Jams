using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.Impl;

public class WaterMover : MonoBehaviour
{

    int oldNumber;

    // GameObject water
    [SerializeField] private GameObject waterObject;

   
    public void SpawnObject()
    {
      
        Vector3 spawnPosition = new Vector3(0f, -1.16f);

        oldNumber = 2;

        // create the object at the set spawnPosition
        Instantiate(waterObject, spawnPosition, Quaternion.identity);

        
    }

    public void MoveWater ()
    {



        // Generates a random integer: 1, 2, 3, 4, or 5
        int randomNumber = UnityEngine.Random.Range(1, 6);
        

        // determines where the water will move to based on the randomizer and the 5 preset locations
        if (randomNumber == 1 && oldNumber != 1)
        {
            transform.position = new Vector3(0f, -3.76f);
        }
        else if (randomNumber == 2 && oldNumber != 2)
        {
            transform.position = new Vector3(0f, -1.16f);
        }
        else if (randomNumber == 3 && oldNumber != 3)
        {
            transform.position = new Vector3(0f, 4.45f);
        }
        else if (randomNumber == 4 && oldNumber != 4)
        {
            transform.position = new Vector3(-8.2f, 1.72f);
        }
        else if (randomNumber == 5 && oldNumber != 5)
        {
            transform.position = new Vector3(8.2f, 1.72f);
        }

        oldNumber = randomNumber;
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {

        // if hit by the player
        if (otherObject.CompareTag("Player"))
        {
            MoveWater();

        }

    }

}
