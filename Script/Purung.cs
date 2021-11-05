using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purung : Entity
{
    int Acount = 0;
    public override void StartTurn()
    {
        if (count >= 1)
        {
            if (Acount < 1)
            {
                if (arrow == 1)
                {
                        Move(1, 0);
                }
                else if (arrow == 0)
                {
                        Move(-1, 0);
                }
                else if (arrow == 2)
                {
                    Move(0, 1);
                }
                else if (arrow == 3)
                {
                    Move(0, -1);
                }
                count = 0;
                Acount++;
            }
            else
            {
                GetComponent<Animator>().SetTrigger("attack");
                if (arrow == 1)
                {
                    if ((manager.characterBlock.x == position.x + 1 || manager.characterBlock.x == position.x + 2) && manager.characterBlock.y == position.y)
                    {
                        manager.MobAttack(1);
                    }
                }
                else if (arrow == 0)
                {
                    if ((manager.characterBlock.x == position.x - 1 || manager.characterBlock.x == position.x - 2) && manager.characterBlock.y == position.y)
                    {
                        manager.MobAttack(1);
                    }
                }
                Acount = 0;
            }
        }
        else
        {
            if (Acount < 1)
            {
                if (manager.characterBlock.y < position.y)
                {
                    manager.Danger(position.x, position.y - 1);
                    arrow = 3;
                }
                else if (manager.characterBlock.y > position.y)
                {
                    manager.Danger(position.x, position.y + 1);
                    arrow = 2;
                }
                else if (manager.characterBlock.x > position.x)
                {
                    manager.Danger(position.x + 1, position.y);
                    transform.localScale = new Vector3(-1, 1, 1);
                    arrow = 1;
                }
                else if (manager.characterBlock.x < position.x)
                {
                    manager.Danger(position.x - 1, position.y);
                    transform.localScale = Vector3.one;
                    arrow = 0;
                }
                count++;
            }
            else
            {
                if (manager.characterBlock.x > position.x)
                {
                    manager.Danger(position.x + 1, position.y);
                    manager.Danger(position.x + 2, position.y);
                    transform.localScale = new Vector3(-1, 1, 1);
                    arrow = 1;
                }
                else
                {
                    manager.Danger(position.x - 1, position.y);
                    manager.Danger(position.x - 2, position.y);
                    transform.localScale = Vector3.one;
                    arrow = 0;
                }
                count++;
            }
        }
    }
}
