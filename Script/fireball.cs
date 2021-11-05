using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : Entity
{
    bool ss;
    public void SetArrow(bool right)
    {
        if(right)
        {
            ss = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            ss = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override void StartTurn()
    {
        if (ss)
        {

            if (manager.characterBlock.x == position.x + 1 && manager.characterBlock.y == position.y)
            {
                manager.MobAttack(1);
                manager.remob(this);
            }
            Move(1, 0);
            manager.Danger(position.x + 1, position.y);
        }
        else
        {
            if (manager.characterBlock.x == position.x - 1 && manager.characterBlock.y == position.y)
            {
                manager.MobAttack(1);
                manager.remob(this);
            }
            Move(-1, 0);
            manager.Danger(position.x - 1, position.y);
        }
    }
}
