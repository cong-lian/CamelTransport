namespace CamelTransport;

public class CamelDp(int capacity)
{
    private readonly int Capacity = capacity <= 0 ? throw new ArgumentException("Capacity must be a positive integer") : capacity;
    private const int BananasPerMile = 1;

    private static readonly int[,] dp = new int[1001, 5001];

    private int RecBananaCount(int miles, int bananas)
    {
        // case 1: too few bananas and too many miles
        if (bananas <= miles * BananasPerMile) return 0;

        // case 2: can carry all bananas in 1 trip
        if (bananas <= Capacity) return bananas - miles * BananasPerMile;

        // distance = 0
        if (miles == 0) return bananas;

        // If the current state is already calculated
        if (dp[miles, bananas] != -1) return dp[miles, bananas];

        // Stores the maximum count of bananas
        var maxCount = Int32.MinValue;

        // Stores the number of trips to transfer B bananas using a camel of Capacity C
        var tripCount = bananas % Capacity == 0 ? ((2 * bananas) / Capacity) - 1
                                   : ((2 * bananas) / Capacity) + 1;

        // Loop to iterate over all the drops in range [1, miles]
        for (var i = 1; i <= miles; i++)
        {
            // Recursive call over the remaining mils
            var curCount = RecBananaCount(miles - i, bananas - tripCount * i * BananasPerMile);

            // Update the maxCount
            if (curCount > maxCount)
            {
                maxCount = curCount;
                // Memoize the current value
                dp[miles, bananas] = maxCount;
            }
        }

        // Return answer
        return maxCount;
    }

    public int Transport(int bananas, int miles)
    {
        if (bananas <= 0) throw new ArgumentException("Number of bananas must be a positive integer");
        if (miles <= 0) throw new ArgumentException("Total miles must be a positive integer");

        // Initialize dp array with -1
        for (int i = 0; i < 1001; i++)
            for (int j = 0; j < 5001; j++)
                dp[i, j] = -1;

        return RecBananaCount(miles, bananas);
    }
}