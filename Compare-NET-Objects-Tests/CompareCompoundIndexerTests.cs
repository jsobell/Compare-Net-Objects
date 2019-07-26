using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace KellermanSoftware.CompareNetObjectsTests
{
    using System;

    [TestFixture]
    public class CompareCompoundIndexerTests
    {
        [Test]
        public void CompareArraysWithComplexIndexer()
        {
            Dictionary<ComplexKey, string> left = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
                {new ComplexKey {Name = "Old", Age = 100}, "aaa"},
            };

            Dictionary<ComplexKey, string> right = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Old", Age = 100}, "aaa"},
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
            };

            var compareLogic = new CompareLogic
            {
                Config = {
                    IgnoreCollectionOrder = true
                }
            };

            var result = compareLogic.Compare(left, right);
            Assert.IsTrue(result.AreEqual, result.DifferencesString);
        }

        
        [Test]
        public void CompareArraysWithComplexIndexer_With_Key_Differences()
        {
            Dictionary<ComplexKey, string> left = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Old1", Age = 100}, "aaa"},
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
            };

            Dictionary<ComplexKey, string> right = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Old2", Age = 100}, "aaa"},
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
            };

            var compareLogic = new CompareLogic
            {
                Config = {
                    IgnoreCollectionOrder = true
                }
            };

            var result = compareLogic.Compare(left, right);
            Assert.IsFalse(result.AreEqual, result.DifferencesString);
        }
       
        
        
        [Test]
        public void CompareArraysWithComplexIndexerOrdered_ShouldFail()
        {
            Dictionary<ComplexKey, string> left = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
                {new ComplexKey {Name = "Old", Age = 100}, "aaa"},
            };

            Dictionary<ComplexKey, string> right = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Old", Age = 100}, "aaa"},
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
            };

            var compareLogic = new CompareLogic
            {
                Config = {
                    IgnoreCollectionOrder = false
                }
            };

            var result = compareLogic.Compare(left, right);
            Assert.IsFalse(result.AreEqual, result.DifferencesString);
        }
        
        [Test]
        public void CompareArraysWithComplexIndexerOrdered_ShouldPass()
        {
            Dictionary<ComplexKey, string> left = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Old", Age = 100}, "aaa"},
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
            };

            Dictionary<ComplexKey, string> right = new Dictionary<ComplexKey, string>
            {
                {new ComplexKey {Name = "Old", Age = 100}, "aaa"},
                {new ComplexKey {Name = "Young", Age = 10}, "aaa"},
            };

            var compareLogic = new CompareLogic
            {
                Config = {
                    IgnoreCollectionOrder = false
                }
            };

            var result = compareLogic.Compare(left, right);
            Assert.IsTrue(result.AreEqual, result.DifferencesString);
        }
        
        public class ComplexKey
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        
/*

        [Test]
        public void CompareCompoundKeyList()
        {
            var comparer = new CompareLogic();
            var a1 = new[] { 1, 2, 3 }.ToImmutableArray();
            var a2 = new[] { 1, 2, 3 }.ToImmutableArray();

            var result = comparer.Compare(a1, a2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void CompareArraysOfDiferentOrder()
        {
            var comparer = new CompareLogic { Config = new ComparisonConfig { IgnoreCollectionOrder = true } };
            var a1 = new[] { 1, 2 }.ToImmutableArray();
            var a2 = new[] { 2, 1 }.ToImmutableArray();

            var result = comparer.Compare(a1, a2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void CompareSameArraysOnLevel2()
        {
            var comparer = new CompareLogic();
            var e1 = new TestEnvelope(new[] { "John", "Jane" });
            var e2 = new TestEnvelope(new[] { "John", "Jane" });

            var result = comparer.Compare(e1, e2);

            if (!result.AreEqual)
                throw new Exception(result.DifferencesString);
        }

        [Test]
        public void CompareDiferentArraysOnLevel2()
        {
            var comparer = new CompareLogic();
            var e1 = new TestEnvelope(new[] { "John", "Jane" });
            var e2 = new TestEnvelope(new[] { "John", "Mary" });

            var result = comparer.Compare(e1, e2);

            Assert.IsFalse(result.AreEqual);
        }*/
    }

    public class TestCompoundList: List<string>
    {
        public TestCompoundList(IEnumerable<string> names)
        {
            Names = names.ToImmutableArray();
        }

        public string this[string key]
        {
            get
            {
                return this.First(r => r.StartsWith(key));
            }
        }
        public ImmutableArray<string> Names { get; }
    }
}
