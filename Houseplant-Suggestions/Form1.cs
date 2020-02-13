using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Houseplant_Suggestions
{
    public partial class Form1 : Form
    {
        /* Declaring and initializing Global variables
         * and add readonly modifiers to them */
        readonly int MinTemp = 50;
        readonly int MaxTemp = 90;

        bool ShownMinWarning = false;
        bool ShownMaxWarning = false; 

        public Form1()
        {
            InitializeComponent();
            this.trkTemp.Scroll += new System.EventHandler(this.HouseConditionsChanged);

            this.trkTemp.Minimum = MinTemp;
            this.trkTemp.Maximum = MaxTemp;
        }

        private void trkTemp_Scroll(object sender, EventArgs e)
        {
            // Use format string, the # symbol is replaced by the number in trkTemp.Value
            // Pressing Alt + 0176 on your number pad types a ° symbol
            lblTemp.Text = trkTemp.Value.ToString("# °F");
        }

        private void HouseConditionsChanged(object sender, EventArgs e)
        {
            int homeTemp = trkTemp.Value;
            bool southFacingWindowAvailable = chkSouthFacing.Checked;
            
            /* Showing a message box for the minimum and maximum temperatures */
            if ( ShownMinWarning == false && homeTemp == MinTemp)
            {
                MessageBox.Show(text: "Your home may be too cold for most houseplants", caption: "Information");
                ShownMinWarning = true;
            }
            if ( ShownMaxWarning == false && homeTemp == MaxTemp)
            {
                MessageBox.Show(text: "Your home may be too hot for most houseplants", caption: "Information");
                ShownMaxWarning = true;
            }

            // Call our method, use return value
            string suggestedPlant = GenerateSuggestion(homeTemp, southFacingWindowAvailable);

            lblSuggestion.Text = suggestedPlant;
        }


        private string GenerateSuggestion(int temp, bool southFacing)
        {
            if (southFacing)
            {
                if (temp > 65)
                //if (homeTemp > 65)
                {
                    return "Peace Lily";
                }
                else
                {
                    return "Spider plant";
                }
            }
            else
            {
                if (temp > 65)
                //if (homeTemp > 65)
                {
                    return "Dragon Tree";
                }
                else
                {
                    return "Ivy";
                }
            }
        }

        private void lnkHousePlantInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblSuggestion.Text == "Plant suggest here")
            {
                ShowWebPage();   // PlanName is optional
            }
            else
            {
                ShowWebPage(lblSuggestion.Text);
            }
            
        }
        private void ShowWebPage(string plantName = null) // Method with optional parameter
        {
            string url = "https://www.houseplant411.com/";
            if (plantName != null)
            {
                // Link a specific plant should be in the form "https://www.houseplant411.com/houseplant?hpq=ivy"
                url = url + "houseplant?hpq=" + plantName;
            }
            System.Diagnostics.Process.Start(url);  // Launch web browser, navigate to URL given
   
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
