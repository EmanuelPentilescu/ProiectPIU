using System;
using System.IO;
using System.Configuration;
using LibrarieFilme;
using NivelStocareDate;

namespace Cinematograf
{
    class Program
    {

        static void Main()
        {
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareFilme_FisierText adminFilme = new AdministrareFilme_FisierText(numeFisier);
            int nrFilme = 0;
            Film FilmNou = new Film();
            adminFilme.GetFilme(out nrFilme);
            string optiune;
            do
            {
                Console.WriteLine("C: Citeste filmele de la tastatura:");
                Console.WriteLine("F: Afiseaza numarul filmelor");
                Console.WriteLine("A: Afisarea ultimului film introdus");
                Console.WriteLine("S: Salveaza film in fisier");
                Console.WriteLine("X: Exit");
                Console.WriteLine("Alegeti o optiune");
                optiune = Console.ReadLine();
                switch (optiune.ToUpper())
                {
                    case "C":
                        nrFilme++;
                        FilmNou= CitesteFilmeTastatura();
                        break;

                    case "F":
                        Film[] filme = adminFilme.GetFilme(out nrFilme);
                        AfisareFilme(filme, nrFilme);
                        break;

                    case "A":
                        AfisareFilm(FilmNou);
                        break;

                    case "S":
                        int idFilm = nrFilme + 1;
                        FilmNou.SetIdFilm(idFilm);
                        adminFilme.AddFilm(FilmNou);
                        nrFilme++;
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }

            } while (optiune.ToUpper() != "X");

            Console.ReadKey();
        }

        public static Film CitesteFilmeTastatura()
        {
            Console.WriteLine("Introduceti titlul filmului:");
            string titlu = Console.ReadLine();

            Film film = new Film(0,titlu);

            return film;

        }

        public static void AfisareFilm(Film film)
        {
            string infoFilm = string.Format("Filmul cu id #{0} are numele: {1}",
                film.GetIdFilm(),
                film.GetTitluFilm() ?? "Necunoscut");
        }
        public static void AfisareFilme(Film[] filme, int nrFilme)
        {
            Console.WriteLine("Filmele sunt:");
            for (int contor = 0; contor < nrFilme; contor++)
            {
                string infoFilm = string.Format("Filmul cu id-ul #{0} are titlul: {1}",
                   filme[contor].GetIdFilm(),
                   filme[contor].GetTitluFilm() ?? " NECUNOSCUT ");

                Console.WriteLine(infoFilm);
            }
        }
    }
}
