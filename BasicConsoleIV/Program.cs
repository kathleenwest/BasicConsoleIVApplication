using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleIV
{
    class Program
    {
        /// <summary>
        /// Main entry for the program of BasicConsoleIV
        /// Kathleen West
        /// </summary>
        /// <param name="args">No arguments are processed</param>
        static void Main(string[] args)
        {
            Statistics();       // Run Lab Part 1 - Statistics
            Coordinates();      // Run Lab Part 2 - Rectangular to Polar
            SumOfSquares();     // Run Lab Part 3 - Sum of Squares

            // Await user entry before exiting console
            Console.ReadLine();
        }

        #region Part:1 Statistics

        /// <summary>
        /// Performs statistical calculations on a set of integer data.
        /// Outputs the Mean, Median, and Mode statistics of the data.
        /// </summary>
        static void Statistics()
        {
            // Data Array value of numbers
            int[] values = { 1, 6, 4, 7, 9, 2, 5, 7, 2, 6, 5, 7, 8, 1, 2, 8 };
            double mean = 0.0;          // Value for Mean
            double median = 0.0;        // Value for Median
            List<int> result = null;    // List for Mode Values

            // Determine the Mean, Median, and Mode Statistics 
            mean = Mean(values);
            median = Median(values);
            result = Mode(values);

            // Output Results to the Console Window
            Console.WriteLine($"Data: {string.Join(", ", values)}");
            Console.WriteLine($"Mean: {mean}");
            Console.WriteLine($"Median: {median}");
            Console.WriteLine($"Mode: {string.Join(", ", result)}");

        } // end of method Statistics

        /// <summary>
        /// Calculates and returns the average of the values stored in the values array parameter. 
        /// The average is the sum of a set of values divided by the number of items in the set.
        /// </summary>
        /// <param name="values">accepts an array of integers</param>
        /// <returns>Mean of the values as double. 
        /// In the event a null value is passed to this method or the array is empty then return double.NaN</returns>
        static double Mean(int[] values)
        {
            int sum = 0;    // sum of the array of values
            int count = 0;  // total count of items in the array

            // If Array is Null or Empty Return NaN
            if ((values == null) || (values.Length == 0))
            {
                return double.NaN;
            }

            // Calculatue Mean
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
                count++;
            }

            // Return the Results
            return (double)sum/count;

        } // end of the method Mean

        /// <summary>
        /// This method accepts an array of integers and returns the median of the values.
        /// The median of a set of values is the center-most value when the set is sorted. 
        /// If the set has an odd number of items then the result is item at the middle 
        /// index of the array. If the set has an even number of elements then the result 
        /// is the average (mean) of the two center-most items.
        /// </summary>
        /// <param name="values">array of integers</param>
        /// <returns>median of the values as double</returns>
        static double Median(int[] values)
        {
            int[] sorted = null;    // Number array for sorted values

            // If Array is Null or Empty return NaN
            if ((values == null) || (values.Length == 0))
            {
                return double.NaN;
            }

            // Create new array, Copy Input Values, Sort
            sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            Array.Sort(sorted);

            // Determine and Return Median
            if (sorted.Length % 2 == 0) // Even Numbered Array
            {
                int position = (sorted.Length / 2);
                int sum = sorted[position-1] + sorted[position];
                return (double)sum / 2;
            } // end of if
            else // The array is odd numbered
            {
                int position = (sorted.Length / 2);
                return sorted[position];
            } // end of else

        } // end of Median()

        /// <summary>
        /// This method accepts an array of integers and returns the mode of the values.
        /// The mode of a set of numbers is value that appears with the highest frequency 
        /// in that set. In the event more than one value appears the same number of times 
        /// then all of those values represent the mode.
        /// </summary>
        /// <param name="values">array of integers</param>
        /// <returns>List of integers representing the Mode(s)</returns>
        static List<int> Mode(int[] values)
        {
            List<int> result = new List<int>();                         // List to return Mode(s) 
            Dictionary<int, int> counts = new Dictionary<int, int>();   // Key, Value List for Numbers and Frequency
            int[] sorted = null;                                        // Sorted array for input of numbers
            int max = 0;                                                // Highest Frequency Count for Key,Values

            // If Array is Empty or Null Return Empty List
            if ((values == null) || (values.Length == 0))
            {
                return result;
            }

            // Create New Array, Copy, and Sort the input numbers
            sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            Array.Sort(sorted);
           
            // Intialize the Dictionary with Key Values from Sorted Array
            foreach (int item in sorted)
            {
                if (!(counts.ContainsKey(item))) // Add only if number is not already in the list of keys
                {
                    counts.Add(item, 0);
                }            
            } // end of foreach

            // Populate the Frequency of Key Values in the Dictionary
            foreach (int key in sorted)
            {                
                if (counts.ContainsKey(key)) // If the key is found, increment the frequency
                {
                    counts[key]++;
                }
            } // end of foreach

            // Determine Maximum of the Dictionary Values
            max = counts.Values.ToArray().Max();

            // Match the Key, Value Pair Against the Maximum
            // If there is a match, add to the result list.
            foreach (var key in counts.Keys)
            {
                if (counts[key] == max)
                {
                    result.Add(key);
                }
            }

            // Return the result list of Mode(s)
            return result;

        } // end of Mode method

        #endregion Part1

        #region Part:2 Rectangular to Polar

        /// <summary>
        /// This method prompts the user to enter a rectangular coordinate
        /// and after valid parsing, calculates the polar coordinate, 
        /// equivalent degrees, and displays the results to the console.
        /// </summary>
        static void Coordinates()
        {
            string input;       // String for user data entry
            double x, y;        // Rectangular coordinates for parsed data entry of values

            // Prompt User for Rectangular Coordinates
            Console.Write("Enter a coordinate in the form (x, y): ");
            input = Console.ReadLine();
            
            // If Data Was Parsed and Valid, Do Math 
            if (TryParsePoint(input, out x, out y))
            {
                // Determine Polar Coordinates Conversion as Tuple
                Tuple<double, double> polar = RectangularToPolar(x, y);

                // Output the Polar Coordinates to the Console
                Console.WriteLine($"r: {polar.Item1}, angle: {polar.Item2} radians");

                // Output the Polar Coordinate and Degree Conversion to Console
                Console.WriteLine($"r: {polar.Item1}, angle: {RadiansToDegrees(polar.Item2)}°");
            } // end of if

        } // end of method

        /// <summary>
        /// This method accepts an input string string and attempts to parse. 
        /// If successful, stores the parsed values into outputs arguments x and y.
        /// </summary>
        /// <param name="input">Cartesian coordinate where a point on a plane is represented 
        /// by two ordinates, x and y in the format (x, y)</param>
        /// <param name="x">Parsed x rectangular coordinate</param>
        /// <param name="y">Parsed y rectangular coordinate</param>
        /// <returns>Boolean: True if successful validation, false otherwise</returns>
        static bool TryParsePoint(string input, out double x, out double y)
        {
            x = 0.0;    // Parsed x rectangular coordinate
            y = 0.0;    // Parsed y rectangular coordinate
            
            // Check if Input is In Format (x,y)
            if(!input.Trim().StartsWith("("))
            {
                return false;
            }
            if (!input.Trim().EndsWith(")"))
            {
                return false;
            }
            if (!input.Trim().Contains(","))
            {
                return false;
            }

            // Remove ( and ) from the string
            input = input.TrimStart('(');
            input = input.TrimEnd(')');

            // Split the input
            string[] splitInput = input.Split(',');

            // If the array does not contain 2 items, return false
            if (splitInput.Length != 2)
            {
                return false;
            }

            // Attempt to Parse the First Element
            if (!double.TryParse(splitInput[0],out x))
            {
                return false;
            }

            // Attempt to Parse the Second Element
            if (!double.TryParse(splitInput[1], out y))
            {
                return false;
            }

            // Everything was Successful and Valid
            return true;

        } // End of Method TryParse

        /// <summary>
        /// This method accepts a pair of doubles, x and y, which represents a Cartesian 
        /// coordinate and calculates the polar equivalent.
        /// </summary>
        /// <param name="x">x coordinate (double)</param>
        /// <param name="y">y coordinate (double)</param>
        /// <returns>Tuple<double, double> data structure for polar coordinate</returns>
        static Tuple<double, double> RectangularToPolar(double x, double y)
        {
            Tuple<double, double> t;    // Represents data structure for polar coordinate

            //1.Point is on origin: r = 0, angle = 0
            if ((x == 0.0) && (y == 0.0))
            {
                t = new Tuple<double, double>(0, 0);
            }

            //2.Point is on positive X axis: r = x, angle = 0
            else if ((x > 0.0) && (y == 0.0))
            {
                t = new Tuple<double, double>(x, 0);
            }

            //3.Point is on positive Y axis: r = y, angle = π/2
            else if ((y > 0.0) && (x == 0.0))
            {
                t = new Tuple<double, double>(y, (1/2)*Math.PI);
            }

            //4.Point is on negative X axis: r = | x |, angle = π
            else if ((x < 0.0) && (y == 0.0))
            {
                t = new Tuple<double, double>(Math.Abs(x), Math.PI);
            }

            //5.Point is on negative Y axis: r = | y |, angle = 3π/2
            else if ((y < 0.0) && (x == 0.0))
            {
                t = new Tuple<double, double>(Math.Abs(y), (3/2)*Math.PI);
            }

            //6.Point is in Quadrant I: r = √(𝑥2 +𝑦2), angle = 𝑡𝑎𝑛−1(𝑦/x)
            else if ((x > 0.0) && (y > 0.0))
            {
                t = new Tuple<double, double>(Math.Sqrt(x * x + y * y), Math.Atan(y/x));
            }

            //7.Point is in Quadrant II: r = √(𝑥2 +𝑦2), angle = π + 𝑡𝑎𝑛−1(𝑦/𝑥)
            else if ((x < 0.0) && (y > 0.0))
            {
                t = new Tuple<double, double>(Math.Sqrt(x * x + y * y), Math.PI + Math.Atan(y / x));
            }

            //8.Point is in Quadrant III: r = √(𝑥2 +𝑦2), angle = π + 𝑡𝑎𝑛−1(𝑦/𝑥)
            else if ((x < 0.0) && (y < 0.0))
            {
                t = new Tuple<double, double>(Math.Sqrt(x * x + y * y), Math.PI + Math.Atan(y / x));
            }

            //9.Point is in Quadrant IV: r = √(𝑥2 +𝑦2), angle = 2π + 𝑡𝑎𝑛−1(𝑦/𝑥)
            else if ((x > 0.0) && (y < 0.0))
            {
                t = new Tuple<double, double>(Math.Sqrt(x * x + y * y), 2*Math.PI + Math.Atan(y / x));
            }

            // Default
            else
            {
                t = new Tuple<double, double>(0, 0);
            }

            // Return the Tuple
            return t;

        } // end of method Tuples

        /// <summary>
        /// This method accepts a value representing the angle of a circle in radians between 
        /// 0 and 2π (roughly 6.28319) and returns the equivalent value in degrees 
        /// (0 ≤ angle < 360)
        /// </summary>
        /// <param name="radians">value (double) representing the angle of a circle in radians 
        /// between 0 and 2π (roughly 6.28319)</param>
        /// <returns>equivalent value (double) in degrees (0 ≤ angle < 360)</returns>
        static double RadiansToDegrees(double radians)
        {
            // Convert the Radians to Degrees
            double degrees = radians * (180 / Math.PI);

            // Make the Degrees within 360 Range
            while(degrees > 360.0)
            {
                degrees -= 360.0;
            }

            // Return the Degree Value
            return degrees;

        } // end of method RadiansToDegrees

        #endregion Part 2 - Rectangular to Polar

        #region Part:3 Sum of Squares

        /// <summary>
        /// This method asks for the user for a positive integer, validates
        /// the data entry input, and calls a method that uses recursion 
        /// to calculate the sum of squares up to and including a given value
        /// </summary>
        static void SumOfSquares()
        {
            bool ok = false;        // Sentenial for while loop
            string input;           // String for user input
            int result = 0;         // Validated Result for User entry

            while (!ok)
            {
                result = 0;
                Console.Write("Enter a positive integer: ");
                input = Console.ReadLine();
                ok = int.TryParse(input, out result);
                if (result < 1)
                {
                    ok = false;
                }
            } // end of while

            // Calculate and Output the Recursive Sum of Squares
            Console.WriteLine($"The sum of squares for {result} is {Squares(result)}");

        } // end of Sum of Squares

        /// <summary>
        /// This method will use recursion to calculate the sum of all squares from 1 to the given value. 
        /// Since the value provided by the user represents the upper value for the range, each recursive 
        /// call is responsible for calculating the square of input value and adding that result to the 
        /// square of value - 1 . However, if the input to this method 
        /// is 1 then this method should return 1 without making additional recursive calls.
        /// </summary>
        /// <param name="value">Value to be Recursively Squared</param>
        /// <returns>Recursive sum of squared values</returns>
        static long Squares(long value)
        {
            if (value == 1) // end recursion on this condition
            {
                return 1;
            }

            return value*value + Squares(value-1);

        } // end of Squares method

        #endregion Part:3 Sum of Squares

    } // end of class program
} // end of namespace
