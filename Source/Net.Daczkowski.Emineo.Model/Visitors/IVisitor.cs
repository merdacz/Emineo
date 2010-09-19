namespace Net.Tgd.NH.Model.Visitors
{
    using System;
    using Net.Daczkowski.Emineo.Model;

    public interface IVisitor
    {
        void Visit(Entity node, Action<IVisitor> visitChildren);
    }
}
