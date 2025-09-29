using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using ProgettoFinaleCaroppoli1.Models;
using System.Globalization;



namespace ProgettoFinaleCaroppoli1.Services
{ }
    public class CsvParserService
    {
        public async Task<List<Corso>> ParseAsync(Stream stream)
        {
            var corsi = new List<Corso>();

            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            });

            await csv.ReadAsync();
            csv.ReadHeader();

        while (await csv.ReadAsync())
        {
            var codice = csv.GetField("CORSO") ?? string.Empty;
            var voto = int.TryParse(csv.GetField("VOTO"), out var v) ? v : 0;

            corsi.Add(new Corso { CodiceCorso = codice, Voto = voto });
        }
        return corsi;
        }
    }
