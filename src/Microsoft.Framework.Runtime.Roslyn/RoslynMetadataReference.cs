// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Microsoft.Framework.Runtime.Roslyn
{
    public class RoslynMetadataReference : IRoslynMetadataReference
    {
        public RoslynMetadataReference(string name, MetadataReference metadataReference)
        {
            Name = name;
            MetadataReference = metadataReference;
        }

        public string Name
        {
            get;
            private set;
        }

        public MetadataReference MetadataReference { get; private set; }

        public override string ToString()
        {
            return MetadataReference.ToString();
        }
    }
}
