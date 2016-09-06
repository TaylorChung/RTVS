﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.R.Host.Client.Test.Fixtures;
using Microsoft.UnitTests.Core.XUnit;
using Microsoft.UnitTests.References;

[assembly: TestFrameworkOverride]
[assembly: AssemblyFixtureImport(typeof(SessionProviderFixture))]
#if VS14
[assembly: Dev14AssemblyLoader]
#endif
#if VS15
[assembly: Dev15AssemblyLoader]
#endif
