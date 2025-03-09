using Statement.UseCases.GetExtractList;

namespace StatementTests;

public class Tests
{
    [Test]
    public void TestStatementHandler()
    {
        // Arrange
        Fakes.FakeRepository repository = new(); 
        var statementHandler = new StatementHandler(repository);
        var statementRequest = new StatementRequest();

        // Act
        var result = statementHandler.Handle(statementRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.AreEqual(200, result.HttpStatusCode);
        Assert.AreEqual("Investments retrieved successfully", result.Message);
    }
}
