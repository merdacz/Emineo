namespace Net.Daczkowski.Emineo.Model
{
    using Net.Tgd.NH.Model;

    public class Category : Entity
    {
        public string Name { get; protected set; }

        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Name).GetHashCode();
        }
    }
}