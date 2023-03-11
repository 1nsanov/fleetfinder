using fleetfinder.service.main.domain.Enums;

namespace fleetfinder.service.main.domain.Bases;

public class EntityBase
{
    public long Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public State State { get; set; }
}