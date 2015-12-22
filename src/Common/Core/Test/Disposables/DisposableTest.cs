﻿using System;
using FluentAssertions;
using Microsoft.Common.Core.Disposables;
using Xunit;

namespace Microsoft.Common.Core.Tests.Disposables {
    public class DisposableTest
    {
        [Fact]
        public void Create()
        {
            var callCount = 0;
            Action callback = () => callCount++;

            var disposable = Disposable.Create(callback);
            callCount.Should().Be(0);

            disposable.Dispose();
            callCount.Should().Be(1);

            disposable.Dispose();
            callCount.Should().Be(1);
        }
    }
}
