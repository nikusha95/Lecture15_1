// See https://aka.ms/new-console-template for more information
//(1+2)*3*(1+2*3)-4

using System.Reflection.Metadata.Ecma335;

double r = CalculateExpression("(1+2)*3*(1+2*3)"); //9*7 = 63

Console.WriteLine(r);

static double CalculateExpression(string expression)
{
    Stack<int> numbers = new Stack<int>();
    Stack<char> operators = new Stack<char>();
    Stack<char> brackets = new Stack<char>();
    double result = 0;
    foreach (var t in expression)
    {
        double tmp = 0;
        if (IsNumber(t))
        {
            if (brackets.Count == 0 && operators.Count != 0)
            {
                result = Calculate(result, t - '0', operators.Pop());
            }
            else
            {
                numbers.Push(t - '0');
            }
        }
        else if (IsOperator(t))
        {
            operators.Push(t);
        }
        else if (t == '(')
        {
            brackets.Push(t);
        }
        else if (t == ')')
        {
            brackets.Pop();
            tmp = numbers.Pop();
            while (numbers.Count != 0)
            {
                double num = numbers.Pop();
                char op = operators.Pop();
                tmp = Calculate(tmp, num, op);
            }

            if (operators.Count != 0)
            {
                result = Calculate(result, tmp, operators.Pop());
            }
            else
            {
                result += tmp;
            }
        }
    }

    return result;
}

static double Calculate(double x, double y, char op)
{
    return op switch
    {
        '*' => x * y,
        '/' => x / y,
        '+' => x + y,
        '-' => x - y,
        _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
    };
}

static bool IsOperator(char c)
{
    return c is '*' or '/' or '+' or '-';
}

static bool IsNumber(char c)
{
    return c is >= '1' and <= '9';
}