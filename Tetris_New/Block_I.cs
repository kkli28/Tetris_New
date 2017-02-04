using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Block_I:Block {
        Point point0;
        Point point1;
        Point point2;
        Point point3;
        int value;
        int type;
        int state;

        //构造函数
        public Block_I() {
            value = new Random().Next(Constant.MIN_BLOCK_VALUE, Constant.MAX_BLOCK_VALUE + 1);
            point0 = new Point(0, 4);
            point1 = new Point(0, 5);
            point2 = new Point(0, 6);
            point3 = new Point(0, 7);
            type = Constant.BLOCK_TYPE_I;
            state = 0;
        }

        //获取Block信息
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
            Point p1 = new Point(point0.X - 1, point0.Y);
            Point p2 = new Point(point2.X + 1, point2.Y);
            Point p3 = new Point(point3.X + 1, point3.Y);
            Point p4 = new Point(point2.X + 2, point2.Y);
            Point p5 = new Point(point3.X + 2, point3.Y);
            Point nextP1 = new Point();
            Point nextP2 = new Point();
            Point nextP3 = new Point();

            if (state == 0) {
                nextP1.setPoint(point1.X - 1, point1.Y);
                nextP2.setPoint(point1.X + 1, point1.Y);
                nextP3.setPoint(point1.X + 2, point1.Y);
            } else {
                nextP1.setPoint(point1.X, point1.Y - 1);
                nextP2.setPoint(point1.X, point1.Y + 1);
                nextP3.setPoint(point1.X, point1.Y + 2);
            }

            if (p1.isValid() && p2.isValid() && p3.isValid() && p4.isValid() && p5.isValid()
                && nextP1.isValid() && nextP2.isValid() && nextP3.isValid()
                && map.getValue(p1) == 0 && map.getValue(p2) == 0 && map.getValue(p3) == 0
                && map.getValue(p4) == 0 && map.getValue(p5) == 0
                && map.getValue(nextP1) == 0 && map.getValue(nextP2) == 0 && map.getValue(nextP3) == 0)
                return true;
            else return false;
        }

        //能否左转
        public override bool canMoveLeft(Map map) {
            if (state == 0) {
                Point p1 = new Point(point0.X, point0.Y - 1);
                if (p1.isValid() && map.getValue(p1) == 0) return true;
                else return false;
            } else {
                Point p1 = new Point(point0.X, point0.Y - 1);
                Point p2 = new Point(point1.X, point1.Y - 1);
                Point p3 = new Point(point2.X, point2.Y - 1);
                Point p4 = new Point(point3.X, point3.Y - 1);

                if (p1.isValid() && p2.isValid() && p3.isValid() && p4.isValid()
                    && map.getValue(p1) == 0 && map.getValue(p2) == 0
                    && map.getValue(p3) == 0 && map.getValue(p4) == 0)
                    return true;
                else return false;
            }
        }

        //能否右移
        public override bool canMoveRight(Map map) {
            if (state == 0) {
                Point p1 = new Point(point3.X, point3.Y + 1);
                if (p1.isValid() && map.getValue(p1) == 0) return true;
                else return false;
            }else {
                Point p1 = new Point(point0.X, point0.Y + 1);
                Point p2 = new Point(point1.X, point1.Y + 1);
                Point p3 = new Point(point2.X, point2.Y + 1);
                Point p4 = new Point(point3.X, point3.Y + 1);

                if (p1.isValid() && p2.isValid() && p3.isValid() && p4.isValid()
                    && map.getValue(p1) == 0 && map.getValue(p2) == 0
                    && map.getValue(p3) == 0 && map.getValue(p4) == 0)
                    return true;
                else return false;
            }
        }

        //能否下移
        public override bool canMoveDown(Map map) {
            if (state == 0) {
                Point p1 = new Point(point0.X+1, point0.Y);
                Point p2 = new Point(point1.X+1, point1.Y);
                Point p3 = new Point(point2.X+1, point2.Y);
                Point p4 = new Point(point3.X+1, point3.Y);

                if (p1.isValid() && p2.isValid() && p3.isValid() && p4.isValid()
                    && map.getValue(p1) == 0 && map.getValue(p2) == 0
                    && map.getValue(p3) == 0 && map.getValue(p4) == 0)
                    return true;
                else return false;
            } else {
                Point p1 = new Point(point3.X + 1, point3.Y);
                if (p1.isValid() && map.getValue(p1) == 0) return true;
                else return false;
            }
        }

        //旋转--更新点和状态
        public override void update_RTT() {
            if (state == 0) {
                --point0.X;
                ++point0.Y;
                ++point2.X;
                --point2.Y;
                point3.X += 2;
                point3.Y -= 2;
            }else {
                ++point0.X;
                --point0.Y;
                --point2.X;
                ++point2.Y;
                point3.X -= 2;
                point3.Y += 2;
            }
            state = (state + 1) % 2;            //更新状态
        }

        //左移--更新点
        public override void update_ML() {
            --point0.Y;
            --point1.Y;
            --point2.Y;
            --point3.Y;
        }

        //右移--更新点
        public override void update_MR() {
            ++point0.Y;
            ++point1.Y;
            ++point2.Y;
            ++point3.Y;
        }

        //下移--更新点
        public override void update_MD() {
            ++point0.X;
            ++point1.X;
            ++point2.X;
            ++point3.X;
        }

        //复制方块信息
        public override void copyFrom(Block b) {
            if (type != b.getType()) throw new Exception();
            point0.copyFrom(b.getPoint0());
            point1.copyFrom(b.getPoint1());
            point2.copyFrom(b.getPoint2());
            point3.copyFrom(b.getPoint3());

            state = b.getState();
            value = b.getValue();
        }
    }
}
