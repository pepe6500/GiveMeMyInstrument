using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class beat : MonoBehaviour {
    /// <summary>
    /// 0 은 없는 상태 , 1 는 왼쪽 클릭 , 2 는 오른쪽 클릭 , 3 는 왼엇박 , 4 는 오른엇박
    /// </summary>
    public int[] Commend = new int[4];
    public int BeatCount;
    float BeatTime;
    public float MaxBeatTime;
    bool BeatDoubleCheck = true;   //정상박 더블 클릭 체크

    public GameObject BeatImage;
    public UnityEvent Beat;
    public UnityEvent BeatHitA;
    public PlayerController player;
    public bool T = true;   //한 박에 두번 치는거 방지
    bool BeatTimerOneClick=true;

    int SyncopationCountA = 0;
    int SyncopationCountB = 0;
    int SyncopationT = 0;
    float SyncopationDoubleChickTimer;      //엇박 더블 클릭 타이머
    float MaxSyncopationDoubleChickTime = 0.1f;
    bool SyncopationDoubleChickCheck;
    int SyncopationDoubleChickCount;    //현재 엇박이 몇개 있는지
    bool SyncopationTripleChickCheck;   //엇박 후 클릭 체크

    bool NoTouch = true;        //박자 쳤는지 안 쳤는지 체크

    float ImageTimer = 0.0f;
    bool ImageTimerON;


    void Start () {
		
	}
	
	void Update () {
        BeatTime += Time.deltaTime;

        if (BeatCount == 4) //모두 정상박으로 쳤을 때
        {
            Debug.Log("커맨드 성공");
            for (int i=0; i<4; i++)
            {
                Commend[i] = 0;
            }
            player.PowerAttack();
            BeatCount = 0;
        }

        for(int i=0; i<4; i++)  //엇박 개수 체크
        {
            if (Commend[i] >= 3)
            {
                SyncopationDoubleChickCount++;
            }
        }
        if (BeatCount >= 3 && SyncopationDoubleChickCount >= 1) //엇박 1개 이상이면 3박 시 체크
        {
            Debug.Log("엇박 커맨드 성공");
            for (int i = 0; i < 4; i++)
            {
                Commend[i] = 0;
            }
            BeatCount = 0;
            SyncopationDoubleChickCount = 0;
        }

        if (SyncopationDoubleChickCheck)    //더블 클릭 딜레이
        {
            SyncopationDoubleChickTimer += Time.deltaTime;
            if (SyncopationDoubleChickTimer >= MaxSyncopationDoubleChickTime)
            {
                SyncopationDoubleChickCheck = false;
                SyncopationDoubleChickTimer = 0;
            }
        }


        if (BeatTime > MaxBeatTime - 0.2f && BeatTime < MaxBeatTime + 0.2f) //정상박 체크
        {
            if (Input.GetMouseButtonDown(0) && T == true && NoTouch)
            {
                BeatHitA.Invoke();
                Commend[BeatCount] = 1;
                BeatCount++;
                T = false;
                NoTouch = false;
            }
            else if (Input.GetMouseButtonDown(0)&&BeatDoubleCheck)  //정상박 더블 클릭
            {
                for (int i = 0; i < 4; i++)
                {
                    Commend[i] = 0;
                }
                BeatCount = 0;
                BeatDoubleCheck = false;
            }
            if (Input.GetMouseButton(1) && T == true && NoTouch)
            {
                Commend[BeatCount] = 2;
                BeatCount++;
                T = false;
                NoTouch = false;
            }
            else if(Input.GetMouseButtonDown(1) && BeatDoubleCheck) //정상박 더블 클릭
            {
                for (int i = 0; i < 4; i++)
                {
                    Commend[i] = 0;
                }
                BeatCount = 0;
                BeatDoubleCheck = false;
            }
        }
        if (BeatTime > 0 && BeatTime < MaxBeatTime - 0.2f)  //엇박
        {
            if (Input.GetMouseButtonDown(0) && !SyncopationDoubleChickCheck && NoTouch)
            {
                SyncopationDoubleChickCheck = true;
                SyncopationCountA++;
            }
            else if (Input.GetMouseButtonDown(0) && SyncopationTripleChickCheck)
            {
                for (int i = 0; i < 4; i++)
                {
                    Commend[i] = 0;
                }
                BeatCount = 0;
            }
            if (Input.GetMouseButtonDown(1) && !SyncopationDoubleChickCheck && NoTouch) 
            {
                SyncopationDoubleChickCheck = true;
                SyncopationCountB++;
            }
            else if (Input.GetMouseButtonDown(1) && SyncopationTripleChickCheck)
            {
                for (int i = 0; i < 4; i++)
                {
                    Commend[i] = 0;
                }
                BeatCount = 0;
            }
        }

        if (SyncopationCountA >= 2) //왼 엇박 체크
        {
            Commend[BeatCount] = 3;
            BeatCount++;
            SyncopationCountA = 0;
            NoTouch = false;
            SyncopationTripleChickCheck = true;
        }
        if (SyncopationCountB >= 2) //오른 엇박 체크
        {
            Commend[BeatCount] = 4;
            BeatCount++;
            SyncopationCountB = 0;
            NoTouch = false;
            SyncopationTripleChickCheck = true;
        }



        if (BeatTime >= MaxBeatTime+0.2f)   //박자가 끝날 때
        {
            BeatTime = 0;
            T = true;
            SyncopationCountA = 0;
            SyncopationCountB = 0;
            BeatDoubleCheck = true;
            BeatTimerOneClick = true;
            SyncopationTripleChickCheck = false;
            if (NoTouch)    //아무 것도 안했을 때
            {
                Debug.Log("한박 쉼");
                for (int i = 0; i < 4; i++)
                {
                    Commend[i] = 0;
                }
                BeatCount = 0;
                
            }
            NoTouch = true;
        }

        if(BeatTime > MaxBeatTime&&BeatTimerOneClick)    
        {
            OnBeat();
            BeatTimerOneClick = false;
        }
        if (BeatTime > MaxBeatTime - 0.2f)  // 이미지
        {
            ImageTimerON = true;
        }
        if (ImageTimerON)
        {
            ImageTimer += Time.deltaTime;
            BeatImage.SetActive(true);
        }
        if (ImageTimer >= 0.4f)
        {
            BeatImage.SetActive(false);
            ImageTimer = 0.0f;
            ImageTimerON = false;
        }

    }
    void OnBeat()
    {
        Debug.Log("Beat");
        Beat.Invoke();
    }
}
