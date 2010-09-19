namespace Net.Daczkowski.Emineo.Model
{
    public class User : Entity
    {
        public string Name { get; protected set; }
        
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + this.Name).GetHashCode();
        }
    }
}