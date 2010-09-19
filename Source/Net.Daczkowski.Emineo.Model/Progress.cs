namespace Net.Daczkowski.Emineo.Model
{
    using System;

    public class Progress
    {
        public TimeSpan Estimate { get; protected set; }

        public TimeSpan TimeSpent { get; protected set; }

        /// <summary>
        /// Gets project realization completeness information.
        /// </summary>
        /// <remarks>
        /// May be over 100% in the case of overrun.
        /// </remarks>
        public double Completeness 
        { 
            get
            {
                double spentHours = this.TimeSpent.TotalHours;
                double estimatedHours = this.Estimate.TotalHours;
                return spentHours / estimatedHours * 100;
            }
        }

        /// <summary>
        /// Gets project overrun in hours. 
        /// </summary>
        public double Overrun
        {
            get
            {
                TimeSpan remainingWork = this.TimeSpent - this.Estimate;
                if (remainingWork < TimeSpan.Zero)
                {
                    return -remainingWork.TotalHours;
                }
             
                return 0;
            }
        }

        public void TrackTimeSpent(TimeSpan timeSpent)
        {
            this.TimeSpent += timeSpent;
        }

        public void TrackEstimate(TimeSpan estimate)
        {
            this.Estimate += estimate;
        }
    }
}
