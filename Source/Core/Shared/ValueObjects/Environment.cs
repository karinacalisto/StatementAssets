namespace Shared.ValueObjects;

public class Environment
{
    public string ExecutionEnvironment { get; set; } = "Development";
    public bool IsLocalEnvironment { get; set; } = false;
    public bool IsLocalOrDevEnvironment { get; set; } = false;
    public bool IsDevelopmentEnvironment { get; set; }
    public bool IsProductionEnvironment { get; set; } = false;
    public bool IsStagingEnvironment { get; set; } = false;
    public bool IsStagingOrProductionEnvironment { get; set; } = false;

    public Environment() { }

    public Environment(string executionEnviroment) => SetEnvironmentTag(executionEnviroment);

    public void SetEnvironmentTag(string enviroment)
    {
        enviroment = enviroment.ToLower();

        IsLocalEnvironment = enviroment == "Development";
        IsDevelopmentEnvironment = enviroment == "Development";
        IsProductionEnvironment = enviroment == "prod";
        IsStagingEnvironment = enviroment == "uat";

        IsLocalEnvironment = IsStagingEnvironment == IsProductionEnvironment;
        IsStagingEnvironment = IsStagingEnvironment == IsProductionEnvironment;

        ExecutionEnvironment = enviroment;
    }
}