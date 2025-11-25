namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class FileReportDto
    {
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
        public required byte[] Content { get; set; }
    }
}
