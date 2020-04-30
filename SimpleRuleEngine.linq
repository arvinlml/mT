<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Linq.Expressions</Namespace>
</Query>

void Main()
{
	List<Rule> rules = new List<Rule> {
		new Rule { MemberName = "Age", Operator = "GreaterThan", TargetValue = "20" },
		new Rule { MemberName = "Name", Operator = "Equal", TargetValue = "John"    }
	};
	var user1 = new User { Age = 13, Name = "royi" };
	var user2 = new User { Age = 33, Name = "john" };
	var user3 = new User { Age = 53, Name = "paul" };
	var rule = new Rule { MemberName = "Age", Operator = "GreaterThan", TargetValue = "20" };
	Func<User, bool> compiledRule = CompileRule<User>(rule);
	compiledRule(user1).Dump(); 
	compiledRule(user2).Dump();
	compiledRule(user3).Dump();
}

public class User
{
	public int Age { get; set; }
	public string Name { get; set; }
}
public class Rule
{
	public string MemberName { get; set; }
	public string Operator { get; set; }
	public string TargetValue { get; set; }
}

static Expression BuildExpr<T>(Rule r, ParameterExpression param)
{
	var left = MemberExpression.Property(param, r.MemberName);
	var tProp = typeof(T).GetProperty(r.MemberName).PropertyType;
	ExpressionType tBinary;
	// is the operator a known .NET operator?
	if (ExpressionType.TryParse(r.Operator, out tBinary))
	{
		var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tProp));
		// use a binary operation, e.g. 'Equal' -> 'u.Age == 15'
		return Expression.MakeBinary(tBinary, left, right);
	}
	else
	{
		var method = tProp.GetMethod(r.Operator);
		var tParam = method.GetParameters()[0].ParameterType;
		var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tParam));
		// use a method call, e.g. 'Contains' -> 'u.Tags.Contains(some_tag)'
		return Expression.Call(left, method, right);
	}
}

public static Func<T, bool> CompileRule<T>(Rule r)
{
	var paramUser = Expression.Parameter(typeof(User));
	Expression expr = BuildExpr<T>(r, paramUser);
	// build a lambda function User->bool and compile it
	return Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile();
}