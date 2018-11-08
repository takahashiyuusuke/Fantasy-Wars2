using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitManager : MonoBehaviour {

    //キャラクターの移動範囲
    [SerializeField]
    int MovePoint;

    Vector3 MoveX = new Vector3(100, 0, 0); // 横1マス移動する
    Vector3 MoveY = new Vector3(0, 100, 0); // 縦1マス移動する

    float speed = 5;
    //目的地の座標
    Vector3 targetPos;
    float targetPosX;
    float targetPosY;

    //タッチされたマスの座標
    public static Vector3  movePos;
    Vector3 PosAAA;
    Vector3 prevPos;

    //移動用リスト
    List<int> movelist = new List<int>();
    //移動計算結果のデータ格納用
    List<List<int>> MoveCell;


    public GameObject Map_script;

    void Start() {
        GetComponent<Map>();
        targetPos = transform.position;
        targetPosX = transform.position.x;
        targetPosY = transform.position.y;

        movelist.Add(1);
        movelist.Add(2);
        movelist.Add(1);
        movelist.Add(4);
        movelist.Add(4);
        movelist.Add(4);
        movelist.Add(2);
        movelist.Add(1);
        movelist.Add(2);
        movelist.Add(3);
        movelist.Add(4);
        movelist.Add(3);
        movelist.Add(4);
        movelist.Add(4);
        movelist.Add(5);
        movelist.Add(4);
    }

    // Update is called once per frame
    void Update() {
        SetPos();
        // 移動中かどうかの判定
        if (transform.position == targetPos) {
            // Listの要素があるかどうか
            if(movelist.Count == 0) {
                Debug.Log("ないよぉ！Listの要素ないよぉ！");
            }
            SetTargetPosition2();
        }
        PlayerMove();
    }

    // キーによる移動処理
    //void SetTargetPosition1() {
    //    prevPos = targetPos;

    //    // 上移動
    //    if (Input.GetKeyDown("up")){
    //        if (transform.position.y < 640){
    //            targetPos = transform.position + MoveY;
    //            return;
    //        }
    //    }
    //    // 下移動 
    //    if (Input.GetKeyDown("down")){
    //        if (transform.position.y > 80)
    //        {
    //            targetPos = transform.position - MoveY;
    //            return;
    //        }
    //    }

    //    //左移動
    //    if (Input.GetKeyDown("left")){
    //        if (transform.position.x > 80)
    //        {
    //            targetPos = transform.position - MoveX;
    //            return;
    //        }
    //    }

    //    //右移動
    //    if (Input.GetKeyDown("right")){
    //        if (transform.position.x < 400)
    //        {
    //            targetPos = transform.position + MoveX;
    //            return;
    //        }
    //    }
    //}

    void SetTargetPosition2() {         // リストによる移動処理
        prevPos = targetPos;

        switch (movelist[0])
        {
            //上移動
            case 1:
                if (transform.position.y < 640 && MovePoint > 0)
                {
                    targetPos = transform.position + MoveY;
                    //return;
                }
                Debug.Log("up");
                movelist.RemoveAt(0);
                break;
            //下移動
            case 2:
                if (transform.position.y > 80 && MovePoint > 0)
                {
                    targetPos = transform.position - MoveY;
                    //return;
                }
                Debug.Log("Down");
                movelist.RemoveAt(0);
                break;

            //左移動
            case 3:
                if (transform.position.x > 80 && MovePoint > 0)
                {
                    targetPos = transform.position - MoveX;
                    //return;
                }
                Debug.Log("Left");
                movelist.RemoveAt(0);
                break;

            //右移動
            case 4:
                if (transform.position.x < 400 && MovePoint > 0)
                {
                    targetPos = transform.position + MoveX;
                    //return;
                }
                Debug.Log("right");
                movelist.RemoveAt(0);
                break;
        }
    }

    void PlayerMove() {
        // 現在地から目標地点までの間を一定速度で移動
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);

        //transform.position = Vector3.MoveTowards(transform.position, movePos, speed);
    }

    public void SetPos() {
        //タッチされたマスの座標を受け取る
        movePos = Map.pos;
        
        // 受け取った座標と現在の座標を引き算で比較
        PosAAA = transform.position - movePos;

        if(transform.position.y <= PosAAA.y) {
            movelist.Add(2);
        }

        if (transform.position.y >= PosAAA.y) {
            movelist.Add(1);
        }
        if (transform.position.x <= PosAAA.x)
        {
            movelist.Add(3);
        }
        if (transform.position.x >= PosAAA.x)
        {
            movelist.Add(4);
        }

    }

    // 移動可能なマスを検索
    public void ontouch(){
    }
}
