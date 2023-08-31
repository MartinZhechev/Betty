using System.Globalization;

double balance = 0;
var input = string.Empty;

while (input != "exit")
{
    Console.WriteLine("Please, submit action:");
    
    input = Console.ReadLine()!.ToLower();
    var split = input.Split(' ');

    if (split.Length < 2)
        continue;

    var parseResult = double.TryParse(split[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var amount);
    
    if (!parseResult)
        continue;

    if (input.Contains("bet"))
    {
        if (amount >= 1 && amount <= 10)
        {
            if (balance < amount)
            {                    
                Console.WriteLine("Insufficient funds!");
                continue;
            }            
            
            double amountWon;
            var random = new Random().Next(10);
            
            switch (random)
            {
                case < 5:
                    balance -= amount;
                    Console.Write("No luck this time!");
                    break;
                case > 5 and < 9:
                    amountWon = WinAmountCalculator(10, 20);
                    balance = (balance - amount) + (amount * amountWon);
                    Console.Write($"Congrats you won ${ToStringWithFormat(amount * amountWon)}!");
                    break;
                default:
                    amountWon = WinAmountCalculator(20, 100);
                    balance = (balance - amount) + (amount * amountWon);
                    Console.Write($"Congrats you won ${ToStringWithFormat(amount * amountWon)}!");
                    break;
                
            }
            Console.WriteLine($" Your current balance is: ${ToStringWithFormat(balance)}");
        }
        else
        {
            Console.WriteLine(amount < 1 ? $"Minimum bet is: 1" : $"Maximum bet is: 10");
        }

        continue;
    }

    if (input.Contains("deposit"))
    {
        balance += amount;
        
        Console.WriteLine($"Your deposit of ${ToStringWithFormat(amount)} was successful. Your current balance is: ${ToStringWithFormat(balance)}");
        continue;
    }

    if (input.Contains("withdraw"))
    {
        if (balance - amount < 0)
            balance = 0;
        else
            balance -= amount;
        
        amount -= balance;
        
        Console.WriteLine($"Your withdrawal of {ToStringWithFormat(amount)} was successful. Your current balance is: {ToStringWithFormat(balance)}");
    }
}

Console.WriteLine($"Thank you for playing! Hope to see you again soon.");

double WinAmountCalculator(int from, int to)
{
    var random = new Random().Next(from, to);
    return (random / 10.0);
}

string ToStringWithFormat(double number) => number.ToString("F", CultureInfo.InvariantCulture);