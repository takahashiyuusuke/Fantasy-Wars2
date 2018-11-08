using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    //タイルのプレハブを置く配列
    public GameObject[] Map = new GameObject[3];
    public GameObject Player;

    List<GameObject> Cells = new List<GameObject>();

    List<Vector3> Poslist = new List<Vector3>();

    [SerializeField]
    Transform MapParent;

    public int[] PlayerPosition = { 200, 300 };

    Vector3 SetPos;

    // マップの配置
    public int[,] MapTile = {
        {1,1,1,1,1,1},
        {0,0,0,0,0,0},
        {0,0,0,0,0,0},
        {0,0,0,0,0,0},
        {0,0,0,0,0,0},
        {0,0,0,0,0,0},
        {0,0,0,0,0,0},
        {0,0,0,0,0,0},
    };

    // Use this for initialization
    void Start() {
        //マップ生成処理
        CreateMap();

    }


    // Update is called once per frame
    void Update() {

    }

    public void CreateMap() {
        //マップ生成
        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 6; x++) {
                Instantiate(Map[MapTile[(MapTile.GetLength(0) - 1 - y), x]], new Vector3((x + 0.5f) * 100, (y + 1) * 100, 0.0f), Quaternion.identity, MapParent);

                //Cells.Add(Map);
            }
        }
        //プレイヤーの生成
        Instantiate(Player, new Vector3(40, 80, 0), Quaternion.identity, MapParent);
    }
    void MoveRangeSearch() {
        //
    }
}
