using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        private static readonly List<string> _defaultDelimiters = new()
        {
            ",",
            "\n"
        };

        /// <exception cref="ArgumentException">This exception is thrown if negatives numbers was passed.</exception>
        public static int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            List<int> result;
            if (HasDelimiters(numbers))
            {
                result = GetSpecificNumbers(numbers, GetDelimiters(numbers));
            }
            else
            {
                result = GetBasicNumbers(numbers, _defaultDelimiters);
            }

            var negativeNumbers = result.Where(x => x < 0);
            if (negativeNumbers.Any())
                throw new ArgumentException($"negatives not allowed {string.Join(' ', negativeNumbers)}");

            return result.Sum();
        }

        private static List<int> GetBasicNumbers(string numbers, List<string> delimiters)
        {
            return numbers.Split(delimiters.ToArray(), StringSplitOptions.None)
                .Select(x => int.Parse(x))
                .Where(x => x <= 1000)
                .ToList();
        }

        private static List<int> GetSpecificNumbers(string numbers, List<string> delimiters)
        {
            var textStartsIndex = numbers.IndexOf('\n');

            return numbers.Substring(textStartsIndex + 1, numbers.Length - textStartsIndex - 1)
                .Split(delimiters.ToArray(), StringSplitOptions.None)
                .Select(x => int.Parse(x))
                .Where(x => x <= 1000)
                .ToList();
        }

        private static List<string> GetDelimiters(string numbers)
        {
            var textStartsIndex = numbers.IndexOf('\n');
            var textOfDel = numbers.Substring(2, textStartsIndex - 1);

            return textOfDel
                .Replace("[", "")
                .Split(']')
                .ToList();
        }

        private static bool HasDelimiters(string numbers)
        {
            return numbers.StartsWith("//");
        }
    }
}