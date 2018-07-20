namespace Eddo.Domain.Entities.Auditing
{
    public interface ICreatAudit : ICreatedTime
    {
        string CreateUserId { get; set; }
    }
}
