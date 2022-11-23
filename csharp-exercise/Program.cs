using System.Reflection.PortableExecutable;
using System.IO.Enumeration;
using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.json");

            try
            {
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerilizeObject(doc);

            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);

        }
    }
}

/* first issues I had in mind seeing that code:
 * 1) all in Main is not as readable, not testable with Unit Tests, it is against Single-Resposibility Principle,
      classes should be cohesive and should usually only have one specific purpose, if possible;
      also it would be getting more ugly when we want to extend it with more options or features
 * 2) the Path.Combine with a relative path can easily be problematic, when actual program is moved to another path
 *    or the executable is run from another path than expected
 * 3) the variable input is only valid in the try block, so there is a compilation error
 * 4) the File.Open would require the Dispose method, to get rid of the file from memory; 
      a better approach would be to use a using block
   5.1) we should avoid using throw ex, since it resetts the stacktrace;
      throw would preserve the stacktrace and we could find the original offender
   5.2) also instead of creating a new general Exception with a message of ex, 
   we would rather throw the 'ex' Exception
 *
 *
 *
 */
