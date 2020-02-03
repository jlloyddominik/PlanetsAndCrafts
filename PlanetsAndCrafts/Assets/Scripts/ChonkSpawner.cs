using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChonkSpawner : MonoBehaviour
{
    public float chonkz;


    public GameObject[] dronks = new GameObject[5];
    public GameObject[] chonks = new GameObject[6];
    public GameObject[] corkos = new GameObject[3];
    public GameObject[] bits = new GameObject[6];


    public bool chonksSpawned = false;
    public Tools tool;


    public void ChonksLoad()
    {
        DeleteChonks();
        tool._state = State.Hand;
        chonksSpawned = false;
        RandomiseBits();
    }

    void Update()
    {
        bool chonksComplete = true;
        if (chonksSpawned)
        {
            foreach (GameObject b in bits)
            {

                if (b.tag == "Piece" && b.GetComponent<Core>().ReturnTopParent().tag != "Core")
                {
                    chonksComplete = false;
                }

            }
            if (chonksComplete == true)
            {
                Debug.Log("CHONKS COMPLETE!"); // replace this with a win condition 
                tool.Win();
            }
        }
    }

    void RandomiseBits()
    {
        bits = new GameObject[6];
        Debug.Log("Chonk Spawned");
        GameObject go = Instantiate(corkos[Random.Range(0, 3)], new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), chonkz), Quaternion.identity);
        bits[0] = go;


        for(int i = 1; i <3; i++)
        {
            go = Instantiate(chonks[Random.Range(0, 6)]);
            go.transform.position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), chonkz);
            go.GetComponent<DragGameSprite>()._tool = tool;
            bits[i] = go;
        }

        //add three chonks
        for(int i = 3; i < 6; i++)
        {
            go = Instantiate(dronks[Random.Range(0, 5)]);
            go.transform.position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), chonkz);
            go.GetComponent<DragGameSprite>()._tool = tool;
            bits[i] = go;
        }


        //SpawnBits(bits);
        chonksSpawned = true;
    }

    private void DeleteChonks()
    {
        for (int i = 0; i < bits.Length; i++)
        {
            if (bits[i] != null)
            {
                Destroy(bits[i]);
            }
        }
    }

    //void SpawnBits(GameObject[] bits)
    //{
    //    foreach (GameObject b in bits)
    //    {
    //        Debug.Log("Chonk Spawned");
    //        GameObject go = Instantiate(b);
    //        go.transform.position = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);
    //    }
    //}
}
