namespace Weather.Domain;
public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
}

