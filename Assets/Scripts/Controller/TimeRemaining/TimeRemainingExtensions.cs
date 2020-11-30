using System.Collections.Generic;

namespace Controller
{
    public static partial class TimeRemainingExtensions
    {
        #region Fields
        
        private static readonly List<ITimeRemaining> _timeRemainings = new List<ITimeRemaining>(63);
        
        #endregion
        
        
        #region Properties

        public static List<ITimeRemaining> TimeRemainings => _timeRemainings;
        
        #endregion
        
        
        #region Execute

        public static void AddTimeRemainingExecute(this ITimeRemaining value)
        {
            if (_timeRemainings.Contains(value))
            {
                return;
            }

            value.CurrentTime = value.Time;
            _timeRemainings.Add(value);
        }

        public static void RemoveTimeRemainingExecute(this ITimeRemaining value)
        {
            if (!_timeRemainings.Contains(value))
            {
                return;
            }
            _timeRemainings.Remove(value);
        }
        
        #endregion
    }
}