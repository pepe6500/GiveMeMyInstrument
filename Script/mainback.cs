using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainback : MonoBehaviour
{
    public Sprite a;
    // Start is called before the first frame update
    void Start()
    {
        if(GameData.Instance.cl)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = a;
        }
        GameData.Instance.cl = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
