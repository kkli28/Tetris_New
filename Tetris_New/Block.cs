using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public abstract class Block {
        public abstract int getType();                         //方块类型
        public abstract Point getCore();                     //方块核心点
        public abstract Point getPoint1();                  //point1
        public abstract Point getPoint2();                  //point2
        public abstract Point getPoint3();                  //point3
        public abstract Point getPoint4();                  //point4，仅超级炸弹会使用
        public abstract int getState();                         //方块状态
        public abstract int getColor();                        //方块颜色
        public abstract int setColor();                         //设置方块颜色
        public abstract bool canRotate(Map map);                //能否旋转
        public abstract bool canMoveLeft(Map map);           //能否左移
        public abstract bool canMoveRight(Map map);        //能否右移
        public abstract bool canMoveDown(Map map);       //能否下移
        public abstract void update_RTT();                            //旋转后更新点
        public abstract void update_ML();                             //左移后更新点
        public abstract void update_MR();                            //右移后更新点
        public abstract void update_MD();                            //下移后更新点
    }
}
