using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectone : MonoBehaviour
{

    public Image[] aa;
    public Sprite[] spr;
    public Sprite[] spr_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(int a)
    {
        for(int i = 0; i < aa.Length; i++)
        {
            if(a == i)
            {
                aa[i].sprite = spr_[i];
            }
            else
            {
                aa[i].sprite = spr[i];
            }
        }
    }
}
