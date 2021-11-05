using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfireb : Entity
{
    int counta = 0;
    bool ar;

    private void Start()
    {
        if(position.x > 0)
        {
            ar = false;
        }
        else
        {
            ar = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void StartTurn()
    {
        counta++;
        if(counta >=6)
        {
            manager.Creatfire(position.x, position.y, ar);
            counta = 0;
        }
    }
}
