﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.Languages.Editor.Controller.Constants;
using Microsoft.R.Components.ContentTypes;
using Microsoft.R.Editor.Application.Test.TestShell;
using Microsoft.UnitTests.Core.Mef;
using Microsoft.UnitTests.Core.XUnit;
using Xunit;

namespace Microsoft.R.Editor.Application.Test.Formatting {
    [ExcludeFromCodeCoverage]
    [Collection(CollectionNames.NonParallel)]
    public class FormatTest : IDisposable {
        private readonly IExportProvider _exportProvider;
        private readonly EditorHostMethodFixture _editorHost;

        public FormatTest(REditorApplicationMefCatalogFixture catalogFixture, EditorHostMethodFixture editorHost) {
            _exportProvider = catalogFixture.CreateExportProvider();
            _editorHost = editorHost;
        }

        public void Dispose() {
            _exportProvider.Dispose();
        }
        

        [CompositeTest]
        [Category.Interactive]
        [InlineData("\nwhile (TRUE) {\n        if(x>1) {\n   }\n}", "\nwhile (TRUE) {\n    if (x > 1) {\n    }\n}")]
        [InlineData("if (1 && # comment\n  2) {x<-1}", "if (1 && # comment\n  2) { x <- 1 }")]
        public void R_FormatDocument(string original, string expected) {
            using (var script = _editorHost.StartScript(_exportProvider, original, RContentTypeDefinition.ContentType)) {
                script.Execute(VSConstants.VSStd2KCmdID.FORMATDOCUMENT, 50);
                string actual = script.EditorText;
                actual.Should().Be(expected);
            }
        }
    }
}
