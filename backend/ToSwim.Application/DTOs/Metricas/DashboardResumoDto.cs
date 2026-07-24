namespace ToSwim.Application.DTOs.Metricas;

public class DashboardResumoDto
{
    public int TotalTreinos { get; set; }
    public int TotalMetrosNadados { get; set; }
    public int TotalTempoSegundos { get; set; }
    public int? PaceMedioGeralSeg { get; set; }
    public int MetasAtivasContagem { get; set; }
    public int MetasConcluidasContagem { get; set; }
}