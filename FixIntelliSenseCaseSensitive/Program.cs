using System.Diagnostics;

DirectoryInfo currentDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

//parent
DirectoryInfo parent = currentDirectoryInfo;
do
{
    parent = parent.Parent;
    if (parent != null) Fix(parent.FullName);
}
while (parent != null);

//current
Fix(currentDirectoryInfo.FullName);

//childs
foreach (var item in currentDirectoryInfo.GetDirectories())
{
    FixChild(item);
}

Console.WriteLine("Done!");
Console.ReadLine();



void FixChild(DirectoryInfo directoryInfo)
{
    Fix(directoryInfo.FullName);
    try
    {
        foreach (var item in directoryInfo.GetDirectories())
        {
            FixChild(item);
        }
    }
    catch { }
}

void Fix(string path)
{
    try
    {
        using Process process = Process.Start("fsutil", $"file setCaseSensitiveInfo \"{path}\" disable");
    }
    catch { }
}