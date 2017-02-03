using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Constant {
        
        //坐标、索引
        public const int MIN_X = 0;                         //横坐标最小值
        public const int MAX_X = 16;                      //横坐标最大值
        public const int MIN_Y = 0;                         //纵坐标最小值
        public const int MAX_Y = 24;                      //纵坐标最大值
        public const int MIN_INDEX = 0;                //索引最小值
        public const int MAX_INDEX = (MAX_X + 1) * (MAX_Y + 1) - 1;             //索引最大值

        //地图元素值
        //0: LightGray    1: Yellow    2: DodgerBlue    3: LighGreen    4: Orange    5: Red    6: Gray
        public const int MIN_VALUE = 0;                 //地图元素最小值
        public const int MAX_VALUE = 6;                //地图元素最大值
        
        //
    }
}
