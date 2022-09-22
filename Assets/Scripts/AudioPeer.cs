using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource audioSource;
    float[] samples = new float[512];
    float[] freqBands = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];
    private float highestBand =0;
    private float spawnDelay = .2f;
    private bool canSpawn = true;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();

        CheckForHighestBand();
        SpawnCube();
        
    }

    void CheckForHighestBand()
    {
        highestBand = 0;
        for (int i = 0; i < 8; i++)
        {
            float band = audioBand[i];
            if (band > highestBand)
                highestBand = band;

            Debug.Log("HighestBand:" + highestBand);
        }
    }

    void SpawnCube()
    {
        if (highestBand > .75f && canSpawn)
        {
            canSpawn = false;
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = spawnPoint.position;
            go.AddComponent<Rigidbody>();
            StartCoroutine(SpawnDelayCo());
        }
    }

    IEnumerator SpawnDelayCo()
    {              
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBands[i];
            }
            audioBand[i] = (freqBands[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples,0,FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (freqBands[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBands[g];
                bufferDecrease[g] = .003f;
            }
            if (freqBands[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        /*
         * 22050 / 512 = 43 hertz per sample
         * 
         * 0-2 = 86 hertz
         * 1-4 = 172 hertz 87-258
         * 2-8 = 344 hertz 259-602
         * 3-16 = 688 hertz 603-1290
         * 4-32 = 1376 hertz 1291-2666
         * 5-64 = 2572 hertz 2667-5418
         * 6-128 = 5504 hertz 5419-10922
         * 7-256 = 11008 hertz 10923-21930
         */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            
            if (i == 7)
                sampleCount += 2;
            
            for (int j = 0; j < sampleCount ; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            freqBands[i] = average * 10;
        }
    }
}
