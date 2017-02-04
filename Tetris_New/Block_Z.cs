using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Block_Z:Block {
        Point point0;
        Point point1;
        Point point2;
        Point point3;

        int value;
        int type;
        int state;

        //构造函数
        public Block_T() {
            value = new Random().Next(Constant.MIN_BLOCK_VALUE, Constant.MAX_BLOCK_VALUE + 1);
            //TODO: 更改值
            point0 = new Point(0, 4);
            point1 = new Point(0, 5);
            point2 = new Point(1, 5);
            point3 = new Point(1, 6);
            type = Constant.BLOCK_TYPE_Z;
            state = 0;
        }

        //获取方块信息
        public override Point getPoint0() { return point0; }
        public override Point getPoint1() { return point1; }
        public override Point getPoint2() { return point2; }
        public override Point getPoint3() { return point3; }
        public override Point getPoint4() { throw new Exception(); }
        public override int getType() { return type; }
        public override int getState() { return state; }
        public override int getValue() { return value; }

        //设置值
        public override void setValue(int val) {
            if (val < Constant.MIN_VALUE || val > Constant.MAX_VALUE)
                throw new Exception();
            value = val;
        }

        //能否旋转
        public override bool canRotate(Map map) {
            if (state == 0) {
                Point p1 = new Point(point3.X + 1, point3.Y);
                Point nextP1 = new Point(point1.X, point1.Y + 1);
                Point nextP2 = new Point(point2.X + 1, point2.Y);

                if (p1.isValid() && nextP1.isValid() && nextP2.isValid())
                    return true;
                else return false;
            }else if (state == 1) {
                Point p1 = new Point(point3.X, point3.Y - 1);
                Point nextP1 = new Point(point2.X, point2.Y - 1);
                Point nextP2 = new Point(point1.X + 1, point1.Y);

                if (p1.isValid() && nextP1.isValid() && nextP2.isValid())
                    return true;
                else return false;
            } else if (state == 2) {
                Point p1 = new Point(point3.X - 1, point3.Y);
                Point nextP1 = new Point(point2.X - 1, point2.Y);
                Point nextP2 = new Point(point1.X, point1.Y - 1);

                if (p1.isValid() && nextP1.isValid() && nextP2.isValid())
                    return true;
                else return false;
            } else {
                Point p1 = new Point(point3.X, point3.Y + 1);
                Point nextP1 = new Point(point1.X - 1, point1.Y);
                Point nextP2 = new Point(point2.X, point2.Y + 1);

                if (p1.isValid() && nextP1.isValid() && nextP2.isValid())
                    return true;
                else return false;
            }
        }

        //能否左移
        public override bool canMoveLeft(Map map) {
            if (state == 0) {
                Point p1 = new Point(point0.X, point1.Y - 1);
                Point p2 = new Point(point2.X, point2.Y - 1);

                if (p1.isValid() && p2.isValid() && map.getValue(p1) == 0 && map.getValue(p2) == 0)
                    return true;
                else return false;
            }else if (state == 1) {
                Point p1 = new Point(point1.X, point1.Y - 1);
                Point p2 = new Point(point2.X, point2.Y - 1);
                Point p3 = new Point(point3.X, point3.Y - 1);

                if (p1.isValid() && p2.isValid() && p3.isValid()
                    && map.getValue(p1) == 0 && map.getValue(p2) == 0 && map.getValue(p3) == 0)
                    return true;
                else return false;
            }else if (state == 2) {
                //TODO: 
            }else {

            }
        }
    }
}
