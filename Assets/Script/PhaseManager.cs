using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Phaseの管理
/// </summary>
public class PhaseManager : MonoBehaviour {

    // UI
    public GameObject activeMenuUI;
    public GameObject battleStandbyUI;
    public GameObject selectUnitInfoUI;
    public GameObject cellInfoUI;
    public GameObject cursorObj;
    public Image playerTurnImage, enemyTurnImage;
    public ExpGaugeController expGaugeController;


    Animator turnImageAnim;

    // 各ボタン
    Button attackBtn;
    Button recoveryBtn;
    Button waitingBtn;
    Button turnEndBtn;

    // マネージャースクリプト
    public BattleManager battleManager;
    public ActiveAreaManager activeAreaManager;
    public MoveMarkerManager moveMarkerManager;
    public AudioManager audioManager;
    PhaseManager phaseManager;

    [HideInInspector]
    public Vector3 cursorPos, oldCursorPos;

    // フォーカス(選択中)Unit関連
    [HideInInspector]
    public GameObject focusUnitObj;
    Vector3 oldFocusUnitPos;
    [HideInInspector]
    public List<Vector3> moveRoot;  // 移動ルートの座標引き渡し用

    // エリア描画用関連
    [HideInInspector]
    public GameObject activeArea, attackArea;
    //public GameObject areaBlue;
    //public GameObject areaRed;
    //public GameObject markerObj;
    public Sprite[] makerSprites;

    // バトル関連
    bool isBattle = false;
    [HideInInspector]
    public bool playerAttack;
    [HideInInspector]
    public GameObject playerUnitObj, enemyUnitObj; // Unitのオブジェクト
    [HideInInspector]
    public int myAttackPower, enemyAttackPower; // 攻撃力
    [HideInInspector]
    public int myDeathblow, enemyDeathblow; //必殺技の発生率
    [HideInInspector]
    public int myAttackCount, enemyAttackCount; // 攻撃回数
    [HideInInspector]
    public int myAccuracy, enemyAccuracy; // 命中率
    [HideInInspector]
    public Enums.BATTLE myAttackState, enemyAttackState; // 攻撃判定
    [HideInInspector]
    public Text textMyHP, textEnemyHP; // 表示用

    // 行動ターン
    Enums.PHASE phase = Enums.PHASE.START;

    // 行動するターンプレイヤー
    Enums.ARMY turnPlayer;

    // 各行動イベント
    Action StartPhase, // プレイヤーターンの開始
    StandbyPhase, // Unit選択中
    FoucusPhase, // Unit選択時
    MovePhase, // Unit行動時
    BattleStandbyPhase, // Unit攻撃選択時
    BattlePhase, // Unit攻撃時
    ResultPhase, // Unit攻撃終了時（まだ見操作のUnitがいれば、SELECTに戻る）
    EndPhase; // プレイヤーのターン終了時


    // Use this for initialization
    void Start() {
        // インスタンスの初期化
        phaseManager = this;

        // エリア描画用関連の読み込み
        attackArea = new GameObject("AttackArea");
        activeArea = new GameObject("activeArea");

        attackArea.transform.parent = transform;
        activeArea.transform.parent = transform;

        // プレイヤーターンから始める
        TurnChange(Enums.ARMY.ALLY);

        attackBtn = GameObject.Find("CanvasUI/ActiveUI/AttackButton").GetComponent<Button>();
        recoveryBtn = GameObject.Find("CanvasUI/ActiveUI/RecoveryButton").GetComponent<Button>();
        waitingBtn = GameObject.Find("CanvasUI/ActiveUI/EndButton").GetComponent<Button>();
        turnEndBtn = GameObject.Find("CanvasUI/ActiveUI/TurnEndButton").GetComponent<Button>();

        battleStandbyUI.SetActive(false);

        //各ボタンの無効化
        attackBtn.interactable = false;
        recoveryBtn.interactable = false;
        waitingBtn.interactable = false;

        // カーソル更新時に呼び出す処理の登録
        CursorController.AddCallBack((Vector3 newPos) => { cursorPos = newPos; });

        Main.GameManager.GetUnit().CheckEnemyUnits();
        Main.GameManager.GetUnit().CheckPlayerUnits();
    }

    void Update() {
        Debug.Log(phase);
        switch (phase)
        {
            case Enums.PHASE.START:
                StartPhase();
                break;
            case Enums.PHASE.STANDBY:
                StandbyPhase();
                break;
            case Enums.PHASE.FOCUS:
                FoucusPhase();
                break;
            case Enums.PHASE.MOVE:
                MovePhase();
                break;
            case Enums.PHASE.BATTLE_STANDBY:
                BattleStandbyPhase();
                break;
            case Enums.PHASE.BATTLE:
                BattlePhase();
                break;
            case Enums.PHASE.RESULT:
                ResultPhase();
                break;
            case Enums.PHASE.END:
                EndPhase();

                break;
        }
    }
    /// <summary>
    /// 外部変更用
    /// </summary>
    /// <param name="player"></param>
    public void ChangePhase(Enums.PHASE newPhase) { phase = newPhase; }

    /// <summary>
    /// ターンの切り替え処理
    /// </summary>
    /// <param name="player"></param>
    void TurnChange(Enums.ARMY player) {
        switch (player)
        {
            case Enums.ARMY.ALLY:
                this.turnPlayer = player;
                StartPhase = MyStartPhase;
                StandbyPhase = MyStandbyPhase;
                FoucusPhase = MyFoucusPhase;
                MovePhase = MyMovePhase;
                BattleStandbyPhase = MyBattleStandbyPhase;
                BattlePhase = MyBattlePhase;
                ResultPhase = MyResultPhase;
                EndPhase = MyEndPhase;
                break;
            case Enums.ARMY.ENEMY:
                this.turnPlayer = player;
                StartPhase = EnemyStartPhase;
                StandbyPhase = EnemyStandbyPhase;
                FoucusPhase = EnemyFoucusPhase;
                MovePhase = EnemyMovePhase;
                BattleStandbyPhase = EnemyBattleStandbyPhase;
                BattlePhase = EnemyBattlePhase;
                ResultPhase = EnemyResultPhase;
                EndPhase = EnemyEndPhase;
                break;
        }
    }
    // 攻撃ボタン処理
    public void OnAttackBtn() {
        // ターンとUI切り替え
        phase = Enums.PHASE.BATTLE_STANDBY;
        activeArea.SetActive(false);
        //activeUI.SetActive(false);
        //rootArea.SetActive(false);
        cursorObj.SetActive(true);
    }

    // 回復ボタン処理
    public void OnRecoveryBtn() {
        focusUnitObj.GetComponent<UnitInfo>().hp += 30; // 行動済み
    }

    // 待機ボタン処理
    public void OnWaitingBtn() {
        MyResultPhase();
    }

    // ターン終了ボタン処理
    public void OnTurnEndBtn() {
        MyEndPhase();
    }

    /// <summary>
    /// 行動終了ボタン処理
    /// </summary>
    public void MoveCansel() {
        // アニメーションを元に戻す
        //if (focusUnit) focusUnit.GetComponent<MoveController>().NotFocuse();

        // ユニットの座標を元に戻す
        focusUnitObj.GetComponent<MoveController>().DirectMove(oldFocusUnitPos);

        activeAreaManager.RemoveActiveArea();
        activeAreaManager.RemoveAttackArea();
        moveMarkerManager.RemoveMarker();
        focusUnitObj = null;

        // ターンとUIの切り替え
        Debug.Log("TURN.SELECT");
        phase = Enums.PHASE.STANDBY;
        //activeUI.SetActive(false);
        cursorObj.SetActive(true);
    }
    /// <summary>
    /// バトルキャンセル処理
    /// </summary>
    void OnCancelBattleStandby() {
        activeAreaManager.RemoveAttackArea();

        // ターンとUIの切り替え
        phase = Enums.PHASE.MOVE;
        battleStandbyUI.SetActive(false);
        //cursorObj.SetActive(false);
        moveMarkerManager.SetActive(true);
    }

    /// <summary>
    /// 引数の確率の検証
    /// </summary>
    /// <returns><c>true</c>, if check was randomed, <c>false</c> otherwise.</returns>
    /// <param name="probability">Probability.</param>
    bool RandomCheck(int probability) { return UnityEngine.Random.Range(1, 101) <= probability ? true : false; }

    /// <summary>
    /// ターン開始時
    /// </summary>
    void MyStartPhase() {
        // ターン終了ボタンの有効化
        turnEndBtn.interactable = true;

        // 自軍のターンBGM再生↓
        audioManager.ChangePlayerBGM();
        //audioManager.TurnChengeSE();
        if (turnImageAnim == null)
        {
            // ターン開始アニメーションの再生
            turnImageAnim = playerTurnImage.gameObject.GetComponent<Animator>();
            playerTurnImage.gameObject.SetActive(true);
            

            // ランダムな未行動ユニット1体の座標にカーソルを合わせる
            focusUnitObj = Main.GameManager.GetUnit().GetUnBehaviorRandomUnit(Enums.ARMY.ALLY);
            cursorPos = focusUnitObj.transform.position;
            cursorObj.transform.position = cursorPos;
        }
        else
        {
            // アニメーションが終了したらターンを開始する
            if (!(turnImageAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
            {
                // ターンとUIの切り替え
                List<GameObject> list = Main.GameManager.GetUnit().GetUnBehaviorUnits(Enums.ARMY.ALLY);
                if (list.Count == 0)
                    phase = Enums.PHASE.END;
                else
                    phase = Enums.PHASE.STANDBY;

                selectUnitInfoUI.SetActive(true);
                cellInfoUI.SetActive(true);
                playerTurnImage.gameObject.SetActive(false);
                activeAreaManager.activeAreaObj.SetActive(true);
                cursorObj.SetActive(true);
                turnImageAnim = null;
            }
        }
    }
    void MyStandbyPhase() {
        turnEndBtn.interactable = true;
        // カーソルが更新されたら
        if (cursorPos != oldCursorPos)
        {
            // カーソルの更新
            oldCursorPos = cursorPos;

            // セル情報の更新
            cellInfoUI.GetComponent<CellInfo>().SetData(Main.GameManager.GetMap().field.cells[-(int)cursorPos.y, (int)cursorPos.x]);

            // ユニット情報の更新
            if (Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos))
                selectUnitInfoUI.GetComponent<SelectUnitInfo>().ShowUnitInfo(Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos));
            //else
            //selectUnitInfoUI.GetComponent<SelectUnitInfo>().CloseUnitInfo();
        }

        // クリック処理
        if (Input.GetMouseButtonDown(0))
        {
            // SE再生
            audioManager.ClickSE();

            // 未行動の自軍ユニットであればフォーカスし、アクティブエリアを表示する
            if (Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos) != null && activeAreaManager.activeAreaList == null)
                if (Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos).aRMY == Enums.ARMY.ALLY)
                    if (!Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos).isMoving())
                    {
                        // フォーカスユニットの取得
                        focusUnitObj = Main.GameManager.GetUnit().GetMapUnitObj(cursorPos);
                        activeAreaManager.CreateActiveArea(focusUnitObj, true);

                        // ターンとUIの切り替え
                        phase = Enums.PHASE.FOCUS;
                        //selectUnitInfoUI.SetActive(false);
                        cellInfoUI.SetActive(false);
                        activeAreaManager.activeAreaObj.SetActive(true);
                        moveMarkerManager.SetActive(true);
                    }
        }
    }
    void MyFoucusPhase() {
        // 移動マーカの更新
        if (cursorPos != oldCursorPos)
        {
            // カーソルの更新
            oldCursorPos = cursorPos;
            moveMarkerManager.AddMarker(ref phaseManager);
        }

        if (Input.GetMouseButtonDown(0))
            // アクティブエリア（移動可能マス）を選択されたら移動する
            if (activeAreaManager.activeAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.MOVE)
            {
                // 他ユニットがいなければ
                if (!Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos))
                {
                    // ユニットの移動前の座標を保存
                    oldFocusUnitPos = focusUnitObj.transform.position;

                    // 移動可能エリアがクリックされたら移動する
                    focusUnitObj.GetComponent<MoveController>().SetMoveRoots(moveRoot);

                    // ターンとUI切り替え
                    phase = Enums.PHASE.MOVE;
                    moveMarkerManager.SetActive(false);
                    //cursorObj.SetActive(false);
                    activeAreaManager.activeAreaObj.SetActive(false);
                }
            }
            else // アクティブエリア外をクリックされたらフォーカスを外す
            {
                // アニメーションを元に戻す
                focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);
                focusUnitObj = null;

                // ターンとUI切り替え
                phase = Enums.PHASE.STANDBY;
                moveMarkerManager.RemoveMarker();
                activeAreaManager.RemoveActiveArea();
            }
    }
    void MyMovePhase() {
        // 
        // 移動が終わったらUIを切り替える
        if (focusUnitObj.GetComponent<MoveController>().IsMoved())
        {
            // ボタンの有効化と無効化
            attackBtn.interactable = true;
            recoveryBtn.interactable = true;
            waitingBtn.interactable = true;
            activeAreaManager.attackAreaObj.SetActive(true);
            activeMenuUI.SetActive(true);
            turnEndBtn.interactable = false;
        }
    }
    void MyBattleStandbyPhase() {
        // 攻撃範囲の描画
        if (activeAreaManager.attackAreaList == null)
            activeAreaManager.CreateAttackArea(focusUnitObj.transform.position, focusUnitObj.GetComponent<UnitInfo>().attackRange);
        else
        {
            // カーソルを敵ユニットに合わせた時の処理
            if (Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos) &&
                Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos).aRMY == Enums.ARMY.ENEMY &&
                activeAreaManager.attackAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.ATTACK)
            {

                // バトルパラメータのセット
                playerUnitObj = focusUnitObj;
                enemyUnitObj = Main.GameManager.GetUnit().GetMapUnitObj(cursorPos);
                textMyHP = battleStandbyUI.GetComponent<BattleStandby>().textMyHP;
                textEnemyHP = battleStandbyUI.GetComponent<BattleStandby>().textEnemyHP;

                myAttackPower = Main.GameManager.GetCommonCalc().GetAttackDamage(focusUnitObj.GetComponent<UnitInfo>(), enemyUnitObj.GetComponent<UnitInfo>());
                myAccuracy = Main.GameManager.GetCommonCalc().GetHitRate(focusUnitObj.GetComponent<UnitInfo>(), enemyUnitObj.GetComponent<UnitInfo>());
                myDeathblow = Main.GameManager.GetCommonCalc().GetDeathBlowRete(focusUnitObj.GetComponent<UnitInfo>(), enemyUnitObj.GetComponent<UnitInfo>());
                myAttackCount = Main.GameManager.GetCommonCalc().GetAttackCount(focusUnitObj.GetComponent<UnitInfo>(), enemyUnitObj.GetComponent<UnitInfo>());

                enemyAttackPower = Main.GameManager.GetCommonCalc().GetAttackDamage(enemyUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
                enemyAccuracy = Main.GameManager.GetCommonCalc().GetHitRate(enemyUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
                enemyDeathblow = Main.GameManager.GetCommonCalc().GetDeathBlowRete(enemyUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
                enemyAttackCount = Main.GameManager.GetCommonCalc().GetAttackCount(enemyUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());

                // 敵の向きに合わせてUnitのアニメーション変更
                if (Mathf.Abs(focusUnitObj.transform.position.x - enemyUnitObj.transform.position.x) <= 0f)
                    if (focusUnitObj.transform.position.y < enemyUnitObj.transform.position.y)
                        focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.UP);
                    else
                        focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);
                else if (focusUnitObj.transform.position.x < enemyUnitObj.transform.position.x)
                    focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.RIGHT);
                else
                    focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.LEFT);

                // UIの切り替え
                if (!battleStandbyUI.activeSelf)
                    battleStandbyUI.SetActive(true);
                battleStandbyUI.GetComponent<BattleStandby>().SetMyUnitData(
                    focusUnitObj.GetComponent<UnitInfo>(), myAttackPower, myAccuracy, myDeathblow);
                battleStandbyUI.GetComponent<BattleStandby>().SetEnemyUnitData(
                    enemyUnitObj.GetComponent<UnitInfo>(), enemyAttackPower, enemyAccuracy, enemyDeathblow);
            }
            else
                if (battleStandbyUI.activeSelf)
                battleStandbyUI.SetActive(false); // UIの切り替え

            // クリック処理
            if (Input.GetMouseButtonDown(0))
            {
                // アクティブエリア（攻撃可能マス）で攻撃対象を選択する
                if (activeAreaManager.attackAreaList[-(int)cursorPos.y, (int)cursorPos.x].aREA == Enums.AREA.ATTACK &&
                    Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos) &&
                        Main.GameManager.GetUnit().GetMapUnitInfo(cursorPos).aRMY == Enums.ARMY.ENEMY)
                {
                    // 移動完了
                    Main.GameManager.GetUnit().MoveMapUnitObj(oldFocusUnitPos, focusUnitObj.transform.position);
                    focusUnitObj.GetComponent<UnitInfo>().Moving(true); // 行動済み

                    // 戦闘開始
                    // ターンとUIの切り替え
                    //cursorObj.SetActive(false);
                    activeAreaManager.attackAreaObj.SetActive(false);
                    phase = Enums.PHASE.BATTLE;
                }
                else OnCancelBattleStandby(); // UIの切り替え
            }
        }
    }

    void MyBattlePhase() {
        if (!isBattle)
        {
            while (true)
            {
                // こちらの攻撃
                if (0 < myAttackCount)
                {
                    // 必殺の検証
                    myAttackState = RandomCheck(myDeathblow) ? Enums.BATTLE.DEATH_BLOW : Enums.BATTLE.NORMAL;

                    // 通常攻撃命中判定
                    if (myAttackState != Enums.BATTLE.DEATH_BLOW)
                        myAttackState = RandomCheck(myAccuracy) ? Enums.BATTLE.NORMAL : Enums.BATTLE.MISS;

                    // 通常攻撃か必殺が発生したら攻撃イベントとして登録する
                    AttackEvent attackEvent = gameObject.AddComponent<AttackEvent>();
                    attackEvent.phaseManager = this;
                    attackEvent.myUnitObj = playerUnitObj;
                    attackEvent.targetUnitObj = enemyUnitObj;
                    attackEvent.myAttackPower = myAttackPower;
                    attackEvent.myAttackState = myAttackState;
                    attackEvent.targetHPText = textEnemyHP;
                    battleManager.AddEvent(attackEvent);

                    myAttackCount--;
                }

                // 敵の反撃
                if (0 < enemyAttackCount)
                {
                    // 必殺の検証
                    enemyAttackState = RandomCheck(enemyDeathblow) ? Enums.BATTLE.DEATH_BLOW : Enums.BATTLE.NORMAL;

                    // 通常攻撃命中判定
                    if (enemyAttackState != Enums.BATTLE.DEATH_BLOW)
                        enemyAttackState = RandomCheck(enemyAccuracy) ? Enums.BATTLE.NORMAL : Enums.BATTLE.MISS;

                    // 通常攻撃か必殺が発生したら攻撃イベントとして登録する
                    AttackEvent attackEvent = gameObject.AddComponent<AttackEvent>();
                    attackEvent.phaseManager = this;
                    attackEvent.myUnitObj = enemyUnitObj;
                    attackEvent.targetUnitObj = playerUnitObj;
                    attackEvent.myAttackPower = enemyAttackPower;
                    attackEvent.myAttackState = enemyAttackState;
                    attackEvent.targetHPText = textMyHP;
                    battleManager.AddEvent(attackEvent);

                    enemyAttackCount--;
                }

                // 戦闘イベント登録終了
                if (myAttackCount <= 0 && enemyAttackCount <= 0) break;
            }

            // 敵をこちらに向かせる
            if (Mathf.Abs(enemyUnitObj.transform.position.x - focusUnitObj.transform.position.x) <= 0f)
                if (enemyUnitObj.transform.position.y < focusUnitObj.transform.position.y)
                    enemyUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.UP);
                else
                    enemyUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);
            else if (enemyUnitObj.transform.position.x < focusUnitObj.transform.position.x)
                enemyUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.RIGHT);
            else
                enemyUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.LEFT);

            // バトルの実行
            battleManager.StartEvent();
            isBattle = true;
        }
        else if(!battleManager.isBattle())
        {
            // 攻撃終了処理
            isBattle = false;

            // プレイヤーUnitが生存していれば経験値取得処理を行う
            if (focusUnitObj)
            {
                // 経験値処理が終わるまでフェーズを停止
                phase = Enums.PHASE.STOP;

                // Exp取得処理の開始
                expGaugeController.GaugeUpdate(1000, focusUnitObj.GetComponent<UnitInfo>(), () =>
                {
                    phase = Enums.PHASE.RESULT;
                });
            }
            else
            {
                phase = Enums.PHASE.RESULT;
            }
        }
    }
    /// <summary>
    /// ユニット同士の戦闘終了後
    /// </summary>
    void MyResultPhase() {
        // アニメーションを元に戻す
        if (focusUnitObj)
            focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);

        // 未移動であれば移動済みとする
        if (!focusUnitObj.GetComponent<UnitInfo>().isMoving())
        {
            Main.GameManager.GetUnit().MoveMapUnitObj(oldFocusUnitPos, focusUnitObj.transform.position);
            focusUnitObj.GetComponent<UnitInfo>().Moving(true); // 行動済み
        }
        // グレースケールにする
        focusUnitObj.GetComponent<EffectController>().GrayScale(true);

        activeAreaManager.RemoveActiveArea();
        activeAreaManager.RemoveAttackArea();
        moveMarkerManager.RemoveMarker();
        focusUnitObj = null;

        // ターンとUIの切り替え
        List<GameObject> list = Main.GameManager.GetUnit().GetUnBehaviorUnits(Enums.ARMY.ALLY);
        if (list.Count == 0)
            phase = Enums.PHASE.END;
        else
            phase = Enums.PHASE.STANDBY;

        //activeMenuUI.SetActive(false);
        battleStandbyUI.SetActive(false);
        cursorObj.SetActive(true);

        // 各ボタンの無効化
        attackBtn.interactable = false;
        recoveryBtn.interactable = false;
        waitingBtn.interactable = false;

        // 敵キャラ数チェック
        Main.GameManager.GetUnit().CheckEnemyUnits();
    }
    /// <summary>
    /// ターン終了時
    /// </summary>
    void MyEndPhase() {
        // 自軍ユニットを全て未行動に戻す
        Main.GameManager.GetUnit().UnBehaviorUnitAll(Enums.ARMY.ALLY);
        //phase = Enums.PHASE.START;
        // 敵ターンに切り替える
        TurnChange(Enums.ARMY.ENEMY);
        phase = Enums.PHASE.START;
    }

    void EnemyStartPhase() {
        // ターン終了ボタンの無効化
        turnEndBtn.interactable = false;

        audioManager.TurnChengeSE();
        // 敵のターンBGMへ変更↓
        audioManager.ChangeEnemyBGM();

        if (turnImageAnim == null)
        {
            // ターン開始アニメーションの再生
            turnImageAnim = enemyTurnImage.gameObject.GetComponent<Animator>();
            enemyTurnImage.gameObject.SetActive(true);
        }
        else
        {
            // アニメーションが終了したらターンを開始する
            if (!(turnImageAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
            {
                // ターンとUI切り替え
                phase = Enums.PHASE.STANDBY;
                //selectUnitInfoUI.SetActive(false);
                //cellInfoUI.SetActive(false);
                enemyTurnImage.gameObject.SetActive(false);
                //cursorObj.SetActive(false);
                turnImageAnim = null;
            }
        }
    }

    void EnemyStandbyPhase() {
        GameObject checkUnitObj = Main.GameManager.GetUnit().GetUnBehaviorRandomUnit(Enums.ARMY.ENEMY);
        if (checkUnitObj != null)
        {
            // アクティブエリアの取得
            activeAreaManager.CreateActiveArea(checkUnitObj, true);

            // 行動範囲内にて攻撃できるプレイヤーUnitを探索する
            List<GameObject> targetList = Main.GameManager.GetEnemyAI().GetAttackTargetList(phaseManager.activeAreaManager.activeAreaList);

            playerUnitObj = Main.GameManager.GetEnemyAI().GetAttackTargetSelection(checkUnitObj.GetComponent<UnitInfo>(), targetList);
            if (playerUnitObj != null)
            {
                // 行動範囲内に攻撃できる対象がいる場合は、移動して攻撃する
                focusUnitObj = checkUnitObj;

                phase = Enums.PHASE.FOCUS;
            }
            else if (false)
            {
                // TODO 攻撃が届かないので、一番近いプレイヤーユニットに近づく



            }
            else
            {
                // このターンは移動しない
                checkUnitObj.GetComponent<UnitInfo>().Moving(true); // 行動済み
                focusUnitObj = null;
                phase = Enums.PHASE.RESULT;
            }
        }
        else phase = Enums.PHASE.END;
    }
    void EnemyFoucusPhase() {
        // ユニットの移動前の座標を保存
        oldFocusUnitPos = focusUnitObj.transform.position;

        // 移動できる範囲で、ターゲットに攻撃できる場所のリストを取得する
        List<Vector3> attackLocationList = Main.GameManager.GetEnemyAI().GetAttackLocationList(phaseManager.activeAreaManager.activeAreaList, focusUnitObj, playerUnitObj);
        // 攻撃対象に対して、攻撃できる場所がなかった場合、行動終了とする
        if (attackLocationList.Count < 1)
        {
            // このターンは移動しない
            focusUnitObj.GetComponent<UnitInfo>().Moving(true); // 行動済み
            focusUnitObj = null;
            phase = Enums.PHASE.RESULT;
            return;
        }
        // TODO とりあえず一つ目を目的地とする
        Vector3 movePos = attackLocationList[0];

        Debug.Log("movePos:" + movePos);

        // 目標までのルートを取得し設定
        Main.GameManager.GetRoute().CheckShortestRoute(ref phaseManager, movePos);
        focusUnitObj.GetComponent<MoveController>().SetMoveRoots(moveRoot);

        // ターンとUI切り替え
        phase = Enums.PHASE.MOVE;
    }
    void EnemyMovePhase() {
        // 移動が終わったらフェイズを切り替える
        if (focusUnitObj.GetComponent<MoveController>().IsMoved())
        {
            // Unitリストの座標を更新
            focusUnitObj.GetComponent<UnitInfo>().Moving(true);
            Main.GameManager.GetUnit().MoveMapUnitObj(oldFocusUnitPos, focusUnitObj.transform.position);
            //Debug.Log(focusUnitObj);
            //Debug.Log(focusUnitObj.transform.position);

            // フェイズの切り替え
            phase = Enums.PHASE.BATTLE_STANDBY;
        }
    }
    void EnemyBattleStandbyPhase() {
        // バトルパラメータのセット
        enemyUnitObj = focusUnitObj;
        textEnemyHP = battleStandbyUI.GetComponent<BattleStandby>().textEnemyHP;
        textMyHP = battleStandbyUI.GetComponent<BattleStandby>().textMyHP;

        // 敵ユニット(攻撃する側)
        enemyAttackPower = Main.GameManager.GetCommonCalc().GetAttackDamage(focusUnitObj.GetComponent<UnitInfo>(), playerUnitObj.GetComponent<UnitInfo>());
        enemyAccuracy = Main.GameManager.GetCommonCalc().GetHitRate(focusUnitObj.GetComponent<UnitInfo>(), playerUnitObj.GetComponent<UnitInfo>());
        enemyDeathblow = Main.GameManager.GetCommonCalc().GetDeathBlowRete(focusUnitObj.GetComponent<UnitInfo>(), playerUnitObj.GetComponent<UnitInfo>());
        enemyAttackCount = Main.GameManager.GetCommonCalc().GetAttackCount(focusUnitObj.GetComponent<UnitInfo>(), playerUnitObj.GetComponent<UnitInfo>());

        // TODO
        //enemyAttackCount = 1;

        // プレイヤーユニット
        if (Main.GameManager.GetCommonCalc().GetCellDistance(
            playerUnitObj.transform.position,
            focusUnitObj.transform.position) <= playerUnitObj.GetComponent<UnitInfo>().attackRange)
        {
            // 反撃可能
            myAttackPower = Main.GameManager.GetCommonCalc().GetAttackDamage(playerUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
            myAccuracy = Main.GameManager.GetCommonCalc().GetHitRate(playerUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
            myDeathblow = Main.GameManager.GetCommonCalc().GetDeathBlowRete(playerUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
            myAttackCount = Main.GameManager.GetCommonCalc().GetAttackCount(playerUnitObj.GetComponent<UnitInfo>(), focusUnitObj.GetComponent<UnitInfo>());
        }
        else
        {
            // 反撃不可
            myAttackPower = -1;
            myAccuracy = -1;
            myDeathblow = -1;
            myAttackCount = 0;
        }
        // 敵の向きに合わせてUnitのアニメーション変更
        Vector3 distance = playerUnitObj.transform.position - focusUnitObj.transform.position;
        if (Mathf.Abs(distance.y) <= Mathf.Abs(distance.x))
        {
            if (playerUnitObj.transform.position.x > focusUnitObj.transform.position.x)
                focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.RIGHT);
            else
                focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.LEFT);
        }
        else
        {
            if (playerUnitObj.transform.position.y > focusUnitObj.transform.position.y)
                focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.UP);
            else
                focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);
        }
        phase = Enums.PHASE.BATTLE;
    }
    void EnemyBattlePhase() {
        
        if (!isBattle)
        {
            while (true)
            {
                // こちらの攻撃
                if (0 < enemyAttackCount)
                {
                    // 必殺の検証
                    enemyAttackState = Main.GameManager.GetCommonCalc().ProbabilityDecision(enemyDeathblow) ? Enums.BATTLE.DEATH_BLOW : Enums.BATTLE.NORMAL;

                    // 通常攻撃命中判定
                    if (enemyAttackState != Enums.BATTLE.DEATH_BLOW)
                        enemyAttackState = Main.GameManager.GetCommonCalc().GetHitDecision(enemyAccuracy) ? Enums.BATTLE.NORMAL : Enums.BATTLE.MISS;

                    // 通常攻撃か必殺が発生したら攻撃イベントとして登録する
                    AttackEvent attackEvent = gameObject.AddComponent<AttackEvent>();
                    attackEvent.phaseManager = this;

                    attackEvent.myUnitObj = enemyUnitObj;
                    attackEvent.myAttackPower = enemyAttackPower;
                    attackEvent.myAttackState = enemyAttackState;

                    attackEvent.targetUnitObj = playerUnitObj;
                    attackEvent.targetHPText = textMyHP;

                    battleManager.AddEvent(attackEvent);

                    enemyAttackCount--;
                }

                // プレイヤーUnitの反撃
                if (0 < myAttackCount)
                {
                    // 必殺の検証
                    myAttackState = Main.GameManager.GetCommonCalc().ProbabilityDecision(myDeathblow) ? Enums.BATTLE.DEATH_BLOW : Enums.BATTLE.NORMAL;

                    // 通常攻撃命中判定
                    if (myAttackState != Enums.BATTLE.DEATH_BLOW)
                        myAttackState = Main.GameManager.GetCommonCalc().GetHitDecision(myAccuracy) ? Enums.BATTLE.NORMAL : Enums.BATTLE.MISS;

                    // 通常攻撃か必殺が発生したら攻撃イベントとして登録する
                    AttackEvent attackEvent = gameObject.AddComponent<AttackEvent>();
                    attackEvent.phaseManager = this;

                    attackEvent.myUnitObj = playerUnitObj;
                    attackEvent.myAttackPower = myAttackPower;
                    attackEvent.myAttackState = myAttackState;

                    attackEvent.targetUnitObj = enemyUnitObj;
                    attackEvent.targetHPText = textEnemyHP;

                    battleManager.AddEvent(attackEvent);

                    myAttackCount--;
                }

                // 戦闘イベント登録終了
                if (myAttackCount <= 0 && enemyAttackCount <= 0) break;
            }

            // 敵をこちらに向かせる
            //Vector3 distance = playerUnitObj.transform.position - focusUnitObj.transform.position;
            //if (Mathf.Abs(distance.y) <= Mathf.Abs(distance.x))
            //{
            //    if (playerUnitObj.transform.position.x < focusUnitObj.transform.position.x)
            //        playerUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.RIGHT);
            //    else
            //        playerUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.LEFT);
            //}
            //else
            //{
            //    if (playerUnitObj.transform.position.y < focusUnitObj.transform.position.y)
            //        playerUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.UP);
            //    else
            //        playerUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);
            //}

            // バトルの実行
            battleManager.StartEvent();
            isBattle = true;
        }
        else if (!battleManager.isBattle())
        {
            // バトル終了
            isBattle = false;

            // プレイヤーUnitが生存していれば経験値取得処理を行う
            if (playerUnitObj)
            {
                // 経験値処理が終わるまでフェーズを停止
                phase = Enums.PHASE.STOP;

                // Exp取得処理の開始
                expGaugeController.GaugeUpdate(3000, playerUnitObj.GetComponent<UnitInfo>(), () =>
                {
                    phase = Enums.PHASE.RESULT;
                });
            }
            else
            {
                phase = Enums.PHASE.RESULT;
            }
        }
    }
    void EnemyResultPhase() {
        
        // 敵の向きを元に戻す
        if (playerUnitObj) playerUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);

        if (focusUnitObj)
        {
            // アニメーションを元に戻す
            focusUnitObj.GetComponent<MoveController>().PlayAnim(Enums.MOVE.DOWN);

            // グレースケールにする
            focusUnitObj.GetComponent<EffectController>().GrayScale(true);

            // 未移動であれば移動済みとする
            if (!focusUnitObj.GetComponent<UnitInfo>().isMoving())
            {
                Main.GameManager.GetUnit().MoveMapUnitObj(oldFocusUnitPos, focusUnitObj.transform.position);
                focusUnitObj.GetComponent<UnitInfo>().Moving(true); // 行動済み
            }
        }
        activeAreaManager.RemoveActiveArea();
        activeAreaManager.RemoveAttackArea();
        focusUnitObj = null;

        // ターンとUIの切り替え
        List<GameObject> list = Main.GameManager.GetUnit().GetUnBehaviorUnits(Enums.ARMY.ENEMY);
        if (list.Count == 0) phase = Enums.PHASE.END;
        else
            phase = Enums.PHASE.STANDBY;

        Main.GameManager.GetUnit().CheckEnemyUnits();
        Main.GameManager.GetUnit().CheckPlayerUnits();
    }
    void EnemyEndPhase() {
        // 自軍ユニットを全て未行動に戻す
        Main.GameManager.GetUnit().UnBehaviorUnitAll(Enums.ARMY.ENEMY);
        // プレイヤーターンに切り替える
        TurnChange(Enums.ARMY.ALLY);
        phase = Enums.PHASE.START;
    }

    /// <summary>
    /// 敵キャラ数をチェックする
    /// </summary>
    void EnemyCheck() {
        Main.GameManager.GetUnit().CheckEnemyUnits();
    }

    /// <summary>
    /// 行動エリアの初期化と削除
    /// </summary>
    //private void RemoveActiveArea() {
    //    activeAreaList = null;
    //    foreach (Transform a in activeArea.transform) Destroy(a.gameObject);
    //}

    ///// <summary>
    ///// マーカーの削除
    ///// </summary>
    //private void RemoveMarker() {
    //    foreach (Transform r in rootArea.transform) Destroy(r.gameObject);
    //}
}
