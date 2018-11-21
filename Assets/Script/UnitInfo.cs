using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {

    public enum MOVE_TYPE {
        WALKING,
        ATHLETE,
        HORSE,
        FLYING,
    }

    public enum ARMY {
        ALLY,
        ENEMY,
        NEUTRAL
    }

    public int ID = 0;
    public int Level = 1;
    public string Name = "No Name";
    public int HP = 20;
    public int maxHP = 20;
    public int moveDistance = 2; // 移動値
    public int attackRange = 1; // 攻撃範囲
    public MOVE_TYPE moveType = 0; // 移動タイプ
    public bool isMoving = false;
    public ARMY aRMY; // 勢力
    [HideInInspector]
    public MoveController moveController;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
