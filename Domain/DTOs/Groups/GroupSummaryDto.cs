namespace PrtgProxyApi.Domain.DTOs.Groups
{
    public class GroupSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalSensors { get; set; }
        public int TotalDevices { get; set; }
        public int Priority { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }

}
