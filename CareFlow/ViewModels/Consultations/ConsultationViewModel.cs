namespace CareFlow.ViewModels.Consultations;

public class ConsultationViewModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
}
