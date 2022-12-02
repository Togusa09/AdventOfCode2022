// See https://aka.ms/new-console-template for more information

using AdventOfCode2022.Day1;

var testData =
    "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000";

var task = new Task1B();
var result = task.Solve(testData.Split('\n'));


var file = File.ReadLines("Data/Task1.txt");
result = new Task1B().Solve(file);

Console.WriteLine(result);
