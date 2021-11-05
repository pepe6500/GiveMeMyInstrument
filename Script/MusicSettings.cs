using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicSetting
{
    AudioClip audioClip;
    float BPM;
    float startTime;
    float judgmentTime;
}

public class MusicSettings : ScriptableObject
{
    public List<MusicSetting> musicSettings;
}