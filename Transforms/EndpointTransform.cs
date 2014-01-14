namespace Transforms
{
    using System;
    using System.Linq;
    using System.Xml;
    using Microsoft.Web.XmlTransform;

    public class EndpointTransform : Transform
    {
        private string _endpoint;

        protected override void Apply()
        {
            Apply((XmlElement)TargetNode, (XmlElement)TransformNode);
        }

        public string Endpoint
        {
            get
            {
                if (_endpoint != null)
                {
                    return _endpoint;
                }
                return _endpoint = Arguments[0];
            }
            internal set { _endpoint = value; }

        }

        public void Apply(XmlElement targetElement, XmlElement transformElement)
        {
            var allEndpoints = targetElement.ChildNodes.OfType<XmlElement>();

            foreach (var endpointNode in allEndpoints)
            {
                var uri = endpointNode.Attributes["address"].Value;

                var endpointUrl = new UriBuilder(uri) { Host = Endpoint };
                
                endpointNode.Attributes["address"].Value = new Uri(endpointUrl.ToString()).ToString();
            }
        }
    }
}