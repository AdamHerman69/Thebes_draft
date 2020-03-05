using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{

    public class NotEnoughTimeException : Exception { }

    public class Time : IComparable<Time>, IEquatable<Time>
    {
        public delegate int PlayersOnWeek(Time time);
        PlayersOnWeek playersOnWeek;

        public delegate void OnNewYear();
        OnNewYear onNewYear;

        public static int weeksInAYear = 52;
        public static int firstYear;
        public static int lastYear;
        public static int startingWeek;

        private static int initialOrderCounter;

        public void Configure(int playerCount)
        {
            // supporting only 2-4 players atm
            if (playerCount < 2 || playerCount > 4)
            {
                throw new ArgumentException("Supportig only 2-4 players atm");
            }

            initialOrderCounter = playerCount - 1;
            lastYear = 1903;

            if (playerCount == 2)
            {
                startingWeek = 1;
                firstYear = 1901;
            }
            else if (playerCount == 3)
            {
                startingWeek = 16;
                firstYear = 1901;
            }
            else if (playerCount == 4)
            {
                startingWeek = 1;
                firstYear = 1902;
            }
        }

        public int CurrentWeek { get; set; }
        public int CurrentYear { get; set; }
        public int SameWeekOrder { get; set; }

        public Time(PlayersOnWeek playersOnWeek, OnNewYear onNewYear)
        {
            if (startingWeek == 0) // hasn't been configured
            {
                throw new InvalidOperationException("Time needs to be configured first");
            }

            this.CurrentWeek = startingWeek;
            this.CurrentYear = firstYear;
            this.SameWeekOrder = initialOrderCounter--;
            this.playersOnWeek = playersOnWeek;
            this.onNewYear = onNewYear;
        }

        public int RemainingWeeks()
        {
            return weeksInAYear - CurrentWeek + ((lastYear - CurrentYear) * weeksInAYear) + 1;
        }

        public bool CanSpendWeeks(int weeks)
        {
            return weeks <= RemainingWeeks();
        }

        /// <summary>
        /// Adds requested amount of weeks to a players counter and sets his order for that week
        /// </summary>
        /// <param name="weeks">Amount of weeks to spend</param>
        public void SpendWeeks(int weeks)
        {
            if (!CanSpendWeeks(weeks))
            {
                throw new NotEnoughTimeException();
            }

            CurrentWeek += weeks;
            while (CurrentWeek > weeksInAYear)
            {
                CurrentYear++;
                CurrentWeek -= weeksInAYear;
                onNewYear();
            }

            this.SameWeekOrder = playersOnWeek(this);
        }

        public void EndYear()
        {
            SpendWeeks(weeksInAYear - CurrentWeek + 1);
        }

        public bool Equals(Time other)
        {
            return this.CurrentWeek == other.CurrentWeek && this.CurrentYear == other.CurrentYear;
        }

        public int CompareTo(Time other)
        {
            int result;
            result = this.CurrentYear.CompareTo(other.CurrentYear);
            if (result == 0)
            {
                result = this.CurrentWeek.CompareTo(other.CurrentWeek);
            }

            if (result == 0)
            {
                result = other.SameWeekOrder.CompareTo(this.SameWeekOrder); // player with highest order goes first
            }

            if (result == 0)
            {
                throw new InvalidOperationException("two players with the same order");
            }

            return result;
        }
    }
}
