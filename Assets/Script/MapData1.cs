using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData1 : MonoBehaviour {
    public Struct.FieldBase GetData(){
        Struct.FieldBase fieldBase = new Struct.FieldBase();
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
