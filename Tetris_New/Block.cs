using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public abstract class Block {
        public abstract Point getPoint0();                     //方块核心点
        public abstract Point getPoint1();                  //point1
        public abstract Point getPoint2();                  //point2
        public abstract Point getPoint3();                  //point3
        public abstract Point getPoint4();                  //point4，仅超级炸弹会使用
        
        public abstract int getType();                         //方块类型
        public abstract int getValue();                        //方块值
        public abstract int getState();                         //方块状态
        
        public abstract void setValue(int val);                          //设置方块值

        public abstract bool canRotate(Map map);                //能否旋转
        public abstract bool canMoveLeft(Map map);           //能否左移
        public abstract bool canMoveRight(Map map);        //能否右移
        public abstract bool canMoveDown(Map map);       //能否下移
        public abstract void update_RTT();                            //旋转--更新点
        public abstract void update_ML();                             //左移--更新点
        public abstract void update_MR();                            //右移--更新点
        public abstract void update_MD();                            //下移--更新点
        public abstract void copyFrom(Block b);                   //复制方块信息

        //检查 pos 方向是否可移动
        public bool checkPos(Map map, Point[] points, int len, string pos) {
            if (len < 0 || len >= Constant.MAX_INDEX) throw new Exception();

            int addX = 0;
            int addY = 0;
            if (pos == "left") addY = -1;
            else if (pos == "right") addY = 1;
            else if (pos == "up") addX = -1;
            else if (pos == "down") addX = 1;
            else throw new Exception();

            for (int i = 0; i < len; ++i) {
                Point p = new Point(points[i].X + addX, points[i].Y + addY);
                if (!p.isValid()) return false;
                if (map.getValue(p) != 0) return false;
            }
            return true;
        }

        public bool checkPoints(Map map,Point[] points,int len) {
            if (len < 0 || len >= Constant.MAX_INDEX) throw new Exception();
            for (int i = 0; i < len; i++) {
                if (!points[i].isValid()) return false;
                if (map.getValue(points[i]) != 0) return false;
            }
            return true;
        }
        
    }
}
