public class AuthenticationEntity
{
    
    public string AuthenticationId { get; set; } 
    
    public string PortId { get; set; }
    public PortEntity Port { get; set; } 
    
    public bool PaymentRequired { get; set; }
}
