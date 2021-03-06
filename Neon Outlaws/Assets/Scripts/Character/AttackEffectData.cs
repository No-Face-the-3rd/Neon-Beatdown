﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EffectZone
{
    public Vector2 min;
    public Vector2 max;
}


public enum effectType
{
    Knockback = 1 << 0,
    Stun = 1 << 1,
    Down = 1 << 2,
    Death = 1 << 3
}

public class AttackEffectData : MonoBehaviour {
    public Vector2 offset;
    public EffectZone zone;
    [EnumMask]
    public effectType type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
