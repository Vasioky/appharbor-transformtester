namespace AppHarbor.TransformTester.UnitTest.Transforms
{
    using global::Transforms;
    using Xunit;
    using Xunit.Extensions;

    public class EndpointTest : TransformBaseTest
    {
        [Theory]
        [InlineData(
         @"<client><endpoint address=""http://localhost/PQS.ServiceHost.Rest/MembershipService"" behaviorConfiguration=""PQSServiceBehavior"" binding=""webHttpBinding"" bindingConfiguration=""PQSBinding"" contract=""PQS.Services.Security.Interface.IMembershipService"" /></client>",
         @"<client></client>", 
         @"<client><endpoint address=""http://10.10.10.10/PQS.ServiceHost.Rest/MembershipService"" behaviorConfiguration=""PQSServiceBehavior"" binding=""webHttpBinding"" bindingConfiguration=""PQSBinding"" contract=""PQS.Services.Security.Interface.IMembershipService"" /></client>")]
        //[InlineData("<foo></foo>", "<bar></bar>", "<foo><bar></bar></foo>")]
        //[InlineData("<foo><bar></bar></foo>", "<bar></bar>", "<foo><bar></bar></foo>")]
        //[InlineData("<foo><baz></baz></foo>", "<bar></bar>", "<foo><baz></baz><bar></bar></foo>")]
        public void Apply_ShouldMerge(string targetXml, string transformXml, string expectedXml)
        {
            var targetDocument = ArrangeTargetDocument(targetXml);
            var transformElement = ArrangeTransformElement(targetDocument, transformXml);

            var endpointTransform = new EndpointTransform { Endpoint = "10.10.10.10" };

            endpointTransform.Apply(targetDocument.DocumentElement, transformElement);

            Assert.Equal(expectedXml.Trim(), targetDocument.OuterXml.Trim());
        }
    }
}