namespace Event_management_Api.Dto;

public class EventDTO
{
    public int? OrganizerId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string Location { get; set; }
    public int? CategoryId { get; set; }
}
