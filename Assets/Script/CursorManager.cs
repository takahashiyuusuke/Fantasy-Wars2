using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {
    //カーソル描画用
    public GameObject cursorObj;
    private Vector3 _cursorPos, cursorPos;

    //インスタンス
    public RouteManager routeManager;
    private CursorManager cursorManager;

    [HideInInspector]
    public UnitInfo focusUnit;
    private Vector3 oldFocusUnitPos;

    [HideInInspector]
    public List<Vector3> moveRoot; // 移動ルートの座標引き渡し用
    [HideInInspector]
    public NodeMove[,] activeAreaList; // 行動可能エリアを管理する配列
    public NodeMove[,] attackAreaList;

    public void Start() {
        //インスタンスの初期化
        cursorManager = GetComponent<CursorManager>();
    }

    private void AddActiveArea() {
        // カーソルを描画が先？
        focusUnit = GameManager.GetMapUnit(cursorPos);
        //アクティブリストの生成と検証
        activeAreaList = new NodeMove[Map.GetFieldData().height, Map.GetFieldData().width];

        // 移動エリアの検証
        routeManager.CheckMoveArea(ref cursorManager);

    }
}
