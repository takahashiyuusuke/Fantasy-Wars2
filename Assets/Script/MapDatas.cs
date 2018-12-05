using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDatas {
    public Struct.FieldBase GetData1() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ1";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,2,3,3,2,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {2,2,0,0,2,2},
            {2,2,0,0,2,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData2() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ2";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,0},
            {2,2,1,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,1,0},
            {0,0,0,0,0,0},
            {0,1,0,0,0,2},
            {0,0,0,0,0,2},
            {0,0,0,0,2,2},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData3() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ3";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,2,2,2,2,2},
            {2,2,0,0,0,2},
            {0,0,0,0,0,2},
            {0,0,0,0,0,0},
            {2,2,0,0,0,2},
            {2,2,2,2,0,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData4() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ4";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,0},
            {2,0,1,1,0,2},
            {0,3,1,1,3,0},
            {0,1,1,3,3,0},
            {0,2,0,0,0,0},
            {0,0,0,0,0,2},
            {0,0,1,1,1,2},
            {2,0,0,0,0,2},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData5() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ5";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,2,0,0},
            {0,0,0,0,0,0},
            {1,0,1,3,0,0},
            {2,0,3,0,3,0},
            {0,0,0,0,0,0},
            {0,0,0,0,2,0},
            {0,3,0,0,0,0},
            {2,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData6() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ6";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,2},
            {0,1,0,0,0,2},
            {0,0,0,0,1,2},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,1},
            {0,0,0,0,0,2},
            {0,0,0,0,0,2},
        };
        return fieldBase;
    }
    public Struct.FieldBase GetData7() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ7";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,2,2,2,2,2},
            {2,2,2,0,2,2},
            {2,0,2,0,0,2},
            {0,0,0,0,0,0},
            {3,3,0,0,0,0},
            {3,3,0,0,0,2},
            {2,2,0,0,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }



    public Struct.FieldBase GetData8() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ8";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {0,0,0,0,0,0},
            {3,0,0,0,0,0},
            {0,0,0,2,0,0},
            {0,0,0,0,3,0},
            {0,2,0,0,2,0},
            {0,0,0,0,0,0},
            {2,2,0,0,2,2},
            {2,0,0,0,0,2},
        };
        return fieldBase;
    }


    public Struct.FieldBase GetData9() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ9";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,0,0,0,0,2},
            {2,0,4,4,0,2},
            {0,1,4,4,0,0},
            {4,0,0,0,0,0},
            {1,0,0,1,0,4},
            {0,1,4,4,0,1},
            {0,0,4,4,0,0},
            {0,0,0,0,0,0},
        };
        return fieldBase;
    }

    public Struct.FieldBase GetData10() {
        Struct.FieldBase fieldBase = new Struct.FieldBase();
        fieldBase.name = "マップ10";
        fieldBase.width = 6;
        fieldBase.height = 8;
        fieldBase.cells = new int[,]{
            {2,2,2,2,2,2},
            {0,2,2,0,2,2},
            {0,0,0,0,0,2},
            {0,0,0,0,2,2},
            {0,0,0,0,0,2},
            {0,0,0,0,0,2},
            {0,0,0,0,0,0},
            {0,0,0,0,2,2},
        };
        return fieldBase;
    }
}

