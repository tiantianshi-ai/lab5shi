namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name:Tiantian Shi 
         * Date: November 2025
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/tiantianshi-ai/lab5shi
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Checked = true;
            //add your name to end of form title
            this.Text += "by Tiantian Shi";
        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();

        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2;
            //call ftn RollDice, placing returned number into integers
            dice1 = RollDice();
            dice2 = RollDice();
            //place integers into labels
            lblDice1.Text = dice1.ToString();
            lblDice2.Text = dice2.ToString();

            // call ftn GetName sending total and returning name
            int total = dice1 + dice2;
            string rollName = GetName(total);
            //display name in label
            lblRollName.Text = rollName;
        }
        //method 

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
        private void ClearOneRoll()
        {
            lblDice1.Text = string.Empty;
            lblDice2.Text = string.Empty;
            lblRollName.Text = string.Empty;
        }


        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
        private void ClearStats()
        {
            
            if (nudNumber.Minimum > 0)
            {
                nudNumber.Value = nudNumber.Minimum;
            }
            else
            {
                nudNumber.Value = 1; 
            }
            chkSeed.Checked = false;

            lblPass.Text = string.Empty;
            lblFail.Text = string.Empty;
            lblAverage.Text = string.Empty;
            lstMarks.Items.Clear();
        }

        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1, 7);
        }


        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int total)
        {
            string rollName;
            switch (total)
            {
                case 2:
                    rollName = "Snake Eyes";
                    break;
                case 3:
                    rollName = "Little Joe";
                    break;
                case 5:
                    rollName = "Fever";
                    break;
                case 7:
                    rollName = "Most Common";
                    break;
                case 9:
                    rollName = "Center Field";
                    break;
                case 11:
                    rollName = "Yo-leven";
                    break;
                case 12:
                    rollName = "Boxcars";
                    break;
                default:
                    rollName = "No special name";
                    break;
            }
            return rollName;
        }

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            //call ftn DataPresent twice sending string returning boolean
            string dice1String = lblDice1.Text;
            string dice2String = lblDice2.Text;
            bool dice1Present = DataPresent(dice1String);
            bool dice2Present = DataPresent(dice2String);

            //if data present in both labels, call SwapData sending both strings
            if (dice1Present && dice2Present)
            {
                SwapData(ref dice1String, ref dice2String);
                //put data back into labels
                lblDice1.Text = dice1String;
                lblDice2.Text = dice2String;
            }
            //if data not present in either label display error msg
            else
            {
                MessageBox.Show("Roll the dice", "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(string data)
        {
            return !string.IsNullOrWhiteSpace(data);
        }


        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(ref string val1, ref string val2)
        {
            string temp = val1;
            val1 = val2;
            val2 = temp;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //declare variables and array
            int count = (int)nudNumber.Value;
            int passCount, failCount;
            int[] marks = new int[count];
            double average;
            //check if seed value
            if (chkSeed.Checked)
            {
                rand = new Random(1000);
            }
            lstMarks.Items.Clear();
            //fill array using random number
            int i = 0;
            while (i < count)
            {
                int mark = rand.Next(40, 101);
                marks[i] = mark;
                lstMarks.Items.Add(mark);
                i++;
            }

            //call CalcStats sending and returning data
            average = CalcStats(marks, out passCount, out failCount);

            //display data sent back in labels - average, pass and fail
            lblPass.Text = passCount.ToString();
            lblFail.Text = failCount.ToString();
            // Format average always showing 2 decimal places 
            lblAverage.Text = average.ToString("N2");

        } // end Generate click

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private double CalcStats(int[] marks, out int pass, out int fail)
        {
            double total = 0;
            pass = 0;
            fail = 0;
            const int PASS_MARK = 60;

            foreach (int mark in marks)
            {
                total += mark;

                if (mark >= PASS_MARK)
                {
                    pass++;
                }
                else
                {
                    fail++;
                }
            }

            // Calculate average
            if (marks.Length > 0)
            {
                return total / marks.Length;
            }
            else
            {
                return 0.0;
            }
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOneRoll.Checked)
            {
                grpOneRoll.Visible = true;
                grpMarkStats.Visible = false;
                ClearOneRoll();
            }
            else
            {
                grpOneRoll.Visible = false;
                grpMarkStats.Visible = true;
                ClearStats();
            }
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeed.Checked)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to use a seed value","cofirm seed value",
        
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    chkSeed.Checked = false;
                }

            }
        }
    }
}

