using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibraryOOP_lab4
{
    public class Author : ICloneable, IComparable
    {
        private string name;
        private string surname;

        public override string ToString()
        {
            return $"[Ім'я: {name}, Прізвище: {surname}]";
        }
        public Author(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }
        public AuthorDTO ConvertToDTO()
        {
            return new AuthorDTO(name, surname);
        }
        public object Clone()
        {
            return new Author(this.name, this.surname);
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Author other = obj as Author;
            if (other != null)
            {
                string thisText = $"{name}{surname}";
                string otherText = $"{other.name}{other.surname}";

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

            Author other = (Author)obj;
            return this.name == other.name && this.surname == other.surname;
        }

        public override int GetHashCode()
        {
            int hashText1 = name == null ? 0 : name.GetHashCode();
            int hashText2 = surname == null ? 0 : surname.GetHashCode();
            return hashText1 ^ hashText2;
        }
    }
    public class AuthorDTO
    {
        public string name { get; set; }
        public string surname { get; set; }
        public AuthorDTO(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }
        public Author ConvertFromDTO()
        {
            return new Author(name, surname);
        }
    }
}

