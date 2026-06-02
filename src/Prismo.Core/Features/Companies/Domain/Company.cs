namespace Prismo.Core.Features.Companies.Domain;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Nit { get; private set; } = null!;

    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }


    private Company(){}

    public static Company Create(string name, string nit)
    { 
        return new Company
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            Nit = nit,
            CreatedAt = DateTime.UtcNow
        };
        
    }
    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    public void UpadateName(string name) => Name = name;
    public void UpadateNit(string nit) => Nit = nit;
    
}
