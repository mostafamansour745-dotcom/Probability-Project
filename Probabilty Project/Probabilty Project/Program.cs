using System;

//                                Mostafa AbdElhay Ahmed Mansour
class Program
{
    static int[] data = { 115, 182, 191, 31, 196, 1099, 5, 172, 10, 179, 83, 21, 20, 21,
                          186, 177, 195, 193, 188, 199, 62, 109, 105, 183, 110 };

    static void Main()
    {
        int n = data.Length;
        double[] sorted = data.Select(x => (double)x).OrderBy(x => x).ToArray();
        
        Console.WriteLine("\t\t\t\t\t====== Part 1: Statistics ======\n");
        Console.WriteLine($"n = {n}");
        Console.WriteLine($"Data (sorted): {string.Join(", ", sorted)}\n");

        // (i) Mean
        double mean = sorted.Average();
        Console.WriteLine($"(i)   Mean               = {mean:F4}");

        // (ii) Mode
        var mode = data.GroupBy(x => x)
                       .OrderByDescending(g => g.Count())
                       .First();
        Console.WriteLine($"(ii)  Mode               = {mode.Key} (appears {mode.Count()} times)");

        // (iii) Median
        double median = Percentile(sorted, 50);
        Console.WriteLine($"(iii) Median             = {median:F4}");

        // (iv) Variance (population variance)
        double variance = sorted.Sum(x => Math.Pow(x - mean, 2)) / n;
        Console.WriteLine($"(iv)  Variance           = {variance:F4}");

        // (v) P20
        double p20 = Percentile(sorted, 20);
        Console.WriteLine($"(v)   P20                = {p20:F4}");

        // (vi) P50
        double p50 = Percentile(sorted, 50);
        Console.WriteLine($"(vi)  P50                = {p50:F4}");

        // (vii) First Quartile Q1 = P25
        double q1 = Percentile(sorted, 25);
        Console.WriteLine($"(vii) First Quartile Q1  = {q1:F4}");

        // (viii) Second Quartile Q2 = P50
        double q2 = Percentile(sorted, 50);
        Console.WriteLine($"(viii)Second Quartile Q2 = {q2:F4}");

        // (ix) Third Quartile Q3 = P75
        double q3 = Percentile(sorted, 75);
        Console.WriteLine($"(ix)  Third Quartile Q3  = {q3:F4}");

        // (x) Range
        double range = sorted[n - 1] - sorted[0];
        Console.WriteLine($"(x)   Range              = {range:F4}");

        // (xi) Interquartile Range
        double iqr = q3 - q1;
        Console.WriteLine($"(xi)  Interquartile Range= {iqr:F4}");

        // (xii) Standard Deviation
        double stdDev = Math.Sqrt(variance);
        Console.WriteLine($"(xii) Standard Deviation = {stdDev:F4}");

        // (xiii) Summation of Divisions (sum of each value divided by mean)
        double sumDivisions = sorted.Sum(x => x / mean);
        Console.WriteLine($"(xiii)Summation of Div.  = {sumDivisions:F4}");

        // Part 2: Outlier Detection using IQR method
        Console.WriteLine("\n\t\t\t\t====== Part 2: Outlier Detection (IQR Method) ======\n");
        double lowerBound = q1 - 1.5 * iqr;
        double upperBound = q3 + 1.5 * iqr;
        Console.WriteLine($"Lower Bound = Q1 - 1.5*IQR = {lowerBound:F4}");
        Console.WriteLine($"Upper Bound = Q3 + 1.5*IQR = {upperBound:F4}\n");

        foreach (int val in data)
        {
            bool isOutlier = val < lowerBound || val > upperBound;
            Console.WriteLine($"  {val,5} -> {(isOutlier ? "OUTLIER " : "Normal")}");
        }
    }

    // Interpolation-based percentile (same method used in statistics textbooks)
    static double Percentile(double[] sorted, double p)
    {
        int n = sorted.Length;
        double L = (p / 100.0) * n;
        int lower = (int)Math.Floor(L);
        int upper = lower + 1;

        if (lower <= 0) return sorted[0];
        if (upper >= n) return sorted[n - 1];

        return sorted[lower - 1] + (L - lower) * (sorted[lower] - sorted[lower - 1]);
    }
}