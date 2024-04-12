using System.Linq.Expressions;

namespace SunRaysMarket.Shared.Extensions.Visitors;

public class PropertyNameExtractingVisitor : ExpressionVisitor
{
    public string? PropertyName { get; private set; }
    public Type? PropertyType { get; private set; }

    protected override Expression VisitMember(MemberExpression member)
    {
        PropertyType = member.Type;
        PropertyName = member.ToString().Split(".").Last();

        return base.VisitMember(member);
    }
}