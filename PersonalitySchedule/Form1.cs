using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalitySchedule
{
    public partial class Form1 : Form
    {
        private List<String> mPersons = new List<String>();
        private List<String> mActions = new List<String>();
        private List<ScheduleItem> mSchedule = new List<ScheduleItem>();
        private Random mRand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            InitializeStatusBar(Constants.APP_VERSION, Constants.APP_AUTHOR, Constants.APP_AUTHOR_URL);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateSchedule();
        }

        private void LoadData()
        {
            using (StreamReader r = new StreamReader(File.Open(Application.StartupPath + Constants.PATH_PERSONS, FileMode.Open)))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    mPersons.Add(line);
                    lstPersons.Items.Add(line);
                }
            }

            using (StreamReader r = new StreamReader(File.Open(Application.StartupPath + Constants.PATH_ACTIONS, FileMode.Open)))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    mActions.Add(line);
                    lstActions.Items.Add(line);
                }
            }
        }

        private void LoadInitialSchedule(int maxHour, int maxMin)
        {
            int currHour = System.DateTime.Now.Hour;
            int currMinute = System.DateTime.Now.Minute;

            for (int i = 0; i < mPersons.Count; i++)
            {
                for (int j = 0; j < mActions.Count; j++)
                {
                    int randHour = 0;
                    int randMinute = 0;

                    randHour = mRand.Next(currHour, maxHour + 1);
                    if (randHour == currHour)
                    {
                        randMinute = mRand.Next(currMinute + 1, 60);
                    }
                    else if (randHour == maxHour)
                    {
                        randMinute = mRand.Next(0, maxMin);
                    }
                    else { randMinute = mRand.Next(0, 60); }

                    ScheduleItem schi = new ScheduleItem(randHour, randMinute, mPersons[i], mActions[j]);
                    mSchedule.Add(schi);
                }
            }
        }

        private void PrintSchedule(List<ScheduleItem> item)
        {
            txtResult.Clear();
            for (int i = 0; i < mSchedule.Count; i++)
            {
                txtResult.AppendText(item[i].ToString() + System.Environment.NewLine);
            }
            txtResult.AppendText(Constants.STR_TOTAL_ACTIONS + item.Count.ToString());
        }

        private void GenerateSchedule()
        {
            mSchedule.Clear();
            LoadInitialSchedule(Convert.ToInt32(numHours.Value), Convert.ToInt32(numMinutes.Value));

            ScheduleComparer comparer = new ScheduleComparer();
            mSchedule.Sort(comparer);

            PrintSchedule(mSchedule);
        }

        private void InitializeStatusBar(String appVersion, String appAuthor, String appAuthorUrl)
        {
            txtToolVersion.Text = "v" + appVersion;
            txtToolAuthor.Text = "© " + appAuthor;
            txtToolWebsite.Text = "  " + appAuthorUrl;
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Constants.APP_AUTHOR_URL);
        }
    }
}