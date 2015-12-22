﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Common.Core.Tests.Utility {
    [ExcludeFromCodeCoverage]
    public static class TestFiles {
        static public string LoadFile(string testRunDirectory, string fileName) {
            var filePath = GetTestFilePath(testRunDirectory, fileName);

            using (var sr = new StreamReader(filePath)) {
                return sr.ReadToEnd();
            }
        }

        static public string GetTestFilesFolder(string testRunDirectory) {
            return Path.Combine(testRunDirectory, CommonTestData.TestFilesRelativePath);
        }

        static public string GetTestFilePath(string testRunDirectory, string fileName) {
            return Path.Combine(GetTestFilesFolder(testRunDirectory), fileName);
        }

        public static IList<string> GetTestFiles(string testRunDirectory, string extension) {
            string path = GetTestFilesFolder(testRunDirectory);
            var files = new List<string>();

            IEnumerable<string> filesInFolder = Directory.EnumerateFiles(path);
            foreach (string name in filesInFolder) {
                if (name.EndsWithIgnoreCase(extension))
                    files.Add(name);
            }

            return files;
        }

        public static void CompareToBaseLine(string baselinefilePath, string actual) {
            string expected;

            using (var streamReader = new StreamReader(baselinefilePath)) {
                expected = streamReader.ReadToEnd();
            }

            // trim whitescpase in the end to avoid false positives b/c file 
            // has extra line break or whitespace at the end.
            expected = expected.TrimEnd(new char[] { ' ', '\r', '\n', '\t' });
            actual = actual.TrimEnd(new char[] { ' ', '\r', '\n', '\t' });

            string baseLine;
            string actualLine;
            int lineNumber = BaselineCompare.CompareLines(expected, actual, out baseLine, out actualLine);

            Assert.AreEqual(0, lineNumber,
                String.Format(CultureInfo.InvariantCulture,
                    "\r\nDifferent at line {0}\r\nExpected: {1}\r\nActual: {2}", lineNumber, baseLine, actualLine));
        }

        public static void UpdateBaseline(string filePath, string content) {
            if (File.Exists(filePath)) {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }

            using (var streamWriter = new StreamWriter(filePath)) {
                streamWriter.Write(content);
            }
        }
    }
}
