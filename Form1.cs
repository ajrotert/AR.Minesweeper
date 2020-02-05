using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AR.Minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int width = 9;
        public static int height = 9;
        public int bombs;
        public int flagged;
        public int avaliable;
        public static bool flag;
        Grid g;
        Button[,] buttonArray = new Button[width, height];
        public Button[] queueHold = new Button[width * height];
        public int queueHoldSize = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            start();
        }

        private void start()
        {
            avaliable = width * height;
            bombs = 10;
            flagged = bombs;
            flag = false;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    buttonArray[w, h] = new System.Windows.Forms.Button();
                    buttonArray[w, h].Enabled = true;
                    buttonArray[w, h].Location = new System.Drawing.Point(123 + (w * 25), 123 + (h * 25));
                    buttonArray[w, h].Name = "button";
                    buttonArray[w, h].Size = new System.Drawing.Size(25, 25);
                    buttonArray[w, h].TabIndex = 0;
                    buttonArray[w, h].UseVisualStyleBackColor = true;
                    buttonArray[w, h].Click += new System.EventHandler(button_Click);
                    buttonArray[w, h].BringToFront();
                    Controls.Add(buttonArray[w, h]);
                }
            }

            restartButton = new System.Windows.Forms.Button();
            flagButton = new System.Windows.Forms.Button();
            size_label1 = new System.Windows.Forms.Label();
            bomb_label1 = new System.Windows.Forms.Label();

            restartButton.BackColor = System.Drawing.SystemColors.ControlLight;
            restartButton.Location = new System.Drawing.Point(123, 369);
            restartButton.Name = "restartButton";
            restartButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            restartButton.Size = new System.Drawing.Size(99, 30);
            restartButton.TabIndex = 0;
            restartButton.Text = "Restart";
            restartButton.UseVisualStyleBackColor = false;
            restartButton.Click += new System.EventHandler(restart_Click);
            Controls.Add(restartButton);

            flagButton.BackColor = System.Drawing.SystemColors.ControlLight;
            flagButton.Location = new System.Drawing.Point(251, 369);
            flagButton.Name = "flagButton";
            flagButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            flagButton.Size = new System.Drawing.Size(99, 30);
            flagButton.TabIndex = 1;
            flagButton.Text = "Flag";
            flagButton.UseVisualStyleBackColor = false;
            flagButton.Click += new System.EventHandler(flag_Click);
            Controls.Add(flagButton);

            size_label1.AutoSize = true;
            size_label1.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            size_label1.ForeColor = System.Drawing.Color.White;
            size_label1.Location = new System.Drawing.Point(120, 36);
            size_label1.Name = "size_label1";
            size_label1.Size = new System.Drawing.Size(81, 39);
            size_label1.TabIndex = 0;
            size_label1.Text = "Size: " + width + " X " + height;
            Controls.Add(size_label1);

            bomb_label1.AutoSize = true;
            bomb_label1.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bomb_label1.ForeColor = System.Drawing.Color.White;
            bomb_label1.Location = new System.Drawing.Point(120, 75);
            bomb_label1.Name = "bomb_label1";
            bomb_label1.Size = new System.Drawing.Size(120, 39);
            bomb_label1.TabIndex = 1;
            bomb_label1.Text = "Bombs: " + flagged;
            Controls.Add(bomb_label1);

            g = new Grid(width, height, bombs);
            Bomb b = new Bomb(g);
            b.SetBombs();
            //g.ShowAll();
        }

        private void queue(Button b)
        {
            int counter = 0;
            if (b.Enabled)
            {
                while (queueHold[counter] != null)
                    counter++;
                queueHold[counter] = b;
                queueHoldSize++;
            }
        }
        private void dequeue()
        {
            Button temp = queueHold[0];
            for(int a =1; a<queueHoldSize; a++)
                queueHold[a - 1] = queueHold[a];
            if (queueHoldSize == 0)
                queueHold[0] = null;
            queueHold[queueHoldSize-1] = null;
            queueHoldSize--;
            if(temp.Enabled != false)
                update(temp);

        }

        private void update(Button b)
        {
            b.Enabled = false;
            int width = ((Convert.ToInt32(b.Location.X) - 123) / 25) +1;
            int height = ((Convert.ToInt32(b.Location.Y) - 123) / 25) +1;
            int loc = g.GetGrid(width, height);
            if(loc == 0)
            {
                width--;
                height--;
                if (width - 1 >= 0 && height >= 0 && width - 1 < 9 && height < 9)
                {
                    queue(buttonArray[width - 1, height]);
                }
                if (width >= 0 && height - 1 >= 0 && width < 9 && height - 1 < 9)
                {
                    queue(buttonArray[width, height - 1]);
                }
                if (width >= 0 && height + 1 >= 0 && width < 9 && height + 1 < 9)
                { 
                    queue(buttonArray[width, height + 1]);
                }
                if (width + 1 >= 0 && height >= 0 && width + 1 < 9 && height < 9)
                { 
                    queue(buttonArray[width + 1, height]);
                }

                if (width + 1 >= 0 && height+1 >= 0 && width + 1 < 9 && height+1 < 9)
                {
                    queue(buttonArray[width + 1, height+1]);
                }
                if (width + 1 >= 0 && height-1 >= 0 && width + 1 < 9 && height-1 < 9)
                {
                    queue(buttonArray[width + 1, height-1]);
                }
                if (width - 1 >= 0 && height-1 >= 0 && width - 1 < 9 && height-1 < 9)
                {
                    queue(buttonArray[width -1, height - 1]);
                }
                if (width - 1 >= 0 && height+1 >= 0 && width - 1 < 9 && height+1 < 9)
                {
                    queue(buttonArray[width - 1, height + 1]);
                }
                avaliable--;
            }
            else if (loc != -1)
            {
                avaliable--;
                b.Text = loc.ToString();
            }
            else
            {
                avaliable--;
                Lose();
                b.Text = '\u00D2'.ToString();
                b.BackColor = System.Drawing.Color.OrangeRed;
                bombs--;

                MessageBox.Show("You Lose");
            }
            if (avaliable == bombs)
            {
                Win();
                MessageBox.Show("You Win");
            }
        }

        private void Lose()
        {
            Button b;
            int wdth, hght, loc;
            for(int w = 0; w<width; w++)
            {
                for(int h = 0; h<height; h++)
                {
                    b = buttonArray[w, h];
                    b.Enabled = false;
                    wdth = ((Convert.ToInt32(b.Location.X) - 123) / 25) + 1;
                    hght = ((Convert.ToInt32(b.Location.Y) - 123) / 25) + 1;
                    loc = g.GetGrid(wdth, hght);
                    if (loc != -1 && loc != 0)
                        b.Text = loc.ToString();
                    else if (loc != 0)
                    {
                        b.BackColor = System.Drawing.Color.OrangeRed;
                        b.Text = '\u00D2'.ToString();
                    }
                }
            }
        }
        private void Win()
        {
            bomb_label1.Text = "Bombs: 0";
            Button b;
            int wdth, hght, loc;
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    b = buttonArray[w, h];
                    b.Enabled = false;
                    wdth = ((Convert.ToInt32(b.Location.X) - 123) / 25) + 1;
                    hght = ((Convert.ToInt32(b.Location.Y) - 123) / 25) + 1;
                    loc = g.GetGrid(wdth, hght);
                    if (loc != -1 && loc != 0)
                        b.Text = loc.ToString();
                    else if (loc != 0)
                    {
                        b.BackColor = System.Drawing.Color.Lime;
                        b.Text = '\u00D2'.ToString();
                    }
                }
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (flag)
            {
                if (b.Text == "")
                {
                    b.BackColor = System.Drawing.Color.DeepSkyBlue;
                    b.Text = '\u00DE'.ToString();
                    flagged--;
                    bomb_label1.Text = "Bombs: " + flagged;
                }
                else
                {
                    b.BackColor = System.Drawing.SystemColors.ControlLight;
                    b.Text = "";
                    flagged++;
                    bomb_label1.Text = "Bombs: " + flagged;
                }
            }
            else if(b.Text == "")
            {
                update(b);
                while (queueHoldSize > 0)
                    dequeue();
            }
        }
        private void restart_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            start();
        }
        private void flag_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                flagButton.Text = "Flag";
            }
            else
            {
                flag = true;
                flagButton.Text = "Select";
            }
        }

        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Button flagButton;
    }
}
