using System.ComponentModel.DataAnnotations;

class Task {
    public int Id { get; set; }
    [MaxLength(200)]
    public required string Name { get; set; }
    public bool isCompleted { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
