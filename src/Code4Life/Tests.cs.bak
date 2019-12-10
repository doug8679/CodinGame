using System;
using System.Collections.Generic;
using System.IO;
using Code4Life;
using Code4Life.States;
using NUnit.Framework;

namespace Code4Life.Tests
{
    public class Tests
    {
        
        Bot b;
        private List<Sample> GetUndiagnosedSampleList()
        {
            return new List<Sample> {new Sample(0,0,1,"",-1,-1,-1,-1,-1,-1)};
        }
        private List<Sample> GetDiagnosedSampleList()
        {
            return new List<Sample> {new Sample(0,0,1,"A",10,2,0,1,1,0)};
        }
        
        [SetUp]
        public void Setup()
        {
            b = new Bot();
        }

        [Test]
        public void StartPosTargetZeroEtaIsAtStartState()
        {
            Assert.NotNull(b);
            b.Update("START_POS", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(AtStart), b.State);
        }
        [Test]
        public void StartPosTargetZeroEtaRequestIsGotoSamples()
        {
            b.Update("START_POS",0,0,0,0,0,0,0,0,0,0,0,0);                
            Assert.AreEqual("GOTO SAMPLES", b.Request(null));
        }
        [Test]
        public void SamplesTargetNonZeroEtaIsToSamplesState()
        {
            Assert.NotNull(b);
            b.Update("SAMPLES", 2, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(ToSamples), b.State);
        }
        [Test]
        public void StartPosTargetNonZeroEtaRequestIsWait() 
        {
            b.Update("SAMPLES", 2, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.AreEqual($"WAIT", b.Request(null));
        }
        [Test]
        public void SamplesTargetZeroEtaIsAtSamplesState()
        {
            Assert.NotNull(b);
            b.Update("SAMPLES", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(AtSamples), b.State);
            
            Assert.AreEqual($"CONNECT 2", b.Request(null));
        }
        [Test]
        public void DiagnosisTargetNonZeroEtaIsToDiagnosisState()
        {
            Assert.NotNull(b);
            b.Update("DIAGNOSIS", 2, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(ToDiagnosis), b.State);
            Assert.AreEqual($"WAIT", b.Request(null));
        }
        [Test]
        public void DiagnosisTargetZeroEtaIsAtDiagnosisState()
        {
            Assert.NotNull(b);
            b.Update("DIAGNOSIS", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(AtDiagnosis), b.State);
        }
        [Test]
        public void UndiagnosedSampleAtDiagnosisConnectTest()
        {
            b.Update("DIAGNOSIS", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.AreEqual($"CONNECT 0", b.Request(GetUndiagnosedSampleList()));
        }
        [Test]
        public void DiagnosedSampleAtDiagnosisMoveTest()
        {
            b.Update("DIAGNOSIS", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.AreEqual($"GOTO MOLECULES", b.Request(GetDiagnosedSampleList()));
        }

        [Test]
        public void MoleculesNonZeroEtaIsToMoleculesState()
        {
            Assert.NotNull(b);
            b.Update("MOLECULES", 2, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(ToMolecules), b.State);
        }
        [Test]
        public void MoleculesZeroEtaIsAtMoleculesState()
        {
            Assert.NotNull(b);
            b.Update("MOLECULES", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(AtMolecules), b.State);
        }

        [Test]
        public void LaboratoryNonZeroEtaIsToLaboratoryState()
        {
            Assert.NotNull(b);
            b.Update("LABORATORY", 2, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(ToLaboratory), b.State);
        }
        [Test]
        public void LaboratoryZeroEtaIsAtLaboratoryState()
        {
            Assert.NotNull(b);
            b.Update("LABORATORY", 0, 0, 0,0,0,0,0,0,0,0,0,0);
            Assert.IsInstanceOf(typeof(AtLaboratory), b.State);
        }
    }
}
