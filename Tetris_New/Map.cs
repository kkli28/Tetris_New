using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Map {
        int[,] map;
        int minX;                       //有值的最小行号
        int minUnchangeX;       //不用更改内容的最小行号

        //构造函数
        public Map() {
            map = new int[Constant.MAX_X + 1, Constant.MAX_Y + 1];
            for (int i = Constant.MIN_X; i <= Constant.MAX_X; i++)
                for (int j = Constant.MIN_Y; j <= Constant.MAX_Y; j++)
                    map[i, j] = 0;
            minX = Constant.MAX_X+1;
            minUnchangeX = Constant.MAX_X+1;
        }

        //重置图
        public void reset() {
            minX = Constant.MAX_X + 1;
            minUnchangeX = Constant.MAX_X + 1;
            for (int i = Constant.MIN_X; i <= Constant.MAX_X; i++)
                for (int j = Constant.MIN_Y; j <= Constant.MAX_Y; j++)
                    map[i, j] = 0;
        }

        //获取地图数组
        public int[,] getMap() {
            return map;
        }

        //获取minX
        public int getMinX() { return minX; }

        //获取minUnchangeX
        public int getMinUnchangeX() { return minUnchangeX; }

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

        //设置值--通过索引
        public void setValue(int index,int value) {
            if (validIndex(index) && validValue(value)) {
                int x = index / (Constant.MAX_Y + 1);
                int y = index % (Constant.MAX_Y + 1);
                map[x, y] = value;
                if (x < minX) minX = x;
            } else throw new Exception();
        }

        //设置值--通过坐标
        public void setValue(int x, int y, int value) {
            if (validX(x) && validY(y) && validValue(value)) {
                map[x, y] = value;
                if (x < minX) minX = x;
            } else throw new Exception();
        }

        //设置值--通过点
        public void setValue(Point p,int val) {
            if (!p.isValid()) throw new Exception();
            if (!validValue(val)) throw new Exception();
            int x = p.X;
            int y = p.Y;
            map[x, y] = val;
            if (x < minX) minX = x;                     //设置有值的最小行数
        }

        //设置值--通过方块
        public void setValue(Block block) {
            int type = block.getType();
            if (type == Constant.BLOCK_TYPE_BOMB || type == Constant.BLOCK_TYPE_SUPER_BOMB) throw new Exception();
            int bc = Constant.getBlockCount(type);

            int value = block.getValue();
            setValue(block.getPoint0(), value);
            setValue(block.getPoint1(), value);

            if (bc >= 3) setValue(block.getPoint2(), value);
            if (bc == 4) setValue(block.getPoint3(), value);
        }

        //获取值--通过坐标
        public int getValue(int x, int y) {
            if (validX(x) && validY(y)) return map[x, y];
            else throw new Exception();
        }

        //获取值--通过索引
        public int getValue(int index) {
            if (validIndex(index)) {
                int x = index / (Constant.MAX_Y + 1);
                int y = index % (Constant.MAX_Y + 1);
                return map[x, y];
            } else throw new Exception();
        }

        //获取值--通过点
        public int getValue(Point p) {
            if (p.isValid()) return map[p.X, p.Y];
            else throw new Exception();
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
        public void eliminate(Block block, out int count,out int[] arr) {
            count = 0;
            arr = new int[4];
            //普通炸弹
            if (block.getType() == Constant.BLOCK_TYPE_BOMB) explosion(block.getPoint0(), Constant.BLOCK_TYPE_BOMB);
            //超级炸弹 
            else if (block.getType() == Constant.BLOCK_TYPE_SUPER_BOMB) explosion(block.getPoint4(), Constant.BLOCK_TYPE_SUPER_BOMB);
            
            //TODO: 更新minX和minUnchangeX
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
                        moveLines(i);                   //第i行上面所有行下移
                        arr[count++] = i;
                        minUnchangeX = i + 1;
                    }
                }
            }
        }
        
        //爆炸
        public void explosion(Point p,int type) {
            int addV = 0;
            if (type == Constant.BLOCK_TYPE_BOMB) {
                addV = 1;
            } else if (type == Constant.BLOCK_TYPE_SUPER_BOMB) {
                addV = 3;
            } else throw new Exception();

            int begX = p.X - addV;
            int endX = p.X + addV;
            int begY = p.Y - addV;
            int endY = p.Y + addV;
            if (begX < 0) begX = 0;
            if (endX > Constant.MAX_X) endX = Constant.MAX_X;
            if (begY < 0) begY = 0;
            if (endY > Constant.MAX_Y) endY = Constant.MAX_Y;

            for (int i = begX; i <= endX; ++i) {
                for (int j = begY; j <= endY; ++j)
                    map[i, j] = 0;
            }
            minUnchangeX = p.X + addV + 1;
            if (minUnchangeX < 0) minUnchangeX = 0;
            if (minUnchangeX > Constant.MAX_X+1) minUnchangeX = Constant.MAX_X+1;
        }
        
        //index行的所有上方行下移
        public void moveLines(int index) {
            if (!validIndex(index)) throw new Exception();

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
            if (minX < 0) minX = 0;
            else if (minX > Constant.MAX_X) minX = Constant.MAX_X;
        }
    }
}
