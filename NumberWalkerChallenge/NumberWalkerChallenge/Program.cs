using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace NumberWalkerChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set file path to text file
            string filePath = @"";

            // If file path is unspecified (set as empty string), prompt during runtime
            if (filePath == string.Empty)
            {
                Console.WriteLine("Please enter target filepath for txt file:");
                Console.Write(">");
                filePath = Console.ReadLine();                
            }

            // Trim any quotes that shouldn't exist in filepath name
            filePath = filePath.Replace("\"", string.Empty);

            Console.WriteLine("Target txt filepath is \"" + filePath + "\".");

            // Confirm file exists before reading
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: filepath does not exist");
                Console.WriteLine("Press any key to close");
                Console.ReadKey();
                return;                
            }

            // Read file and populate dictionary
            // End result is a dictionary where each key is a number, and the value of the key is the # of occurences.
            Dictionary<int, int> tracker = new Dictionary<int, int>();
            StreamReader fileStream = new StreamReader(filePath);
            string currentLine;
            int currentNumber;
            
            while ((currentLine = fileStream.ReadLine()) != null)
            {
                // Check that current line is a number. Skip line if not numeric.
                if (Int32.TryParse(currentLine, out currentNumber))
                {
                    // Get current number and add it to dictionary
                    if (!tracker.ContainsKey(currentNumber))
                    {
                        tracker[currentNumber] = 1;
                    }
                    else
                    {
                        tracker[currentNumber]++;
                    }
                } else
                {
                    // Output error message if line is not numeric
                    Console.WriteLine("Error: A non-integer value \"" + currentLine + 
                        "\" was found in the text file. This line has been skipped.");
                }                
                
            }

            // Initialize tracking variables for least occuring number and it's occurences
            int frequencyNumber = 0;
            int frequencyAmount = 0;

            // Iterate through dictionary to determine which key occured the least amount of times
            foreach (int key in tracker.Keys)
            {
                // Start by assuming that the first number is the least occuring
                if (frequencyAmount == 0)
                {
                    frequencyNumber = key;
                    frequencyAmount = tracker[key];
                }

                // If the current number:
                // a) occurs less than the tracked number
                // b) occurs the same amount as the tracked number and it's value is lower
                // then set this number to be the current least occuring number
                if (tracker[key] < frequencyAmount)
                {
                    frequencyNumber = key;
                    frequencyAmount = tracker[key];
                } else if ((tracker[key] == frequencyAmount) && (key < frequencyNumber))
                {
                    frequencyNumber = key;
                }
                
            }

            // Output results
            Console.WriteLine("The lowest least occuring number is " + frequencyNumber.ToString() + 
                " which occured " + frequencyAmount.ToString() + " time(s).");

            // Close application on key press
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }        
    }
}
