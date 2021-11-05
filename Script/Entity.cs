using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Pos position = new Pos();
    public GameManager manager;
    public bool move;
    public float moveTimer;
    public float playerMoveJumpPower = 1;
    public Vector2 movePosition;
    public Vector2 charPosition;

    public int arrow;

    public int count;

    public int hp;

    public Pos Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            transform.localPosition = new Vector3(manager.GetBlockSize().x * value.x, manager.GetBlockSize().y * value.y);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Vector2 tempPo;
            moveTimer += Time.deltaTime;
            float beatTime = 30 / manager.commend.BPM;
            if (beatTime <= moveTimer)
            {
                move = false;
                tempPo = movePosition;
                moveTimer = 0;
            }
            else
            {
                if (moveTimer <= beatTime / 2)
                {
                    tempPo.y = charPosition.y + playerMoveJumpPower * (moveTimer / (beatTime / 2));
                }
                else
                {
                    tempPo.y = (charPosition.y + playerMoveJumpPower) - playerMoveJumpPower * ((moveTimer - (beatTime / 2)) / (beatTime / 2));
                }
                tempPo.x = charPosition.x + (movePosition.x - charPosition.x) * (moveTimer / beatTime);
            }
            transform.localPosition = tempPo;
        }
    }

    public virtual void Move(int x,int y)
    {
        charPosition = transform.localPosition;
        position.x += x;
        position.y += y;
        movePosition = charPosition;
        movePosition.x = manager.GetBlockSize().x * position.x;
        movePosition.y = manager.GetBlockSize().y * position.y;
        move = true;
    }

    public virtual void StartTurn()
    {
        if (count >= 1) 
        {
            Debug.Log("adsfasf");
            if (arrow == 1)
            {
                if (manager.characterBlock.x == position.x + 1 && manager.characterBlock.y == position.y)
                {
                    GetComponent<Animation>().Play("mob_attack"); manager.MobAttack(1);
                }
                else
                {
                    Move(1,0);
                }
            }
            else if(arrow == 0)
            {
                if (manager.characterBlock.x == position.x - 1 && manager.characterBlock.y == position.y)
                {
                    GetComponent<Animation>().Play("mob_attack"); manager.MobAttack(1);
                }
                else
                {
                    Move(-1,0);
                }
            }
            else if (arrow == 2)
            {
                    Move(0,1);
            }
            else if (arrow == 3)
            {
                Move(0, -1);
            }
            count = 0;
        }
        else
        {
            if(manager.characterBlock.y < position.y)
            {
                manager.Danger(position.x, position.y - 1);
                arrow = 3;
            }
            else if(manager.characterBlock.y > position.y)
            {
                manager.Danger(position.x, position.y + 1);
                arrow = 2;
            }
            else if (manager.characterBlock.x > position.x && manager.characterBlock.y == position.y)
            {
                manager.Danger(position.x + 1, position.y);
                transform.localScale = new Vector3(-1, 1,1);
                arrow = 1;
            }
            else if(manager.characterBlock.x < position.x && manager.characterBlock.y == position.y)
            {
                manager.Danger(position.x - 1, position.y);
                transform.localScale = Vector3.one;
                arrow = 0;
            }
            count++;
        }
    }

    public void SetManager(GameManager _manager)
    {
        manager = _manager;
    }

    public void attacked(int a)
    {
        hp -= a;
        if(hp <= 0)
        {
            manager.remob(this);
        }
    }
}
