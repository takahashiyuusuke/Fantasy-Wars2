using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Novel {

    public class Touch_screenComponent : AbstractComponent {

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Touch_screenComponent() {
            //必須項目
            this.arrayVitalParam = new List<string> {
    "touch"
    };
        }

        public override void start() {
            string flg = this.param["touch"];
                 
        }
    }
}