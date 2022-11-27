using System.IO;

namespace CsharpExercise.Tests
{
    public class MockSources
    {
        // this class contained objects should replace source 
        // content in Unit Tests e.g. sources such as Document1.xml, 
        // Document1.json with appropiate formats;
        // its objects are meant to be used in place of the results 
        // of ISource.GetData(), which returns a object of type MemoryStream 

        private const string _XmlTitleText = @"<xml>
	<title>A short title</title>
	<text>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet.</text>
</xml>";

        private const string _JsonTitleText = @"{
            ""Title"":""A short title"",""Text"":""Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.Nulla consequat massa quis enim.Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu.In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo.Nullam dictum felis eu pede mollis pretium.Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim.Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.Phasellus viverra nulla ut metus varius laoreet.""}";

        private const string _invalidFormat = "abc";

        public static MemoryStream GetXmlTitleTextStreamValidFormat()
        {
            return GenerateStreamFromString(_XmlTitleText);
        }

        public static MemoryStream GetInvalidFormat()
        {
            return GenerateStreamFromString(_invalidFormat);
        }


        public static MemoryStream GetJsonTitleTextValidFormat()
        {
            return GenerateStreamFromString(_JsonTitleText);
        }

        private static MemoryStream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}