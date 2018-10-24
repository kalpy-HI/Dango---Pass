using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

/*
    pictureBox 1~36為第一個鍵盤
    pictureBox 37~72為第二個鍵盤
    pictureBox 73~102為認證系統之格
 */
namespace TestKeyboard
{
    public partial class Form1 : Form
    {
        int[] keyboard1 = new int[36];//大寫和特殊符號:前10為特殊符號,後26為大寫英文字母
        int[] keyboard2 = new int[36];//小寫和數字:前10為數字,後26為小寫英文字母
        int[,] CirCle = new int[5, 6];//左方圓圈內之顏色
        int[] color = new int[6];//紅:0 黃:1 灰:2 藍:3 綠:4 白:5 
        int CurrentRow = 0;

        public Form1()
        {
            InitializeComponent();
            PaintCircle();
            RandomColor();
            GenerateColor();
            RandomLoginColor();

            //Test For Circle's Color
            string a = "";
            for (int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    a = a + CirCle[i,j] + " ";
                }
                a += "\n";
            }
            label74.Text = a;
            //End Test
             
            //this.label2.Parent = this.pictureBox1;         Test For Label's Back Color 
            //this.label2.BackColor = Color.Transparent;     But not works
        }

        public void ShiftColor(int row, int LR)//Test SLL=>LR:0 SRL=>LR:1
        {
            int[] temp = new int[6];
            switch (LR)
            {
                case 0:
                    for (int j = 0; j < 6; j++)
                    {
                        temp[j] = CirCle[row, j];
                    }
                    for (int k = 0; k < 6; k++)
                    {
                        if (k == 5) CirCle[row, k] = temp[0];
                        else CirCle[row, k] = temp[k + 1];
                    }
                    break;
                case 1:
 
                    for (int j = 0; j < 6; j++)
                    {
                        temp[j] = CirCle[row, j];
                    }
                    for (int k = 0; k < 6; k++)
                    {
                        if (k == 0) CirCle[row, k] = temp[5];
                        else CirCle[row, k] = temp[k - 1];
                    }
                    break;
                default:
                    Console.WriteLine("Error Error!!!");
                    break;
            }

             for(int i = 0; i < 6; i++)
             {
                string pictbox = "pictureBox" + (73 + 6 * row+i);
                Control ctro = tableLayoutPanel1.Controls[pictbox]; //控制TABLE中之圖
                int currentColor = CirCle[row,i];
                if (currentColor == 0) //紅色
                {
                    ctro.BackColor = Color.Red;
                }
                else if (currentColor == 1) //黃色
                {
                    ctro.BackColor = Color.Yellow;
                }
                else if (currentColor == 2) //灰色
                {
                    ctro.BackColor = Color.Gray;
                }
                else if (currentColor == 3) //藍色
                {
                    ctro.BackColor = Color.Blue;
                }
                else if (currentColor == 4) //綠色
                {
                    ctro.BackColor = Color.Green;
                }
                else if (currentColor == 5) //白色
                {
                    ctro.BackColor = Color.White;
                }
             }
        }

        public void PaintCircle()//把左方的pictureBox變成圓形
        {
            for(int i = 73; i <= 102; i++)
            {
                string pictbox = "pictureBox" + i;
                Control ctro = tableLayoutPanel1.Controls[pictbox]; //控制TABLE中之圖
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(ctro.ClientRectangle);
                Region reg = new Region(path);
                ctro.Region = reg;
            }
        }

        public void RandomLoginColor()//將左方之pictureBox著色並記錄
        {
            Random rnd = new Random();
            for(int i = 73; i <= 102; i+=6)
            {
                int[] ColorInRow = new int[6];
                for (int j = 0; j < 6; j++)
                {
                    string pictbox = "pictureBox" + (i+j);
                    Control ctro = tableLayoutPanel1.Controls[pictbox]; //控制TABLE中之圖

                    int dice = rnd.Next(0, 6);
                    while (ColorInRow[dice] == 1)
                    {
                        dice = rnd.Next(0, 6);
                    }
                    ColorInRow[dice] = 1;
                    this.CirCle[(i-73)/6,j] = dice; //紀錄Circle之顏色

                    int currentColor = dice;
                    if (currentColor == 0) //紅色
                    {
                        ctro.BackColor = Color.Red;
                    }
                    else if (currentColor == 1) //黃色
                    {
                        ctro.BackColor = Color.Yellow;
                    }
                    else if (currentColor == 2) //灰色
                    {
                        ctro.BackColor = Color.Gray;
                    }
                    else if (currentColor == 3) //藍色
                    {
                        ctro.BackColor = Color.Blue;
                    }
                    else if (currentColor == 4) //綠色
                    {
                        ctro.BackColor = Color.Green;
                    }
                    else if (currentColor == 5) //白色
                    {
                        ctro.BackColor = Color.White;
                    }
                }
            }
        }

        public void RandomColor()//random character's color
        {
            Random rnd = new Random();

            for (int i = 0; i < 36; i++) { //random 大寫keyboard
                int dice = rnd.Next(0, 6);
                while (this.color[dice] == 12)
                {
                    dice = rnd.Next(0, 6);
                }
                this.keyboard1[i] = dice;
                this.color[dice]++;
            }
            for (int i = 0; i < 36; i++)
            {
                int dice = rnd.Next(0, 6);
                while (this.color[dice] == 12)
                {
                    dice = rnd.Next(0, 6);
                }
                this.keyboard2[i] = dice;
                this.color[dice]++;
            }
        }

        public void GenerateColor()//random後將對應之格子著色
        {
            for (int i = 0; i < 36; i++)
            {
                string picbox = "pictureBox" + (i + 1);
                Control ctn = this.Controls[picbox];
                int currentColor = this.keyboard1[i];

                if (currentColor == 0) //紅色
                {
                    ctn.BackColor = Color.Red;

                }
                else if(currentColor == 1) //黃色
                {
                    ctn.BackColor = Color.Yellow;
                }
                else if (currentColor == 2) //灰色
                {
                    ctn.BackColor = Color.Gray;
                }
                else if (currentColor == 3) //藍色
                {
                    ctn.BackColor = Color.Blue;
                }
                else if (currentColor == 4) //綠色
                {
                    ctn.BackColor = Color.Green;
                }
                else if (currentColor == 5) //白色
                {
                    ctn.BackColor = Color.White;
                }
            }
            for (int i = 0; i < 36; i++)
            {
                string picbox = "pictureBox" + (i + 1 + 36);
                Control ctn = this.Controls[picbox];
                int currentColor = this.keyboard2[i];

                if (currentColor == 0) //紅色
                {
                    ctn.BackColor = Color.Red;
                }
                else if (currentColor == 1) //黃色
                {
                    ctn.BackColor = Color.Yellow;
                }
                else if (currentColor == 2) //灰色
                {
                    ctn.BackColor = Color.Gray;
                }
                else if (currentColor == 3) //藍色
                {
                    ctn.BackColor = Color.Blue;
                }
                else if (currentColor == 4) //綠色
                {
                    ctn.BackColor = Color.Green;
                }
                else if (currentColor == 5) //白色
                {
                    ctn.BackColor = Color.White;
                }
            }
        }
        
        private void TestButton_Click(object sender, EventArgs e)//測試用
        {
            ShiftColor(0,1);
            //Test For Circle's Color
            string a = "";
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    a = a + CirCle[i, j] + " ";
                }
                a += "\n";
            }
            label74.Text = a;
            //End Test
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)//WASD控制方向
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    ShiftColor(CurrentRow, 0);
                    //Test For Circle's Color
                    string a = "";
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            a = a + CirCle[i, j] + " ";
                        }
                        a += "\n";
                    }
                    label74.Text = a;
                    //End Test
                    break;
                case Keys.D:
                    ShiftColor(CurrentRow, 1);
                    //Test For Circle's Color
                    string b = "";
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            b = b + CirCle[i, j] + " ";
                        }
                        b += "\n";
                    }
                    label74.Text = b;
                    //End Test
                    break;
                case Keys.W:
                    if (CurrentRow == 0) break;
                    else CurrentRow -= 1;
                    break;
                case Keys.S:
                    if (CurrentRow == 4) break;
                    else CurrentRow += 1;
                    break;
                default:
                    break;
            }
            switch (CurrentRow)
            {
                case 0:
                    Arrow1.Location = new Point(636, 17);
                    break;
                case 1:
                    Arrow1.Location = new Point(636, 17+77);
                    break;
                case 2:
                    Arrow1.Location = new Point(636, 17+77*2);
                    break;
                case 3:
                    Arrow1.Location = new Point(636, 17+77*3);
                    break;
                case 4:
                    Arrow1.Location = new Point(636, 17+77*4);
                    break;
            }
        }
    }
}
