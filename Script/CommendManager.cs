using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Commend
{
    public int[] commend = new int[3];
    public override bool Equals(object other)
    {
        Commend _commend = other as Commend;
        for (int i = 0; i < 3; i++)
        {
            if (commend[i] != _commend.commend[i])
            {
                return false;
            }
        }
        return true;
    }

    public void Reset()
    {
        for (int i = 0; i < 3; i++)
        {
            commend[i] = 0;
        }
    }
}

public class CommendManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource tickSource;
    [SerializeField] AudioClip tick;
    [SerializeField] AudioClip tock;
    [SerializeField] float startTime;
    [SerializeField] int count = 0;
    [SerializeField] Commend commend = new Commend();
    [SerializeField] Commend moveLCommend;
    [SerializeField] Commend moveRCommend;
    [SerializeField] Commend moveUpCommend;
    [SerializeField] Commend moveDownCommend;
    [SerializeField] Commend attackLCommend;
    [SerializeField] Commend attackRCommend;
    [SerializeField] int[][] commendList;
    [SerializeField] public float BPM;
    [SerializeField] float judgmentTime;
    [SerializeField] float delayTime;
    [SerializeField] bool turnAct = false;
    bool tickAct;
    bool turnGone;
    bool pro;
    [SerializeField] SpriteRenderer judspr;
    [SerializeField] UnityEngine.UI.Image judimg;
    [SerializeField] Sprite miss;
    Sprite arrow;
    // Start is called before the first frame update
    void Start()
    {
        turnGone = true;
        turnAct = false;
        tickAct = false;
        pro = false;
        arrow = judspr.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (BGM.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                InputCommend(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                InputCommend(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                InputCommend(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                InputCommend(4);
            }
            if (turnAct)
            {
                if ((BGM.time - startTime) % (60 / BPM) > judgmentTime && ((60 / BPM) - (BGM.time - startTime) % (60 / BPM)) > judgmentTime)
                {
                    //Debug.Log("a : " + (BGM.time - startTime) % (60 / BPM));
                    //Debug.Log("b : " + ((60 / BPM) - (BGM.time - startTime) % (60 / BPM)));
                    if (count == 3)
                    {
                        count = 0;
                    }
                    else
                    {
                        //Debug.Log("턴쉼 " + count);
                        judspr.sprite = miss;
                        judspr.color = new Color(1, 1, 1, 1);
                        judspr.flipX = false;
                        judspr.flipY = false;
                        Vector3 temp = judspr.transform.localPosition;
                        temp.y = 3.4f;
                        judspr.transform.localPosition = temp;
                        commend.commend[count] = 0;
                        count++;
                    }
                    turnAct = false;
                }
            }
            else
            {
                if (((60 / BPM) - (BGM.time - startTime) % (60 / BPM)) <= judgmentTime && turnGone && !pro)
                {
                    //Debug.Log("a : " + (BGM.time - startTime) % (60 / BPM));
                    //Debug.Log("b : " + ((60 / BPM) - (BGM.time - startTime) % (60 / BPM)));
                    //Debug.Log("턴시작");
                    turnAct = true;
                    tickAct = true;
                    turnGone = false;
                    pro = true;
                }
            }
            if (tickAct)
            {
                if ((BGM.time - startTime) % (60 / BPM) < ((60 / BPM) - (BGM.time - startTime) % (60 / BPM)))
                {
                    //Debug.Log("a : " + (BGM.time - startTime) % (60 / BPM));
                    //Debug.Log("b : " + ((60 / BPM) - (BGM.time - startTime) % (60 / BPM)));
                    //Debug.Log("타이밍");
                    if (count == 3 && !turnGone)
                    {
                        if (commend.Equals(moveLCommend))
                        {
                            gameManager.MovePlayer(-1);
                        }
                        else if (commend.Equals(moveRCommend))
                        {
                            gameManager.MovePlayer(1);
                        }
                        else if (commend.Equals(attackLCommend))
                        {
                            gameManager.PlayerAttack(gameManager.characterBlock.x - 1, gameManager.characterBlock.y);
                            gameManager.PlayerAttack(gameManager.characterBlock.x - 2, gameManager.characterBlock.y);
                            if(gameManager.s2)
                            {
                                gameManager.PlayerAttack(gameManager.characterBlock.x - 3, gameManager.characterBlock.y);
                            }
                            gameManager.character.GetComponent<Animator>().SetTrigger("attack");
                        }
                        else if (commend.Equals(attackRCommend))
                        {
                            gameManager.PlayerAttack(gameManager.characterBlock.x + 1, gameManager.characterBlock.y);
                            gameManager.PlayerAttack(gameManager.characterBlock.x + 2, gameManager.characterBlock.y);
                            gameManager.character.GetComponent<Animator>().SetTrigger("attack");
                            if (gameManager.s2)
                            {
                                gameManager.PlayerAttack(gameManager.characterBlock.x + 3, gameManager.characterBlock.y);
                            }
                        }
                        else if (commend.Equals(moveUpCommend))
                        {
                            gameManager.MovePlayerUp(1);
                        }
                        else if (commend.Equals(moveDownCommend))
                        {
                            gameManager.MovePlayerUp(-1);
                        }
                        judimg.color = new Color(255, 255, 0, 1);
                        tickSource.PlayOneShot(tock);
                        commend.Reset();
                        gameManager.TurnEnd();
                    }
                    else
                    {
                        judimg.color = new Color(255, 255, 255, 1);
                        tickSource.PlayOneShot(tick);
                    }
                    turnGone = true;
                    tickAct = false;
                    pro = false;
                }
            }
        }
        if (judspr.color.a > 0)
        {
            judspr.color = judspr.color - new Color(0, 0, 0, 2 * Time.deltaTime);
        }
        if (judspr.transform.localPosition.y < 4.7f)
        {
            judspr.transform.localPosition += Vector3.up * Time.deltaTime * 3;
        }
        {
            judspr.color = judspr.color - new Color(0, 0, 0, 2 * Time.deltaTime);
        }
        if (judimg.color.a > 0)
        {
            judimg.color = judimg.color - new Color(0, 0, 0, 4 * Time.deltaTime);
        }
    }

    public void InputCommend(int num)
    {
        if ((((BGM.time - startTime + delayTime) % (60 / BPM) <= judgmentTime) || (((60 / BPM) - (BGM.time - startTime + delayTime) % (60 / BPM)) <= judgmentTime)) && count != 3)
        {
            //Debug.Log("성공 " + count);
            judspr.sprite = arrow;
            if (num == 1)
            {
                judspr.color = new Color(0, 1, 0.3f, 1f);
                judspr.flipX = true;
                judspr.flipY = false;
            }
            else if (num == 2)
            {
                judspr.color = new Color(0, 0.73f, 1, 1f);
                judspr.flipX = true;
                judspr.flipY = true;
            }
            else if (num == 3)
            {
                judspr.color = new Color(1, 1, 0, 1f);
                judspr.flipX = false;
                judspr.flipY = true;
            }
            else
            {
                judspr.color = new Color(1, 0, 0.1f, 1f);
                judspr.flipX = false;
                judspr.flipY = false;
            }

            Vector3 temp = judspr.transform.localPosition;
            temp.y = 3.4f;
            judspr.transform.localPosition = temp;
            if (turnAct)
            {
                commend.commend[count] = num;
                count++;
                turnGone = true;
            }
            turnAct = false;
        }
        else
        {
            //Debug.Log("a : " + (BGM.time - startTime + delayTime) % (60 / BPM));
            //Debug.Log("b : " + ((60 / BPM) - (BGM.time - startTime + delayTime) % (60 / BPM)));
            Debug.Log("실패 " + count);
            judspr.sprite = miss;
            judspr.color = new Color(1, 1, 1, 1);
            judspr.flipX = false;
            judspr.flipY = false;
            Vector3 temp = judspr.transform.localPosition;
            temp.y = 3.4f;
            judspr.transform.localPosition = temp;
            if (turnAct && count != 3)
            {
                commend.commend[count] = 0;
                count++;
                turnAct = false;
                turnGone = true;
            }
        }
    }
}
