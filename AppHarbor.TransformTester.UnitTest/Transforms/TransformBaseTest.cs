namespace AppHarbor.TransformTester.UnitTest.Transforms
{
    using System.IO;
    using System.Xml;

    public abstract class TransformBaseTest
    {
        protected XmlDocument ArrangeTargetDocument(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            return document;
        }

        protected XmlElement ArrangeTransformElement(XmlDocument document, string xml)
        {
            using (var textReader = new StringReader(xml))
            {
                using (var xmlReader = new XmlTextReader(textReader))
                {
                    var node = document.ReadNode(xmlReader);
                    return (XmlElement)node;
                }
            }
        }
    }
}