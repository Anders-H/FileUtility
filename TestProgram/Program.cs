using FileUtility;

var p = new PathInfo(new FileInfo(@"C:\Program Files (x86)\CBM prg Studio\CBMPrgStudio.exe"));

Console.WriteLine(p.SizeBytes);

Console.WriteLine(p.SizeKiloBytes);

Console.WriteLine(p.SizeMegaBytes);

Console.WriteLine(p.SizeGigaBytes);

Console.WriteLine(p.SizeAsString);

p = new PathInfo(new DirectoryInfo(@"C:\Program Files (x86)\CBM prg Studio\"));

Console.WriteLine(p.SizeBytes);

Console.WriteLine(p.SizeKiloBytes);

Console.WriteLine(p.SizeMegaBytes);

Console.WriteLine(p.SizeGigaBytes);

Console.WriteLine(p.SizeAsString);