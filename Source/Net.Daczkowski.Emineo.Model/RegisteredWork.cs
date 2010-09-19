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

        public TimeSpan WorkAmount { get; protected set; }

        public User Developer { get; protected set; }

        public Place Place { get; protected set; }

        public Task Parent { get; protected set; }

        TimeSpan IHaveRegisteredWork.Amount
        {
            get { return this.WorkAmount; }
        }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.WorkAmount).GetHashCode();
        }
    }
}