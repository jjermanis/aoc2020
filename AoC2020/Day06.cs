using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day06 : DayBase, IDay
    {
        private readonly IEnumerable<string> _data;

        public Day06(string filename)
        {
            _data = TextFileLines(filename);
        }

        public Day06() : this("Day06.txt")
        {
        }

        public void Do()
        {
            Console.WriteLine($"QuestionsSumAnyone: {QuestionsSumAnyone()}");
            Console.WriteLine($"QuestionsSumEveryone: {QuestionsSumEveryone()}");
        }

        public int QuestionsSumAnyone() => QuestionsSum(new QuestionsAnyoneAnswered());


        public int QuestionsSumEveryone() => QuestionsSum(new QuestionsEveryoneAnswered());

        private int QuestionsSum(IQuestionProcessor processor)
        {
            var lines = _data.GetEnumerator();
            var result = 0;
            bool isDone = false;

            // Accumulate the question sum for each group
            while (!isDone)
            {
                HashSet<char> questions;
                (questions, isDone) = processor.GetQuestions(lines);
                result += questions.Count;
            }
            return result;
        }
    }

    public interface IQuestionProcessor
    {
        (HashSet<char>, bool) GetQuestions(IEnumerator<string> elements);
    }

    class QuestionsAnyoneAnswered : IQuestionProcessor
    {
        public (HashSet<char>, bool) GetQuestions(IEnumerator<string> elements)
        {
            var questions = new HashSet<char>();

            // Add any question that appears in any line in the group
            while (elements.MoveNext())
            {
                if (string.IsNullOrWhiteSpace(elements.Current))
                    return (questions, false);
                foreach (var c in elements.Current)
                    questions.Add(c);
            }
            return (questions, true);
        }
    }

    class QuestionsEveryoneAnswered : IQuestionProcessor
    {
        public (HashSet<char>, bool) GetQuestions(IEnumerator<string> elements)
        {
            var questions = new HashSet<char>();

            // Start with the questions answered in the first line
            elements.MoveNext();
            foreach (var c in elements.Current)
            {
                questions.Add(c);
            }

            while (elements.MoveNext())
            {
                if (string.IsNullOrWhiteSpace(elements.Current))
                    return (questions, false);

                // From the original set of questions, remove any question not answered in the current line
                var pendingRemovals = new List<char>();
                foreach (var q in questions)
                {
                    if (!elements.Current.Contains(q))
                        pendingRemovals.Add(q);
                }
                foreach (var c in pendingRemovals)
                    questions.Remove(c);
            }
            return (questions, true);
        }
    }

}
