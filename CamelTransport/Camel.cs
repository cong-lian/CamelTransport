namespace CamelTransport;

public class Camel(int capacity)
{
    private readonly int Capacity = capacity <= 0 ? throw new ArgumentException("Capacity must be a positive integer") : capacity;
    private const int BananasPerMile = 1;

    // return remaining bananas after transporting them an amount of miles.
    public int Transport(int numberOfBananas, int totalMiles)
    {
        if (numberOfBananas <= 0) throw new ArgumentException("Number of bananas must be a positive integer");
        if (totalMiles <= 0) throw new ArgumentException("Total miles must be a positive integer");

        if (numberOfBananas < BananasPerMile * totalMiles) return 0;
        // one run
        if (numberOfBananas <= Capacity) return numberOfBananas - BananasPerMile * totalMiles;

        // more than one run
        var remainingBananas = numberOfBananas;
        var remainingMiles = totalMiles;

        while (remainingMiles > 0)
        {
            var numberOfRoundTrips = remainingBananas / Capacity;
            var milesToTake = Capacity / (2 * numberOfRoundTrips - 1);

            if (milesToTake >= remainingMiles)
            {
                remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * remainingMiles * BananasPerMile;
                break;
            }

            remainingMiles -= milesToTake;
            remainingBananas = Capacity * numberOfRoundTrips - milesToTake * (2 * numberOfRoundTrips - 1) * BananasPerMile;
        }

        return remainingBananas;
    }
}