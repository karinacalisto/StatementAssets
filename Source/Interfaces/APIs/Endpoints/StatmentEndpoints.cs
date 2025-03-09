using Statement.Entities;

namespace StatementAssets.Endpoints;

public static class StatmentEndpoints
{
    public static void MapStatementEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("statement/GetExtractList",
        ([FromServices] ILogger<Program> logger,
         [FromServices] StatementContract.IRequestHandlers<StatementRequest, BaseResponse<StatementResponse>> handler,
         [FromQuery] string agency, string accountNumber, int DAC, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate) =>
        {
            try
            {
                logger.LogInformation("GetExtractList endpoint called. Agency: {Agency}, AccountNumber: {AccountNumber}, DAC: {DAC}, StartDate: {StartDate}, EndDate: {EndDate}",
                    agency, accountNumber, DAC, startDate, endDate);

                var request = new StatementRequest
                {
                    Agency = agency,
                    AccountNumber = accountNumber,
                    DAC = DAC,
                    StartDate = startDate,
                    EndDate = endDate
                };

                var response = handler.Handle(request);
                logger.LogInformation("GetExtractList successfully processed.");
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing GetExtractList.");
                return Results.StatusCode(500);
            }
        });

        app.MapPost("/cashcompositions", async (ILogger<Program> logger, CashComposition cashComposition, StatementDbContext db) =>
        {
            try
            {
                logger.LogInformation("Creating a new cash composition: {CashComposition}", cashComposition);
                db.CashCompositions.Add(cashComposition);
                await db.SaveChangesAsync();
                logger.LogInformation("Cash composition created successfully with ID {Id}", cashComposition.Id);
                return Results.Created($"/cashcompositions/{cashComposition.Id}", cashComposition);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a cash composition.");
                return Results.StatusCode(500);
            }
        });

        app.MapPost("/accounts", async (ILogger<Program> logger, Account account, StatementDbContext db) =>
        {
            try
            {
                account.CreatedAt = DateTimeOffset.UtcNow;
                account.UpdatedAt = DateTimeOffset.UtcNow;

                logger.LogInformation("Creating a new account: {Account}", account);
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
                logger.LogInformation("Account created successfully with ID {Id}", account.Id);
                return Results.Created($"/accounts/{account.Id}", account);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating an account.");
                return Results.StatusCode(500);
            }
        });

        app.MapPost("/investments", async (ILogger<Program> logger, InvestmentRequest request, StatementDbContext db) =>
        {
            try
            {
                logger.LogInformation("Creating a new investment for ProductId: {ProductId}, AccountId: {AccountId}", request.ProductId, request.AccountId);

                var account = await db.Accounts.FindAsync(request.AccountId);
                if (account == null)
                {
                    logger.LogWarning("Account not found with ID {AccountId}", request.AccountId);
                    return Results.NotFound("Account not found.");
                }

                var cashComposition = await db.CashCompositions.FindAsync(request.ProductId);
                if (cashComposition == null)
                {
                    logger.LogWarning("Fund not found with ProductId {ProductId}", request.ProductId);
                    return Results.NotFound("Fund not found.");
                }

                var investment = new Investments
                {
                    AccountId = request.AccountId,
                    ProductId = request.ProductId,
                    InvestedBalance = request.InvestedBalance,
                    InvestedAt = DateTimeOffset.UtcNow,
                    Account = account,
                    CashComposition = cashComposition,
                    ProductName = cashComposition.ProductName,
                    ProductCode = cashComposition.ProductCode
                };

                db.InvestmentStatement.Add(investment);
                await db.SaveChangesAsync();

                logger.LogInformation("Investment created successfully with ID {Id}", investment.Id);
                return Results.Created($"/investments/{investment.Id}", investment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating an investment.");
                return Results.StatusCode(500);
            }
        });
    }

    public record InvestmentRequest(Guid ProductId, Guid AccountId, decimal InvestedBalance);
}
