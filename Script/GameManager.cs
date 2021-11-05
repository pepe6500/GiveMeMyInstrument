using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class MobTimming
{
    public int time;
    public int kind;
    public int x;
    public int y;
}

    public class Pos
    {
    public override bool Equals(object obj)
    {
        Pos _pos = obj as Pos;
        if(_pos.x == x && _pos.y == y)
        {
            return true;
        }
        return false;
    }
    public int x;
        public int y;
    }
public class GameManager : MonoBehaviour
{

    [SerializeField] public CommendManager commend;
    [SerializeField] GameObject block_Prefab;
    [SerializeField] GameObject attack_Par;
    [SerializeField] GameObject monster_Pre;
    [SerializeField] GameObject monster_Pre1;
    [SerializeField] GameObject monster_Pre2;
    [SerializeField] Vector2 blockSize;
    public GameObject character;
    [SerializeField] GameObject danger_Pre;
    Vector2 charPosition;
    Vector2 movePosition;
    bool playerMove = false;
    float playerMoveTimer = 0;
    public Pos characterBlock = new Pos();
    [SerializeField] float playerMoveJumpPower;
    [SerializeField] heart heart;
    List<Entity> mobs = new List<Entity>();
    List<GameObject> dangers = new List<GameObject>();

    public int nowturn;
    public MobTimming[] mobTimmings;
    int mobtimmingcount = 0;

    public List<Item> items = new List<Item>();
    public string nextScene;
    public SceneLoadManager manager;
    public int endTurn;

    public GameObject fireball;

    public bool s2;
    
    public Animation sil;
    int silc = 3;
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 11; i++)
            {
                GameObject temp = Instantiate(block_Prefab);
                temp.transform.parent = transform;
                temp.transform.localPosition = new Vector3(((i - 5) * blockSize.x), blockSize.y * 2 - j,j*-1f);
            }
        }
        //if (singl.singlt)
        //{
        //    for (int i = 0; i < singl.singlt.items.Count; i++)
        //    {
        //        GetItem(singl.singlt.items[i].kind);
        //    }
        //}
    }

    public void remob(Entity a)
    {
        mobs.Remove(a);
        Destroy(a.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove)
        {
            Vector2 tempPo;
            playerMoveTimer += Time.deltaTime;
            float beatTime = 30 / commend.BPM;
            if (beatTime <= playerMoveTimer)
            {
                playerMove = false;
                tempPo = movePosition;
                playerMoveTimer = 0;
            }
            else
            {
                tempPo = charPosition + (movePosition - charPosition) * (playerMoveTimer / beatTime);
            }
            character.transform.localPosition = tempPo;
        }
    }

    public void MovePlayer(int _block)
    {
        if (characterBlock.x + _block > 4)
        {
            _block = 0;
        }
        else if (characterBlock.x + _block < -4)
        {
            _block = 0;
        }
        characterBlock.x += _block;
        movePosition = new Vector2((character.transform.localPosition.x + blockSize.x * _block), character.transform.localPosition.y);
        charPosition = character.transform.localPosition;
        Debug.Log(charPosition);
        playerMoveTimer = 0;
        playerMove = true;
    }

    public void MovePlayerUp(int _block)
    {
        if (characterBlock.y + _block > 2)
        {
            _block = 0;
        }
        else if (characterBlock.y + _block < 0)
        {
            _block = 0;
        }
        characterBlock.y += _block;
        movePosition = new Vector2(character.transform.localPosition.x, (character.transform.localPosition.y + blockSize.y * _block));
        charPosition = character.transform.localPosition;
        Debug.Log(charPosition);
        playerMoveTimer = 0;
        playerMove = true;
    }

    public void PlayerAttack(Pos _block)
    {
        GameObject temp = Instantiate(attack_Par);
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector2(blockSize.x * _block.x,blockSize.y * _block.y);
        foreach (Entity mob in mobs)
        {
            if(mob.Position == _block)
            {
                mob.attacked(1);
            }
        }
    }
    public void PlayerAttack(int x,int y)
    {
        GameObject temp = Instantiate(attack_Par);
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector2(blockSize.x * x, blockSize.y * y);
        foreach (Entity mob in mobs)
        {
            if (mob.Position.x == x && mob.Position.y == y)
            {
                if (s2)
                {
                    mob.attacked(2);
                }
                else
                {
                    mob.attacked(1);
                }
            }
        }
    }

    public void MobAttack(int power)
    {
        if (!s2)
        {
            heart.HP -= power;
        }
        else
        {
            if(silc > 0)
            {
                silc--;
                sil.Play();
            }
            else
            {
                heart.HP -= power;
            }
        }

    }

    public void CreatMonter(int x, int y)
    {
        GameObject temp = Instantiate(monster_Pre);
        temp.GetComponent<Entity>().SetManager(this);
        temp.transform.parent = transform;
        Pos _pos = new Pos();
        _pos.x = x;
        _pos.y = y;
        temp.GetComponent<Entity>().Position = _pos;
        mobs.Add(temp.GetComponent<Entity>());
    }
    public void CreatMonter1(int x, int y)
    {
        GameObject temp = Instantiate(monster_Pre1);
        temp.GetComponent<Entity>().SetManager(this);
        temp.transform.parent = transform;
        Pos _pos = new Pos();
        _pos.x = x;
        _pos.y = y;
        temp.GetComponent<Entity>().Position = _pos;
        mobs.Add(temp.GetComponent<Entity>());
    }
    public void CreatMonter2(int x, int y)
    {
        GameObject temp = Instantiate(monster_Pre2);
        temp.GetComponent<Entity>().SetManager(this);
        temp.transform.parent = transform;
        Pos _pos = new Pos();
        _pos.x = x;
        _pos.y = y;
        temp.GetComponent<Entity>().Position = _pos;
        mobs.Add(temp.GetComponent<Entity>());
    }

    public void Creatfire(int x, int y, bool right)
    {
        GameObject temp = Instantiate(fireball);
        temp.GetComponent<Entity>().SetManager(this);
        temp.transform.parent = transform;
        Pos _pos = new Pos();
        _pos.x = x;
        _pos.y = y;
        temp.GetComponent<Entity>().Position = _pos;
        mobs.Add(temp.GetComponent<Entity>());
        temp.GetComponent<fireball>().SetArrow(right);
    }

    public Vector2 GetBlockSize()
    {
        return blockSize;
    }

    public void TurnEnd()
    {

        foreach (GameObject temp in dangers)
        {
            temp.SetActive(false);
        }
        foreach (Entity mob in mobs)
        {
            mob.StartTurn();
        }

        if(mobtimmingcount < mobTimmings.Length && nowturn >= mobTimmings[mobtimmingcount].time)
        {
            if(mobTimmings[mobtimmingcount].kind == 1)
            {
                CreatMonter(mobTimmings[mobtimmingcount].x, mobTimmings[mobtimmingcount].y);
            }
            else if(mobTimmings[mobtimmingcount].kind == 2)
            {
                CreatMonter1(mobTimmings[mobtimmingcount].x, mobTimmings[mobtimmingcount].y);
            }
            else
            {
                CreatMonter2(mobTimmings[mobtimmingcount].x, mobTimmings[mobtimmingcount].y);
            }
            mobtimmingcount++;
        }

        if(nowturn >= endTurn)
        {
            End();
        }
        nowturn++;
    }

    public void Danger(Pos _num)
    {
        GameObject danger = null;
        foreach(GameObject temp in dangers)
        {
            if(temp.activeInHierarchy == false)
            {
                danger = temp;
                danger.SetActive(true);
                break;
            }
        }
        if(danger == null)
        {
            danger = Instantiate(danger_Pre);
            dangers.Add(danger);
        }
        danger.transform.parent = transform;
        danger.transform.localPosition = new Vector3(blockSize.x * _num.x, blockSize.y * _num.y);
    }
    public void Danger(int x,int y)
    {
        GameObject danger = null;
        foreach (GameObject temp in dangers)
        {
            if (temp.activeInHierarchy == false)
            {
                danger = temp;
                danger.SetActive(true);
                break;
            }
        }
        if (danger == null)
        {
            danger = Instantiate(danger_Pre);
            dangers.Add(danger);
        }
        danger.transform.parent = transform;
        danger.transform.localPosition = new Vector3(blockSize.x * x, blockSize.y * y);
    }

    public void End()
    {
        //    if(singl.singlt == null)
        //singl.singlt = new singl();
        //singl.singlt.items = items;
        if (!s2)
        {
            manager.LoadScene(nextScene);
        }
        else
        {
            monster_Pre2.SetActive(true);
            commend.enabled = false;
        }
    }

    public void GetItem(int a)
    {
        if(a == 1)
        {

        }
        else if(a == 2)
        {

        }
    }
}
