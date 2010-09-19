namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;

    public class WorkProgressVisitor : IVisitor
    {
        public WorkProgressVisitor()
        {
            this.Progress = new Progress();
        }

        public Progress Progress { get; protected set; }
     
        public void Visit(Entity node, Action<IVisitor> visitChildren)
        {
            var providesWork = node as IHaveRegisteredWork;
            if (providesWork != null)
            {
                this.Progress.TrackTimeSpent(providesWork.Amount);
            }

            var providesEstimate = node as IHaveEstimate;
            if (providesEstimate != null)
            {
                this.Progress.TrackEstimate(providesEstimate.Estimate);
            }

            visitChildren(this);
        }
    }
}