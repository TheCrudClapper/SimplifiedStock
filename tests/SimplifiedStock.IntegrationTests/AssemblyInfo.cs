using SimplifiedStock.IntegrationTests.Base;
using Xunit;

[CollectionDefinition("Integration")]
public class IntegrationTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
    
}