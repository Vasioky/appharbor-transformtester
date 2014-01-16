namespace AppHarbor.TransformTester.UnitTest.Transforms
{
    using global::Transforms;
    using Xunit;
    using Xunit.Extensions;

    public class EndpointTest : TransformBaseTest
    {
        [Theory]
        [InlineData(
         @"<client><endpoint address=""http://localhost/ServiceHost.Rest/MembershipService"" behaviorConfiguration=""ServiceBehavior"" binding=""webHttpBinding"" bindingConfiguration=""Binding"" contract=""Services.Security.Interface.IMembershipService"" /></client>",
         @"<client></client>", 
         @"<client><endpoint address=""http://10.10.10.10/ServiceHost.Rest/MembershipService"" behaviorConfiguration=""ServiceBehavior"" binding=""webHttpBinding"" bindingConfiguration=""Binding"" contract=""Services.Security.Interface.IMembershipService"" /></client>",
         "http", "10.10.10.10", 80)]
        [InlineData(
         @"<client><endpoint address=""http://localhost/ServiceHost.Rest/MembershipService"" behaviorConfiguration=""ServiceBehavior"" binding=""webHttpBinding"" bindingConfiguration=""Binding"" contract=""Services.Security.Interface.IMembershipService"" /></client>",
         @"<client></client>", 
         @"<client><endpoint address=""https://10.10.10.10:8080/ServiceHost.Rest/MembershipService"" behaviorConfiguration=""ServiceBehavior"" binding=""webHttpBinding"" bindingConfiguration=""Binding"" contract=""Services.Security.Interface.IMembershipService"" /></client>",
         "https", "10.10.10.10", 8080)]
        public void Apply_ShouldMerge(string targetXml, string transformXml, string expectedXml,
            string scheme, string host, int port)
        {
            var targetDocument = ArrangeTargetDocument(targetXml);
            var transformElement = ArrangeTransformElement(targetDocument, transformXml);

            var endpointTransform = new EndpointTransform
            {
                Scheme = scheme,
                Host = host, 
                Port = port
            };

            endpointTransform.Apply(targetDocument.DocumentElement, transformElement);

            Assert.Equal(expectedXml.Trim(), targetDocument.OuterXml.Trim());
        }
    }
}