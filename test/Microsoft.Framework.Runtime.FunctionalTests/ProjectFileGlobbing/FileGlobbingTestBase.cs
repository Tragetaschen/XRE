﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Framework.Runtime.FunctionalTests.Utilities;
using System.IO;

namespace Microsoft.Framework.Runtime.FunctionalTests.ProjectFileGlobbing
{
    public abstract class FileGlobbingTestBase : IDisposable
    {
        protected readonly DisposableProjectContext _context;

        public FileGlobbingTestBase()
        {
            _context = CreateContext();
        }

        protected abstract DisposableProjectContext CreateContext();

        protected void VerifyFilePathsCollection(IEnumerable<string> actualFiles, params string[] expectFiles)
        {
            var expectFilesInFullpath = expectFiles.Select(relativePath => Path.GetFullPath(Path.Combine(_context.RootPath, relativePath)));
            var actualFilesInFullpath = actualFiles.Select(filePath => Path.GetFullPath(filePath));

            AssertHelpers.SortAndEqual(expectFilesInFullpath, actualFilesInFullpath, StringComparer.InvariantCultureIgnoreCase);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}