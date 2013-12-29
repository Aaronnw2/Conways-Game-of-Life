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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Button[,] buttonArray = new Button[30, 30];

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
                    buttonArray[i, j].Click += button_Click;
                    this.Controls.Add(buttonArray[i, j]);
                }
            }
            Timer gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Start();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
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
                            buttonArray[i, j].BackColor = Color.Gray;
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

        public void button_Click(object sender, buttonEventArgs e)
        {

        }
    }
}
