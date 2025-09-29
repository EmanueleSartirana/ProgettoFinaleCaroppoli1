using Microsoft.AspNetCore.Mvc;

namespace ProgettoFinaleCaroppoli1.Models
{
    public class UploadResult
    {
        public required string NomeFile { get; set; }
        public long Dimensione { get; set; }
        public required string Messaggio { get; set; }
        public required List<Corso> Corso { get; set; }
    }
}
