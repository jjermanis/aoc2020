using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Day01 : DayBase, IDay
    {
        private readonly IEnumerable<int> _expenses;

        public Day01(string filename)
        {
            _expenses = TextFileLines(filename).Select(m => int.Parse(m));
        }

        public Day01() : this("Day01.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"ExpensePairProduct: {ExpensePairProduct()}");
            Console.WriteLine($"ExpenseTripleProduct: {ExpenseTripleProduct()}");
        }

        public int ExpensePairProduct()
        {
            var expenseMap = _expenses.ToHashSet();

            foreach (var expense in _expenses)
            {
                var pair = 2020 - expense;
                if (expenseMap.Contains(pair))
                    return expense * pair;
            }

            throw new Exception("No paired entries found!");
        }

        public int ExpenseTripleProduct()
        {
            var expenses = _expenses.ToList();
            for (var a = 0; a < expenses.Count; a++)
                for (var b = a + 1; b < expenses.Count; b++)
                    for (var c = b + 1; c < expenses.Count; c++)
                        if (expenses[a] + expenses[b] + expenses[c] == 2020)
                            return expenses[a] * expenses[b] * expenses[c];

            throw new Exception("No tripled entries found!");
        }
    }
}
