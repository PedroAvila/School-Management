namespace School.Entities;

public class Teacher
{
    public int Id { get; set; }
    public int SchoolId { get; set; }
    public int Code { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
