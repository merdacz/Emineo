namespace Net.Daczkowski.Emineo.Model
{
    using System;

    /// <summary>
    /// Represents overall progress for a given project part.
    /// </summary>
    public class Progress : Entity
    {
        public virtual TimeSpan Estimate { get; protected set; }

        public virtual TimeSpan TimeSpent { get; protected set; }

        /// <summary>
        /// Gets project realization completeness information.
        /// </summary>
        /// <remarks>
        /// May be over 100% in the case of overrun.
        /// </remarks>
        public virtual double Completeness 
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
        public virtual double Overrun
        {
            get
            {
                TimeSpan remainingWork = this.Estimate - this.TimeSpent;
                if (remainingWork < TimeSpan.Zero)
                {
                    return -remainingWork.TotalHours;
                }
             
                return 0;
            }
        }

        public virtual void TrackTimeSpent(TimeSpan timeSpent)
        {
            this.TimeSpent += timeSpent;
        }

        public virtual void TrackEstimate(TimeSpan estimate)
        {
            this.Estimate += estimate;
        }

        public override string ToString()
        {
            if (this.Overrun > 0)
            {
                return "Overrun by " + this.Overrun + "hours";
            }

            return this.TimeSpent.TotalHours + "/" + this.Estimate.TotalHours + "[h]";
        }
    }
}
