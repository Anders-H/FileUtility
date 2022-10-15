# FileUtility
A library for displaying file names and retrieving directory content, implemented in a PathInfo class.

.NET 6.0, Windows only. Can only be used if target framework is set to `.net6.0-windows`.

```
<TargetFramework>net6.0-windows</TargetFramework>
```

Install:

```
Install-Package WinSoftFileUtility
```

Shorten a path:

```
var max = 15;
var f = new PathInfo(new FileInfo(@"C:\Temp\hello.txt"));
Console.WriteLine(f.CompactPathForDisplay(15))

// Result: C:...\hello.txt
```

Retrieve directory contents in two levels:


```
var l = new PathInfo(new DirectoryInfo(@"C:\")).GetDirectoryContent(2);
Console.WriteLine(l.Count);
foreach (var p in l)
    Console.WriteLine(p);
```

Retrieve directory contents in three levels, if file ending is `.ico`:

```
bool Guard(PathInfo p) =>
    p.ContainsDirectory || (p.ContainsFile && p.FullName.EndsWith(".ico", StringComparison.CurrentCultureIgnoreCase));

var l = new PathInfo(new DirectoryInfo(@"C:\")).GetDirectoryContent(3, Guard);
Console.WriteLine(l.Count);
foreach (var p in l)
    Console.WriteLine(p);
```
