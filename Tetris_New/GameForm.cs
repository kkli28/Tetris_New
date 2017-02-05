using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tetris_New {
    public partial class GameForm : Form {
        Random random;               //随机数生成器
        int gameMode;                   //游戏模式
        int score;                             //得分
        int record;                           //记录
        int time;                               //游戏时间
        bool bdAcc;                         //边界跨越选项
        bool haveBomb;                  //是否有炸弹
        int hurdle;                            //关卡
        int[] blockArray;                   //自定义模式中的残留方块位置
        int blockCount;                     //自定义模式中残留方块个数

        Map map;                            //游戏地图
        bool inGame;                       //在游戏中
        bool gameIsOver;                //游戏结束

        Block block1;                       //第一个方块
        Block block2;                       //第二个方块

        string[] records;                   //文件中的记录数组

        //构造函数--需要参数：游戏模式、是否跨越边界、是否有炸弹、第几关
        public GameForm(int mode, bool boundA, bool haveB, int hurd, int[] stt) {
            InitializeComponent();

            random = new Random();
            score = 0;
            record = 0;
            gameMode = mode;
            bdAcc = boundA;
            haveBomb = haveB;
            hurdle = hurd;
            time = 0;

            map = new Map();
            inGame = false;
            gameIsOver = false;

            //读取记录
            records = new string[3];
            records = File.ReadAllLines("record.txt");

            //显示游戏记录
            try {
                record = Int32.Parse(records[gameMode]);
                recordLB.Text = records[gameMode];
            } catch (Exception e) {
                File.WriteAllLines("record.txt", new string[3] { "0", "0", "0" });
                MessageBox.Show("记录文件损坏，已重置记录！");
            }

            //使用前必须保证已初始化
            block1 = new Block_Bomb();
            block2 = new Block_Bomb();

            //自定义模式下，初始化残留方块数组
            if (gameMode == Constant.CUSTOM_MODE) {
                blockArray = new int[Constant.MAX_INDEX + 1];
                blockCount = 0;
                for (int i = 0; i < Constant.MAX_INDEX + 1; ++i)
                    if (stt[i] == Constant.BUTTON_STATE1) blockArray[blockCount++] = i;
            }

            //显示游戏模式、跨越边界、是否有炸弹的标签
            gameModeLB.Text = gameMode.ToString();
            boundaryAccLB.Text = bdAcc.ToString();
            bombLB.Text = haveBomb.ToString();

            initBlocks();                   //加载方块
        }

        //开始游戏
        public void startGame() {
            resetBtns();
            initBlocks();
            inGame = true;
            block1 = getBlock();
            block2 = getBlock();

            time = 0;
            score = 0;
            scoreLB.Text = "0";

            //避免两个方块颜色相同，置第二个方块颜色为前一个方块颜色的下一个颜色
            int val = block1.getValue() + 1;
            if (val > Constant.MAX_BLOCK_VALUE) val = Constant.MIN_BLOCK_VALUE;
            block2.setValue(val);

            showBlock(block1);

            //重置游戏难度计时器
            mainTimer.Interval = Constant.DEFAULT_INTERVAL;

            mainTimer.Start();
        }

        //显示方块
        public void showBlock(Block b) {
            int bc = Constant.getBlockCount(b.getType());
            int type = b.getType();
            blockTypeLB.Text = type.ToString();

            int index = -1;
            Point p = b.getPoint0();
            index = p.X * (Constant.MAX_Y + 1) + p.Y;
            setBtnBC(index, Constant.valueToColor(b.getValue()));
            blockTypeLB.Text = blockTypeLB.Text + "\n" + p.X+" "+p.Y;

            p = b.getPoint1();
            index = p.X * (Constant.MAX_Y + 1) + p.Y;
            setBtnBC(index, Constant.valueToColor(b.getValue()));
            blockTypeLB.Text = blockTypeLB.Text + "\n" + p.X + " " + p.Y;

            //有第三个方块
            if (bc>=3) {
                p = b.getPoint2();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.valueToColor(b.getValue()));
                blockTypeLB.Text = blockTypeLB.Text + "\n" + p.X + " " + p.Y;
            }

            //有第四个方块
            if (bc>=4) {
                p = b.getPoint3();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.valueToColor(b.getValue()));
                blockTypeLB.Text = blockTypeLB.Text + "\n" + p.X + " " + p.Y;
            } 
            
            //超级炸弹，有第五个方块
            if (bc==5) {
                p = b.getPoint4();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.valueToColor(b.getValue()));
                blockTypeLB.Text = blockTypeLB.Text + "\n" + p.X + " " + p.Y;
            }
        }

        //设置按钮颜色
        public void setBtnBC(int index, Color color) {
            getBtn(index).BackColor = color;
        }

        //擦除方块
        public void eraseBlock(Block b) {
            int type = b.getType();
            if ((type >= Constant.MIN_BLOCK_TYPE && type <= Constant.BLOCK_TYPE_EXTENSION8)) {

            }
        }

        //获得方块
        public Block getBlock() {
            if (gameMode == Constant.CLASSIC_MODE) {
                return Constant.getBlock(random.Next(0, Constant.MAX_CLASSIC_BLOCK_TYPE + 1));
            } else if (gameMode == Constant.FIRE_MODE) {
                return Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 2));          //小炸弹当做普通方块产生
            } else {
                return Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 1));          //不产生炸弹
            }
        }

        //重置游戏背景
        public void resetBtns() {
            foreach (Control c in this.Controls) {
                try {
                    int val = Int32.Parse(c.Name[6].ToString());
                    c.BackColor = Constant.DEFAULT_BTNS_COLOR;
                } catch (Exception) {
                    continue;
                }
            }
        }

        private void GameForm_Load(object sender, EventArgs e) {

        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e) {
            MenuForm.thisForm.Show();
        }

        //加载方块
        public void initBlocks() {
            switch (gameMode) {
                case Constant.CLASSIC_MODE: {
                        break;
                    }
                case Constant.CHALLENGE_MODE: {
                        initHurdle();
                        break;
                    }
                case Constant.CUSTOM_MODE: {
                        setBlocks(blockArray, blockCount);
                        break;
                    }
                case Constant.FIRE_MODE: {
                        break;
                    }
                default: throw new Exception();
            }
        }

        //加载关卡
        public void initHurdle() {
            if (hurdle == 0) {    //第0关
                int[] array = {
                    265,277,255,267,279,245,257,269,281,235,
                    247,259,271,283,225,237,249,261,273,285,
                    215,227,239,251,263,275,287
                };
                setBlocks(array, 27);
            } else if (hurdle == 1) {
                int[] array = {
                    216,217,218,224,225,228,229,230,235,236,
                    238,239,240,242,245,246,253,256,259,260,
                    262,264,265,266,267,268,269,271,274,278,
                    279,282,285,287
                };
                setBlocks(array, 34);
            } else if (hurdle == 2) {
                int[] array = {
                    144,156,157,169,170,182,183,195,196,208,
                    209,221,222,234,235,247,248,260,261,273,
                    274,286,287,216,228,229,241,242,254,255,
                    267,268,280,281
                };
                setBlocks(array, 34);
            } else if (hurdle == 3) {
                int[] array = {
                    144,145,158,168,169,180,181,182,194,195,
                    206,208,216,217,223,225,229,231,233,236,
                    237,241,244,247,250,251,255,257,258,260,
                    263,271,273,274,283
                };
                setBlocks(array, 35);
            } else if (hurdle == 4) {
                int[] array = {
                    168,171,174,177,180,183,186,189,205,217,
                    219,231,233,245,247,259,261,273,275,287
                };
                setBlocks(array, 20);
            } else if (hurdle == 5) {
                int[] array = {
                    240,241,242,243,252,253,254,255,264,265,
                    266,267,276,277,278,279,200,201,202,203,
                    212,213,214,215,224,225,226,227,236,237,
                    238,239,144,145,146,147,156,157,158,159,
                    168,169,170,171,180,181,182,183
                };
                setBlocks(array, 48);
            } else if (hurdle == 6) {
                int[] array = {
                    172,173,174,175,176,177,178,179,192,193,
                    194,195,196,197,198,199,220,221,222,223,
                    224,225,226,227,240,241,242,243,244,245,
                    246,247,268,269,270,271,272,273,274,275,
                };
                setBlocks(array, 40);
            } else if (hurdle == 7) {
                int[] array = {
                    96,97,100,103,105,
                    108,111,112,114,116,117,118,119,
                    125,130,
                    136,138,140,141,142,            //20
                    144,145,146,147,150,
                    157,159,161,163,166,
                    169,170,171,173,177,            //35
                    181,182,188,
                    195,196,201,
                    205,212,
                    216,221,223,224,226,
                    230,232,236,238,
                    243,244,245,247,
                    253,255,260,261,263,            //61
                    267,269,270,272,275,
                    280,281,282,285                     //70
                };
                setBlocks(array, 70);
            } else throw new Exception();
        }

        //通过值获取按钮
        public Button getBtn(int val) {
            foreach (Control c in this.Controls)
                if (c is Button && c.Name == "button" + val) return (Button)c;
            return startBtn;
        }

        //通过数组设置游戏界面
        public void setBlocks(int[] array, int len) {
            int value = 0;
            for (int i = 0; i < len; ++i) {
                value = array[i];
                getBtn(value).BackColor = Constant.CustomBlockColor;
                map.setValue(value, Constant.CUSTOM_VALUE);
            }
        }

        private void startBtn_Click(object sender, EventArgs e) {
            startGame();
        }
    }
}
