using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_New {
    public class Point {
        int x;              //横坐标
        int y;              //纵坐标
        int color;        //颜色值

        //构造函数--默认
        public Point() {
            x = -1;
            y = -1;
            color = -1;
        }

        //构造函数--通过坐标和值
        public Point(int posX, int posY, int cl) {
            x = posX;
            y = posY;
            if (cl < Constant.MIN_VALUE || cl > Constant.MAX_VALUE)
                throw new Exception("Point::Point(int,int,int), 颜色错误！cl: "+cl.ToString());
            else cl = color;
        }

        //构造函数--通过点
        public Point(Point p) {
            x = p.X;
            y = p.Y;
            color = p.Color;
        }

        //getter和setter
        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public int Color {
            get { return color; }
            set {
                if (value<Constant.MIN_VALUE || value>Constant.MAX_VALUE)
                    throw new Exception("Point::Color, 颜色值错误！value: " + value.ToString());
                else color = value;
            }
        }

        public bool isValid() {
            if (x < Constant.MIN_X || x > Constant.MAX_X) return false;
            if (y < Constant.MIN_Y || y > Constant.MAX_Y) return false;
            return true;
        }

        //设置点--通过索引
        public void setPoint(int index) {
            x = index / (Constant.MAX_Y + 1);
            y = index % (Constant.MAX_Y + 1);
        }

        //设置点--通过坐标
        public void setPoint(int posX, int posY) {
            x = posX;
            y = posY;
        }

        //设置点--通过坐标和颜色值
        public void setPoint(int posX,int posY,int cl) {
            x = posX;
            y = posY;
            if (cl < Constant.MIN_VALUE || cl > Constant.MAX_VALUE)
                throw new Exception("Point::setPoint(int,int,int), cl: " + cl.ToString());
            else color = cl;
        }

        //获取点的信息
        public string getInfo() {
            return "(x,y,valid): " + x.ToString() + "," + y.ToString() + "," + isValid().ToString();
        }
    }
}
