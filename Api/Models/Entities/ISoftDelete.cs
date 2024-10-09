namespace Api.Models.Entities;

public interface ISoftDelete
{
    Boolean Deleted { get; set; }
}