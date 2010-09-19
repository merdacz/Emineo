namespace Net.Daczkowski.Emineo.Model.Visitors
{
    using System;
    using Net.Daczkowski.Emineo.Model;

    public interface IVisitor
    {
        void Visit(Entity node, Action<IVisitor> visitChildren);
    }
}
