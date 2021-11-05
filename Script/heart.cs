using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class heart : MonoBehaviour
{
    int RememberHP;
    public int MaxHP = 8;
    int hp = 10;

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (value >= 0)
            {
                hp = value;
                setHeart(value);
            }
            else
            {
                hp = 0;
                setHeart(0);
            }
        }
    }

    public Sprite FullHeartSprite;
    public Sprite HalfHeartSprite;
    public Sprite BlankHeartSpite;

    void Start()
    {
        RememberHP = MaxHP;
        setHeart(HP);
    }


    void Update()
    {

    }

    void setHeart(int HP)
    {
        if(RememberHP != MaxHP)     //최대체력 변경시 오브젝트 추가
        {
            for(int i=0; i<MaxHP-RememberHP/2; i++)
            {
                //Image obj = Instantiate(FullHeart, new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z), Quaternion.identity);
                //obj.gameObject.transform.parent = gameObject.transform;
                GameObject obj = new GameObject("FullHeart");
                obj.gameObject.AddComponent<Image>();
                obj.gameObject.GetComponent<Image>().sprite = FullHeartSprite;
                obj.gameObject.transform.parent = gameObject.transform;
                obj.tag = "FullHeart";
                obj.gameObject.transform.localScale = Vector3.one;
                //Image obj2 = Instantiate(HalfHeart, new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z), Quaternion.identity);
                //obj2.gameObject.transform.parent = gameObject.transform;
                GameObject obj2 = new GameObject("HalfHeart");
                obj2.gameObject.AddComponent<Image>();
                obj2.gameObject.GetComponent<Image>().sprite = HalfHeartSprite;
                obj2.gameObject.transform.parent = gameObject.transform;
                obj2.tag = "HalfHeart";
                obj2.gameObject.transform.localScale = Vector3.one;
                //Image obj3 = Instantiate(BlankHeart, new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z), Quaternion.identity);
                //obj3.gameObject.transform.parent = gameObject.transform;
                GameObject obj3 = new GameObject("BlankHeart");
                obj3.gameObject.AddComponent<Image>();
                obj3.gameObject.GetComponent<Image>().sprite = BlankHeartSpite;
                obj3.gameObject.transform.parent = gameObject.transform;
                obj3.gameObject.transform.localScale = Vector3.one;
                obj3.tag = "BlankHeart";
            }
        }

        if (gameObject.transform.childCount == 0)  //맨처음 생성
        {
            for(int i=0; i<MaxHP/2; i++)
            {
                //Image obj = Instantiate(FullHeart, new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z), Quaternion.identity);
                //obj.gameObject.transform.parent = gameObject.transform;
                GameObject obj = new GameObject("FullHeart");
                obj.gameObject.AddComponent<Image>();
                obj.gameObject.GetComponent<Image>().sprite = FullHeartSprite;
                obj.gameObject.GetComponent<Image>().SetNativeSize();
                obj.gameObject.transform.parent = gameObject.transform;
                obj.tag = "FullHeart";
                //Image obj2 = Instantiate(HalfHeart, new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z), Quaternion.identity);
                //obj2.gameObject.transform.parent = gameObject.transform;
                GameObject obj2 = new GameObject("HalfHeart");
                obj2.gameObject.AddComponent<Image>();
                obj2.gameObject.GetComponent<Image>().sprite = HalfHeartSprite;
                obj2.gameObject.GetComponent<Image>().SetNativeSize();
                obj2.gameObject.transform.parent = gameObject.transform;
                obj2.tag = "HalfHeart";
                //Image obj3 = Instantiate(BlankHeart, new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z), Quaternion.identity);
                //obj3.gameObject.transform.parent = gameObject.transform;
                GameObject obj3 = new GameObject("BlankHeart");
                obj3.gameObject.AddComponent<Image>();
                obj3.gameObject.GetComponent<Image>().sprite = BlankHeartSpite;
                obj3.gameObject.GetComponent<Image>().SetNativeSize();
                obj3.gameObject.transform.parent = gameObject.transform;
                obj3.tag = "BlankHeart";
            }
            RememberHP = MaxHP;
        }
        
        for(int k=0; k<gameObject.transform.childCount; k++)    //오브젝트 초기화
        {
           gameObject.transform.GetChild(k).gameObject.SetActive(false);
        }
        for (int i = 1; i <= HP / 2; i++)
        {
            for (int c = 0; c < gameObject.transform.childCount; c++)  // 꽉찬하트 생성
            {
                if (gameObject.transform.GetChild(c).tag == "FullHeart"&& gameObject.transform.GetChild(c).gameObject.activeSelf==false)
                {
                    gameObject.transform.GetChild(c).gameObject.SetActive(true);
                    gameObject.transform.GetChild(c).gameObject.transform.localPosition = new Vector3(transform.position.x + i * 100, transform.position.y, transform.position.z);
                        gameObject.transform.GetChild(c).gameObject.transform.localScale = Vector3.one;
                    break;
                }
            }

            if (HP % 2 == 1 && i == HP / 2)     // 반칸하트 생성
            {
                for (int v = 0; v < gameObject.transform.childCount; v++)
                {
                    if (gameObject.transform.GetChild(v).tag == "HalfHeart" && gameObject.transform.GetChild(v).gameObject.activeSelf == false)
                    {
                        gameObject.transform.GetChild(v).gameObject.SetActive(true);
                        gameObject.transform.GetChild(v).gameObject.transform.localPosition = new Vector3(transform.position.x + (i+1) * 100, transform.position.y, transform.position.z);
                        gameObject.transform.GetChild(v).gameObject.transform.localScale = Vector3.one;
                        break;
                    }
                }
            }
             if (i == HP / 2)   // 빈칸하트 생성
            {
                for (int h=0; h<(MaxHP-HP)/2; h++)
                {

                    for (int b = 0; b < gameObject.transform.childCount; b++)
                    {
                        if (gameObject.transform.GetChild(b).tag == "BlankHeart" && gameObject.transform.GetChild(b).gameObject.activeSelf == false)
                        {
                            gameObject.transform.GetChild(b).gameObject.SetActive(true);
                            gameObject.transform.GetChild(b).gameObject.transform.localPosition = new Vector3(transform.position.x + (MaxHP / 2 - h) * 100, transform.position.y, transform.position.z);
                        gameObject.transform.GetChild(b).gameObject.transform.localScale = Vector3.one;
                            break;
                        }
                    }
                }
            }
        }
        if (HP == 1)    // 체력 1일때 (특수 경우)
        {

            for (int v = 0; v < gameObject.transform.childCount; v++)
            {
                if (gameObject.transform.GetChild(v).tag == "HalfHeart" && gameObject.transform.GetChild(v).gameObject.activeSelf == false)
                {
                    gameObject.transform.GetChild(v).gameObject.SetActive(true);
                    gameObject.transform.GetChild(v).gameObject.transform.localPosition = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);
                        gameObject.transform.GetChild(v).gameObject.transform.localScale = Vector3.one;
                    break;
                }
            }
            for (int h = 0; h < (MaxHP - HP) / 2; h++)
            {
                for (int b = 0; b < gameObject.transform.childCount; b++)
                {
                    if (gameObject.transform.GetChild(b).tag == "BlankHeart" && gameObject.transform.GetChild(b).gameObject.activeSelf == false)
                    {
                        gameObject.transform.GetChild(b).gameObject.SetActive(true);
                        gameObject.transform.GetChild(b).gameObject.transform.localPosition = new Vector3(transform.position.x + (MaxHP / 2 - h) * 100, transform.position.y, transform.position.z);
                        gameObject.transform.GetChild(b).gameObject.transform.localScale = Vector3.one;
                        break;
                    }
                }
            }
        }
        if (HP <= 0)    // 체력 0 이하 일 때 모두 빈칸 (특수 경우)
        {
            for (int h = 0; h < (MaxHP - HP) / 2; h++)
            {
                for (int b = 0; b < gameObject.transform.childCount; b++)
                {
                    if (gameObject.transform.GetChild(b).tag == "BlankHeart" && gameObject.transform.GetChild(b).gameObject.activeSelf == false)
                    {
                        gameObject.transform.GetChild(b).gameObject.SetActive(true);
                        gameObject.transform.GetChild(b).gameObject.transform.localPosition = new Vector3(transform.position.x + (MaxHP / 2 - h) * 100, transform.position.y, transform.position.z);
                        gameObject.transform.GetChild(b).gameObject.transform.localScale = Vector3.one;
                        break;
                    }
                }
            }
        }
    }
    public void Hit()
    {
        HP--;
    }
}
