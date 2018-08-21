using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowrunCombatHelper.Models;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunCombatHelperTests
{
    [TestClass]
    public class MagicTraditionTest
    {

        private MagicTradition traditionToTest;
        private string prop;

        [TestInitialize]
        public void Startup()
        {
            traditionToTest = new MagicTradition();
            traditionToTest.PropertyChanged += CapturePropertyChangesFiring;
            prop = string.Empty;

        }

        [TestMethod]
        public void SetResistDrainAttributes_FiresPropertyChangedEventHandlers()
        {
            traditionToTest.ResistDrainAttributes = new List<Skill.Attributes>() { Skill.Attributes.AGI, Skill.Attributes.BOD };
            Assert.AreEqual("ResistDrainAttributes;ResistDrainAttributesString;", prop);
        }

        [TestMethod]
        public void SetTraditionName_FiresPropertyChangedEventHandlers()
        {
            traditionToTest.TraditionName = "Tradition1";
            Assert.AreEqual("TraditionName;", prop);
        }

        [DataTestMethod]
        [DataRow(new Skill.Attributes[] { },"")]
        [DataRow(new Skill.Attributes[] { Skill.Attributes.AGI}, "AGI")]
        [DataRow(new Skill.Attributes[] { Skill.Attributes.STR, Skill.Attributes.AGI },"STR + AGI")]
        public void ResistDrainAttributesString_GeneratesCorrectStrings(Skill.Attributes[] attrs, string expected)
        {
            traditionToTest.ResistDrainAttributes = attrs.ToList<Skill.Attributes>();
            string attrString = traditionToTest.ResistDrainAttributesString;
            Assert.AreEqual(expected, attrString);
        }

        [TestMethod]
        public void TestEquals_DifferentObjectReturnsFalse()
        {
            traditionToTest.TraditionName = "Tradition1";
            string tradition = "Tradition1";
            Assert.IsFalse(traditionToTest.Equals(tradition));
        }

        [DataTestMethod]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI}, "tradition2", new Skill.Attributes[] { Skill.Attributes.STR }, false)]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition1", new Skill.Attributes[] { Skill.Attributes.STR }, false)]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition2", new Skill.Attributes[] { Skill.Attributes.AGI}, false)]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, true)]
        public void TestEquals_EqualsOperatorCorrectlyCompares(string trad1Name, Skill.Attributes[] trad1Attributes, string trad2name, Skill.Attributes[] trad2Attributes, bool expected)
        {
            traditionToTest.TraditionName = trad1Name;
            traditionToTest.ResistDrainAttributes = trad1Attributes.ToList<Skill.Attributes>();
            MagicTradition tradition2 = new MagicTradition(trad2name, trad2Attributes.ToList<Skill.Attributes>());
            Assert.AreEqual(traditionToTest.Equals(tradition2), expected);
        }

        [DataTestMethod]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition2", new Skill.Attributes[] { Skill.Attributes.STR }, false)]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition1", new Skill.Attributes[] { Skill.Attributes.STR }, false)]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition2", new Skill.Attributes[] { Skill.Attributes.AGI }, false)]
        [DataRow("tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, "tradition1", new Skill.Attributes[] { Skill.Attributes.AGI }, false)]
        public void GetHashCode_GeneratesAppropriatelyEqualHashCodes(string trad1Name, Skill.Attributes[] trad1Attributes, string trad2name, Skill.Attributes[] trad2Attributes, bool expected)
        {
            traditionToTest.TraditionName = trad1Name;
            traditionToTest.ResistDrainAttributes = trad1Attributes.ToList<Skill.Attributes>();
            MagicTradition tradition2 = new MagicTradition(trad2name, trad2Attributes.ToList<Skill.Attributes>());
            Assert.AreEqual(traditionToTest.GetHashCode() == tradition2.GetHashCode(), expected);
        }

        private void CapturePropertyChangesFiring(object o, PropertyChangedEventArgs e)
        {
            prop += e.PropertyName + ";";
        }
    }
}
