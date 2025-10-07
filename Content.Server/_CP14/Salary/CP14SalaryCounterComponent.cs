namespace Content.Server._CE14.Salary;

[RegisterComponent, Access(typeof(CE14SalarySystem))]
public sealed partial class CE14SalaryCounterComponent : Component
{
    [DataField, AutoNetworkedField]
    public TimeSpan NextSalaryTime = TimeSpan.Zero;

    [DataField]
    public TimeSpan Frequency = TimeSpan.FromMinutes(20);

    [DataField]
    public int Salary = 100;

    [DataField]
    public int UnpaidSalary = 0;
}
