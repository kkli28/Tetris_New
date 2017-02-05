using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Block_Bomb:Block {
        Point point0;

        int value;
        int type;
        int state;

        //构造函数
        public Block_Bomb() {
            value = Constant.BLOCK_BOMB_OUT_VALUE;
            //TODO: 修改值
            point0 = new Point(0, 5);
            type = Constant.BLOCK_TYPE_BOMB;
            state = 0;
        }

        //获取方块信息
        public override Point getPoint0() { return point0; }
        public override Point getPoint1() { throw new Exception(); }
        public override Point getPoint2() { throw new Exception(); }
        public override Point getPoint3() { throw new Exception(); }
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
            return false;
        }

        //能否左移
        public override bool canMoveLeft(Map map) {
            Point p1 = new Point(point0.X, point0.Y - 1);
            if (p1.isValid() && map.getValue(p1) == 0) return true;
            else return false;
        }

        //能否右移
        public override bool canMoveRight(Map map) {
            Point p1 = new Point(point0.X, point0.Y + 1);
            if (p1.isValid() && map.getValue(p1) == 0) return true;
            else return false;
        }

        //能否下移
        public override bool canMoveDown(Map map) {
            Point p1 = new Point(point0.X+1, point0.Y);
            if (p1.isValid() && map.getValue(p1) == 0) return true;
            else return false;
        }

        //旋转--更新点
        public override void update_RTT() {
            ;
        }

        //左移--更新点
        public override void update_ML() {
            --point0.Y;
        }

        //右移--更新点
        public override void update_MR() {
            ++point0.Y;
        }

        //下移--更新点
        public override void update_MD() {
            ++point0.X;
        }

        //复制方块信息
        public override void copyFrom(Block b) {
            if (type != b.getType()) throw new Exception();
            point0.copyFrom(b.getPoint0());
        }
    }
}
