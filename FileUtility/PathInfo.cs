﻿using System.Runtime.InteropServices;
using System.Text;

namespace FileUtility;

public class PathInfo
{
    [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
    private static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

    public FileInfo? FileInfo { get; }
    public DirectoryInfo? DirectoryInfo { get; }

    public PathInfo(FileInfo fileInfo)
    {
        FileInfo = fileInfo;
        DirectoryInfo = null;
    }

    public PathInfo(DirectoryInfo directoryInfo)
    {
        FileInfo = null;
        DirectoryInfo = directoryInfo;
    }

    public bool ContainsFile =>
        FileInfo != null;

    public bool ContainsDirectory =>
        DirectoryInfo != null;

    public string FullName =>
        FileInfo != null ? FileInfo.FullName : (DirectoryInfo != null ? DirectoryInfo.FullName : "");

    public DirectoryInfo? GetDirectoryInfo() =>
        DirectoryInfo ?? FileInfo?.Directory;

    public List<PathInfo> GetDirectoryContent(int depth) =>
        GetDirectoryContent(depth, _ => true);

    public List<PathInfo> GetDirectoryContent(int depth, Func<PathInfo, bool> include)
    {
        var currentDepth = 1;

        var result = new List<PathInfo>();

        if (depth <= 0)
            return result;

        var di = GetDirectoryInfo();

        if (di == null)
            return result;

        var files = di.GetFiles();
        foreach (var f in files)
        {
            try
            {
                var p = new PathInfo(f);
                if (include(p))
                    result.Add(new PathInfo(f));
            }
            catch
            {
                // ignored
            }
        }

        var dirs = di.GetDirectories();
        foreach (var d in dirs)
        {
            try
            {
                var p = new PathInfo(d);
                if (include(p))
                {
                    result.Add(new PathInfo(d));
                    if (currentDepth < depth)
                        GetChildContent(p, include, ref result, depth, currentDepth + 1);
                }
            }
            catch
            {
                // ignored
            }
        }

        return result;
    }

    private void GetChildContent(PathInfo p, Func<PathInfo, bool> include, ref List<PathInfo> result, int depth, int currentDepth)
    {
        var files = p.GetDirectoryInfo()!.GetFiles();
        foreach (var f in files)
        {
            try
            {
                var p2 = new PathInfo(f);
                if (include(p2))
                    result.Add(new PathInfo(f));
            }
            catch
            {
                // ignored
            }
        }

        var dirs = p.GetDirectoryInfo()!.GetDirectories();
        foreach (var d in dirs)
        {
            try
            {
                var p2 = new PathInfo(d);
                if (include(p2))
                {
                    result.Add(new PathInfo(d));
                    if (currentDepth < depth)
                        GetChildContent(p2, include, ref result, depth, currentDepth + 1);
                }
            }
            catch
            {
                // ignored
            }
        }
    }

    public string CompactPathForDisplay(int wantedLength)
    {
        if (wantedLength <= 0)
            return "";

        var longPathName = FullName;
        var s = new StringBuilder(wantedLength + 1);
        PathCompactPathEx(s, longPathName, wantedLength + 1, 0);
        return s.ToString();
    }

    public override string ToString() =>
        FullName;
}