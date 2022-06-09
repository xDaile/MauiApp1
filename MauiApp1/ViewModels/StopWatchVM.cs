using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;


namespace MauiApp1.ViewModels
{
   
    public class StopWatchVM
    {
        public Stopwatch stopwatch = new();

        private TimeSpan elapsed;

        public string remainingTime;

        private bool isRunning;

        private TimeSpan Remaining;


        public StopWatchVM(double totalMinutes)
        {
            int minutes = Convert.ToInt32(Math.Floor(totalMinutes));
            double restOfMinute = totalMinutes - minutes;
            int seconds = Convert.ToInt32(Math.Floor(restOfMinute * 60));
            //string q = String.Format("00:{0}:{1}", minutes.ToString(), seconds.ToString());
            Remaining = new TimeSpan(0,minutes,seconds);
            this.remainingTime = FormatTime(Remaining);
            this.isRunning = false;
        }


        private string FormatTime(TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"mm\:ss");
        }

        public void Start()
        {
            stopwatch.Restart();

            isRunning = true;

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    remainingTime = FormatTime( this.Remaining - stopwatch.Elapsed);
                });
                return isRunning;
            });
        }

        public void Stop()
        {
            stopwatch.Stop();
            isRunning = false;
        }

      

        public void Clear()
        {

            remainingTime = "00:00.000";

        }


    }
}
