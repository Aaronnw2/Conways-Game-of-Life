using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        Button[,] buttonArray = new Button[30, 30];
        bool[,] currentBoolArray = new bool[30, 30];
        bool[,] nextBoolArray = new bool[30, 30];

        private void Form1_Load(object sender, EventArgs e)
        {
            //make the 15x15 button array
            int height = 20, width = 20;
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    int currentButtonLocationHorizontal = i * width;
                    int currentButtonLocationVerticle = j * height;
                    buttonArray[i, j] = new Button();
                    buttonArray[i, j].Size = new Size(width, height);
                    buttonArray[i, j].Location = new Point(currentButtonLocationHorizontal, currentButtonLocationVerticle);
                    buttonArray[i, j].BackColor = Color.Black;
                    buttonArray[i, j].Click += new EventHandler(button_Click);
                    this.Controls.Add(buttonArray[i, j]);
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //This doesn't work properly. changing the buttons one at a time doesn't work, as evaluating one at a time will change the 
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    int numberOfLivingNeighbors = 0;
                    if (i > 0)
                    {
                        //check left
                        if (buttonArray[i - 1, j].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if (i < 29)
                    {
                        //check right
                        if (buttonArray[i + 1, j].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if (j > 0)
                    {
                        //check up
                        if (buttonArray[i, j - 1].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if (j < 29)
                    {
                        //check down
                        if (buttonArray[i, j + 1].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if(i > 0 && j > 0)
                    {
                        //check upper left diagonal
                        if (buttonArray[i - 1, j - 1].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if (i < 29 && j < 29)
                    {
                        //check lower right diagonal
                        if (buttonArray[i + 1, j + 1].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if (i > 0 && j < 29)
                    {
                        //check lower left
                        if (buttonArray[i - 1, j + 1].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    if (i < 29 && j > 0)
                    {
                        //check upper right
                        if (buttonArray[i + 1, j - 1].BackColor == Color.Green)
                            numberOfLivingNeighbors++;
                    }
                    switch (numberOfLivingNeighbors)
                    {
                        case 0:
                        case 1:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            //cell dies
                            buttonArray[i, j].BackColor = Color.Black;
                            break;
                        case 2:
                            //if living, stay living, if dead, stay dead
                            break;
                        case 3:
                            //turn green no matter what
                            buttonArray[i, j].BackColor = Color.Green;
                            break;
                    }
                }
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton.BackColor == Color.Black)
            {
                clickedButton.BackColor = Color.Green;
                
            }
            else
                clickedButton.BackColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text == "Start")
            {
                gameTimer.Start();
                btnStartStop.Text = "Stop";
            }
            else
            {
                gameTimer.Stop();
                btnStartStop.Text = "Start";
            }
        }
    }
}
