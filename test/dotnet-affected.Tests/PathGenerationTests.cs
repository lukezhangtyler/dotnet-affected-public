﻿using Affected.Cli.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Affected.Cli.Tests
{
    public class PathGenerationTests
    {
        private class RepositoryPathsClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    "/home/lchaia/dotnet-affected", "", "/home/lchaia/dotnet-affected"
                };
                yield return new object[]
                {
                    "", "/home/lchaia/dotnet-affected/Affected.sln",
                    Path.GetDirectoryName("/home/lchaia/dotnet-affected/Affected.sln")
                };
                yield return new object[]
                {
                    "/home/lchaia/dotnet-affected", "/home/lchaia/dotnet-affected/subdirectory/other/Affected.sln",
                    "/home/lchaia/dotnet-affected"
                };
                yield return new object[]
                {
                    "", "", Environment.CurrentDirectory
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(RepositoryPathsClassData))]
        public void Should_determine_repository_path_correctly(
            string repositoryPath,
            string solutionPath,
            string expected)
        {
            var data = new CommandExecutionData(
                repositoryPath,
                solutionPath,
                string.Empty,
                string.Empty,
                false,
                Enumerable.Empty<string>(),
                new string[0],
                false,
                string.Empty,
                string.Empty);

            Assert.Equal(expected, data.RepositoryPath);
        }

        private class OutputDirPathsClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    "/home/lchaia/dotnet-affected", "", "/home/lchaia/dotnet-affected"
                };
                yield return new object[]
                {
                    "/home/lchaia/dotnet-affected", "relative/path",
                    Path.Combine("/home/lchaia/dotnet-affected", "relative/path")
                };
                yield return new object[]
                {
                    "/home/lchaia/dotnet-affected", "/some/absolute/path", "/some/absolute/path"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(OutputDirPathsClassData))]
        public void Should_determine_output_dir_correctly(
            string repositoryPath,
            string outputDir,
            string expected)
        {
            var data = new CommandExecutionData(
                repositoryPath,
                String.Empty,
                string.Empty,
                string.Empty,
                false,
                Enumerable.Empty<string>(),
                new string[0],
                false,
                outputDir,
                string.Empty);

            Assert.Equal(expected, data.OutputDir);
        }
    }
}
