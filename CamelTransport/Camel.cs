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

        // case 1: too few bananas and too many miles
        if (numberOfBananas < BananasPerMile * totalMiles) return 0;

        var numberOfRoundTrips = (int)Math.Ceiling((double)numberOfBananas / Capacity);
        var numberOfBananasAtLastTrip = numberOfBananas % Capacity;

        // case 2: can carry all bananas in 1 trip
        if (numberOfRoundTrips == 1) return numberOfBananas - BananasPerMile * totalMiles;

        // case 3: can not carry all bananas in 1 trip
        var remainingBananas = numberOfBananas;
        var remainingMiles = totalMiles;

        do
        {
            // no need to return
            if (numberOfRoundTrips == 1)
            {
                remainingBananas -= remainingMiles * BananasPerMile; break;
            }
            // fully loaded in last trip
            if (numberOfBananasAtLastTrip == 0)
            {
                var milesToTake = Capacity / (2 * numberOfRoundTrips - 1);

                // reached destination
                if (milesToTake >= remainingMiles)
                {
                    remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * remainingMiles * BananasPerMile;
                    break;
                }
                // not yet reached destination
                else
                {
                    remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * milesToTake * BananasPerMile;
                    remainingMiles -= milesToTake;
                }
            }
            // not fully loaded in last trip
            else
            {
                var milesToTake = Capacity / (2 * numberOfRoundTrips - 1);

                // it is worth to make the last trip
                if (numberOfBananasAtLastTrip > 2 * remainingMiles * BananasPerMile || numberOfBananasAtLastTrip > 2 * milesToTake * BananasPerMile)
                {
                    numberOfRoundTrips--;

                    // reached destination
                    if (milesToTake >= remainingMiles)
                    {
                        remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * remainingMiles * BananasPerMile;
                        remainingBananas += numberOfBananasAtLastTrip - 2 * remainingMiles * BananasPerMile;
                        break;
                    }
                    // not yet reached destination
                    else
                    {
                        remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * milesToTake * BananasPerMile;
                        remainingBananas += numberOfBananasAtLastTrip - 2 * milesToTake * BananasPerMile;
                        remainingMiles -= milesToTake;
                    }
                }
                // it is better to abandon the last trip
                else
                {
                    numberOfRoundTrips--;
                    milesToTake = Capacity / (2 * numberOfRoundTrips - 1);

                    // reached destination
                    if (milesToTake >= remainingMiles)
                    {
                        remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * remainingMiles * BananasPerMile;
                        break;
                    }
                    // not yet reached destination
                    else
                    {
                        remainingBananas = Capacity * numberOfRoundTrips - (2 * numberOfRoundTrips - 1) * milesToTake * BananasPerMile;
                        remainingMiles -= milesToTake;
                    }
                }
            }
            numberOfRoundTrips = (int)Math.Ceiling((double)remainingBananas / Capacity);
            numberOfBananasAtLastTrip = remainingBananas % Capacity;

            // till reached the destination or no bananas anymore
        } while (remainingMiles > 0 && remainingBananas > 0);

        return remainingBananas <= 0 ? 0 : remainingBananas;
    }
}