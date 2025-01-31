﻿using System.Collections.Generic;
using System.Linq;
using PSTree.Extensions;

namespace PSTree;

internal sealed class Cache
{
    private readonly List<PSTreeFileSystemInfo> _items = [];

    private readonly List<PSTreeFile> _files = [];

    internal void Add(PSTreeFile file) => _files.Add(file);

    internal void Add(PSTreeDirectory directory) => _items.Add(directory);

    internal void Flush()
    {
        if (_files.Count > 0)
        {
            _items.AddRange([.. _files]);
            _files.Clear();
        }
    }

    internal PSTreeFileSystemInfo[] GetTree(bool condition)
    {
        PSTreeFileSystemInfo[] result = condition
            ? [.. _items.Where(static e => e.ShouldInclude)]
            : [.. _items];

        return result.Format(GetItemCount(result));
    }

    private static Dictionary<string, int> GetItemCount(PSTreeFileSystemInfo[] items)
    {
        Dictionary<string, int> counts = [];
        foreach (PSTreeFileSystemInfo item in items)
        {
            string? path = item.ParentNode?.FullName;
            if (path is null)
            {
                continue;
            }

            if (!counts.ContainsKey(path))
            {
                counts[path] = 0;
            }

            counts[path]++;
        }

        return counts;
    }

    internal void Clear()
    {
        _files.Clear();
        _items.Clear();
    }
}
