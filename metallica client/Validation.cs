using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;


namespace metallica_client
{
    public class ValidationBirthYear : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int year = int.Parse(value.ToString());

                if (year < 1850)
                {
                    return new ValidationResult(false, "Too old");
                }
                if (year > DateTime.Today.Year)
                {
                    return new ValidationResult(false, "ay wth thats not possible");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidationName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string firstName = value.ToString();
                if (firstName.Length < Min)
                    return new ValidationResult(false, "Name too short");
                if (firstName.Length > Max)
                    return new ValidationResult(false, "Name too long");
                if (!Char.IsLetter(firstName[0]))
                    return new ValidationResult(false, "First char must be a letter");
                if (firstName.IndexOf("  ") != -1)
                    return new ValidationResult(false, "Name must not include more then one space in a row");

                for (int i = 0; i < firstName.Length; i++)
                {
                    if (!Char.IsLetter(firstName[i]) && !Char.IsWhiteSpace(firstName[i]))
                    {
                        return new ValidationResult(false, "Bruh is there a number or symbol in your name? i dont think so");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidationUserName : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string symbols = "#?$!-_~";
                string firstName = value.ToString();
                if (firstName.Length < 3)
                    return new ValidationResult(false, "Username too short");
                if (firstName.Length > 25)
                    return new ValidationResult(false, "Username too long");
                if (firstName.IndexOf(" ") != -1)
                    return new ValidationResult(false, "Username must not include space");

                for (int i = 0; i < firstName.Length; i++)
                {
                    if (!Char.IsLetter(firstName[i]) && !Char.IsDigit(firstName[i]) && symbols.IndexOf(firstName[i]) == -1)
                    {
                        return new ValidationResult(false, "man you screwed up so bad, like what symbol did you find...");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidationPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool upperExist = false, lowerExist = false, numExist = false;
            char current = ' ', prev = 'o', prev2 = ' ';
            try
            {
                string firstName = value.ToString();
                if (firstName.Length < 6)
                    return new ValidationResult(false, "Password too short");
                if (firstName.Length > 16)
                    return new ValidationResult(false, "Password too long");

                for (int i = 0; i < firstName.Length; i++)
                {
                    if (Char.IsUpper(firstName[i])) upperExist = true;
                    else if (Char.IsLower(firstName[i])) lowerExist = true;
                    else if (Char.IsNumber(firstName[i])) numExist = true;
                    prev2 = prev;
                    prev = current;
                    current = firstName[i];
                    if (prev == current && current == prev2)
                    {
                        return new ValidationResult(false, "Dont create a row of 3 of the same letters");
                    }
                    if (Char.IsNumber(current) && Char.IsNumber(prev) && Char.IsNumber(prev2))
                    {
                        if ((current + 1 == prev && current + 2 == prev2) || (current - 1 == prev && current - 2 == prev2))
                        {
                            return new ValidationResult(false, "There are 3 numbers in order");
                        }
                    }
                }
                if (!(upperExist && lowerExist && numExist))
                    throw new Exception("Password must contain a capital letter, a regular letter and a number");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidationEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Regex validEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if(!validEmail.IsMatch(value.ToString()))
                    throw new Exception("Email invalid");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
}
