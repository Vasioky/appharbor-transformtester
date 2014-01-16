namespace Transforms
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml;
    using Microsoft.Web.XmlTransform;

    public class EndpointTransform : Transform
    {
        private string _host;
        private string _scheme;
        private int _port;

        protected override void Apply()
        {
            Apply((XmlElement)TargetNode, (XmlElement)TransformNode);
        }

        public string Host
        {
            get
            {
                if (_host != null)
                {
                    return _host;
                }
                return _host = Arguments[1];
            }
            internal set { _host = value; }
        }

        public string Scheme
        {
            get
            {
                if (_scheme != null)
                {
                    return _scheme;
                }
                return _scheme = Arguments[0];
            }
            internal set { _scheme = value; }
        }  
        
        public int Port
        {
            get
            {
                if (_port != 0)
                {
                    return _port;
                }
                var port = Arguments[2].Trim();
                int portOut;
                return _port =  string.IsNullOrEmpty(port)? 0 : int.TryParse(port, out portOut) ? portOut : 0;
            }
            internal set { _port = value; }
        }

        public void Apply(XmlElement targetElement, XmlElement transformElement)
        {
            var allEndpoints = targetElement.ChildNodes.OfType<XmlElement>();

            foreach (var endpointNode in allEndpoints)
            {
                var uri = endpointNode.Attributes["address"].Value;

                var endpointUrl = new UriBuilder(uri) { Host = Host };

                if (Port != 0)
                {
                    endpointUrl.Port = Port;
                }
                if (!string.IsNullOrEmpty(Scheme))
                {
                    endpointUrl.Scheme = Scheme;
                }
                
                endpointNode.Attributes["address"].Value = endpointUrl.Uri.ToString();
            }
        }
    }
}