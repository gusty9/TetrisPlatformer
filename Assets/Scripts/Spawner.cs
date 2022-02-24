using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Groups
    public GameObject[] groups;


    // Start is called before the first frame update
    void Start()
    {
        //spawnNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to spawn the next random block into the world
    public void spawnNext()
    {
        //get a random index in the range for the group
        int i = Random.Range(0, groups.Length);

        //spawn that object
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}
