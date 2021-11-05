using UnityEngine;
using System.Collections;

public class GameData
{

    private static GameData instance = null;
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
    }

    private GameData()
    {
    }

    public int _test; // 선언할 변수들
    public bool cl;
}