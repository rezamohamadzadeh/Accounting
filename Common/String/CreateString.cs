using System;
using System.Collections.Generic;
using System.Text;

namespace Common.String
{
    public class CreateString
    {
        public string CreateStringByLength(int length)
        {
            //return "C" characters
            Random r = new Random();

            string buidedString = "";
            for (int i = 1; i <= length; i++)
            {
                int af = r.Next(0, 35);
                char character;
                switch (af)
                {
                    case 0:
                        character = 'a';
                        break;
                    case 1:
                        character = 'b';
                        break;
                    case 2:
                        character = 'c';
                        break;
                    case 3:
                        character = 'd';
                        break;
                    case 4:
                        character = 'e';
                        break;
                    case 5:
                        character = 'f';
                        break;
                    case 6:
                        character = 'g';
                        break;
                    case 7:
                        character = 'h';
                        break;
                    case 8:
                        character = 'i';
                        break;
                    case 9:
                        character = 'j';
                        break;
                    case 10:
                        character = 'k';
                        break;
                    case 11:
                        character = 'l';
                        break;
                    case 12:
                        character = 'm';
                        break;
                    case 13:
                        character = 'n';
                        break;
                    case 14:
                        character = 'o';
                        break;
                    case 15:
                        character = 'p';
                        break;
                    case 16:
                        character = 'q';
                        break;
                    case 17:
                        character = 'r';
                        break;
                    case 18:
                        character = 's';
                        break;
                    case 19:
                        character = 't';
                        break;
                    case 20:
                        character = 'u';
                        break;
                    case 21:
                        character = 'v';
                        break;
                    case 22:
                        character = 'w';
                        break;
                    case 23:
                        character = 'x';
                        break;
                    case 24:
                        character = 'y';
                        break;
                    case 25:
                        character = 'z';
                        break;
                    case 26:
                        character = '0';
                        break;
                    case 27:
                        character = '1';
                        break;
                    case 28:
                        character = '2';
                        break;
                    case 29:
                        character = '3';
                        break;
                    case 30:
                        character = '4';
                        break;
                    case 31:
                        character = '5';
                        break;
                    case 32:
                        character = '6';
                        break;
                    case 33:
                        character = '7';
                        break;
                    case 34:
                        character = '8';
                        break;
                    case 35:
                        character = '9';
                        break;
                    case 36:
                        character = 'A';
                        break;
                    case 37:
                        character = 'B';
                        break;
                    case 38:
                        character = 'C';
                        break;
                    case 39:
                        character = 'D';
                        break;
                    case 40:
                        character = 'E';
                        break;
                    case 41:
                        character = 'F';
                        break;
                    case 42:
                        character = 'G';
                        break;
                    case 43:
                        character = 'H';
                        break;
                    case 44:
                        character = 'I';
                        break;
                    case 45:
                        character = 'J';
                        break;
                    case 46:
                        character = 'K';
                        break;
                    case 47:
                        character = 'L';
                        break;
                    case 48:
                        character = 'M';
                        break;
                    case 49:
                        character = 'N';
                        break;
                    case 50:
                        character = 'O';
                        break;
                    case 51:
                        character = 'P';
                        break;
                    case 52:
                        character = 'Q';
                        break;
                    case 53:
                        character = 'R';
                        break;
                    case 54:
                        character = 'S';
                        break;
                    case 55:
                        character = 'T';
                        break;
                    case 56:
                        character = 'U';
                        break;
                    case 57:
                        character = 'V';
                        break;
                    case 58:
                        character = 'W';
                        break;
                    case 59:
                        character = 'X';
                        break;
                    case 60:
                        character = 'Y';
                        break;
                    case 61:
                        character = 'Z';
                        break;
                    default:
                        character = 'z';
                        break;
                }
                System.Threading.Thread.Sleep(20);
                buidedString += character.ToString();
            }
            return buidedString;
        }
    }
}
