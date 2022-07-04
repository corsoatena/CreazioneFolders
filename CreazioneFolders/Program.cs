using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CreazioneFolders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Banca ING = new Banca("ING");
            List<Client> ING_CLIENT_LIST = new List<Client>()
            {
                new Client("Bruno","FRRBRI82483NNENR"),
                new Client("Marco", "GENGGRRK8903845ONG"),
                new Client("Elena", "DPFPEN3935994NINID")
            };
            foreach (Client client in ING_CLIENT_LIST)
            {
                ING.AddCliente(client);
            }
            ING.WriteClientsToLog();
            var ALEX = new Client("Alex", "ALXGGNFNG87GNNG8Y");
            ING.AddCliente(ALEX);
            ING.WriteClientsToLog();

            //foreach (var cliente in ING_CLIENT_LIST)
            //{
            //    ING.AddConto(cliente);

            //}
            //foreach (var cliente in ING_CLIENT_LIST)
            //{

            //    ING.Withdraw(cliente._conto ,1000M,OPERATION_FOLDER.FIAT); // 
            //}

        }
    } 

    public class Banca
    {
        public string _name { get; set; }
        public List<Conto> Conti { get; set; }
        public List<Client> Clienti { get; set; }

        static string _path = @"C:\";
        static string _logFolder = @"logs";


        public Banca(string Name)
        {
            _name = Name;
            Conti = new List<Conto>();
            Clienti = new List<Client>();

            ///CREAZION FOLDER PER LA BANCA
            string s = Path.Combine(_path, _name);
            _path = s;
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            /// creare anche la FOLDER per i LOGS
        }
        public void AddCliente(Client client)
        {
            Clienti.Add(client);
        }
        public void WriteClientsToLog()
        {
           string path =  Path.Combine(_path, _logFolder); 
           Writelog(path,Clienti);
        }
        public void AddConto(Client client)
        {
            long AccountNumber = new Random().Next(10000, 100000);
            Conti.Add(new Conto() { Client = client, _accontNumbers = AccountNumber });
            SetupClient(client, AccountNumber);            
        } 
        public void Withdraw(Conto Conto, decimal Amount, OPERATION_FOLDER OperationFolder)
        {
            string file = "Withdraw";            
            Conto._saldo -= Amount;            
            string msg = $"{DateTime.Now}: Perelevati {Amount} da {OperationFolder}";
            
        }
        private void SetupClient(Client client, long accountNumber)
        {
            string personalFolder = Path.Combine(_path, client._cf);
            Directory.CreateDirectory(personalFolder);
            SetupFolder(accountNumber, personalFolder);
        }
        private void SetupFolder(long AccountNumber, string ClientFolder)
        {
            string accountFolder = Path.Combine(ClientFolder, AccountNumber.ToString());   
            string FiatFolder = Path.Combine(accountFolder, OPERATION_FOLDER.FIAT.ToString());
            Directory.CreateDirectory(FiatFolder);
            string CryptoFolder = Path.Combine(accountFolder, OPERATION_FOLDER.CRYPTO.ToString());
            Directory.CreateDirectory(CryptoFolder);
        } 
        public static void Writelog(string path,List<Client> Clienti)
        {
           /// TODO 
        }
    }
    public class Client
    {
        public string _name { get; set; }    
        public string _cf{ get; set; }
        public Conto _conto; 
        public Client(string Name, string CF)
        {
            _name = Name;   
            _cf = CF;
            _conto = new Conto(); 
        }

    } 
    public class Conto
    {
        public Client Client { get; set; }
        public long _accontNumbers { get; set; }
        public decimal _saldo { get; set; } 
    }

    public enum OPERATION_FOLDER
    {
        CRYPTO,
        FIAT
    }
}
