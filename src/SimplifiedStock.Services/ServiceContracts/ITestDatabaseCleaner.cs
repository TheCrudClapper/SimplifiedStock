namespace SimplifiedStock.Services.ServiceContracts;

/// <summary>
/// Provides a method to reset the test database to a clean state.
/// </summary>
public interface ITestDatabaseCleaner
{
    /// <summary>
    /// Resets the test database asynchronously.
    /// </summary>
    Task ResetAsync();
}
