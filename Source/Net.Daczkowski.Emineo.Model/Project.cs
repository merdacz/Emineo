namespace Net.Daczkowski.Emineo.Model
{
    using System.Collections.Generic;
    using Net.Tgd.NH.Model;

    public class Project : Entity
    {
        public string Name { get; protected set; }

        public User Manager { get; protected set; }

        public IEnumerable<Task> Tasks { get; protected set; }

        public IEnumerable<Task> AllTasks
        {
            get
            {
                var result = new List<Task>();
                return result;
            }
        }
        
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Name).GetHashCode();
        }
    }
}