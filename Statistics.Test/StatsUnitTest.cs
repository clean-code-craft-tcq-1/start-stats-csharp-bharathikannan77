using System;
using Xunit;
using Statistics;
using System.Collections.Generic;

namespace Statistics.Test
{
    public class StatsUnitTest
    {
        [Fact]
        public void ReportsAverageMinMax()
        {
            var statsComputer = new StatsComputer();
            var computedStats = statsComputer.CalculateStatistics(
                new List<float> { 1.5F, 8.9F, 3.2F, 4.5F });
            float epsilon = 0.001F;

            Assert.True(Math.Abs(statsComputer.average - 4.525F) <= epsilon);
            Assert.True(Math.Abs(statsComputer.max - 8.9F) <= epsilon);
            Assert.True(Math.Abs(statsComputer.min - 1.5F) <= epsilon);
        }
        [Fact]
        public void ReportsNaNForEmptyInput()
        {
            var statsComputer = new StatsComputer();
            var computedStats = statsComputer.CalculateStatistics(
                new List<float> { });

            Assert.True(computedStats[0].Equals(float.NaN));
            Assert.True(computedStats[1].Equals(float.NaN));
            Assert.True(computedStats[2].Equals(float.NaN));
            //All fields of computedStats (average, max, min) must be
            //Double.NaN (not-a-number), as described in
            //https://docs.microsoft.com/en-us/dotnet/api/system.double.nan?view=netcore-3.1
        }
        [Fact]
        public void RaisesAlertsIfMaxIsMoreThanThreshold()
        {
            var emailAlert = new EmailAlert();
            var ledAlert = new LEDAlert();
            IAlerter[] alerters = { (IAlerter)emailAlert, (IAlerter)ledAlert };

            const float maxThreshold = 10.2F;
            var statsAlerter = new StatsAlerter(maxThreshold, alerters);
            var response = statsAlerter.CheckAndAlert(new List<float> { 0.2F, 11.9F, 4.3F, 8.5F });

            Assert.True(response[0], emailAlert.emailSent);
            Assert.True(response[1], ledAlert.ledGlows);
        }
    }

    internal class StatsAlerter
    {
        private float maxThreshold;
        public IAlerter[] alerters;
        public bool sendEmail;
        private bool GlowLed;


        public StatsAlerter(float maxThreshold, IAlerter[] alerters)
        {
            this.maxThreshold = maxThreshold;
            this.alerters = alerters;
        }

        internal List<bool> CheckAndAlert(List<float> lists)
        {
            var listofBool = new List<bool>();
            foreach (var a in lists)
            {
                if (a > maxThreshold)
                {
                    sendEmail = true;
                    GlowLed = true;
                    break;
                }
                sendEmail = false;
                GlowLed = false;



            }
            listofBool.Add(sendEmail);
            listofBool.Add(GlowLed);
            return listofBool;
        }
    }

    internal class LEDAlert : IAlerter
    {
        internal string ledGlows;

        public LEDAlert()
        {
            ledGlows = "LED Glows";
        }

        public void EmailAlertMethod()
        {
            //implemented the mandatory method from interface
        }
        public void LEDAlertMethod()
        {
            //implemented the mandatory method from interface
        }
    }

    internal class EmailAlert : IAlerter
    {
        internal string emailSent;

        public EmailAlert()
        {
            emailSent = "emailSent";
        }
        public void EmailAlertMethod()
        {
            //implemented the mandatory method from interface
        }
        public void LEDAlertMethod()
        {
            //implemented the mandatory method from interface
        }
    }

    internal interface IAlerter
    {
        void EmailAlertMethod();
        void LEDAlertMethod();
    }
}
