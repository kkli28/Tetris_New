﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Block_O : Block {
        Point point0;                   //核心点
        Point point1;
        Point point2;
        Point point3;

        int value;              //方块颜色
        int type;               //方块类型
        int state;              //方块状态。O型方块只有一个状态
        
        //构造函数
        public Block_O() {
            value = new Random().Next(Constant.MIN_BLOCK_VALUE, Constant.MAX_BLOCK_VALUE+1);
            point0 = new Point(0, 5);
            point1 = new Point(0, 6);
            point2 = new Point(1, 5);
            point3 = new Point(1, 6);
            type = Constant.BLOCK_TYPE_O;
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
            return false;           //O型方块不能旋转
        }

        //能否左移
        public override bool canMoveLeft(Map map) {
            Point[] points = { point0, point2 };
            return checkPos(map, points, 2, "left");
        }

        //能否右移
        public override bool canMoveRight(Map map) {
            Point[] points = { point1, point3 };
            return checkPos(map, points, 2, "right");
        }

        //能否下移
        public override bool canMoveDown(Map map) {
            Point[] points = { point2, point3 };
            return checkPos(map, points, 2, "down");
        }

        //旋转--更新点和状态
        public override void update_RTT() {
            return;                 //O型方块不旋转
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
