using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTests.Data
{
    public class CalculatorCsvData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines(path: @"Data\\TestData.csv");
                var testCases = new List<object[]>();
                foreach (var item in csvLines)
                {
                    IEnumerable<int> values = item.Split(",").Select(p => int.Parse(p));
                    object[] testCase = values.Cast<object>().ToArray();
                    testCases.Add(testCase);
                }
                return testCases;
            }
        }
    }
}
