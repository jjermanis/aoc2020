using System.Collections.Generic;
using System.IO;

namespace AoC2020
{
    public abstract class DayBase
    {
        private const string FILE_PATH = @"..\..\..\..\AoC2020\Inputs\";

        protected IEnumerable<string> TextFileLines(string fileName)
            => File.ReadLines(FILE_PATH + fileName);

        protected string TextFile(string fileName)
            => File.ReadAllText(FILE_PATH + fileName);
    }
}
