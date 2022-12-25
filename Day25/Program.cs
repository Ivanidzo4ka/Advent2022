using System.Text;
StepOne();
void StepOne()
{
    long sum = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        sum += ConvertToNormal(line);
    }

    Console.WriteLine(ConvertToSNAFU(sum));
}

long ConvertToNormal(string s)
{
    long power = 1;
    long ans = 0;
    for (int i = s.Length - 1; i >= 0; i--)
    {
        ans += (power * s[i] switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '-' => -1,
            '=' => -2,
        });
        power *= 5;
    }
    return ans;
}

string ConvertToSNAFU(long number)
{

    var stack = new Stack<char>();
    var carry = 0;

    while (number != 0)
    {
        var rem = (number + carry) % 5;
        carry = 0;
        if (rem <= 2)
        {
            stack.Push((char)(rem + '0'));
        }
        else if (rem == 3)
        {
            stack.Push('=');
            carry = 1;
        }
        else if (rem == 4)
        {
            stack.Push('-');
            carry = 1;
        }
        number /= 5;
    }
    var sb = new StringBuilder();
    while (stack.Count != 0)
    {
        sb.Append(stack.Pop());
    }
    return sb.ToString();
}