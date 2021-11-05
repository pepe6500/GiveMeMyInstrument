using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeunDle : MonoBehaviour
{
    public float delay;
    public float dis;
    float timer=0;
    Vector3 ori;

    // Start is called before the first frame update
    void Start()
    {
        ori = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            timer -= delay;
            transform.position = ori + new Vector3(Random.Range(-dis, dis), Random.Range(-dis, dis));
        }
    }
}
