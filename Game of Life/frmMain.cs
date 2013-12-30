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

        //dynamic button array
        Button[,] buttonArray = new Button[30, 30];

        private void Form1_Load(object sender, EventArgs e)
        {
            //make the 15x15 button array
            int height = 20, width = 20;
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    //find the proper location
                    int currentButtonLocationHorizontal = i * width;
                    int currentButtonLocationVerticle = j * height;
                    buttonArray[i, j] = new Button();
                    buttonArray[i, j].Size = new Size(width, height);
                    buttonArray[i, j].Location = new Point(currentButtonLocationHorizontal, currentButtonLocationVerticle);
                    buttonArray[i, j].BackColor = Color.Black;
                    //add the click event to each button
                    buttonArray[i, j].Click += new EventHandler(button_Click);
                    this.Controls.Add(buttonArray[i, j]);
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //This doesn't work properly. changing the buttons one at a time doesn't work, as evaluating one at a time will change the current state
            bool[,] currentBoolArray = new bool[30, 30];
            bool[,] nextBoolArray = new bool[30, 30];
            //record the current state
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (buttonArray[i, j].BackColor == Color.Green)
                        currentBoolArray[i, j] = true;
                    else
                        currentBoolArray[i, j] = false;
                }
            }
            //find the number of nieghbors each cell has
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    //make sure we're not trying to check an edge that doesn't have certian neighbors
                    int numberOfLivingNeighbors = 0;
                    if (i > 0)
                    {
                        //check left
                        if (currentBoolArray[i - 1, j] == true)
                            numberOfLivingNeighbors++;
                    }
                    if (i < 29)
                    {
                        //check right
                        if (currentBoolArray[i + 1, j] == true)
                            numberOfLivingNeighbors++;
                    }
                    if (j > 0)
                    {
                        //check up
                        if (currentBoolArray[i, j - 1] == true)
                            numberOfLivingNeighbors++;
                    }
                    if (j < 29)
                    {
                        //check down
                        if (currentBoolArray[i, j + 1] == true)
                            numberOfLivingNeighbors++;
                    }
                    if(i > 0 && j > 0)
                    {
                        //check upper left diagonal
                        if (currentBoolArray[i - 1, j - 1] == true)
                            numberOfLivingNeighbors++;
                    }
                    if (i < 29 && j < 29)
                    {
                        //check lower right diagonal
                        if (currentBoolArray[i + 1, j + 1] == true)
                            numberOfLivingNeighbors++;
                    }
                    if (i > 0 && j < 29)
                    {
                        //check lower left
                        if (currentBoolArray[i - 1, j + 1] == true)
                            numberOfLivingNeighbors++;
                    }
                    if (i < 29 && j > 0)
                    {
                        //check upper right
                        if (currentBoolArray[i + 1, j - 1] == true)
                            numberOfLivingNeighbors++;
                    }
                    //find the state for the next generation
                    switch (numberOfLivingNeighbors)
                    {
                        case 0:
                        case 1:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            //cell dies, either too many neighbors or too few
                            nextBoolArray[i, j] = false;
                            break;
                        case 2:
                            //if living, stay living, if dead, stay dead
                            if (currentBoolArray[i, j] == true)
                                nextBoolArray[i, j] = true;
                            else
                                nextBoolArray[i, j] = false;
                            break;
                        case 3:
                            //turn green no matter what
                            nextBoolArray[i, j] = true;
                            break;
                    }
                }
            }
            //assign the next state to the buttonsfrom the boolean array
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (nextBoolArray[i, j] == true)
                        buttonArray[i, j].BackColor = Color.Green;
                    else
                        buttonArray[i, j].BackColor = Color.Black;
                }
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            //check if the cell is alive or not, and switch it to the opposite
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
            //pause or start/resume the game
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            //stops the timer and clears the game board: OH THE HUMANITY!
            gameTimer.Stop();
            btnStartStop.Text = "Start";
            for (int i = 0; i < 30; i++)
                for (int j = 0; j < 30; j++)
                    buttonArray[i, j].BackColor = Color.Black;
        }
    }
}
