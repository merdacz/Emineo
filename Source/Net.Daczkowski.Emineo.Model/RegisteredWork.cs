namespace Net.Daczkowski.Emineo.Model
{
    using System;
    using Net.Daczkowski.Emineo.Model.Visitors;

    /// <summary>
    /// Represents task registered work.
    /// </summary>
    public class RegisteredWork : Entity, IHaveRegisteredWork
    {
        protected RegisteredWork()
        {
        }

        public RegisteredWork(TimeSpan workAmount, User developer, Place place, Task task)
        {
            this.WorkAmount = workAmount;
            this.Developer = developer;
            this.Place = place;
            this.Parent = task;
        }

        public virtual TimeSpan WorkAmount { get; protected set; }

        public virtual User Developer { get; protected set; }

        public virtual Place Place { get; protected set; }

        public virtual Task Parent { get; protected set; }

        TimeSpan IHaveRegisteredWork.Amount
        {
            get { return this.WorkAmount; }
        }
    }
}