using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFtsRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFtsRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);
                return node;
            }

            if (node.Method.DeclaringType == typeof(string))
            {
                switch (node.Method.Name)
                {
                    case "Equals":
                        Visit(node.Object);
                        _resultStringBuilder.Append("(");
                        Visit(node.Arguments[0]);
                        _resultStringBuilder.Append(")");
                        return node;

                    case "StartsWith":
                        Visit(node.Object);
                        _resultStringBuilder.Append("(");
                        Visit(node.Arguments[0]);
                        _resultStringBuilder.Append("*)");
                        return node;

                    case "EndsWith":
                        Visit(node.Object);
                        _resultStringBuilder.Append("(*");
                        Visit(node.Arguments[0]);
                        _resultStringBuilder.Append(")");
                        return node;

                    case "Contains":
                        Visit(node.Object);
                        _resultStringBuilder.Append("(*");
                        Visit(node.Arguments[0]);
                        _resultStringBuilder.Append("*)");
                        return node;

                    default:
                        throw new NotSupportedException($"Method {node.Method.Name} is not supported yet.");
                }

            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    {
                        Expression leftNode;
                        Expression rightNode;
                        try
                        {
                            var nodes = new[] { node.Left, node.Right };

                            leftNode = nodes.Single(x => x.NodeType is ExpressionType.MemberAccess);
                            rightNode = nodes.Single(x => x.NodeType is ExpressionType.Constant);
                        }
                        catch (Exception)
                        {
                            throw new NotSupportedException($"Operand should be property, field or constant: {node.NodeType}");
                        }

                        Visit(leftNode);
                        _resultStringBuilder.Append("(");
                        Visit(rightNode);
                        _resultStringBuilder.Append(")");
                        break;
                    }

                case ExpressionType.AndAlso:
                    {
                        Visit(node.Left);
                        _resultStringBuilder.Append(" AND ");
                        Visit(node.Right);
                        break;
                    }

                default:
                    throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append(node.Value);

            return node;
        }

        #endregion
    }
}
