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
        public void setValue(int x, int y, int value) {
            if (validX(x) && validY(y) && validValue(value))
                map[x, y] = value;
            else throw new Exception("Map::setValue(int,int,int), (x,y,vlaue): "
                + x.ToString() + "," + y.ToString() + "," + value.ToString());
        }

        //设置值--通过点
        public void setValue(Point p) {
            if (!p.isValid()) throw new Exception("Map::setValue(Point), 点不合法！");
            int x = p.X;
            int y = p.Y;
            map[x, y] = p.Color;
            if (x < minX) minX = x;                     //设置有值的最小行数
        }

        //获取值--通过坐标
        public int getValue(int x, int y) {
            if (validX(x) && validY(y)) return map[x, y];
            else throw new Exception("Map::getValue(int,int), (x,y): " + x.ToString() + "," + y.ToString());
        }

        //获取值--通过索引
        public int getValue(int index) {
            if (validIndex(index)) {
                int x = index / (Constant.MAX_Y + 1);
                int y = index % (Constant.MAX_Y + 1);
                return map[x, y];
            } else throw new Exception("Map::getValue(int), index: " + index.ToString());
        }

        //获取值--通过点
        public int getValue(Point p) {
            if (p.isValid()) return map[p.X, p.Y];
            else throw new Exception("Map::getValue(Point), 点不合法！");
        }

        //获取图信息
        public string getInfo() {
            string s = "";
            for (int i = 0; i < Constant.MAX_X + 1; ++i) {
                for (int j = 0; j < Constant.MAX_Y + 1; ++j) {
                    s += map[i, j] + " ";
                }
                s += "\n";
            }
            return s;
        }

        //消除全不为0的行，并将上面的所有行下移，out参数标志消除的行数
        public void eliminate(Block block, out int count) {
            count = 0;
            
            //普通炸弹
            if (block.getType() == Constant.BLOCK_TYPE_BOMB) {
                smallExplosion(block.getCore());
            }
            
            //超级炸弹 
            else if (block.getType() == Constant.BLOCK_TYPE_SUPER_BOMB) {
                bigExplosion(block.getCore());
            } 
            
            //普通方块
            else {
                bool needElim = false;
                for (int i = minX; i < Constant.MAX_X + 1; ++i) {
                    needElim = true;
                    for (int j = 0; j < Constant.MAX_Y + 1; ++j) {
                        if (map[i, j] == Constant.VALUE_NONE) {
                            needElim = false;
                            break;
                        }
                    }
                    if (needElim) {
                        ++count;
                        moveLines(i);                   //第i行上面所有行下移
                    }
                }
            }
        }
        
        //以点p为中心，小型爆炸
        public void smallExplosion(Point p) {
            int begIndex = (p.X - 2) * (Constant.MAX_Y + 1) + (p.Y - 2);
            for (int i = 0; i < 5; ++i) {
                resetValue(begIndex, begIndex + 4);
                begIndex += Constant.MAX_Y + 1;
            }
        }

        //以点p为中心，大型爆炸
        public void bigExplosion(Point p) {
            int begIndex = (p.X - 4) * (Constant.MAX_Y + 1) + (p.Y - 4);
            for(int i = 0; i < 9; ++i) {
                resetValue(begIndex, begIndex + 8);
                begIndex += Constant.MAX_Y + 1;
            }
        }

        //清除索引从index1到index2的位置的值
        public void resetValue(int index1,int index2) {
            int x = 0;
            int y = 0;
            for(int index = index1; index <= index2; ++index) {
                if (!validIndex(index)) continue;                   //索引不合法就跳过
                x = index / (Constant.MAX_Y + 1);
                y = index % (Constant.MAX_Y + 1);
                map[x, y] = Constant.VALUE_NONE;
            }
        }

        //index行的所有上方行下移
        public void moveLines(int index) {
            if (!validIndex(index)) throw new Exception("Map::moveLines(int), index: " + index.ToString());

            //行从index到minX-1，所有上行复制值到下行
            for (int i = index; i > minX; --i) {
                for (int j = 0; j < Constant.MAX_Y + 1; ++j)
                    map[i, j] = map[i - 1, j];
            }

            //清空minX行所有元素
            for (int j = 0; j < Constant.MAX_Y + 1; ++j)
                map[minX, j] = 0;

            //更新minX
            ++minX;
        }
    }
}
