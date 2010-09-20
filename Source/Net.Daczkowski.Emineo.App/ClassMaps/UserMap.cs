namespace Net.Daczkowski.Emineo.App.ClassMaps
{
    using FluentNHibernate.Mapping;
    using Net.Daczkowski.Emineo.Model;

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.LastLogin);
        }
    }
}
