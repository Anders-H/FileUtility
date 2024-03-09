using FileUtility;

var p = new PathInfo(new FileInfo(@"C:\Program Files (x86)\CBM prg Studio x86\CBMPrgStudio.exe"));
Console.WriteLine(p.SizeBytes.ToString("000 000 000.000") + " bytes");
Console.WriteLine(p.SizeKiloBytes.ToString("000 000 000.000") + " Kb");
Console.WriteLine(p.SizeMegaBytes.ToString("000 000 000.000") + " Mb");
Console.WriteLine(p.SizeGigaBytes.ToString("000 000 000.000") + " Gb");
Console.WriteLine(p.SizeAsString);

p = new PathInfo(new DirectoryInfo(@"C:\Program Files (x86)\CBM prg Studio x86"));
Console.WriteLine(p.SizeBytes.ToString("000 000 000.000") + " bytes");
Console.WriteLine(p.SizeKiloBytes.ToString("000 000 000.000") + " Kb");
Console.WriteLine(p.SizeMegaBytes.ToString("000 000 000.000") + " Mb");
Console.WriteLine(p.SizeGigaBytes.ToString("000 000 000.000") + " Gb");
Console.WriteLine(p.SizeAsString);

var temp = new Temp();
Console.WriteLine($"Temp path: {temp.GetTemporaryDirectoryInfo().FullName}");
Console.WriteLine($"Temp file: {temp.GetTemporaryFileInfo().FullName}");
Console.WriteLine($"Temp file from name: {temp.GetTemporaryFileInfo("MyData.xml").FullName}");