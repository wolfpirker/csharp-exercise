using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace csharp_exercise.Formats
{
    [XmlRoot(ElementName = "xml")]
    public class XmlTitleText
    {
        [XmlElement(ElementName = "title")]
        public string? Title { get; set; }
        [XmlElement(ElementName = "text")]
        public string? Text { get; set; }
    }
}