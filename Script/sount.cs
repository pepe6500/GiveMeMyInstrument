using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sount : MonoBehaviour
{
    public UnityEngine.UI.Slider aa;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = aa.value;
    }
}
