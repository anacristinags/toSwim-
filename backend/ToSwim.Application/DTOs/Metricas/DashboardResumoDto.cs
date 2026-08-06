namespace ToSwim.Application.DTOs.Metricas;

public class DashboardResumoDto
{
    public int TotalTreinos { get; set; }
    public int TotalMetrosNadados { get; set; }
    public decimal TotalTempoSegundos { get; set; }
    public decimal? PaceMedioGeralSeg { get; set; }
    public int MetasAtivasContagem { get; set; }
    public int MetasConcluidasContagem { get; set; }
}