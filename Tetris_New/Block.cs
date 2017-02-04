using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public abstract class Block {
        public abstract int getType();                         //方块类型
        public abstract Point getPoint0();                     //方块核心点
        public abstract Point getPoint1();                  //point1
        public abstract Point getPoint2();                  //point2
        public abstract Point getPoint3();                  //point3
        public abstract Point getPoint4();                  //point4，仅超级炸弹会使用
        public abstract int getState();                         //方块状态
        public abstract int getValue();                        //方块值
        public abstract void setValue(int);                          //设置方块值
        public abstract bool canRotate(Map);                //能否旋转
        public abstract bool canMoveLeft(Map);           //能否左移
        public abstract bool canMoveRight(Map);        //能否右移
        public abstract bool canMoveDown(Map);       //能否下移
        public abstract void update_RTT();                            //旋转--更新点
        public abstract void update_ML();                             //左移--更新点
        public abstract void update_MR();                            //右移--更新点
        public abstract void update_MD();                            //下移--更新点
        public abstract void copyFrom(Block);                   //复制方块信息
    }
}
