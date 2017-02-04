﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        //0: Black    1: Yellow    2: DodgerBlue    3: Lime    4: Orange    5: Red    6: Gray    7:LightGray (爆炸中)
        public const int MIN_VALUE = 0;                 //地图元素最小值
        public const int MAX_VALUE = 7;                //地图元素最大值
        public const int VALUE_NONE = 0;

        //数值到颜色的转换
        public Color valueToColor(int value) {
            switch (value) {
                case 0: return Color.Black;
                case 1: return Color.Yellow;
                case 2: return Color.DodgerBlue;
                case 3: return Color.Lime;
                case 4: return Color.Orange;
                case 5: return Color.Red;
                case 6: return Color.Gray;
                case 7: return Color.LightGray;
                default: throw new Exception("Constant::valueToColor(int), value: " + value.ToString());
            }
        }

        //方块类型
        public const int MIN_BLOCK_TYPE = 0;
        public const int MAX_BLOCK_TYPE = 15;
        public const int BLOCK_TYPE_O = 0;
        public const int BLOCK_TYPE_I = 1;
        public const int BLOCK_TYPE_T = 2;
        public const int BLOCK_TYPE_L = 3;
        public const int BLOCK_TYPE_J = 4;
        public const int BLOCK_TYPE_Z = 5;
        public const int BLOCK_TYPE_S = 6;
        public const int BLOCK_TYPE_BOMB = 7;
        public const int BLOCK_TYPE_SUPER_BOMB = 8;
        public const int BLOCK_TYPE_EXTENSION1 = 9;                     //方块形状（3x3方块，1~9）：1 5 9
        public const int BLOCK_TYPE_EXTENSION2 = 10;                   //4 5 6
        public const int BLOCK_TYPE_EXTENSON3 = 11;                    //4 8
        public const int BLOCK_TYPE_EXTENSION4 = 12;                   //2 4 6
        public const int BLOCK_TYPE_EXTENSION5 = 13;                   //4 5
        public const int BLOCK_TYPE_EXTENSON6 = 14;                    //1 2 7 8
        public const int BLOCK_TYPE_EXTENSION7 = 15;                   //1 2 8 9


    }
}
