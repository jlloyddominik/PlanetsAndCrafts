using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChonkSpawner : MonoBehaviour
{
    public float chonkz;

    public LayerMask mask;

    public GameObject[] dronks = new GameObject[5];
    public float dronksMinimumSizeMultiplier;
    public float dronksMaximumSizeMultiplier;
    public GameObject[] chonks = new GameObject[6];
    public float chonksMinimumSizeMultiplier;
    public float chonksMaximumSizeMultiplier;
    public GameObject[] corkos = new GameObject[3];
    public float corkoMinimumSizeMultiplier;
    public float corkoMaximumSizeMultiplier;
    public GameObject[] bits = new GameObject[6];

    private Vector3[] positions = new Vector3[6];
    private float[] scalars = new float[6];

    public Color[] colors;


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
        go.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
        bits[0] = go;
        scalars[0] = Random.Range(corkoMinimumSizeMultiplier, corkoMaximumSizeMultiplier);
        go.transform.localScale *= scalars[0];
        go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        positions[0] = go.transform.position;


        for(int i = 1; i <3; i++)
        {
            go = Instantiate(chonks[Random.Range(0, 6)]);
            positions[i] = GeneratedEmptyPosition(i);
            go.transform.position = positions[i];
            go.GetComponent<DragGameSprite>()._tool = tool;
            go.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
            bits[i] = go;
            scalars[i] = Random.Range(chonksMinimumSizeMultiplier, chonksMaximumSizeMultiplier);
            go.transform.localScale *= scalars[i];
            go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

        }

        //add three chonks
        for (int i = 3; i < 6; i++)
        {
            go = Instantiate(dronks[Random.Range(0, 5)]);
            positions[i] = GeneratedEmptyPosition(i);
            go.transform.position = positions[i];
            go.GetComponent<DragGameSprite>()._tool = tool;
            go.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
            bits[i] = go;
            scalars[i] = Random.Range(dronksMinimumSizeMultiplier, dronksMaximumSizeMultiplier);
            go.transform.localScale *= scalars[i];
            go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

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

    private Vector3 GeneratedEmptyPosition(int i)
    {
        Vector3 position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), chonkz);
        if (Vector3.Distance(position, positions[0]) < 2.5 * scalars[0]) return GeneratedEmptyPosition(i);
        for (int it = 1; it < i; it++)
        {
            if (Vector3.Distance(position, positions[it]) < scalars[i]) return GeneratedEmptyPosition(i);
        }
        return position;
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
