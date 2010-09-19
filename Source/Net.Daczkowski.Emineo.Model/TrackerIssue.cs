namespace Net.Daczkowski.Emineo.Model
{
    using Net.Tgd.NH.Model;

    public class TrackerIssue : Entity
    {
        public string Code { get; protected set; }

        public User Reporter { get; protected set; }

        public Category Category { get; protected set; }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Code).GetHashCode();
        }
    }
}