using CsharpExercise.Contracts;
using CsharpExercise.Formats;
using CsharpExercise.Repository;
using MELT;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.IO;

namespace CsharpExercise.Tests;

[TestFixture]
public class Xml_Reader_Tests
{
    private XmlReader<XmlTitleText>? xr;
    private Status _stat;
    private MemoryStream ms;

    [SetUp]
    public void Setup()
    {
        // Arrange
        ms = MockSources.GetXmlTitleTextStreamValidFormat();
        var loggerFactory = MELTBuilder.CreateLoggerFactory();
        // NuGet Package MELT to replace the ILogger         
        var logger = loggerFactory.CreateLogger<ILogService>();
        xr = new XmlReader<XmlTitleText>(logger);
    }

    [Test]
    public void DeserializeDataWithSuccess()
    {
        // Act
        XmlTitleText xmlFormat = xr.GetData(ms, out _stat);

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