using System;
using System.IO;
using FileUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileUtilityTests;

[TestClass]
public class PathTests
{
    [TestMethod]
    public void FullName()
    {
        var f = new PathInfo(new FileInfo(@"C:\Temp\hello.txt"));
        Assert.AreEqual(@"C:\Temp\hello.txt", f.FullName);

        var d = new PathInfo(new DirectoryInfo(@"C:\Temp\"));
        Assert.AreEqual(@"C:\Temp\", d.FullName);
    }

    [TestMethod]
    public void CompactPathForDisplay()
    {
        var f = new PathInfo(new FileInfo(@"C:\Temp\hello.txt"));
        Assert.AreEqual(@"", f.CompactPathForDisplay(0));
        Assert.AreEqual(@".", f.CompactPathForDisplay(1));
        Assert.AreEqual(@"..", f.CompactPathForDisplay(2));
        Assert.AreEqual(@"...\.", f.CompactPathForDisplay(5));
        Assert.AreEqual(@"...\hel...", f.CompactPathForDisplay(10));
        Assert.AreEqual(@"C:...\hello.txt", f.CompactPathForDisplay(15));
        Assert.AreEqual(@"C:\Temp\hello.txt", f.CompactPathForDisplay(20));

        var d = new PathInfo(new DirectoryInfo(@"C:\Temp\"));
        Assert.AreEqual(@"", d.CompactPathForDisplay(0));
        Assert.AreEqual(@".", d.CompactPathForDisplay(1));
        Assert.AreEqual(@"..", d.CompactPathForDisplay(2));
        Assert.AreEqual(@"...\.", d.CompactPathForDisplay(5));
        Assert.AreEqual(@"C:\Temp\", d.CompactPathForDisplay(10));
    }

    [TestMethod]
    public void GetDirectoryInfo()
    {
        var f = new PathInfo(new FileInfo(@"C:\Temp\hello.txt"));
        Assert.AreEqual(@"C:\Temp", f.GetDirectoryInfo()!.FullName);

        var d = new PathInfo(new DirectoryInfo(@"C:\Temp\"));
        Assert.AreEqual(@"C:\Temp\", d.GetDirectoryInfo()!.FullName);
    }

    [TestMethod]
    public void GetDirectoryContent()
    {
        // All files and directories in two levels.

        var l = new PathInfo(new DirectoryInfo(@"C:\")).GetDirectoryContent(2);
        Console.WriteLine(l.Count);
        foreach (var p in l)
            Console.WriteLine(p);

        // All folders and icon files in three levels.

        static bool Guard(PathInfo p) =>
            p.ContainsDirectory || (p.ContainsFile && p.FullName.EndsWith(".ico", StringComparison.CurrentCultureIgnoreCase));

        l = new PathInfo(new DirectoryInfo(@"C:\")).GetDirectoryContent(3, Guard);
        Console.WriteLine(l.Count);
        foreach (var p in l)
            Console.WriteLine(p);
    }
}