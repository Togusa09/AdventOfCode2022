namespace AdventOfCode2022.Task7;

public class Task7B : ITask<IEnumerable<string>, int>
{
    private int TotalDiskSize = 70000000;
    private int RequiredFreeSize = 30000000;

    class AdventDirectory
    {
        


        public AdventDirectory(string name, AdventDirectory? parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; }
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


                    } while (pointer < val.Length - 1 && val[pointer + 1].FirstOrDefault() != '$');

                    break;
            }
        }

        var totalUsedSpace = rootDirectory.Size;
        var availableFreeSpace = TotalDiskSize - totalUsedSpace;
        var spaceRequiredToFree = RequiredFreeSize - availableFreeSpace;
        var test = directories.Where(x => x.Size >= spaceRequiredToFree).OrderBy(x => x.Size);
        var directory = directories.Where(x => x.Size >= spaceRequiredToFree).OrderBy(x => x.Size).First();

        return directory.Size;
    }
}