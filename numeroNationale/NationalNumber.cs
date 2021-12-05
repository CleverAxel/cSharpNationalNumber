using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numeroNationale {
    internal class NationalNumber {

        /// <summary>
        /// Retourne vrai si numéro national valide.
        /// </summary>
        /// <param name="nationalNumber"></param>
        /// <returns></returns>
        public static bool IsValid(string nationalNumber) {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < nationalNumber.Length; i++) {
                if (Char.IsDigit(nationalNumber[i])) {
                    sb.Append(nationalNumber[i]);
                }
            }

            string normalizedValue = sb.ToString();

            if(normalizedValue.Length == 11) {
                if (ConvertDateToNumber(normalizedValue) && IsOrderNumberValid(normalizedValue) && isControlNumberValid(normalizedValue)) {
                    return true;
                }
            } else {
                Console.WriteLine("entrée trop longue ou trop courte");
            }
            
            return false;
        }

        private static bool ConvertDateToNumber(string normalizedValue) {
            int year = 0;
            int month = 0;
            int day = 0;

            int currentNumber = 0;
            string currentInformation = "";
            
            while(currentNumber < 2) {
                currentInformation += normalizedValue[currentNumber];
                currentNumber++;
            }
            year = int.Parse(currentInformation);
            currentInformation = "";

            while(currentNumber < 4) {
                currentInformation += normalizedValue[currentNumber];
                currentNumber++;
            }
            month = int.Parse(currentInformation);
            currentInformation = "";

            while (currentNumber < 6) {
                currentInformation += normalizedValue[currentNumber];
                currentNumber++;
            }
            day = int.Parse(currentInformation);

            return IsDateValid(year, month, day);
        }

        private static bool IsDateValid(int year, int month, int day) {
            if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day <= 31) {
                return true;
            } else {
                if ((month == 4 || month == 6 || month == 9 || month == 11) && day <= 30) {
                    return true;
                } else {
                    if((month == 2 && day <= 28) && year % 4 != 0) {
                        return true;
                    } else {
                        if((month == 2 && day <= 29) && year % 4 == 0) {
                            return true;
                        }
                    }
                }
            }
            Console.WriteLine("y a un problème avec la date Marty Mcfly");
            return false;
        }

        private static bool IsOrderNumberValid(string normalizedValue) {
            string orderNumber = "";
            for (int i = 6; i < 9; i++) {
                orderNumber += normalizedValue[i];
            }

            int orderNumberConverted = int.Parse(orderNumber);
            if (orderNumberConverted > 0 && orderNumberConverted < 999) {
                return true;
            }

            Console.WriteLine("y'a un problème avec le numéro d'ordre");
            Console.WriteLine("/)*3*(\\, ce sont les 3 chiffres");
            return false;
        }

        private static bool isControlNumberValid(string normalizedValue) {
            string controlNumber = "";
            string otherInfoToCalculOrderNumber = "";
            int i = 0;
            while(i < normalizedValue.Length - 2) {
                otherInfoToCalculOrderNumber += normalizedValue[i];
                i++;
            }

            while(i < normalizedValue.Length) {
                controlNumber += normalizedValue[i];
                i++;
            }

            int controlNumberConverted = int.Parse(controlNumber);
            int otherInfoConverted = int.Parse(otherInfoToCalculOrderNumber);

            if((otherInfoConverted % 97) - 97 == -controlNumberConverted) {
                return true;
            }

            Console.WriteLine("y'a un problème avec le numéro de contrôle SAPERLIPOPETTE");
            Console.WriteLine("Ce sont les deux derniers numéros :3");
            return false;
        }
    }
}
