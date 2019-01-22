using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Phaseの管理
/// </summary>
public class PhaseManager : MonoBehaviour {

    // UI
    public GameObject activeMenuUI;
    public GameObject buttleStandbyUI;
    public GameObject selectUnitInfoUI;
    public GameObject cellInfoUI;
    public GameObject cursorObj;

    // マネージャースクリプト
    PhaseManager phaseManager;
    RouteManager routeManager;

    [HideInInspector]
    public Vector3 cursorPos, oldCursorPos;

    // フォーカス(選択中)Unit関連
    [HideInInspector]
    public GameObject focusUnitObj;
    [HideInInspector]
    public Vector3 oldFocusUnitPos;
    [HideInInspector]
    public List<Vector3> moveRoot;  // 移動ルートの座標引き渡し用
    [HideInInspector]
    public Struct.NodeMove[,] activeAreaList; // 行動可能エリアを管理する配列
    [HideInInspector]
    public Struct.NodeMove[,] attackAreaList; // 攻撃可能エリアを管理する配列

    // エリア描画用関連
    [HideInInspector]
    public GameObject activeArea, attackArea, rootArea;
    public GameObject areaBlue;
    public GameObject areaRed;
    public GameObject markerObj;
    public Sprite[] makerSprites;

    // 行動ターン
    Enums.PHASE phase = Enums.PHASE.START;

    bool isBattle = false;

    // バトル用
    [HideInInspector]
    public bool playerAttack;
    [HideInInspector]
    public GameObject myUnitObj, EnemyObj;
    [HideInInspector]
    public int myAttackPower, enemyAttackPower; // 攻撃力
    [HideInInspector]
    public int myDeathblow, enemyDeathblow; //必殺技の発動確率
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
