using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalitySchedule
{
    class ScheduleComparer : IComparer<ScheduleItem>
    {
        public int Compare(ScheduleItem x, ScheduleItem y)
        {
            if (x.getCompareTime() > y.getCompareTime()) { return 1; }
            else if (x.getCompareTime() < y.getCompareTime()) { return -1; }
            else { return 0; }
        }
    }
    class ScheduleItem
    {
        private int mHour;
        private int mMinute;
        private String mPerson;
        private String mAction;
        private int mCompareDate;

        public ScheduleItem(int h, int m, String p, String a)
        {
            mHour = h;
            mMinute = m;
            mPerson = p;
            mAction = a;
            InitCompareDate();
        }

        public int getCompareTime() { return mCompareDate; }

        public override string ToString()
        {
            String hour;
            String minute;
            if (mHour < 10) { hour = "0" + mHour.ToString(); }
            else { hour = mHour.ToString(); }
            if (mMinute < 10) { minute = "0" + mMinute.ToString(); }
            else { minute = mMinute.ToString(); }
            return hour + ":" + minute + " | " + mPerson + " | " + mAction.ToString();
        }

        private void InitCompareDate()
        {
            String hour;
            String minute;
            if (mHour < 10) { hour = "0" + mHour.ToString(); }
            else { hour = mHour.ToString(); }
            if (mMinute < 10) { minute = "0" + mMinute.ToString(); }
            else { minute = mMinute.ToString(); }
            string tmp = hour + minute;
            mCompareDate = Convert.ToInt32(tmp);
        }
    }
}
