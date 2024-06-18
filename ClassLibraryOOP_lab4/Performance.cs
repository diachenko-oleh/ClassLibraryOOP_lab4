using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ClassLibraryOOP_lab4
{
    public enum WorkType
    {
        instrumental,
        vocal,
        poetry,
        prose
    }
    public class Performance : ICloneable, IComparable
    {
        private Author author;
        private WorkType work;
        public Performance(Author author, WorkType work)
        {
            this.author = author;
            this.work = work;
        }
        public override string ToString()
        {
            return $"Автор: {author}, Твір: {work}; ";
        }
        public PerformanceDTO ConvertToDTO()
        {
            return new PerformanceDTO(author, work);
        }
        public object Clone()
        {
            return new Performance(this.author, this.work);
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Performance other = obj as Performance;
            if (other != null)
            {
                string thisText = $"{author}{work}";
                string otherText = $"{other.author}{other.work}";

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

            Performance other = (Performance)obj;
            return this.author == other.author && this.work == other.work;
        }

        public override int GetHashCode()
        {
            int hashText1 = author == null ? 0 : author.GetHashCode();
            int hashText2 = work.GetHashCode();
            return hashText1 ^ hashText2;
        }
    }

    public class PerformanceDTO
    {
        public Author author { get; set; }
        public WorkType work { get; set; }
        public PerformanceDTO(Author author, WorkType work)
        {
            this.author = author;
            this.work = work;
        }
        public Performance ConvertFromDTO()
        {
            return new Performance(author, work);
        }
    }
}
