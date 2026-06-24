namespace zenvy.application.DTOs.SalesChannels;

public class SalesChannelRequest
{
    public string ChannelName { get; set; } = string.Empty;
    public string? ChannelType { get; set; }
    public int Status { get; set; } = 1;
}

public class SalesChannelResponse
{
    public int ChannelId { get; set; }
    public string ChannelName { get; set; } = string.Empty;
    public string? ChannelType { get; set; }
    public int Status { get; set; }
}
