namespace CarRental.WebApi.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = true,
        Inherited = true
    )]
    public class NoCheckPermission : Attribute { }
}
