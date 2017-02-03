using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Map {
        int[,] map;
        int minX;                       //有值的最小行号
        
        //构造函数
        public Map() {
            map = new int[Constant.MAX_X + 1, Constant.MAX_Y + 1];
            for (int i = Constant.MIN_X; i <= Constant.MAX_X; i++)
                for (int j = Constant.MIN_Y; j <= Constant.MAX_Y; j++)
                    map[i, j] = 0;
        }

        //判断值是否合法
        public bool validX(int x) {
            if (x < Constant.MIN_X || x > Constant.MAX_X) return false;
            return true;
        }

        public bool validY(int y) {
            if (y < Constant.MIN_Y || y > Constant.MAX_Y) return false;
            return true;
        }

        public bool validIndex(int index) {
            if (index < Constant.MIN_INDEX || index > Constant.MAX_INDEX) return false;
            return true;
        }

        public bool validValue(int value) {
            if (value < Constant.MIN_VALUE || value > Constant.MAX_VALUE) return false;
            return true;
        }

        //设置值--通过坐标
        public void setValue(int i,int j,int value) {
            if (validX(i) && validY(j) && validValue(value))
                map[i, j] = value;
            else throw new Exception("Map::setValue(int,int,int), ["
                +i.ToString()+","+j.ToString()+","+value.ToString()+"].");
        }

        //设置值--通过点
        public void setValue() {
            //TODO:
        }
    }
}
