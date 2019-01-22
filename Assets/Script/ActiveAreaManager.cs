using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAreaManager : MonoBehaviour {

    // エリア描画用関連
    [HideInInspector]
    public GameObject activeAreaObj, attackAreaObj;
    public GameObject areaBlue;
    public GameObject areaRed;

    [HideInInspector]
    public Struct.NodeMove[,] activeAreaList; // 行動可能エリアを管理する配列
    public Struct.NodeMove[,] attackAreaList; // 攻撃可能エリアを管理する配列


    // Use this for initialization
    void Start () {
        // エリア描画関連用の読み込み
        activeAreaObj = new GameObject("ActiveArea");
        attackAreaObj = new GameObject("AttackArea");

        activeAreaObj.transform.parent = transform;
        attackAreaObj.transform.parent = transform;
    }

    /// <summary>
    /// アクティブエリアの表示
    /// </summary>
    /// <param name="phaseManager"></param>
    public void CreateActiveArea(ref PhaseManager phaseManager) {
        // アクティブリストの生成と検証
        activeAreaList = new Struct.NodeMove[Main.GameManager.GetMap().field.height, Main.GameManager.GetMap().field.width];

        // 移動エリアの検証
        Main.GameManager.GetRoute().CheckMoveArea(ref phaseManager);

        // エリアパネルの表示
        for (int y = 0; y < Main.GameManager.GetMap().field.height; y++)
            for (int x = 0; x < Main.GameManager.GetMap().field.width; x++)
                if (activeAreaList[y, x].aREA == Enums.AREA.MOVE || activeAreaList[y, x].aREA == Enums.AREA.UNIT)
                {
                    // 移動エリアの表示
                    Instantiate(areaBlue, new Vector3(x, -y, 0), Quaternion.identity).transform.parent = activeAreaObj.transform;

                    // 攻撃エリアの検証
                    Main.GameManager.GetRoute().CheckAttackArea(ref activeAreaList, new Vector3(x, -y, 0), phaseManager.focusUnitObj.GetComponent<UnitInfo>().attackRange);
                }

        // 攻撃エリアの表示
        for (int ay = 0; ay < Main.GameManager.GetMap().field.height; ay++)
            for (int ax = 0; ax < Main.GameManager.GetMap().field.width; ax++)
            {
                if (activeAreaList[ay, ax].aREA == Enums.AREA.ATTACK)
                    Instantiate(areaRed, new Vector3(ax, -ay, 0), Quaternion.identity).transform.parent = activeAreaObj.transform;

            }
    }

    /// <summary>
    /// 攻撃エリアの表示
    /// </summary>
    /// <param name="pos">Position.</param>
    /// <param name="attackRange">Attack range.</param>
    public void CreateAttackArea(Vector3 pos, int attackRange) {
        // アクティブリストの生成と検証
        attackAreaList = new Struct.NodeMove[Main.GameManager.GetMap().field.height, Main.GameManager.GetMap().field.width];

        // 攻撃エリアの検証と表示
        Main.GameManager.GetRoute().CheckAttackArea(ref attackAreaList, pos, attackRange);
        for (int ay = 0; ay < Main.GameManager.GetMap().field.height; ay++)
            for (int ax = 0; ax < Main.GameManager.GetMap().field.width; ax++)
                if (attackAreaList[ay, ax].aREA == Enums.AREA.ATTACK)
                    Instantiate(areaRed, new Vector3(ax, -ay, 0), Quaternion.identity).transform.parent = attackAreaObj.transform;
    }

    /// <summary>
    /// 行動エリアの初期化と削除
    /// </summary>
    public void RemoveActiveArea() {
        activeAreaList = null;
        foreach (Transform a in activeAreaObj.transform) Destroy(a.gameObject);
    }
    /// <summary>
    /// 攻撃エリアの初期化と削除
    /// </summary>
    public void RemoveAttackArea() {
        attackAreaList = null;
        foreach (Transform a in attackAreaObj.transform) Destroy(a.gameObject);
    }

}
