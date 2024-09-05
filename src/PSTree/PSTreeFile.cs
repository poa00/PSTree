using System.IO;
using PSTree.Style;

namespace PSTree;

public sealed class PSTreeFile : PSTreeFileSystemInfo<FileInfo>
{
    public DirectoryInfo Directory => Instance.Directory;

    public string DirectoryName => Instance.DirectoryName;

    private PSTreeFile(
        FileInfo file, string hierarchy, string source, int depth)
        : base(file, hierarchy, source, depth) =>
        Length = file.Length;

    private PSTreeFile(
        FileInfo file, string hierarchy, string source)
        : base(file, hierarchy, source) =>
        Length = file.Length;

    internal static PSTreeFile Create(FileInfo file, string source)
    {
        string styled = TreeStyle.Instance.GetColoredName(file);
        return new PSTreeFile(file, styled, source);
    }

    internal static PSTreeFile Create(FileInfo file, string source, int depth)
    {
        string styled = TreeStyle.Instance.GetColoredName(file).Indent(depth);
        return new PSTreeFile(file, styled, source, depth);
    }
}
