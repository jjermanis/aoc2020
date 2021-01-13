using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC2020
{
    public class Day07 : DayBase, IDay
    {
        private const string SHINY_GOLD = "shiny gold";

        private readonly IEnumerable<string> _data;
        private readonly IDictionary<string, Bag> _bags;
        private IDictionary<string, bool> _memo;

        public Day07(string filename)
        {
            _data = TextFileLines(filename);
            _bags = InitBags();
        }

        public Day07() : this("Day07.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"CanContainShinyGoldBagCount: {CanContainShinyGoldBagCount()}");
            Console.WriteLine($"BagsInsideShinyGoldBagCount: {BagsInsideShinyGoldBagCount()}");
        }

        public int CanContainShinyGoldBagCount()
        {
            _memo = new Dictionary<string, bool>();

            var result = 0;
            foreach (var bag in _bags.Keys)
            {
                // Skip shiny gold itself - these are bags that contain shiny gold
                if (bag.Equals(SHINY_GOLD))
                    continue;

                if (HasGoldBag(bag))
                    result++;
            }
            return result;
        }

        public int BagsInsideShinyGoldBagCount() => 
            InnerBagCount(SHINY_GOLD);

        public int InnerBagCount(string name)
        {
            var result = 0;

            var bag = _bags[name];
            foreach (var child in bag.Bags)
            {
                // Counted the contained bags...
                result += child.Value * InnerBagCount(child.Key);
                // And the bags themselves
                result += child.Value;
            }
            return result;
        }

        private bool HasGoldBag(string bagName)
        {
            if (_memo.ContainsKey(bagName))
                return _memo[bagName];

            if (bagName.Equals(SHINY_GOLD))
            {
                _memo[SHINY_GOLD] = true;
                return true;
            }

            var bag = _bags[bagName];
            foreach (var child in bag.Bags.Keys)
            {
                if (HasGoldBag(child))
                {
                    _memo[child] = true;
                    return true;
                }
            }

            _memo[bagName] = false;
            return false;
        }

        private IDictionary<string, Bag> InitBags()
        {
            var result = new Dictionary<string, Bag>();
            var startRegex = new Regex(@"(^[a-z\s]+) bags contain (.*)$");
            var bagRegex = new Regex(@"(^[0-9]+) ([a-z\s]+) bag[s]*[,.](.*)$");

            foreach (var line in _data)
            {
                var startResult = startRegex.Matches(line);
                var color = startResult[0].Groups[1].Value;
                var bags = startResult[0].Groups[2].Value;

                var currBag = new Bag() 
                { 
                    Color = color,
                    Bags = new Dictionary<string, int>()
                };

                while (true)
                {
                    var nextResults = bagRegex.Matches(bags);
                    if (nextResults.Count == 0)
                        break;
                    var count = int.Parse(nextResults[0].Groups[1].Value);
                    var bagColor = nextResults[0].Groups[2].Value;
                    bags = nextResults[0].Groups[3].Value.Trim();

                    currBag.Bags.Add(bagColor, count);
                }

                result.Add(color, currBag);
            }
            return result;
        }
    }

    class Bag
    {
        public string Color { get; set; }
        public IDictionary<string, int> Bags { get; set; }
    }
}
