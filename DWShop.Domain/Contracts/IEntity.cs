namespace DWShop.Domain.Contracts
{
    public interface IEntity
    {
    }
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }

}
