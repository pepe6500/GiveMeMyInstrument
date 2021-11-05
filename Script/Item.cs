using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int kind;
    Item(GameManager _mana)
    {
        manager = _mana;
    }

    public GameManager manager;

    virtual public void Attack()
    {
        return;
    }
    virtual public void Move(int a)
    {
        return;
    }
    virtual public void MoveUp(int a)
    {
        return;
    }
    virtual public void GetItem()
    {
        return;
    }
    virtual public void StartTurn()
    {
        return;
    }
    virtual public void EndTurn()
    {
        return;
    }
    virtual public void OnHit()
    {
        return;
    }
}
