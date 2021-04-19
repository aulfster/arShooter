using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject soundsGameObject;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sources = soundsGameObject.GetComponents<AudioSource>();

        if (sources.Length >= 2)
        {
            sources[0].Stop();
            sources[1].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
