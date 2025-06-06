using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace obd_ta_1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public List<Data> Entries { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public DateTime? StartTime { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? EndTime { get; set; }

        public IndexModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            var resourcePath = Path.Combine(_env.ContentRootPath, "Resources", "ta_exceedences.csv");
            using var reader = new StreamReader(resourcePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<DataMap>();
            var allEntries = csv.GetRecords<Data>().ToList();

            if (StartTime.HasValue)
                allEntries = allEntries.Where(e => e.Time >= StartTime.Value).ToList();
            if (EndTime.HasValue)
                allEntries = allEntries.Where(e => e.Time <= EndTime.Value).ToList();

            Entries = allEntries;
        }
    }
}
