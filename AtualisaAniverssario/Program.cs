using AtualisaAniverssario.Data;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Globalization;
using System.Linq;

namespace AtualisaAniverssario
{
    class Program
    {
        public static string connFb = "DataSource=192.168.0.21;Port=3050;Database=/banco/Matur.FDB; User=SYSDBA; Password=FPuZw9T@!0;Charset=ISO8859_1;Connection lifetime=0;Connection timeout=1;Pooling=false;";
        static void Main(string[] args)
        {
            //AtualizaAniver();
            //InabilitaUsuario();

        }

        public static void PegaIdEmpresa() { 
        
        
        }
        public static void InabilitaUsuario() 
        {
            using (var ferradura = new AtContext())
            {
                foreach (var a in ferradura.Usuarios.Where(a => a.Ativo != 1 ).ToList())
                {

                    //ESTA COM DEFEITO POIS O NOME COMPLETO DO USUÁRIO NO AD QUE MIGRA PARA O FERRAMENTAS ESTA DIFERENTE DO BANCO SISMATUR


                    //Console.WriteLine("nome = "+a.Nome.ToString());

                    //codigo de busca firebird
                    //using (FbConnection dbConn3 = new FbConnection(ConfigurationManager.ConnectionStrings["FireBirdConnMatur"].ConnectionString))
                    //{
                    //    var nomez = TrocaEsp(a.Nome);
                    //    var queryUsers = "select FUNCPESSOA.NOMEFUNC,FUNCCONTRATO.DATADEM from FUNCPESSOA " +
                    //        " inner join FUNCCONTRATO on FUNCCONTRATO.CODIGOFUNCPESSOA = FUNCPESSOA.CODIGOFUNCPESSOA " +
                    //        " where NOMEFUNC like '%"+nomez+"%'; ";
                    //    FbCommand myCommand3 = new FbCommand(queryUsers, dbConn3);
                    //    dbConn3.Open();
                    //    myCommand3.CommandTimeout = 0;
                    //    var myReaderUser3 = myCommand3.ExecuteReader();
                    //    int d = 0;
                    //    var ativo = 0;
                    //    while (myReaderUser3.Read())
                    //    {
                    //        var data = myReaderUser3.GetString(1).ToString();
                    //        if (String.IsNullOrEmpty(data))
                    //        {
                    //            ativo = 1;
                    //            Console.WriteLine("linha _______ATIVO____________" + d.ToString());
                    //            Console.WriteLine(myReaderUser3.GetString(0).ToString() + " - " + myReaderUser3.GetString(1).ToString());
                    //            d++;
                    //        }
                    //        else {
                    //            //Console.WriteLine("linha _________INATIVO__________" + d.ToString());
                    //            //Console.WriteLine(myReaderUser3.GetString(0).ToString() + " - " + myReaderUser3.GetString(1).ToString());
                    //            d++;
                    //        }

                    //    }
                    //    a.Ativo = ativo;
                    //    ferradura.SaveChanges();
                    //}


                }
                //Console.ReadLine();
            }

        }

        public static void AtualizaAniver() {
            using (var ferra = new AtContext())
            {

                int x = 1;
                foreach (var a in ferra.Usuarios.Where(a => a.Nascimento.Value == null ).ToList())
                {
                    var nome = TrocaEsp(a.Nome);
                    x++;

                    //codigo de busca firebird
                    using (FbConnection dbConn2 = new FbConnection(connFb))
                    {
                        var queryUsers = "SELECT distinct FUNCPESSOA.NOMEFUNC, FUNCPESSOA.DATANASC" +
                            " FROM FUNCPESSOA" +
                            " INNER JOIN FUNCCONTRATO ON(FUNCCONTRATO.CODIGOFUNCPESSOA = FUNCPESSOA.CODIGOFUNCPESSOA)" +
                            " Where EXTRACT(MONTH FROM FUNCPESSOA.DATANASC) = EXTRACT(month from current_date)" +
                            " and FUNCCONTRATO.DATADEM is null" +
                            " and FUNCPESSOA.NOMEFUNC LIKE '" + nome + "%';";
                        FbCommand myCommand2 = new FbCommand(queryUsers, dbConn2);
                        dbConn2.Open();
                        myCommand2.CommandTimeout = 0;
                        var myReaderUser = myCommand2.ExecuteReader();
                        while (myReaderUser.Read())
                        {
                            Console.WriteLine(myReaderUser.FieldCount.ToString());
                            if (!String.IsNullOrEmpty(myReaderUser.GetString(0).ToString()))
                            {
                                Console.WriteLine("Atualizando "+ myReaderUser.GetString(0).ToString());

                                DateTime data = Convert.ToDateTime(myReaderUser.GetString(1).ToString());
                                a.Nascimento = data;
                                ferra.SaveChanges();

                                Console.WriteLine(myReaderUser.GetString(0).ToString() + " - " + data.ToString("yyyy-MM-dd HH:mm:ss"));
                            }

                        }
                    }
                    //FileMode codigo de busca firebirt
                }
                Console.ReadLine();

            }

        }

        public static string TrocaEsp(string texto)
        {
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                texto = texto.Replace(acentos[i], semAcento[i]);
            }

            texto = texto.ToUpper(new CultureInfo("pt-BR", false));

            return texto;
        }
    }
}
