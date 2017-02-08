using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Block_S:Block {
        Point point0;
        Point point1;
        Point point2;
        Point point3;

        int value;
        int type;
        int state;

        //构造函数
        public Block_S() {
            value = new Random().Next(Constant.MIN_BLOCK_VALUE, Constant.MAX_BLOCK_VALUE + 1);
            point0 = new Point(0, 5);
            point1 = new Point(0, 6);
            point2 = new Point(1, 4);
            point3 = new Point(1, 5);
            type = Constant.BLOCK_TYPE_S;
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
            Point[] points = {
                new Point(),
                new Point(),
                new Point()
            };

            if (state == 0) {
                points[0].set(point2.X + 1, point2.Y);
                points[1].set(point0.X, point0.Y - 1);
                points[2].set(point3.X + 1, point3.Y);
            }else {
                points[0].set(point2.X, point2.Y - 1);
                points[1].set(point1.X, point1.Y + 1);
                points[2].set(point1.X, point1.Y + 2);
            }

            return checkPoints(map, points, 3);
        }

        //能否左移
        public override bool canMoveLeft(Map map) {
            Point[] points;
            int size = 0;

            if (state == 0) {
                points = new Point[] { point0, point2 };
                size = 2;
            }else {
                points = new Point[] { point0, point1, point2 };
                size = 3;
            }

            return checkPos(map, points, size, "left");
        }

        //能否右移
        public override bool canMoveRight(Map map) {
            Point[] points;
            int size = 0;

            if (state == 0) {
                points = new Point[] { point1, point3 };
                size = 2;
            }else {
                points = new Point[] { point1, point2, point3 };
                size = 3;
            }

            return checkPos(map, points, size, "right");
        }

        //能否下移
        public override bool canMoveDown(Map map) {
            Point[] points;
            int size = 0;

            if (state == 0) {
                points = new Point[] { point1, point2, point3 };
                size = 3;
            }else {
                points = new Point[] { point0, point2 };
                size = 2;
            }

            return checkPos(map, points, size, "down");
        }

        //旋转--更新点
        public override void update_RTT() {
            if (state == 0) {
                ++point0.X;
                --point0.Y;
                point1.Y -= 2;
                ++point2.X;
                ++point2.Y;
            } else {
                --point0.X;
                ++point0.Y;
                point1.Y += 2;
                --point2.X;
                --point2.Y;
            }

            state = (state + 1) % 2;
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
