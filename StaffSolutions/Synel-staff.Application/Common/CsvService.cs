
using System.Globalization;
using CsvHelper;
using CsvHelper.TypeConversion;
using Synel_staff.Domain.Entities;


namespace Synel_staff.Application.Common
{
    public class CsvService
    {
        public IEnumerable<Employee> ReadCsvFile(Stream csvFileStream)
        {
            try
            {
                //CSVHelper library was chosen for reading csv files 

                var reader = new StreamReader(csvFileStream);

                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Context.RegisterClassMap<MappingClass>();

                var options = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy", "dd/M/yyyy", "d/MM/yyyy" } };

                csv.Context.TypeConverterOptionsCache.AddOptions<DateOnly>(options);

                var records = csv.GetRecords<Employee>().ToList();

                return records;
            }
            catch (HeaderValidationException msg)
            {
                // Specific exception for header issues
                throw new ApplicationException("CSV file header is invalid.", msg);
            }
            catch (TypeConverterException ex)
            {
                // Specific exception for type conversion issues
                throw new ApplicationException("CSV file contains invalid data format.", ex);
            }
            catch (Exception ex)
            {
                // General exception for other issues
                throw new ApplicationException("Error reading CSV file", ex);
            }
        }
    }
}
