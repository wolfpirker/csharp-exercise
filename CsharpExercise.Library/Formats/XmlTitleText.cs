using System.Xml.Serialization;
namespace CsharpExercise.Formats
{
    // classes in Formats are used
    // for deserialized objects;
    // only Json, Xml with these specific 
    // formats are supported

    [XmlRoot(ElementName = "xml")]
    public class XmlTitleText
    {
        [XmlElement(ElementName = "title")]
        public string? Title { get; set; }
        [XmlElement(ElementName = "text")]
        public string? Text { get; set; }
    }
}