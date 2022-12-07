using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Task7
{
    public class Task7A : ITask<IEnumerable<string>, int>
    {
        /*
         * $ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
         */

        
        class AdventDirectory
        {
            public AdventDirectory(string name, AdventDirectory? parent)
            {
                Name = name;
                Parent = parent;
            }

            public string Name { get;  }
            public AdventDirectory? Parent { get; } = null;
            
            public List<AdventFile> Files = new List<AdventFile>();
            public List<AdventDirectory> Directories = new List<AdventDirectory>();

            public int Size => Files.Sum(x => x.size) + Directories.Sum(x => x.Size);
        }

        record AdventFile(String name, int size);


        public int Solve(IEnumerable<string> data)
        {
            AdventDirectory rootDirectory = new AdventDirectory("/", default);
            var currentDirectory = rootDirectory;
            var directories = new List<AdventDirectory>()
            {
                rootDirectory
            };
            var val = data.ToArray();

            for (var pointer = 0; pointer < val.Length; pointer++)
            {
                if (string.IsNullOrWhiteSpace(val[pointer])) continue;
                
                var command = val[pointer].Substring(2, 2);
                switch (command)
                {
                    case "cd":
                        var directoryName = val[pointer].Substring(5);
                        if (directoryName == ".." && currentDirectory.Parent != null)
                        {
                            currentDirectory = currentDirectory.Parent;
                        }
                        else
                        {
                            if (directoryName != "/")
                            {
                                currentDirectory = currentDirectory.Directories.First(x => x.Name == directoryName);
                            }
                        }

                        break;
                    case "ls":
                        do
                        {
                            pointer++;

                            if (string.IsNullOrWhiteSpace(val[pointer])) break;

                            if (val[pointer].Substring(0, 3) == "dir")
                            {
                                var newDirectory = new AdventDirectory(val[pointer].Substring(4), currentDirectory);
                                directories.Add(newDirectory);
                                currentDirectory.Directories.Add(newDirectory);
                            }
                            else
                            {
                                var consoleVals = val[pointer].Split();
                                var size = int.Parse(consoleVals[0]);
                                var file = new AdventFile(consoleVals[1], size);
                                currentDirectory.Files.Add(file);
                            }


                        } while (pointer < val.Length -1 && val[pointer + 1].FirstOrDefault() != '$');

                        break;
                }
            }

            //var test = directories.ToDictionary(x => x.Name, x => x.Size);

            //var total = rootDirectory.Directories.Where(x => x.Size <= 100000).Sum(x => x.Size);

            var total = directories.Where(x => x.Size <= 100000).Sum(x => x.Size);

            return total;
        }
    }
}
