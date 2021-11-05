using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!GameData.Instance.cl)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
