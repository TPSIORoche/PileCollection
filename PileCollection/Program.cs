using System;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PileCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //TestePileVidePleine(5);
                //TestePileVidePleine(0);
                //TesteEmpiler(5);
                //TesteEmpiler(2);
                //TesteEmpilerDepiler(5);
                //TesteConversion();
                //Console.WriteLine(ConvertActBaseToNewBase(2,111,8));
                //Console.WriteLine(ConvNewBaseToDecim(2,10));
                //Console.WriteLine(RecupereLoremIpsum(3));
                //Console.WriteLine(TesteSplit());
                //TesteInversePhrase();
                string a = "Un wiki est une application web qui permet la création, la modification et l'illustration collaboratives de pages à l'intérieur d'un site web. Il utilise un langage de balisage et son contenu est modifiable au moyen d’un navigateur web. C'est un logiciel de gestion de contenu, dont la structure implicite est minimale, tandis que la structure explicite se met en place progressivement en fonction des besoins des usagers.";
                Console.WriteLine(InversePhraseMieux2(a));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Fin du programme, Appuyez sur une touche pour terminer");
            Console.ReadKey();
        }
        struct Pile
        {
            public int maxElt;
            public ArrayList tabElem;
        }

        static void InitPile(ref Pile pUnePile, int PNbElemt)
        {
            pUnePile.maxElt = PNbElemt;
            pUnePile.tabElem = new ArrayList();
        }
        static bool PileVide(ref Pile pUnePile)
        {
            return (pUnePile.tabElem.Count == 0);
        }

        static bool PilePleine(ref Pile pUnePile)
        {
            return (pUnePile.tabElem.Count == pUnePile.maxElt);
        }

        static void Empiler(ref Pile pUnePile, Object PObj)
        {
            if (!PilePleine(ref pUnePile))
            {
                pUnePile.tabElem.Add(PObj);
            }
            else
            {
                throw new Exception("Pile pleine, impossible d'empiler un élément");
            }
        }
        static object Depiler(ref Pile pUnePile)
        {
            if (!PileVide(ref pUnePile))
            {
                int res = (int)pUnePile.tabElem[pUnePile.tabElem.Count - 1];
                pUnePile.tabElem.RemoveAt(pUnePile.tabElem.Count - 1);
                return res;
            }
            throw new Exception("Pile vide, impossible de d'épiler un élément");


        }

        static string Convertir(int pNbElements, int pNbAConvertir, int pNewbase)
        {
            Pile unePile = new Pile();

            InitPile(ref unePile, pNbElements);
            int a = pNbAConvertir;
            int r;
            string resultat = "";

            while (a > 0)
            {
                r = a % pNewbase;
                Empiler(ref unePile, r);
                a /= pNewbase;
            }
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            while (!PileVide(ref unePile))
            {
                if ((int)unePile.tabElem[unePile.tabElem.Count - 1] > 9)
                //if ((int)Depiler(ref unePile) > 9)
                {
                    int alte = ((int)Depiler(ref unePile) % pNewbase) - 10;
                    resultat += alpha[alte];
                }
                else
                {
                    resultat += Depiler(ref unePile).ToString();
                }
            }
            return resultat;
        }

        static void TestePileVidePleine(int nbElements)
        {
            Pile unePile = new Pile();
            InitPile(ref unePile, nbElements);
            if (PileVide(ref unePile))
            {
                Console.WriteLine("la pile est vide");
            }
            else
            {
                Console.WriteLine("la pile n'est pas vide");
            }
            if (PilePleine(ref unePile))
            {
                Console.WriteLine("la pile est pleine");
            }
            else
            {
                Console.WriteLine("la pile n'est pas pleine");
            }
        }

        static void TesteEmpiler(int nbElements)
        {
            //try
            //{
            Pile unePile = new Pile();
            InitPile(ref unePile, nbElements);
            Empiler(ref unePile, 2);
            Empiler(ref unePile, 14);
            Empiler(ref unePile, 6);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        static void TesteConversion()
        {
            Console.Write("Entrez le nombre d'éléments du tableau : ");
            string input = Console.ReadLine();
            int a = int.Parse(input);
            Console.Write("Entrez le nombre à convertir : ");
            string input2 = Console.ReadLine();
            int b = int.Parse(input2);
            Console.Write("Entrez la nouvelle base entre 2 et 16 : ");
            string input3 = Console.ReadLine();
            int c = int.Parse(input3);
            while (2 > int.Parse(input3) || (16 < int.Parse(input3)))
            {
                Console.Write("Entrez la nouvelle base entre 2 et 16 : ");
                input3 = Console.ReadLine();
                c = int.Parse(input3);
            }
            if (Math.Pow(c, a) < b)
            {
                Console.Write("Impossible de convertir, la pile est trop petite");
            }
            else
            {
                string d = Convertir(a, b, c);
                Console.Write($"La valeur de {b} (base 10) vaut {d} en base {c}\n");
            }
        }

        static void TesteEmpilerDepiler(int nbElements)
        {
            try
            {
                Pile unePile = new Pile();
                InitPile(ref unePile, nbElements);
                Empiler(ref unePile, 2);
                Empiler(ref unePile, 22);
                int valeurDepilee = (int)Depiler(ref unePile);
                Console.WriteLine("valeur dépilée : " + valeurDepilee);
                Empiler(ref unePile, 17);
                while (!PileVide(ref unePile))
                {
                    valeurDepilee = (int)Depiler(ref unePile);
                    //valeurDepilee = (int)Depiler(ref unePile);
                    //valeurDepilee = (int)Depiler(ref unePile);
                    //valeurDepilee = (int)Depiler(ref unePile);
                }
                Console.WriteLine(valeurDepilee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////


        static int ConvDecimToNewBase(int pNbAConvertir, int pNewbase)
        {
            int i = 1;
            while (Math.Pow(pNewbase, i) < pNbAConvertir)
            {
                i++;
            }
            Pile unePile = new Pile();
            InitPile(ref unePile, i);
            int a = pNbAConvertir;
            int r;
            if (a == 0)
            {
                return 0;
            }
            while (a > 0)
            {
                r = a % pNewbase;
                Empiler(ref unePile, r);
                a /= pNewbase;
            }
            string resultat = "";
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            while (!PileVide(ref unePile))
            {
                if ((int)unePile.tabElem[unePile.tabElem.Count - 1] > 9)
                {
                    int alte = ((int)Depiler(ref unePile) % pNewbase) - 10;
                    resultat += alpha[alte];

                }
                else
                {
                    resultat += Depiler(ref unePile).ToString();
                }
            }
            return int.Parse(resultat);
        }

        static int ConvNewBaseToDecim(int pNewbase, int pNbAConvertir)
        {
            double resultat = 0;
            string nb = pNbAConvertir.ToString();
            int n = nb.Length;
            for (int i = 0; i < n; i++)
            {
                int nbloc = int.Parse(nb[i].ToString());
                resultat += nbloc * (Math.Pow(pNewbase, (n - i - 1)));
            }
            return (int)resultat;
        }

        static int ConvertActBaseToNewBase(int pBase, int pNbAConvertir, int pNewbase)
        {
            return (ConvDecimToNewBase(ConvNewBaseToDecim(pBase, pNbAConvertir), pNewbase));
        }
        /////////////////////////////////////////////////////////////////////////////////////////

        static String RecupereLoremIpsum(int nbParagraphes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/text"));
            string url = $"https://loripsum.net/api/{nbParagraphes}/short/plaintext";
            var reponse = client.GetAsync(url).Result;

            if (reponse.IsSuccessStatusCode)
            {
                string responseBody = reponse.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            else
            {
                throw new Exception("Erreur API : " + reponse.StatusCode + " " + reponse.ReasonPhrase);
            }
        }

        //HttpClient client = new HttpClient();  client represente un objet de type HttpClient
        //Http
        //un booleen
        static void TesteSplit()
        {
            String phrase = "Il fait toujours beau Toulon";
            var tab = phrase.Split(';');
        }

        static string InversePhrase(string phrase)
        {
            Pile maPile = new Pile();
            InitPile(ref maPile, 200);
            var tab = phrase.Split(' ');
            foreach (string mot in tab)
            {
                Empiler(ref maPile, mot);
            }
            string message = "";
            while (!PileVide(ref maPile))
            {
                message += " " + Depiler(ref maPile);
            }
            return message;
        }

        static void TesteInversePhrase()
        {
            try
            {
                string phrase = RecupereLoremIpsum(3);
                Console.WriteLine(phrase);
                String phraseInversee = InversePhrase(phrase);
                Console.WriteLine("\n Version Pile");
                Console.WriteLine(phraseInversee);
                Console.WriteLine(phraseInversee);
                phraseInversee = InversePhraseMieux(phrase);
                Console.WriteLine("\n Version Améliorée");
                Console.WriteLine(phraseInversee);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        static string InversePhraseMieux(string phrase)
        {
            var tab = phrase.Split(' ');
            int i = tab.Length - 1;
            string message = "";
            while (i >= 1)
            {
                message += tab[i] + " ";
                i -= 1;
            }
            message += tab[i];
            return message;
        }

        static string InversePhraseMieux2(string phrase)
        {
            var tab = phrase.Split(' ');
            string message = "";
            for (int i = tab.Length - 1; i > -1; --i)
            {
                message += tab[i] + " ";
            }
            return message;

        }
    }
}
