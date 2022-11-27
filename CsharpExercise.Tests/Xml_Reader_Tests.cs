using NUnit.Framework;
using CsharpExercise.Repository;
using Moq;
using MELT;
using CsharpExercise.Contracts;
using System.IO;
using CsharpExercise.Formats;
using Microsoft.Extensions.Logging;

namespace CsharpExercise.Tests;

[TestFixture]
public class Xml_Reader_Tests
{
    private XmlReader<XmlTitleText>? xr;
    private Status _stat;

    [SetUp]
    public void Setup()
    {
        // Arrange
        Mock<ISource<MemoryStream>> mockSrcFromXmlFile = new();
        mockSrcFromXmlFile.CallBase = true;
        mockSrcFromXmlFile.Setup(x => x.GetData(out _stat)).Returns(MockSources.GetXmlTitleTextStreamValidFormat());
        var loggerFactory = MELTBuilder.CreateLoggerFactory();
        // NuGet Package MELT to replace the ILogger         
        var logger = loggerFactory.CreateLogger<ILogService>();
        xr = new XmlReader<XmlTitleText>(logger, mockSrcFromXmlFile.Object);
    }

    [Test]
    public void DeserializeDataWithSuccess()
    {
        // Act
        XmlTitleText xmlFormat = xr.GetData(out _stat);

        // Assert
        Assert.That(_stat, Is.EqualTo(Status.Success),
            $"The result should have been {Status.Success}, but was {_stat}");
        Assert.IsNotNull(xmlFormat);
    }

    /* what other tests would I add in Xml_Reader_Tests? 
      > status with Error and null returned, if the MemoryStream data has no valid XML
      > add one more XML Formats which has a more complex setup than the contents of Document1.xml
    */




}