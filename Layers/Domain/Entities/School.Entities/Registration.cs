namespace School.Entities;

public class Registration
{
    public int Id { get; set; }
    public int Code { get; set; }
    public int StudentId { get; set; }
    public int SchoolId { get; set; }
    public DateTime RegistrationDate { get; set; }
}
