
namespace JokerService.SyncXmlWithCloud.Models
{
    public class Delivery
    {
        public required string key { get; set; }
        public required string issueDate { get; set; }
        public required string document { get; set; }
        public required Emitter emitter { get; set; }
        public required Receiver receiver { get; set; }
        public Carrier? carrier { get; set; }
    }
}

public class Emitter
{
    public required string name { get; set; }
    public required string document { get; set; }
    public required string city { get; set; }
    public required string state { get; set; }
}

public class Receiver
{
    public required string name { get; set; }
    public required string document { get; set; }
    public required string city { get; set; }
    public required string state { get; set; }
}

public class Carrier
{
    public required string? name { get; set; }
    public required string? document { get; set; }
    public required string? city { get; set; }
    public required string? state { get; set; }
}