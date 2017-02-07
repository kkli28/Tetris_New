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
        int count;                             //一次消除的行数
        bool haveExtra;                   //是否启用扩展方块
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
        bool isBomb;                       //显示“爆炸”效果时，是炸弹还是行消除
        int[] lines;                             //待消除行的序列

        bool eatSuperBomb;           //产生的所有超级炸弹中，只有奇数序或偶数序的超级炸弹出现，其余的都被我吃掉了，muah ha ha
        
        //构造函数--需要参数：游戏模式、是否跨越边界、是否有炸弹、第几关
        public GameForm(int mode, bool extra, bool haveB, int hurd, int[] stt) {
            InitializeComponent();
            
            random = new Random();
            score = 0;
            record = 0;
            count = 0;
            gameMode = mode;
            haveExtra = extra;
            haveBomb = haveB;
            hurdle = hurd;
            time = 0;

            map = new Map();
            inGame = false;
            gameIsOver = false;
            isBomb = false;

            //读取记录
            records = new string[3] { "0", "0", "0" };

            //显示游戏记录
            if (gameMode != Constant.CUSTOM_MODE) {
                try {
                    records = File.ReadAllLines("record.txt");
                    record = Int32.Parse(records[gameMode]);
                    recordLB.Text = records[gameMode];
                } catch (Exception e) {
                    File.WriteAllLines("record.txt", new string[3] { "0", "0", "0" });
                    MessageBox.Show("记录文件损坏，已重置记录！");
                    MessageBox.Show(e.Message);
                }
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
            string str = "";
            switch (gameMode) {
                case Constant.CLASSIC_MODE: str = "经典模式";break;
                case Constant.CHALLENGE_MODE: str = "挑战模式";break;
                case Constant.FIRE_MODE: str = "无限火力模式"; break;
                case Constant.CUSTOM_MODE: str = "自定义模式";break;
                default: throw new Exception();
            }
            gameModeLB.Text = str;
            
            switch (haveExtra) {
                case Constant.NO_EXTRA: str = "不启用扩展方块";break;
                case Constant.HAVE_EXTRA: str = "启用扩展方块";break;
                default: throw new Exception();
            }
            extraBlcokLB.Text = str;

            switch (haveBomb) {
                case Constant.HAVE_BOMB: str = "启用炸弹";break;
                case Constant.NO_BOMB: str = "不启用炸弹";break;
                default: throw new Exception();
            }
            bombLB.Text = str;

            initBlocks();                   //加载方块
        }
        
        //开始游戏
        public void startGame() {
            resetBtns();
            map.reset();
            initBlocks();
            inGame = true;
            gameIsOver = false;
            block1 = getBlock();
            block2 = getBlock();

            time = 0;
            score = 0;
            count = 0;
            scoreLB.Text = "0";

            eatSuperBomb = true;
            isBomb = false;

            //避免两个方块颜色相同，置第二个方块颜色为前一个方块颜色的下一个颜色
            int val = block1.getValue() + 1;
            if (val > Constant.MAX_BLOCK_VALUE) val = Constant.MIN_BLOCK_VALUE;
            block2.setValue(val);
            
            //重置游戏难度计时器
            mainTimer.Interval = Constant.DEFAULT_INTERVAL;
            mainTimer.Start();

            if (gameMode == Constant.CUSTOM_MODE) {
                map.eliminate(new Block_O(), out count, out lines);
                if (count != 0) {
                    showLines(lines, count, Constant.EXPLOSION_COLOR);
                    bombTimer.Start();
                    return;
                }else {
                    showBlock(block1);
                    showNextBlock(block2);
                    timeTimer.Start();
                }
            } else {
                showBlock(block1);
                showNextBlock(block2);
                timeTimer.Start();
            }
        }

        //显示方块
        public void showBlock(Block b) {
            int bc = Constant.getBlockCount(b.getType());
            int value = b.getValue();

            int type = b.getType();

            int index = -1;
            Point p = b.getPoint0();
            index = p.X * (Constant.MAX_Y + 1) + p.Y;
            setBtnBC(index, Constant.valueToColor(value));

            //有第二个方块
            if (bc >= 2) {
                p = b.getPoint1();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.valueToColor(value));
            }

            //有第三个方块
            if (bc>=3) {
                p = b.getPoint2();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.valueToColor(value));
            }

            //有第四个方块
            if (bc>=4) {
                p = b.getPoint3();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.valueToColor(value));
            } 
            
            //超级炸弹，有第五个方块
            if (bc==5) {
                p = b.getPoint4();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.BOMB_CORE_COLOR);
            }
        }
        
        //设置按钮颜色
        public void setBtnBC(int index, Color color) {
            getBtn(index).BackColor = color;
        }

        //擦除展示框的方块
        public void eraseNextBlock() {
            Color color = Constant.BTNS_DEFAULT_COLOR;
            nextBtn1.BackColor = color;
            nextBtn2.BackColor = color;
            nextBtn3.BackColor = color;
            nextBtn4.BackColor = color;
            nextBtn5.BackColor = color;
            nextBtn6.BackColor = color;
            nextBtn7.BackColor = color;
            nextBtn8.BackColor = color;
            nextBtn9.BackColor = color;
            nextBtn10.BackColor = color;
            nextBtn11.BackColor = color;
            nextBtn12.BackColor = color;
            nextBtn13.BackColor = color;
            nextBtn14.BackColor = color;
            nextBtn15.BackColor = color;
            nextBtn16.BackColor = color;
        }

        //擦除方块，不能用于擦除展示框的方块
        public void eraseBlock(Block b) {
            int bc = Constant.getBlockCount(b.getType());

            int index = -1;
            Point p = b.getPoint0();
            index = p.X * (Constant.MAX_Y + 1) + p.Y;
            setBtnBC(index, Constant.BTNS_DEFAULT_COLOR);

            if (bc >= 2) {
                p = b.getPoint1();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.BTNS_DEFAULT_COLOR);
            }

            if (bc >= 3) {
                p = b.getPoint2();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.BTNS_DEFAULT_COLOR);
            }

            if (bc >= 4) {
                p = b.getPoint3();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.BTNS_DEFAULT_COLOR);
            }

            if (bc == 5) {
                p = b.getPoint4();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                setBtnBC(index, Constant.BTNS_DEFAULT_COLOR);
            }
        }

        //获得方块
        public Block getBlock() {

            //经典模式
            if (gameMode == Constant.CLASSIC_MODE) {
                return Constant.getBlock(random.Next(0, Constant.MAX_CLASSIC_BLOCK_TYPE + 1));
            } 
            
            //无限火力模式
            else if (gameMode == Constant.FIRE_MODE) {
                Block b=Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 3));
                if (b.getType() == Constant.BLOCK_TYPE_SUPER_BOMB) {
                    if (eatSuperBomb) {
                        eatSuperBomb = false;
                        b = Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 3));
                    } else eatSuperBomb = true;
                }
                return b;
            }
            
            //挑战模式或自定义模式
            else {
                //启用扩展方块，启用炸弹
                if (haveExtra && haveBomb) {
                    Block b = Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 3));
                    if (b.getType() == Constant.BLOCK_TYPE_SUPER_BOMB) {
                        if (eatSuperBomb) {
                            eatSuperBomb = false;
                            b = Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 3));
                        } else eatSuperBomb = true;
                    }
                    return b;
                }
                
                //只启用扩展方块
                else if (haveExtra) {
                    return Constant.getBlock(random.Next(0, Constant.MAX_NO_BOMB_BLOCK_TYPE + 1));
                } 
                
                //只启用炸弹
                else if (haveBomb) {
                    int value = random.Next(0, Constant.MAX_CLASSIC_BLOCK_TYPE + 3);
                    if (value > Constant.MAX_CLASSIC_BLOCK_TYPE) {
                        if (value == Constant.MAX_CLASSIC_BLOCK_TYPE + 1) value = Constant.BLOCK_TYPE_BOMB;
                        else {
                            int v = random.Next(0, Constant.MAX_CLASSIC_BLOCK_TYPE + 2);
                            if (v == Constant.MAX_CLASSIC_BLOCK_TYPE + 1) value = Constant.BLOCK_TYPE_SUPER_BOMB;
                            else value = v;
                        }
                    }
                    return Constant.getBlock(value);
                }
                
                //只有经典方块（不启用扩展方块，不启用炸弹）
                else {
                    return Constant.getBlock(random.Next(0, Constant.MAX_CLASSIC_BLOCK_TYPE + 1));
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
        
        //刷新地图
        public void refreshBtns(int cnt) {
            int[,] array = map.getMap();
            int minX = map.getMinX() - cnt;
            int minUnchangeX = map.getMinUnchangeX();
            
            for(int i = minX; i < minUnchangeX; ++i) {
                for (int j = 0; j < Constant.MAX_Y + 1; ++j)
                    getBtn(i*(Constant.MAX_Y+1)+j).BackColor = Constant.valueToColor(array[i, j]);
            }
        }

        //置block1为block2，block2重新随机获得一个方块
        public void setBlock1AndBlock2() {

            //更新block1
            switch (block2.getType()) {
                case Constant.BLOCK_TYPE_O: block1 = new Block_O(); break;
                case Constant.BLOCK_TYPE_I: block1 = new Block_I(); break;
                case Constant.BLOCK_TYPE_T: block1 = new Block_T(); break;
                case Constant.BLOCK_TYPE_L: block1 = new Block_L(); break;
                case Constant.BLOCK_TYPE_J: block1 = new Block_J(); break;
                case Constant.BLOCK_TYPE_Z: block1 = new Block_Z(); break;
                case Constant.BLOCK_TYPE_S: block1 = new Block_S(); break;
                case Constant.BLOCK_TYPE_EXTENSION1: block1 = new Block_E1(); break;
                case Constant.BLOCK_TYPE_EXTENSION2: block1 = new Block_E2(); break;
                case Constant.BLOCK_TYPE_EXTENSION3: block1 = new Block_E3(); break;
                case Constant.BLOCK_TYPE_EXTENSION4: block1 = new Block_E4(); break;
                case Constant.BLOCK_TYPE_EXTENSION5: block1 = new Block_E5(); break;
                case Constant.BLOCK_TYPE_EXTENSION6: block1 = new Block_E6(); break;
                case Constant.BLOCK_TYPE_EXTENSION7: block1 = new Block_E7(); break;
                case Constant.BLOCK_TYPE_EXTENSION8: block1 = new Block_E8(); break;
                case Constant.BLOCK_TYPE_BOMB: block1 = new Block_Bomb(); break;
                case Constant.BLOCK_TYPE_SUPER_BOMB: block1 = new Block_SuperBomb(); break;
                default: throw new Exception();
            }
            block1.copyFrom(block2);
            
            //更新block2
            Block block = getBlock();
            switch (block.getType()) {
                case Constant.BLOCK_TYPE_O: block2 = new Block_O(); break;
                case Constant.BLOCK_TYPE_I: block2 = new Block_I(); break;
                case Constant.BLOCK_TYPE_T: block2 = new Block_T(); break;
                case Constant.BLOCK_TYPE_L: block2 = new Block_L(); break;
                case Constant.BLOCK_TYPE_J: block2 = new Block_J(); break;
                case Constant.BLOCK_TYPE_Z: block2 = new Block_Z(); break;
                case Constant.BLOCK_TYPE_S: block2 = new Block_S(); break;
                case Constant.BLOCK_TYPE_EXTENSION1: block2 = new Block_E1(); break;
                case Constant.BLOCK_TYPE_EXTENSION2: block2 = new Block_E2(); break;
                case Constant.BLOCK_TYPE_EXTENSION3: block2 = new Block_E3(); break;
                case Constant.BLOCK_TYPE_EXTENSION4: block2 = new Block_E4(); break;
                case Constant.BLOCK_TYPE_EXTENSION5: block2 = new Block_E5(); break;
                case Constant.BLOCK_TYPE_EXTENSION6: block2 = new Block_E6(); break;
                case Constant.BLOCK_TYPE_EXTENSION7: block2 = new Block_E7(); break;
                case Constant.BLOCK_TYPE_EXTENSION8: block2 = new Block_E8(); break;
                case Constant.BLOCK_TYPE_BOMB: block2 = new Block_Bomb(); break;
                case Constant.BLOCK_TYPE_SUPER_BOMB: block2 = new Block_SuperBomb(); break;
                default: throw new Exception();
            }
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
            if (inGame == true) {
                mainTimer.Stop();
                DialogResult result = MessageBox.Show("确定重新开始游戏？", "提示",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                    startGame();
                else mainTimer.Start();
            } else {
                startGame();
            }
        }

        //显示爆炸效果
        public void showExplosion(Block block, Color color) {
            int type = block.getType();
            int addV = 0;
            Point p;
            if (type == Constant.BLOCK_TYPE_BOMB) {
                addV = 1;
                p = block.getPoint0();
            } else if (type == Constant.BLOCK_TYPE_SUPER_BOMB) {
                addV = 3;
                p = block.getPoint4();
            } else throw new Exception();

            int begX = p.X - addV;
            int begY = p.Y - addV;
            int endX = p.X + addV;
            int endY = p.Y + addV;

            if (begX < 0) begX = 0;
            if (endX > Constant.MAX_X) endX = Constant.MAX_X;
            if (begY < 0) begY = 0;
            if (endY > Constant.MAX_Y) endY = Constant.MAX_Y;

            int index = -1;
            for (int i = begX; i <= endX; ++i) {
                for (int j = begY; j <= endY; ++j) {
                    index = i * (Constant.MAX_Y + 1) + j;
                    getBtn(index).BackColor = color;
                }
            }
        }

        //显示行消除效果
        public void showLines(int[] arr,int cnt,Color color) {
            int index = -1;
            for(int i = 0; i < cnt; ++i) {
                for(int j = 0; j < Constant.MAX_Y + 1; ++j) {
                    index = arr[i] * (Constant.MAX_Y + 1) + j;
                    getBtn(index).BackColor = color;
                }
            }
        }

        //判断游戏是否结束
        public bool isGameOver(Block block) {
            int type = block.getType();
            //是炸弹则游戏没有结束
            if (type == Constant.BLOCK_TYPE_BOMB || type == Constant.BLOCK_TYPE_SUPER_BOMB) return false;
            int bc = Constant.getBlockCount(block.getType());

            int index = -1;
            Point p = block.getPoint0();
            index = p.X * (Constant.MAX_Y + 1) + p.Y;
            if (map.getValue(index) != 0) return true;

            p = block.getPoint1();
            index = p.X * (Constant.MAX_Y + 1) + p.Y;
            if (map.getValue(index) != 0) return true;
            
            if (bc >= 3) {
                p = block.getPoint2();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                if (map.getValue(index) != 0) return true;
            }

            if (bc == 4) {
                p = block.getPoint3();
                index = p.X * (Constant.MAX_Y + 1) + p.Y;
                if (map.getValue(index) != 0) return true;
            }

            return false;
        }
        
        private void mainTimer_Tick(object sender, EventArgs e) {
            if (gameIsOver) {
                mainTimer.Stop();
                timeTimer.Stop();
                GameOverForm gof = new GameOverForm(gameMode, score, record, records);
                gof.ShowDialog();
                if (record < score) {
                    record = score;
                    recordLB.Text = record.ToString();
                }
                return;
            }

            if (inGame) {

                //能下移
                if (block1.canMoveDown(map)) {
                    eraseBlock(block1);
                    block1.update_MD();
                    showBlock(block1);
                }

                //不能下移
                else {

                    //重置变量，以及不允许用户按键
                    count = 0;
                    isBomb = false;
                    inGame = false;
                    int[] array = new int[Constant.MAX_X + 1];
                    int type = block1.getType();

                    //炸弹
                    if (type == Constant.BLOCK_TYPE_BOMB || type == Constant.BLOCK_TYPE_SUPER_BOMB) {
                        map.eliminate(block1, out count,out lines);
                        isBomb = true;
                        mainTimer.Stop();
                        showExplosion(block1, Constant.EXPLOSION_COLOR);                            //显示爆炸效果
                        bombTimer.Start();
                        return;                                                 //显示爆炸后不再执行后面的代码
                    }

                    //普通方块
                    else {
                        addBlockToMap(block1);
                        map.eliminate(block1, out count,out lines);
                        if (count == 0) ;                   //没有产生消除则不做任何事
                        else {
                            switch (count) {
                                case 1: score += 1; break;
                                case 2: score += 3; break;
                                case 3: score += 5; break;
                                case 4: score += 10; break;
                                default: score += 20; break;
                            }

                            scoreLB.Text = score.ToString();

                            mainTimer.Stop();
                            showLines(lines, count, Constant.EXPLOSION_COLOR);                          //显示行消除效果
                            bombTimer.Start();
                            return;                                         //显示行消除后，不再执行后续的代码
                        }
                    }

                    //走到这里，说明没有产生爆炸，也没有产生消除
                    setBlock1AndBlock2();

                    if (isGameOver(block1)) {
                        gameIsOver = true;
                        inGame = false;
                        showLastBlock();
                        showNextBlock(block2);
                        return;
                    } else {
                        showBlock(block1);
                        showNextBlock(block2);
                        inGame = true;
                    }
                }
            }
        }

        private void bombTimer_Tick(object sender, EventArgs e) {
            bombTimer.Stop();

            if (isBomb) showExplosion(block1, Constant.BTNS_DEFAULT_COLOR);               //结束爆炸效果
            else {
                showLines(lines, count, Constant.BTNS_DEFAULT_COLOR);
                refreshBtns(count);
            }

            setBlock1AndBlock2();

            //即使是爆炸或消除后，也可能下一个方块的出现位置已被占据
            if (isGameOver(block1)) {
                gameIsOver = true;
                inGame = false;
                showLastBlock();
                showNextBlock(block2);
            }else {
                showBlock(block1);
                showNextBlock(block2);
                inGame = true;
            }
            mainTimer.Start();
        }

        //设置边框颜色
        public void setEdgeColor(Color color) {
            edgeBtn0.BackColor = color;
            edgeBtn1.BackColor = color;
            edgeBtn2.BackColor = color;
            edgeBtn3.BackColor = color;
        }
        
        //显示游戏结束时最后的方块
        public void showLastBlock() {
            if (map.getMinX() == 0) return;                //地图中有值的最小行已是最顶行，则不显示了
            Color color = Constant.valueToColor(block1.getValue());
            int[] arr = new int[4] { 0, 0, 0, 0 };
            int len = 0;

            switch (block1.getType()) {
                case Constant.BLOCK_TYPE_O: arr = new int[2] { 5, 6 }; len = 2; break;
                case Constant.BLOCK_TYPE_I: arr = new int[4] { 4, 5, 6, 7 }; len = 4; break;
                case Constant.BLOCK_TYPE_T: arr = new int[3] { 4, 5, 6 }; len = 3; break;
                case Constant.BLOCK_TYPE_L: arr = new int[3] { 4, 5, 6 }; len = 3; break;
                case Constant.BLOCK_TYPE_J: arr = new int[3] { 4, 5, 6 }; len = 3; break;
                case Constant.BLOCK_TYPE_S: arr = new int[2] { 4,5 }; len = 2; break;
                case Constant.BLOCK_TYPE_Z: arr = new int[2] { 5, 6 }; len = 2; break;
                case Constant.BLOCK_TYPE_EXTENSION1: arr = new int[1] { 6 }; len = 1; break;
                case Constant.BLOCK_TYPE_EXTENSION2: arr = new int[3] { 4,5, 6 }; len = 3; break;
                case Constant.BLOCK_TYPE_EXTENSION3: arr = new int[1] { 6 }; len = 1; break;
                case Constant.BLOCK_TYPE_EXTENSION4: arr = new int[1] { 5 }; len = 1; break;
                case Constant.BLOCK_TYPE_EXTENSION5: arr = new int[2] { 5, 6 }; len = 2; break;
                case Constant.BLOCK_TYPE_EXTENSION6: arr = new int[2] { 5, 6 }; len = 2; break;
                case Constant.BLOCK_TYPE_EXTENSION7: arr = new int[2] { 5, 6 }; len = 2; break;
                case Constant.BLOCK_TYPE_EXTENSION8: arr = new int[2] { 4,5 }; len = 2; break;
                default: throw new Exception();
            }

            for (int i = 0; i < len; ++i)
                getBtn(arr[i]).BackColor = color;
        }
        
        //将方块添加到图中
        public void addBlockToMap(Block block) {
            int bc = Constant.getBlockCount(block.getType());

            Point p = block.getPoint0();
            int value = block.getValue();
            map.setValue(p,value);

            p = block.getPoint1();
            map.setValue(p, value);

            if (bc >= 3) {
                p = block.getPoint2();
                map.setValue(p, value);
            }
            if (bc == 4) {
                p = block.getPoint3();
                map.setValue(p, value);
            }
        }

        

        //显示下一个方块
        //因为方块的Point对应的位置与右侧的展示框Buttons的编号没有对应关系，故嘿嘿嘿！！！
        //因为太麻烦，故不再用Constant.BLOCK_TYPE_X了，直接上数字
        public void showNextBlock(Block block) {
            eraseNextBlock();
            Color color = Constant.valueToColor(block.getValue());
            Color bombCoreColor = Constant.BOMB_CORE_COLOR;
            switch (block.getType()) {
                case 0: {
                        nextBtn6.BackColor = color;
                        nextBtn7.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color;break;
                    }
                case 1: {
                        nextBtn5.BackColor = color;
                        nextBtn6.BackColor = color;
                        nextBtn7.BackColor = color;
                        nextBtn8.BackColor = color; break;
                    }
                case 2: {
                        nextBtn6.BackColor = color;
                        nextBtn9.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 3: {
                        nextBtn7.BackColor = color;
                        nextBtn9.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 4: {
                        nextBtn5.BackColor = color;
                        nextBtn9.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 5: {
                        nextBtn5.BackColor = color;
                        nextBtn6.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 6: {
                        nextBtn6.BackColor = color;
                        nextBtn7.BackColor = color;
                        nextBtn9.BackColor = color;
                        nextBtn10.BackColor = color; break;
                    }
                case 7: {
                        nextBtn2.BackColor = color;
                        nextBtn3.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 8: {
                        nextBtn1.BackColor = color;
                        nextBtn2.BackColor = color;
                        nextBtn10.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 9: {
                        nextBtn2.BackColor = color;
                        nextBtn3.BackColor = color;
                        nextBtn9.BackColor = color;
                        nextBtn10.BackColor = color; break;
                    }
                case 10: {
                        nextBtn1.BackColor = color;
                        nextBtn6.BackColor = color;
                        nextBtn11.BackColor = color;break;
                    }
                case 11: {
                        nextBtn5.BackColor = color;
                        nextBtn6.BackColor = color;
                        nextBtn7.BackColor = color;break;
                    }
                case 12: {
                        nextBtn5.BackColor = color;
                        nextBtn7.BackColor = color;
                        nextBtn10.BackColor = color; break;
                    }
                case 13: {
                        nextBtn6.BackColor = color;
                        nextBtn7.BackColor = color; break;
                    }
                case 14: {
                        nextBtn6.BackColor = color;
                        nextBtn11.BackColor = color; break;
                    }
                case 15: {
                        nextBtn6.BackColor = color; break;
                    }
                case 16: {
                        nextBtn2.BackColor = color;
                        nextBtn5.BackColor = color;
                        nextBtn6.BackColor = bombCoreColor;
                        nextBtn7.BackColor = color;
                        nextBtn10.BackColor = color; break;
                    }
                default: throw new Exception();
            }
        }

        //下方的两个函数因为严重影响效率，故采用暴力手段
        //
        //
        //Power！！！ Unlimited Power！！！Woahw！！！
        //
        //
        ////通过值获取按钮，但是效率不高，采用下方的暴力手段效果更佳
        //public Button getBtn(int val) {
        //    foreach (Control c in this.Controls)
        //        if (c is Button && c.Name == "button" + val) return (Button)c;
        //    return startBtn;
        //}

        //获取按钮，效率更高，妈妈再也不用担心我的界面卡顿了
        public Button getBtn(int val) {
            switch (val) {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                case 16: return button16;
                case 17: return button17;
                case 18: return button18;
                case 19: return button19;
                case 20: return button20;
                case 21: return button21;
                case 22: return button22;
                case 23: return button23;
                case 24: return button24;
                case 25: return button25;
                case 26: return button26;
                case 27: return button27;
                case 28: return button28;
                case 29: return button29;
                case 30: return button30;
                case 31: return button31;
                case 32: return button32;
                case 33: return button33;
                case 34: return button34;
                case 35: return button35;
                case 36: return button36;
                case 37: return button37;
                case 38: return button38;
                case 39: return button39;
                case 40: return button40;
                case 41: return button41;
                case 42: return button42;
                case 43: return button43;
                case 44: return button44;
                case 45: return button45;
                case 46: return button46;
                case 47: return button47;
                case 48: return button48;
                case 49: return button49;
                case 50: return button50;
                case 51: return button51;
                case 52: return button52;
                case 53: return button53;
                case 54: return button54;
                case 55: return button55;
                case 56: return button56;
                case 57: return button57;
                case 58: return button58;
                case 59: return button59;
                case 60: return button60;
                case 61: return button61;
                case 62: return button62;
                case 63: return button63;
                case 64: return button64;
                case 65: return button65;
                case 66: return button66;
                case 67: return button67;
                case 68: return button68;
                case 69: return button69;
                case 70: return button70;
                case 71: return button71;
                case 72: return button72;
                case 73: return button73;
                case 74: return button74;
                case 75: return button75;
                case 76: return button76;
                case 77: return button77;
                case 78: return button78;
                case 79: return button79;
                case 80: return button80;
                case 81: return button81;
                case 82: return button82;
                case 83: return button83;
                case 84: return button84;
                case 85: return button85;
                case 86: return button86;
                case 87: return button87;
                case 88: return button88;
                case 89: return button89;
                case 90: return button90;
                case 91: return button91;
                case 92: return button92;
                case 93: return button93;
                case 94: return button94;
                case 95: return button95;
                case 96: return button96;
                case 97: return button97;
                case 98: return button98;
                case 99: return button99;
                case 100: return button100;
                case 101: return button101;
                case 102: return button102;
                case 103: return button103;
                case 104: return button104;
                case 105: return button105;
                case 106: return button106;
                case 107: return button107;
                case 108: return button108;
                case 109: return button109;
                case 110: return button110;
                case 111: return button111;
                case 112: return button112;
                case 113: return button113;
                case 114: return button114;
                case 115: return button115;
                case 116: return button116;
                case 117: return button117;
                case 118: return button118;
                case 119: return button119;
                case 120: return button120;
                case 121: return button121;
                case 122: return button122;
                case 123: return button123;
                case 124: return button124;
                case 125: return button125;
                case 126: return button126;
                case 127: return button127;
                case 128: return button128;
                case 129: return button129;
                case 130: return button130;
                case 131: return button131;
                case 132: return button132;
                case 133: return button133;
                case 134: return button134;
                case 135: return button135;
                case 136: return button136;
                case 137: return button137;
                case 138: return button138;
                case 139: return button139;
                case 140: return button140;
                case 141: return button141;
                case 142: return button142;
                case 143: return button143;
                case 144: return button144;
                case 145: return button145;
                case 146: return button146;
                case 147: return button147;
                case 148: return button148;
                case 149: return button149;
                case 150: return button150;
                case 151: return button151;
                case 152: return button152;
                case 153: return button153;
                case 154: return button154;
                case 155: return button155;
                case 156: return button156;
                case 157: return button157;
                case 158: return button158;
                case 159: return button159;
                case 160: return button160;
                case 161: return button161;
                case 162: return button162;
                case 163: return button163;
                case 164: return button164;
                case 165: return button165;
                case 166: return button166;
                case 167: return button167;
                case 168: return button168;
                case 169: return button169;
                case 170: return button170;
                case 171: return button171;
                case 172: return button172;
                case 173: return button173;
                case 174: return button174;
                case 175: return button175;
                case 176: return button176;
                case 177: return button177;
                case 178: return button178;
                case 179: return button179;
                case 180: return button180;
                case 181: return button181;
                case 182: return button182;
                case 183: return button183;
                case 184: return button184;
                case 185: return button185;
                case 186: return button186;
                case 187: return button187;
                case 188: return button188;
                case 189: return button189;
                case 190: return button190;
                case 191: return button191;
                case 192: return button192;
                case 193: return button193;
                case 194: return button194;
                case 195: return button195;
                case 196: return button196;
                case 197: return button197;
                case 198: return button198;
                case 199: return button199;
                case 200: return button200;
                case 201: return button201;
                case 202: return button202;
                case 203: return button203;
                case 204: return button204;
                case 205: return button205;
                case 206: return button206;
                case 207: return button207;
                case 208: return button208;
                case 209: return button209;
                case 210: return button210;
                case 211: return button211;
                case 212: return button212;
                case 213: return button213;
                case 214: return button214;
                case 215: return button215;
                case 216: return button216;
                case 217: return button217;
                case 218: return button218;
                case 219: return button219;
                case 220: return button220;
                case 221: return button221;
                case 222: return button222;
                case 223: return button223;
                case 224: return button224;
                case 225: return button225;
                case 226: return button226;
                case 227: return button227;
                case 228: return button228;
                case 229: return button229;
                case 230: return button230;
                case 231: return button231;
                case 232: return button232;
                case 233: return button233;
                case 234: return button234;
                case 235: return button235;
                case 236: return button236;
                case 237: return button237;
                case 238: return button238;
                case 239: return button239;
                case 240: return button240;
                case 241: return button241;
                case 242: return button242;
                case 243: return button243;
                case 244: return button244;
                case 245: return button245;
                case 246: return button246;
                case 247: return button247;
                case 248: return button248;
                case 249: return button249;
                case 250: return button250;
                case 251: return button251;
                case 252: return button252;
                case 253: return button253;
                case 254: return button254;
                case 255: return button255;
                case 256: return button256;
                case 257: return button257;
                case 258: return button258;
                case 259: return button259;
                case 260: return button260;
                case 261: return button261;
                case 262: return button262;
                case 263: return button263;
                case 264: return button264;
                case 265: return button265;
                case 266: return button266;
                case 267: return button267;
                case 268: return button268;
                case 269: return button269;
                case 270: return button270;
                case 271: return button271;
                case 272: return button272;
                case 273: return button273;
                case 274: return button274;
                case 275: return button275;
                case 276: return button276;
                case 277: return button277;
                case 278: return button278;
                case 279: return button279;
                case 280: return button280;
                case 281: return button281;
                case 282: return button282;
                case 283: return button283;
                case 284: return button284;
                case 285: return button285;
                case 286: return button286;
                case 287: return button287;
                default: throw new Exception();
            }
        }

        ////重置游戏背景，效率不高，采用下方暴力手段效果更佳
        //public void resetBtns() {
        //    foreach (Control c in this.Controls) {
        //        try {
        //            int val = Int32.Parse(c.Name[6].ToString());
        //            c.BackColor = Constant.BTNS_DEFAULT_COLOR;
        //        } catch (Exception) {
        //            continue;
        //        }
        //    }
        //}
        //重置游戏背景，Power！！！Aowu~
        public void resetBtns() {
            Color color = Constant.BTNS_DEFAULT_COLOR;
            button0.BackColor = color;
            button1.BackColor = color;
            button2.BackColor = color;
            button3.BackColor = color;
            button4.BackColor = color;
            button5.BackColor = color;
            button6.BackColor = color;
            button7.BackColor = color;
            button8.BackColor = color;
            button9.BackColor = color;
            button10.BackColor = color;
            button11.BackColor = color;
            button12.BackColor = color;
            button13.BackColor = color;
            button14.BackColor = color;
            button15.BackColor = color;
            button16.BackColor = color;
            button17.BackColor = color;
            button18.BackColor = color;
            button19.BackColor = color;
            button20.BackColor = color;
            button21.BackColor = color;
            button22.BackColor = color;
            button23.BackColor = color;
            button24.BackColor = color;
            button25.BackColor = color;
            button26.BackColor = color;
            button27.BackColor = color;
            button28.BackColor = color;
            button29.BackColor = color;
            button30.BackColor = color;
            button31.BackColor = color;
            button32.BackColor = color;
            button33.BackColor = color;
            button34.BackColor = color;
            button35.BackColor = color;
            button36.BackColor = color;
            button37.BackColor = color;
            button38.BackColor = color;
            button39.BackColor = color;
            button40.BackColor = color;
            button41.BackColor = color;
            button42.BackColor = color;
            button43.BackColor = color;
            button44.BackColor = color;
            button45.BackColor = color;
            button46.BackColor = color;
            button47.BackColor = color;
            button48.BackColor = color;
            button49.BackColor = color;
            button50.BackColor = color;
            button51.BackColor = color;
            button52.BackColor = color;
            button53.BackColor = color;
            button54.BackColor = color;
            button55.BackColor = color;
            button56.BackColor = color;
            button57.BackColor = color;
            button58.BackColor = color;
            button59.BackColor = color;
            button60.BackColor = color;
            button61.BackColor = color;
            button62.BackColor = color;
            button63.BackColor = color;
            button64.BackColor = color;
            button65.BackColor = color;
            button66.BackColor = color;
            button67.BackColor = color;
            button68.BackColor = color;
            button69.BackColor = color;
            button70.BackColor = color;
            button71.BackColor = color;
            button72.BackColor = color;
            button73.BackColor = color;
            button74.BackColor = color;
            button75.BackColor = color;
            button76.BackColor = color;
            button77.BackColor = color;
            button78.BackColor = color;
            button79.BackColor = color;
            button80.BackColor = color;
            button81.BackColor = color;
            button82.BackColor = color;
            button83.BackColor = color;
            button84.BackColor = color;
            button85.BackColor = color;
            button86.BackColor = color;
            button87.BackColor = color;
            button88.BackColor = color;
            button89.BackColor = color;
            button90.BackColor = color;
            button91.BackColor = color;
            button92.BackColor = color;
            button93.BackColor = color;
            button94.BackColor = color;
            button95.BackColor = color;
            button96.BackColor = color;
            button97.BackColor = color;
            button98.BackColor = color;
            button99.BackColor = color;
            button100.BackColor = color;
            button101.BackColor = color;
            button102.BackColor = color;
            button103.BackColor = color;
            button104.BackColor = color;
            button105.BackColor = color;
            button106.BackColor = color;
            button107.BackColor = color;
            button108.BackColor = color;
            button109.BackColor = color;
            button110.BackColor = color;
            button111.BackColor = color;
            button112.BackColor = color;
            button113.BackColor = color;
            button114.BackColor = color;
            button115.BackColor = color;
            button116.BackColor = color;
            button117.BackColor = color;
            button118.BackColor = color;
            button119.BackColor = color;
            button120.BackColor = color;
            button121.BackColor = color;
            button122.BackColor = color;
            button123.BackColor = color;
            button124.BackColor = color;
            button125.BackColor = color;
            button126.BackColor = color;
            button127.BackColor = color;
            button128.BackColor = color;
            button129.BackColor = color;
            button130.BackColor = color;
            button131.BackColor = color;
            button132.BackColor = color;
            button133.BackColor = color;
            button134.BackColor = color;
            button135.BackColor = color;
            button136.BackColor = color;
            button137.BackColor = color;
            button138.BackColor = color;
            button139.BackColor = color;
            button140.BackColor = color;
            button141.BackColor = color;
            button142.BackColor = color;
            button143.BackColor = color;
            button144.BackColor = color;
            button145.BackColor = color;
            button146.BackColor = color;
            button147.BackColor = color;
            button148.BackColor = color;
            button149.BackColor = color;
            button150.BackColor = color;
            button151.BackColor = color;
            button152.BackColor = color;
            button153.BackColor = color;
            button154.BackColor = color;
            button155.BackColor = color;
            button156.BackColor = color;
            button157.BackColor = color;
            button158.BackColor = color;
            button159.BackColor = color;
            button160.BackColor = color;
            button161.BackColor = color;
            button162.BackColor = color;
            button163.BackColor = color;
            button164.BackColor = color;
            button165.BackColor = color;
            button166.BackColor = color;
            button167.BackColor = color;
            button168.BackColor = color;
            button169.BackColor = color;
            button170.BackColor = color;
            button171.BackColor = color;
            button172.BackColor = color;
            button173.BackColor = color;
            button174.BackColor = color;
            button175.BackColor = color;
            button176.BackColor = color;
            button177.BackColor = color;
            button178.BackColor = color;
            button179.BackColor = color;
            button180.BackColor = color;
            button181.BackColor = color;
            button182.BackColor = color;
            button183.BackColor = color;
            button184.BackColor = color;
            button185.BackColor = color;
            button186.BackColor = color;
            button187.BackColor = color;
            button188.BackColor = color;
            button189.BackColor = color;
            button190.BackColor = color;
            button191.BackColor = color;
            button192.BackColor = color;
            button193.BackColor = color;
            button194.BackColor = color;
            button195.BackColor = color;
            button196.BackColor = color;
            button197.BackColor = color;
            button198.BackColor = color;
            button199.BackColor = color;
            button200.BackColor = color;
            button201.BackColor = color;
            button202.BackColor = color;
            button203.BackColor = color;
            button204.BackColor = color;
            button205.BackColor = color;
            button206.BackColor = color;
            button207.BackColor = color;
            button208.BackColor = color;
            button209.BackColor = color;
            button210.BackColor = color;
            button211.BackColor = color;
            button212.BackColor = color;
            button213.BackColor = color;
            button214.BackColor = color;
            button215.BackColor = color;
            button216.BackColor = color;
            button217.BackColor = color;
            button218.BackColor = color;
            button219.BackColor = color;
            button220.BackColor = color;
            button221.BackColor = color;
            button222.BackColor = color;
            button223.BackColor = color;
            button224.BackColor = color;
            button225.BackColor = color;
            button226.BackColor = color;
            button227.BackColor = color;
            button228.BackColor = color;
            button229.BackColor = color;
            button230.BackColor = color;
            button231.BackColor = color;
            button232.BackColor = color;
            button233.BackColor = color;
            button234.BackColor = color;
            button235.BackColor = color;
            button236.BackColor = color;
            button237.BackColor = color;
            button238.BackColor = color;
            button239.BackColor = color;
            button240.BackColor = color;
            button241.BackColor = color;
            button242.BackColor = color;
            button243.BackColor = color;
            button244.BackColor = color;
            button245.BackColor = color;
            button246.BackColor = color;
            button247.BackColor = color;
            button248.BackColor = color;
            button249.BackColor = color;
            button250.BackColor = color;
            button251.BackColor = color;
            button252.BackColor = color;
            button253.BackColor = color;
            button254.BackColor = color;
            button255.BackColor = color;
            button256.BackColor = color;
            button257.BackColor = color;
            button258.BackColor = color;
            button259.BackColor = color;
            button260.BackColor = color;
            button261.BackColor = color;
            button262.BackColor = color;
            button263.BackColor = color;
            button264.BackColor = color;
            button265.BackColor = color;
            button266.BackColor = color;
            button267.BackColor = color;
            button268.BackColor = color;
            button269.BackColor = color;
            button270.BackColor = color;
            button271.BackColor = color;
            button272.BackColor = color;
            button273.BackColor = color;
            button274.BackColor = color;
            button275.BackColor = color;
            button276.BackColor = color;
            button277.BackColor = color;
            button278.BackColor = color;
            button279.BackColor = color;
            button280.BackColor = color;
            button281.BackColor = color;
            button282.BackColor = color;
            button283.BackColor = color;
            button284.BackColor = color;
            button285.BackColor = color;
            button286.BackColor = color;
            button287.BackColor = color;
        }
        
        private void GameForm_KeyDown(object sender, KeyEventArgs e) {
            if (!inGame) return;
            switch (e.KeyCode) {
                case Keys.W: {
                        if (block1.canRotate(map)) {
                            eraseBlock(block1);
                            block1.update_RTT();
                            showBlock(block1);
                        }
                        break;
                    }
                case Keys.S: {
                        if (block1.canMoveDown(map)) {
                            mainTimer.Stop();
                            eraseBlock(block1);
                            block1.update_MD();
                            showBlock(block1);
                            mainTimer.Start();
                        }
                        break;
                    }
                case Keys.A: {
                        if (block1.canMoveLeft(map)) {
                            eraseBlock(block1);
                            block1.update_ML();
                            showBlock(block1);
                        }
                        break;
                    }
                case Keys.D: {
                        if (block1.canMoveRight(map)) {
                            eraseBlock(block1);
                            block1.update_MR();
                            showBlock(block1);
                        }
                        break;
                    }
                default: break;
            }
        }

        private void timeTimer_Tick(object sender, EventArgs e) {
            ++time;
            if (time == 20) {
                time = 0;
                if (mainTimer.Interval > 200) mainTimer.Interval -= 20;
            }
        }
    }
}
