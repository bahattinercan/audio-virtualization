using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int band;
    float startScale=1, scaleMultiplier=5;
    public bool useBuffer;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (useBuffer)
        {
            if (float.IsNaN(AudioPeer.audioBandBuffer[band]))
                AudioPeer.audioBandBuffer[band] = 1;
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z);
            float audioFloat = AudioPeer.audioBandBuffer[band];
            Color color = new Color(audioFloat, audioFloat, audioFloat);
            material.SetColor("_EmissionColor", color);
        }
        else
        {
            if (float.IsNaN(AudioPeer.audioBand[band]))
                AudioPeer.audioBand[band] = 1;
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
            float audioFloat = AudioPeer.audioBand[band];
            Color color = new Color(audioFloat, audioFloat, audioFloat);
            material.SetColor("_EmissionColor", color);
        }       
           
    }
}
