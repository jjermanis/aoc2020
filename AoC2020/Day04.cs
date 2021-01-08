using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day04 : DayBase, IDay
    {
        private readonly IEnumerable<string> _data;

        public Day04(string filename)
        {
            _data = TextFileLines(filename);
        }

        public Day04() : this("Day04.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"SimpleValidCount: {SimpleValidCount()}");
            Console.WriteLine($"ComplexVaidCount: {ComplexVaidCount()}");
        }

        public int SimpleValidCount() => ValidCount(new SimpleValidator());
        public int ComplexVaidCount() => ValidCount(new Part2Validator());

        private int ValidCount(IValidator validator)
        {
            var lines = _data.GetEnumerator();
            var result = 0;
            bool isDone = false;

            while (!isDone)
            {
                Dictionary<string, string> file;
                (file, isDone) = NextFile(lines);
                if (validator.IsValid(file))
                    result++;
            }
            return result;
        }

        private (Dictionary<string, string>, bool) NextFile(IEnumerator<string> elements)
        {
            var fields = new Dictionary<string, string>();

            while (elements.MoveNext())
            {
                if (string.IsNullOrWhiteSpace(elements.Current))
                    return (fields, false);
                UpdateFields(elements.Current, fields);
            }
            return (fields, true);
        }

        private void UpdateFields(string line, Dictionary<string, string> fields)
        {
            var pairs = line.Split(' ');
            foreach (var pair in pairs)
            {
                var kvp = pair.Split(':');
                fields.Add(kvp[0], kvp[1]);
            }
            return;
        }
    }

    interface IValidator
    {
        bool IsValid(Dictionary<string, string> file);
    }

    class SimpleValidator : IValidator
    {
        private readonly List<string> REQUIRED_KEYS = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        public virtual bool IsValid(Dictionary<string, string> file)
        {
            foreach (var key in REQUIRED_KEYS)
                if (!file.ContainsKey(key))
                    return false;
            return true;
        }
    }

    class Part2Validator : SimpleValidator, IValidator
    {
        private readonly HashSet<string> EYE_COLORS = new HashSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public override bool IsValid(Dictionary<string, string> file)
        {
            if (!base.IsValid(file))
                return false;

            return ValidateYear(file["byr"], 1920, 2002) && ValidateYear(file["iyr"], 2010, 2020) && ValidateYear(file["eyr"], 2020, 2030) &&
                ValidateHeight(file["hgt"]) && ValidateHair(file["hcl"]) && ValidateEye(file["ecl"]) && ValidatePid(file["pid"]);
        }

        private bool ValidateYear(string year, int min, int max)
        {
            if (!int.TryParse(year, out int value))
                return false;
            return value >= min && value <= max;
        }

        private bool ValidateHeight(string height)
        {
            if (height.Length < 4)
                return false;
            var value = int.Parse(height[..^2]);
            var units = height[^2..];
            return units switch
            {
                "in" => value >= 59 && value <= 76,
                "cm" => value >= 150 && value <= 193,
                _ => false
            };
        }

        private bool ValidateHair(string color)
        {
            if (color.Length != 7)
                return false;
            if (color[0] != '#')
                return false;
            foreach (var c in color[^6..])
                if (!((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f')))
                    return false;
            return true;
        }

        private bool ValidateEye(string color)
        {
            return EYE_COLORS.Contains(color);
        }

        private bool ValidatePid(string pid)
        {
            if (pid.Length != 9)
                return false;
            foreach (var c in pid)
                if (!(c >= '0' && c <= '9'))
                    return false;
            return true;
        }
    }
}
