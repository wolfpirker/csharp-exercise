using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using CsharpExercise.Repository;
using Moq;
using CsharpExercise.Contracts;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MELT;

namespace CsharpExercise.Tests
{
    public class Source_From_File_Tests
    {
        private SourceFromFile<MemoryStream>? _sff;

        [SetUp]
        public void Setup()
        {
            // Arrange
            Mock<IAppSettingsConfig> mockConfig = new();
            var cfg = new MockAppSettingsConfig();
            mockConfig.CallBase = true;
            mockConfig.Setup(x => x.GetConversionSource()).Returns(cfg.GetConversionSource());
            var loggerFactory = MELTBuilder.CreateLoggerFactory();
            // NuGet Package MELT to replace the ILogger         
            var logger = loggerFactory.CreateLogger<ILogService>();
            _sff = new SourceFromFile<MemoryStream>(logger, mockConfig.Object);
        }

        [Test]
        public void ReturnMemoryStreamNotNullOnSuccess()
        {
            // Note: current Problem with this test:
            // the method GetData() is coupled with reading a file on the filesystem
            // currently this test can only pass when there is the "Source Files" 
            // directory with Document1.xml in the same root as the binary of 
            // the test!

            // so instead of Reading the file in _sff.GetData(), it could be done 
            // in Program Main() and be given to SourceFromFile object as input objects,
            // however this makes SourceFromFile object almost redundant, since it would only
            // convert the FileStream to MemoryStream 
            MemoryStream ms = _sff.GetData(out Status stat);

            // Assert
            Assert.That(stat, Is.EqualTo(Status.Success),
                $"The result should have been {Status.Success}, but was {stat}");
            Assert.IsNotNull(ms);
        }

        /* what other tests would I add in Source_From_File_Tests? 
          > status with Error and null returned, if the file is not available
        */
    }
}