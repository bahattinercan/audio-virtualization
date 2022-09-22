using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instatiate512Cubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    GameObject[] sampleCubes = new GameObject[512];
    private float maxScale=1000;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject Go = Instantiate(sampleCubePrefab);
            Go.transform.position = this.transform.position;
            Go.transform.parent = this.transform;
            Go.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -.703125f*i, 0);
            Go.transform.position = Vector3.forward * 100;
            sampleCubes[i] = Go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (sampleCubes != null)
            {
                sampleCubes[i].transform.localScale = new Vector3(1, ((AudioPeer.audioBandBuffer[i] * maxScale) + 2), 1);
            }
        }
    }
}
