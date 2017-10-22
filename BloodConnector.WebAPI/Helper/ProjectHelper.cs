using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace BloodConnector.WebAPI.Helper
{
    public class ProjectHelper
    {
        public static string GetUserName(string fName, string lName, string nName)
        {
            var fullName = $"{fName} {lName}".Trim();

            return string.IsNullOrEmpty(nName) ?  fullName : nName;
        }

        public static string GetFullName(string fName, string lName, string nName)
        {
            var fullName = $"{fName} {lName}".Trim();

            return string.IsNullOrEmpty(nName) ? fullName: $"{fullName} ({nName})";
        }

        public static string GetName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        private static string GetPropertyNameCore(Expression propertyRefExpr)
        {
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                return memberExpr.Member.Name;

            throw new ArgumentException("No property reference expression was found.",
                             "propertyRefExpr");
        }
    }
}