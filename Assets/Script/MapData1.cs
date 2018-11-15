using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData1 : MonoBehaviour {
    public FieldBase GetData(){
        FieldBase fieldBase = new FieldBase();
        fieldBase.name = "マップ1";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {1,1,1,1,1,1},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }
}
