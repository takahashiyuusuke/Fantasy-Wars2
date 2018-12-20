using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CursorManager : MonoBehaviour {
    // カーソル描画関連
    public GameObject cursorObj;    // カーソルObj
    private Vector2 mouseScreenPos;
    private Vector3 _cursorPos, cursorPos;
    private bool cursorActive = false;

    // アクティブUI
    //public GameObject activeUI;

    // スタンバイUI
    public GameObject standbyUI;

    // フォーカスUnit関連  
    [HideInInspector]
    public UnitInfo focusUnit;
    private Vector3 oldFocusUnitPos;
    [HideInInspector]
    public List<Vector3> moveRoot; // 移動ルートの座標引き渡し用
    [HideInInspector]
    public Struct.NodeMove[,] activeAreaList; // 行動可能エリアを管理する配列
    public Struct.NodeMove[,] attackAreaList;

    // エリア描画用関連
    private GameObject activeArea;
    private GameObject attackArea;
    private GameObject rootArea;
    public GameObject areaBlue;
    public GameObject areaRed;
    public GameObject markerObj;
    public Sprite[] makerSprites;

    // インスタンス
    private RouteManager routeManager;
    public UIUnitInfo uIUnitInfo;
    public UICellInfo uIcellInfo;
    private CursorManager cursorManager;

    // 行動ターン
    Enums.TURN turn = Enums.TURN.START;

    // ボタン
    Button AttuckBtn;
    Button RecoveryBtn;
    Button EndBtn;

    void Start() {
        // UIの非表示
        //activeUI.SetActive(false);
        standbyUI.SetActive(false);

        // カーソルの生成
        cursorObj = Instantiate(cursorObj, Vector3.zero, Quaternion.identity);

        // エリア描画用関連の読み込み
        attackArea = new GameObject("AttackArea");
        activeArea = new GameObject("ActiveArea");
        rootArea = new GameObject("rootArea");
        attackArea.transform.parent = transform;
        activeArea.transform.parent = transform;
        rootArea.transform.parent = transform;

        // インスタンスの初期化
        routeManager = new RouteManager();
        cursorManager = GetComponent<CursorManager>();


        AttuckBtn = GameObject.Find("CanvasUI/ActiveUI/AttackButton").GetComponent<Button>();
        RecoveryBtn = GameObject.Find("CanvasUI/ActiveUI/RecoveryButton").GetComponent<Button>();
        EndBtn = GameObject.Find("CanvasUI/ActiveUI/EndButton").GetComponent<Button>();


        //各ボタンの無効化
        AttuckBtn.interactable = false;
        RecoveryBtn.interactable = false;
        EndBtn.interactable = false;
    }

    public void Update() {
        switch (turn)
        {
            case Enums.TURN.START:
                turnStart();
                break;

            case Enums.TURN.SELECT:
                trunSelect();
                break;

            case Enums.TURN.FOCUS:
                turnFoucus();
                break;

            case Enums.TURN.MOVE:
                turnMove();
                break;

            case Enums.TURN.BATTLE:
                turnBattle();
                break;

            case Enums.TURN.RESULT:
                turnResult();
                break;

            case Enums.TURN.END:
                turnEnd();
                break;
        }
    }

    /// <summary>
    /// ターン開始時
    /// </summary>
    private void turnStart() {
        turn = Enums.TURN.SELECT;
        Debug.Log("TURN.SELECT");
    }

    /// <summary>
    /// ユニット選択前
    /// </summary>
    private void trunSelect() {
        // カーソルの更新
        CursorUpdate(false);

        // クリック処理
        if (Input.GetMouseButtonDown(0) & cursorActive)
            if (Main.GameManager.GetMapUnit(cursorPos) != null && activeAreaList == null)
                if (!Main.GameManager.GetMapUnitInfo(cursorPos).isMoving)
                    AddActiveArea(); // 未行動のユニットであればフォーカスする
    }

    /// <summary>
    /// ユニット選択後
    /// </summary>
    private void turnFoucus() {
        // カーソルの更新
        CursorUpdate(true);

        if (Input.GetMouseButtonDown(0) & cursorActive)
            // アクティブエリア（移動可能マス）を選択されたら移動する
            if (activeAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.MOVE)
            {
                // 他ユニットがいなければ
                if (!Main.GameManager.GetMapUnit(cursorPos))
                {
                    // ユニットの移動前の座標を保存
                    oldFocusUnitPos = focusUnit.GetComponent<MoveController>().getPos();

                    // 移動可能エリアがクリックされたら移動する
                    focusUnit.GetComponent<MoveController>().setMoveRoots(moveRoot);

                    // ターンとUI切り替え
                    Debug.Log("TURN.MOVE");
                    turn = Enums.TURN.MOVE;
                    rootArea.SetActive(false);
                    cursorObj.SetActive(false);
                    activeArea.SetActive(false);
                }
            }
            else // アクティブエリア外をクリックされたらフォーカスを外す
            {
                // アニメーションを元に戻す
                //focusUnit.GetComponent<MoveController>().NotFocuse();
                focusUnit = null;

                // ターンとUI切り替え
                Debug.Log("TURN.SELECT");
                turn = Enums.TURN.SELECT;
                RemoveMarker();
                RemoveActiveArea();
            }
    }

    /// <summary>
    /// ユニットの移動
    /// </summary>
    private void turnMove() {
        //各ボタンを有効化
        if (turn == Enums.TURN.MOVE)
        {
            AttuckBtn.interactable = true;
        }
        else
        {
            AttuckBtn.interactable = false;
            RecoveryBtn.interactable = false;
        }
        EndBtn.interactable = true;

        //回復ボタンの有効化
        if (0 < focusUnit.recoveryCount && focusUnit.vitality > focusUnit.hp)
        {
            RecoveryBtn.interactable = true;
        }
        else
        {
            RecoveryBtn.interactable = false;
        }

        // 移動が終わったらUIを切り替える
        //if (!focusUnit.GetComponent<MoveController>().movingFlg)
        //activeUI.SetActive(true);
    }

    private void turnBattleStanby() {
        // 攻撃範囲内
    }
    /// <summary>
    /// ユニット同士の戦闘
    /// </summary>
    private void turnBattle() {
        // カーソルの更新
        CursorUpdate(false);

        // 攻撃範囲の描画
        if (attackAreaList == null)
        {
            AddAttackArea();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // アクティブエリア（攻撃可能マス）で攻撃対象を選択する
                if (activeAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.ATTACK)
                {
                    // 敵プレイヤーをタップしたら
                    if (Main.GameManager.GetMapUnitInfo(cursorPos).aRMY == Enums.ARMY.ENEMY)
                    {

                    }
                    else
                    {


                    }
                }
                else
                {
                    OnCancelStandby();
                }
            }
        }


    }

    /// <summary>
    /// ユニット同士の戦闘終了後
    /// </summary>
    private void turnResult() {

    }

    /// <summary>
    /// ターン終了時
    /// </summary>
    private void turnEnd() {

    }

    /// <summary>
    /// Ons the attack button.
    /// </summary>
    public void OnAttackBtn() {
        // ターンとUI切り替え
        Debug.Log("TURN.BATTLE");
        turn = Enums.TURN.BATTLE;
        activeArea.SetActive(false);
        //activeUI.SetActive(false);
        rootArea.SetActive(false);
        standbyUI.SetActive(true);
        cursorObj.SetActive(true);
    }

    /// <summary>
    /// 行動画面からのキャンセルボタン処理（画面外のクリック）
    /// </summary>
    public void OnCancelActive() {
        // アニメーションを元に戻す
        //if (focusUnit) focusUnit.GetComponent<MoveController>().NotFocuse();

        // ユニットの座標を元に戻す
        focusUnit.GetComponent<MoveController>().DirectMove(oldFocusUnitPos);

        RemoveActiveArea();
        RemoveMarker();
        focusUnit = null;

        // ターンとUIの切り替え
        Debug.Log("TURN.SELECT");
        turn = Enums.TURN.SELECT;
        //activeUI.SetActive(false);
        cursorObj.SetActive(true);
    }

    /// <summary>
    /// 攻撃選択画面からのキャンセルボタン処理（画面外のクリック）
    /// </summary>
    public void OnCancelStandby() {
        RemoveAttackArea();

        // ターンとUIの切り替え
        Debug.Log("TURN.MOVE");
        turn = Enums.TURN.MOVE;
        standbyUI.SetActive(false);
        cursorObj.SetActive(false);
        rootArea.SetActive(true);
        activeArea.SetActive(true);
    }

    /// <summary>
    /// 行動終了ボタン処理
    /// </summary>
    public void OnEndBtn() {
        // アニメーションを元に戻す
        // if (focusUnit) focusUnit.GetComponent<MoveController>().NotFocuse();

        // ユニット管理リストの更新
        Debug.Log(oldFocusUnitPos);

        Debug.Log(focusUnit.GetComponent<MoveController>().getPos());
        Main.GameManager.MoveMapUnitData(oldFocusUnitPos, focusUnit.GetComponent<MoveController>().getPos());

        RemoveActiveArea();
        RemoveMarker();
        focusUnit = null;

        // ターンとUIの切り替え
        Debug.Log("TURN.SELECT");
        turn = Enums.TURN.SELECT;
        //activeUI.SetActive(false);
        cursorObj.SetActive(true);

        //ボタンを無効化
        EndBtn.interactable = false;
        RecoveryBtn.interactable = false;
        AttuckBtn.interactable = false;

    }

    // 回復ボタン処理
    public void OnRecoveryBtn() {
        // ユニット管理リストの更新
        // Main.GameManager.MoveMapUnitData(oldFocusUnitPos, focusUnit.GetComponent<MoveController>().getPos());
        Main.GameManager.GetMapUnitInfo(oldFocusUnitPos).hp += 30;
        //focusUnit.hp += 10;
        focusUnit.recoveryCount--;

        uIUnitInfo.ShowUnitInfo(Main.GameManager.GetMapUnitInfo(oldFocusUnitPos));

        //回復した時にhpがmaxHpを超えるなら最大値で上書き
        if (focusUnit.hp > focusUnit.vitality)
        {
            focusUnit.hp = focusUnit.vitality;
        }
        Debug.Log("回復");
        Debug.Log(oldFocusUnitPos);
    }

    /// <summary>
    /// カーソルの描画処理
    /// </summary>
    private void CursorUpdate(bool showMarker) {
        // マウスの座標を変換して取得
        mouseScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cursorPos = new Vector3(MultipleRound(mouseScreenPos.x, 1),
                                MultipleRound(mouseScreenPos.y, 1), 0);

        // マップ内なら新しいカーソル座標を取得する
        if (0 <= _cursorPos.x && _cursorPos.x < MapManager.GetFieldData().width &&
            0 <= -_cursorPos.y && -_cursorPos.y < MapManager.GetFieldData().height)
        {
            cursorActive = true;
            cursorPos = _cursorPos;
        }
        else
        {
            cursorActive = false;
        }

        // カーソル座標が更新されてないなら更新する
        if (cursorObj.transform.position != cursorPos)
        {
            // カーソルの座標を更新
            cursorObj.transform.position = cursorPos;

            // セル情報の更新
            uIcellInfo.SetData(MapManager.GetFieldData().cells[-(int)cursorPos.y, (int)cursorPos.x]);

            // 移動マーカの更新
            if (showMarker) AddMarker();

            // ユニット情報の更新
            if (Main.GameManager.GetMapUnit(cursorPos))
                uIUnitInfo.ShowUnitInfo(Main.GameManager.GetMapUnitInfo(cursorPos));
            else
                uIUnitInfo.CloseUnitInfo();
        }
    }

    /// <summary>
    /// アクティブエリアの表示
    /// </summary>
    private void AddActiveArea() {
        // フォーカスユニットの取得
        focusUnit = Main.GameManager.GetMapUnitInfo(cursorPos);

        // アクティブリストの生成と検証
        activeAreaList = new Struct.NodeMove[MapManager.GetFieldData().height, MapManager.GetFieldData().width];

        // 移動エリアの検証
        routeManager.CheckMoveArea(ref cursorManager);

        // エリアパネルの表示
        for (int y = 0; y < MapManager.GetFieldData().height; y++)
            for (int x = 0; x < MapManager.GetFieldData().width; x++)
                if (activeAreaList[y, x].aREA == Enums.AREA.MOVE || activeAreaList[y, x].aREA == Enums.AREA.UNIT)
                {
                    // 移動エリアの表示
                    Instantiate(areaBlue, new Vector3(x, -y, 0), Quaternion.identity).transform.parent = activeArea.transform;

                    // 攻撃エリアの検証
                    routeManager.CheckAttackArea(ref activeAreaList, new Vector3(x, -y, 0), ref focusUnit);
                }

        // 攻撃エリアの表示
        for (int ay = 0; ay < MapManager.GetFieldData().height; ay++)
            for (int ax = 0; ax < MapManager.GetFieldData().width; ax++) {
                if (activeAreaList[ay, ax].aREA == Enums.AREA.ATTACK)
                    Instantiate(areaRed, new Vector3(ax, -ay, 0), Quaternion.identity).transform.parent = activeArea.transform;

            }
        // ターンとUIの切り替え
        Debug.Log("TURN.FOCUS");
        turn = Enums.TURN.FOCUS;
        activeArea.SetActive(true);
        rootArea.SetActive(true);
    }


    /// <summary>
    /// 攻撃エリアの表示
    /// </summary>
    private void AddAttackArea() {
        // アクティブリストの生成と検証
        attackAreaList = new Struct.NodeMove[MapManager.GetFieldData().height, MapManager.GetFieldData().width];

        // 攻撃エリアの検証と表示
        routeManager.CheckAttackArea(ref attackAreaList, focusUnit.GetComponent<MoveController>().getPos(), ref focusUnit);
        for (int ay = 0; ay < MapManager.GetFieldData().height; ay++)
            for (int ax = 0; ax < MapManager.GetFieldData().width; ax++)
                if (attackAreaList[ay, ax].aREA == Enums.AREA.ATTACK)
                    Instantiate(areaRed, new Vector3(ax, -ay, 0), Quaternion.identity).transform.parent = attackArea.transform;
    }

    /// <summary>
    /// markerの表示
    /// </summary>
    private void AddMarker() {
        // アクティブエリアがあるなら、マーカを表示する
        if (activeAreaList != null)
            // 移動エリア内ならマーカを表示する
            if (activeAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.MOVE)
            {
                // マーカの削除
                RemoveMarker();

                // 目標までのルートを取得
                routeManager.CheckShortestRoute(ref cursorManager, cursorPos);

                // マーカの生成とスプライト変更
                Vector3 nextPos = focusUnit.GetComponent<MoveController>().getPos();
                int spriteId = 0;
                Quaternion angle = Quaternion.identity;
                int moveRootCount = moveRoot.Count;
                if (moveRootCount != 0)
                {
                    if (moveRoot[0] == Vector3.down) angle.eulerAngles = new Vector3(180, 0, 0);
                    else if (moveRoot[0] == Vector3.left) angle.eulerAngles = new Vector3(0, 0, 90);
                    else if (moveRoot[0] == Vector3.right) angle.eulerAngles = new Vector3(0, 0, -90);
                    markerObj.GetComponent<SpriteRenderer>().sprite = makerSprites[spriteId];
                    Instantiate(markerObj, nextPos, angle).transform.parent = rootArea.transform;
                }
                for (int i = 0; i < moveRootCount; i++)
                {
                    if (moveRoot[i] == Vector3.up)
                    {
                        if (i + 1 == moveRootCount)
                        {
                            spriteId = 3;
                            angle = Quaternion.identity;
                        }
                        else
                        {
                            if (moveRoot[i + 1] != Vector3.up)
                            {
                                if (moveRoot[i + 1] == Vector3.left)
                                    angle.eulerAngles = new Vector3(0, 180, 0);
                                else
                                    angle = Quaternion.identity;
                                spriteId = 2;
                            }
                            else
                            {
                                spriteId = 1;
                                angle = Quaternion.identity;
                            }
                        }
                    }
                    else if (moveRoot[i] == Vector3.down)
                    {
                        if (i + 1 == moveRootCount)
                        {
                            spriteId = 3;
                            angle.eulerAngles = new Vector3(0, 0, 180);
                        }
                        else
                        {
                            if (moveRoot[i + 1] != Vector3.down)
                            {
                                if (moveRoot[i + 1] == Vector3.left)
                                    angle.eulerAngles = new Vector3(0, 0, 180);
                                else
                                    angle.eulerAngles = new Vector3(180, 0, 0);
                                spriteId = 2;
                            }
                            else
                            {
                                spriteId = 1;
                                angle.eulerAngles = new Vector3(0, 0, 180);
                            }
                        }

                    }
                    else if (moveRoot[i] == Vector3.right)
                    {
                        if (i + 1 == moveRootCount)
                        {
                            spriteId = 3;
                            angle.eulerAngles = new Vector3(0, 0, -90);
                        }
                        else
                        {
                            if (moveRoot[i + 1] != Vector3.right)
                            {
                                if (moveRoot[i + 1] == Vector3.up)
                                    angle.eulerAngles = new Vector3(0, 180, 90);
                                else
                                    angle.eulerAngles = new Vector3(0, 0, -90);
                                spriteId = 2;
                            }
                            else
                            {
                                spriteId = 1;
                                angle.eulerAngles = new Vector3(0, 0, -90);
                            }
                        }
                    }
                    else
                    {
                        if (i + 1 == moveRootCount)
                        {
                            spriteId = 3;
                            angle.eulerAngles = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            if (moveRoot[i + 1] != Vector3.left)
                            {
                                if (moveRoot[i + 1] == Vector3.up)
                                    angle.eulerAngles = new Vector3(0, 0, 90);
                                else
                                    angle.eulerAngles = new Vector3(0, 180, -90);
                                spriteId = 2;
                            }
                            else
                            {
                                spriteId = 1;
                                angle.eulerAngles = new Vector3(0, 0, 90);
                            }
                        }
                    }
                    markerObj.GetComponent<SpriteRenderer>().sprite = makerSprites[spriteId];
                    Instantiate(markerObj, nextPos += moveRoot[i], angle).transform.parent = rootArea.transform;
                }
            }
            else if (activeAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.UNIT)
                RemoveMarker(); // カーソルがユニット上なら表示しない
    }

    /// <summary>
    /// 行動エリアの初期化と削除
    /// </summary>
    private void RemoveActiveArea() {
        activeAreaList = null;
        foreach (Transform a in activeArea.transform) Destroy(a.gameObject);
    }

    /// <summary>
    /// 攻撃エリアの初期化と削除
    /// </summary>
    private void RemoveAttackArea() {
        attackAreaList = null;
        foreach (Transform a in attackArea.transform) Destroy(a.gameObject);
    }

    /// <summary>
    /// マーカの削除
    /// </summary>
    private void RemoveMarker() {
        foreach (Transform r in rootArea.transform) Destroy(r.gameObject);
    }

    /// <summary>
    /// 倍数での四捨五入のような値を求める（ｎおきの数の中間の値で切り捨て・切り上げをする）
    ///（例）倍数 = 10 のとき、12 → 10, 17 → 20
    /// </summary>
    /// <param name="value">入力値</param>
    /// <param name="multiple">倍数</param>
    /// <returns>倍数の中間の値で、切り捨て・切り上げした値</returns>
    private static float MultipleRound(float value, float multiple) {
        return MultipleFloor(value + multiple * 0.5f, multiple);
    }

    /// <summary>
    /// より小さい倍数を求める（倍数で切り捨てられるような値）
    ///（例）倍数 = 10 のとき、12 → 10, 17 → 10
    /// </summary>
    /// <param name="value">入力値</param>
    /// <param name="multiple">倍数</param>
    /// <returns>倍数で切り捨てた値</returns>
    private static float MultipleFloor(float value, float multiple) {
        return Mathf.Floor(value / multiple) * multiple;
    }
}