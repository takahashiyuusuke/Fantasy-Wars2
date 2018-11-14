using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour {
    public enum AREA { NONE, UNIT, MOVE, ATTACK }


    public void CheckMoveArea(ref CursorManager cursorManager) {
        //スタート地点からエンドまで再帰的に移動コストをチェックする
        cursorManager.activeAreaList = new NodeMove[Map.GetFieldData().height, Map.GetFieldData().width];
        cursorManager.activeAreaList[-(int)cursorManager.focusUnit.moveController.getPos().y, (int)cursorManager.focusUnit.moveController.getPos().x].aREA = AREA.UNIT;
        CheckMoveAreaRecursive(ref cursorManager, cursorManager.focusUnit.moveController.getPos(), 0);
    }

    private void CheckMoveAreaRecursive(ref CursorManager cursorManager, Vector3 checkPos, int previousCost) {
        //

    }
}

public struct NodeMove {
    public int cost;
    public RouteManager.AREA aREA;
}
