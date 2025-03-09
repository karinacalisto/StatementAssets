namespace Shared.Entities;

public abstract class Entity<T>
{
    public T Id { get; set; }

    protected Entity() => Id = default!;

    protected Entity(T id) => Id = id;
}