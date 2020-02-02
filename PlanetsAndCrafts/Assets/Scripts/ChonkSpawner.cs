using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChonkSpawner : MonoBehaviour
{
    public GameObject[] dronks = new GameObject[5];
    public GameObject[] chonks = new GameObject[6];
    public GameObject[] corkos = new GameObject[3];
    public GameObject[] bits = new GameObject[5];

    public Tools tool;

    public void ChonksLoad()
    {
        RandomiseBits();
    }

    void Update()
    {
        foreach (GameObject b in bits)
        {
            
            if (b.tag == "Pieces" && b.GetComponent<Core>().ReturnTopParent().tag != "Core")
            {
                chonksComplete = false;
            }

        }
        if (chonksComplete == true)
        {
            Debug.Log("CHONKS COMPLETE!"); // replace this with a win condition
            tool._state = State.GooglyEyes;
        }
    }

    void RandomiseBits()
    {

        //add one corko
        bits[0] = corkos[Random.Range(0, 2)];

        //add three chonks
        for(int i =1; i < 4; i++)
        {
            bits[i] = chonks[Random.Range(0, 5)];
        }

        //add 2 dronks
        for (int i = 4; i < 5; i++)
        {
            bits[i] = dronks[Random.Range(0, 4)];
        }

        SpawnBits(bits);

        CheckBits();
    }

    void CheckBits()
    {
        bool chonksComplete = true;

        foreach (GameObject b in bits)
        {

            if (b.tag == "Pieces" && b.GetComponent<Core>().ReturnTopParent().tag != "Core")
            {
                chonksComplete = false;
            }

        }
        if (chonksComplete == true)
        {
            Debug.Log("CHONKS COMPLETE!"); // replace this with a win condition 

        }
    }

    void SpawnBits(GameObject[] bits)
    {
        foreach (GameObject b in bits)
        {
            Debug.Log("Chonk Spawned");
            GameObject go = Instantiate(b);
            go.transform.position = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
        }
    }
}
