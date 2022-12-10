// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using AdventOfCode2022;
using AdventOfCode2022.Day1;
using AdventOfCode2022.Task1;
using AdventOfCode2022.Task2;
using AdventOfCode2022.Task3;
using AdventOfCode2022.Task4;
using AdventOfCode2022.Task5;
using AdventOfCode2022.Task6;




namespace AdventOfCode2022.Console // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static void RunTask(Type type)
        {
            var fileName = "Data/" + type.Name.Substring(0, type.Name.Length-1) + ".txt";

            if (!File.Exists(fileName)) return;

            var file = File.ReadLines(fileName);

            var arguments = type.GetInterfaces().First().GetGenericArguments();
            
            Object data = null;
            if (arguments[0] == typeof(string))
            {
                data = file.First();
            }
            else if(arguments[0] == typeof(string[]) || arguments[0] == typeof(IEnumerable<string>))
            {
                data = file;
            }

            var task = Activator.CreateInstance(type);
            var method = type.GetMethod("Solve");
            var result = method.Invoke(task, new[] { data });
            
            System.Console.WriteLine("{0}:\t{1}", type.Name, result);
        }

        static void Main(string[] args)
        {
            var tasks = typeof(Task1A).Assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsInterface && x.Name.Contains("Task"));

            foreach (var task in tasks)
            {
                RunTask(task);
            }

            System.Console.ReadLine();
        }
    }
}