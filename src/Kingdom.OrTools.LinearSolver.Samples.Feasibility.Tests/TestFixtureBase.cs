﻿namespace Kingdom.OrTools.LinearSolver.Samples.Feasibility
{
    using NUnit.Framework;

    [TestFixture]
    public abstract class TestFixtureBase
    {
        [TestFixtureSetUp]
        public virtual void SetupFixture()
        {
        }

        [TestFixtureTearDown]
        public virtual void TearDownFixture()
        {
        }

        [SetUp]
        public virtual void SetUp()
        {
        }

        [TearDown]
        public virtual void TearDown()
        {
        }
    }
}
