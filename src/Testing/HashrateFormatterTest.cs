using System;

namespace spectrespy.Testing
{
    public class HashrateFormatterTest
    {
        public static void TestHashrateFormatting()
        {
            Console.WriteLine("Testing Hashrate Formatting:");
            Console.WriteLine("============================");
            
            // Test different hashrate values
            var testValues = new double[]
            {
                2.9488591636968298e-05,  // Current API value
                0.0004161540937688964,   // Max API value
                12000132,                // Documentation example
                1e12,                    // 1 TH/s
                5.6e9,                   // 5.6 GH/s
                750e6,                   // 750 MH/s
                50e3,                    // 50 KH/s
                123.45,                  // 123.45 H/s
                0.5,                     // 0.5 H/s
                0.001234,                // 1.234 mH/s
                0.000005678,             // 5.678 μH/s
                1e-9                     // Very small value
            };

            foreach (var value in testValues)
            {
                string formatted = FormatHashrate(value);
                Console.WriteLine($"Raw: {value:E6} => Formatted: {formatted}");
            }
        }

        private static string FormatHashrate(double hr)
        {
            if (hr >= 1e12) return $"{hr / 1e12:F2} TH/s";
            if (hr >= 1e9) return $"{hr / 1e9:F2} GH/s";
            if (hr >= 1e6) return $"{hr / 1e6:F2} MH/s";
            if (hr >= 1e3) return $"{hr / 1e3:F2} KH/s";
            if (hr >= 1) return $"{hr:F2} H/s";
            if (hr >= 1e-3) return $"{hr * 1e3:F2} mH/s";
            if (hr >= 1e-6) return $"{hr * 1e6:F2} μH/s";
            return $"{hr:E2} H/s";
        }
    }
}
