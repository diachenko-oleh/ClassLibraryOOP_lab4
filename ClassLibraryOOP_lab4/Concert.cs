using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ClassLibraryOOP_lab4
{
    public class Concert : ICloneable, IComparable
    {
        private string organisator;
        private DateTime concertDate;
        public List<Performance> performances;
        public Concert()
        {
        }
        public Concert(string organisator, DateTime concertDate, List<Performance> performances)
        {
            this.organisator = organisator;
            this.concertDate = concertDate;
            this.performances = performances;
        }
        public void Addperformance(string name, string surname, WorkType work)
        {
            performances.Add(new Performance(new Author(name, surname), work));
        }
        public override string ToString()
        {
            string str = $"Організатор: {organisator}, Дата концерту: {concertDate}, Виступ(и): ";
            foreach (var performance in performances)
            {
                str += performance.ToString()+"\n";
            }
            return str;
        }
        public static List<Concert> ConvertFromString(List<string> concertStrings)
        {
            List<Concert> concerts = new List<Concert>();

            foreach (var concertString in concertStrings)
            {
                Concert concert = new Concert();
                concert.performances = new List<Performance>();

                var organizerMatch = Regex.Match(concertString, @"Організатор:\s*(.+?),");
                if (organizerMatch.Success)
                {
                    concert.organisator = organizerMatch.Groups[1].Value.Trim();
                }

                var dateMatch = Regex.Match(concertString, @"Дата концерту:\s*(.+?),");
                if (dateMatch.Success)
                {
                    concert.concertDate = DateTime.Parse(dateMatch.Groups[1].Value.Trim());
                }

                var performanceMatches = Regex.Matches(concertString, @"Автор:\s*\[Ім'я:\s*(.+?),\s*Прізвище:\s*(.+?)\],\s*Твір:\s*(.+?);");
                foreach (Match performanceMatch in performanceMatches)
                {
                    Performance performance = new Performance
                    (
                        new AuthorDTO(performanceMatch.Groups[1].Value.Trim(), performanceMatch.Groups[2].Value.Trim()).ConvertFromDTO(),
                        (WorkType)Enum.Parse(typeof(WorkType), performanceMatch.Groups[3].Value.Trim())
                    );
                    concert.performances.Add(performance);
                }

                concerts.Add(concert);
            }

            return concerts;
        }
        public string ToShortString()
        {
            return $"Організатор: {organisator}, Дата концерту: {concertDate}";
        }
        public ConcertDTO ConvertToDTO()
        {
            return new ConcertDTO(organisator, concertDate, performances);
        }
        public object Clone()
        {
            return new Concert(this.organisator, this.concertDate,this.performances);
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Concert other = obj as Concert;
            if (other != null)
            {
                string thisText = $"{organisator}{concertDate}{performances}";
                string otherText = $"{other.organisator}{other.concertDate}{other.performances}";

                return thisText.CompareTo(otherText);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            Concert other = (Concert)obj;
            return this.organisator == other.organisator && this.concertDate == other.concertDate;
        }

        public override int GetHashCode()
        {
            int hashText1 = organisator == null ? 0 : organisator.GetHashCode();
            int hashText2 = concertDate == null ? 0 : organisator.GetHashCode();
            return hashText1 ^ hashText2;
        }
        public static List<string> ConvertToString(List<Concert> concerts)
        {
            List<string> concertsStr = new List<string>();
            foreach (var concert in concerts)
            {
                concertsStr.Add(concert.ToString());
            }
            return concertsStr;
        }
    }
    public class ConcertDTO
    {
        public string organisator { get; set; }
        public DateTime concertDate { get; set; }
        public List<Performance> performances { get; set; }
        public ConcertDTO(string organisator, DateTime concertDate, List<Performance> performances)
        {
            this.organisator = organisator;
            this.concertDate = concertDate;
            this.performances = performances;
        }
        public Concert ConvertFromDTO()
        {
            return new Concert(organisator, concertDate, performances);
        }
    }
}
