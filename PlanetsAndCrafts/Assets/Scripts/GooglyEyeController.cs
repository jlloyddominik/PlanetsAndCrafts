using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglyEyeController : MonoBehaviour
{
public Transform myObject;

    void Start()
    {
        Vector3 randomDirection = new Vector3(0f, 0f, Random.Range(-359, 359));
        myObject.Rotate(randomDirection);

        myObject.GetComponent<Rigidbody>().AddForce(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), ForceMode.Impulse);
    }
}
