using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris_New {
    public class Constant {
        
        //坐标、索引
        public const int MIN_X = 0;                         //横坐标最小值
        public const int MAX_X = 23;                      //横坐标最大值
        public const int MIN_Y = 0;                         //纵坐标最小值
        public const int MAX_Y = 11;                      //纵坐标最大值
        public const int MIN_INDEX = 0;                //索引最小值
        public const int MAX_INDEX = (MAX_X + 1) * (MAX_Y + 1) - 1;             //索引最大值

        //地图元素值，代表颜色
        //0: (224,224,224)    1: Yellow    2: DodgerBlue    3: Lime    4: Orange    5: Red    6: Blue    7: Gray    8: White
        public const int MIN_VALUE = 0;                 //地图元素最小值
        public const int MAX_VALUE = 8;                //地图元素最大值
        public const int VALUE_NONE = 0;
        public const int MIN_BLOCK_VALUE = 1;                           //普通方块值的最小值
        public const int MAX_BLOCK_VALUE = 6;                          //普通方块值的最大值
        public const int CUSTOM_VALUE = 3;                               //自定义模式中的残留方块的值
        public const int BLOCK_BOMB_CORE_VALUE = 5;            //超级炸弹中心方块
        public const int BLOCK_BOMB_OUT_VALUE = 7;              //超级炸弹周围方块
        public const int EXPLOSION_VALUE = 8;                          //爆炸中的方块值
        
        //数值到颜色的转换
        public static Color valueToColor(int value) {
            switch (value) {
                case 0: return Color.FromArgb(224, 224, 224);
                case 1: return Color.Yellow;
                case 2: return Color.DodgerBlue;
                case 3: return Color.Lime;
                case 4: return Color.Orange;
                case 5: return Color.Red;
                case 6: return Color.Blue;
                case 7: return Color.Gray;
                case 8: return Color.White;
                default: throw new Exception();
            }
        }

        //方块类型
        public const int MIN_BLOCK_TYPE = 0;
        public const int MAX_BLOCK_TYPE = 16;
        public const int MAX_CLASSIC_BLOCK_TYPE = 6;
        public const int MAX_NO_BOMB_BLOCK_TYPE = 14;
        public const int BLOCK_TYPE_O = 0;
        public const int BLOCK_TYPE_I = 1;
        public const int BLOCK_TYPE_T = 2;
        public const int BLOCK_TYPE_L = 3;
        public const int BLOCK_TYPE_J = 4;
        public const int BLOCK_TYPE_Z = 5;
        public const int BLOCK_TYPE_S = 6;
        public const int BLOCK_TYPE_EXTENSION6 = 7;                    //1 2 7 8
        public const int BLOCK_TYPE_EXTENSION7 = 8;                   //1 2 8 9
        public const int BLOCK_TYPE_EXTENSION8 = 9;                   //2 3 7 8
        public const int BLOCK_TYPE_EXTENSION1 = 10;                 //方块形状（3x3方块，1~9）：1 5 9
        public const int BLOCK_TYPE_EXTENSION2 = 11;                 //4 5 6
        public const int BLOCK_TYPE_EXTENSION4 = 12;                 //1 3 5
        public const int BLOCK_TYPE_EXTENSION5 = 13;                 //4 5
        public const int BLOCK_TYPE_EXTENSION3 = 14;                 //4 8
        public const int BLOCK_TYPE_BOMB = 15;
        public const int BLOCK_TYPE_SUPER_BOMB = 16;

        //获取方块的小方块数目，若方块类型的常数改变，也需要作出相应改变
        public static int getBlockCount(int blockType) {
            if (blockType >= 0 && blockType <= 9) return 4;
            else if (blockType >= 10 && blockType <= 12) return 3;
            else if (blockType == 13 || blockType == 14) return 2;
            else if (blockType == 15) return 1;
            else return 5;
        }

        //获取方块
        public static Block getBlock(int val) {
            switch (val) {
                case 0: return new Block_O();
                case 1: return new Block_I();
                case 2: return new Block_T();
                case 3: return new Block_L();
                case 4: return new Block_J();
                case 5: return new Block_Z();
                case 6: return new Block_S();
                case 7: return new Block_E1();
                case 8: return new Block_E2();
                case 9: return new Block_E3();
                case 10: return new Block_E4();
                case 11:return new Block_E5();
                case 12: return new Block_E6();
                case 13: return new Block_E7();
                case 14: return new Block_E8();
                case 15: return new Block_Bomb();
                case 16: return new Block_SuperBomb();
                default: throw new Exception();
            }
        }
        
        //游戏模式
        public const int CLASSIC_MODE = 0;
        public const int CHALLENGE_MODE = 1;
        public const int FIRE_MODE = 2;
        public const int CUSTOM_MODE = 3;

        //颜色
        public static Color BorderColor0 = Color.Silver;
        public static Color BorderColor1 = Color.Blue;
        public static Color TextColor0 = Color.Silver;
        public static Color TextColor1 = Color.Blue;
        public static Color CustomBlockColor = Color.Lime;
        public static Color BTNS_DEFAULT_COLOR = Color.FromArgb(224, 224, 224);
        public static Color BOMB_CORE_COLOR = Color.Red;
        public static Color EXPLOSION_COLOR = Color.White;

        //扩展方块
        public const bool NO_EXTRA = false;
        public const bool HAVE_EXTRA = true;

        //炸弹是否可用
        public const bool NO_BOMB = false;
        public const bool HAVE_BOMB = true;

        //按钮状态
        public const int BUTTON_STATE0 = 0;
        public const int BUTTON_STATE1 = 1;
        public static Color cantClickBtnColor = Color.Gray;

        //关卡
        public const int MIN_HURDLE = 0;
        public const int MAX_HURDLE = 7;

        //计时器间隔
        public const int DEFAULT_INTERVAL = 500;
    }
}
