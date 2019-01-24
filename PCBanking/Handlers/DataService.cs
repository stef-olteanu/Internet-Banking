using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    class DataService
    {
        #region Proprietati Private
        private string _connectionString;
        #endregion


        #region Constructor
        public DataService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["atmBank"].ConnectionString;
        }
        #endregion


        #region DatabaseInteraction


        public void ModifyName(int id, string new_name)
        {


            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Utilizatori SET FullName=@New WHERE IdUtilizator=@Id";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@New",
                        Value = new_name
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id
                    }
                );

                var reader = command.ExecuteReader();

            }


        }

        public void ModifySerie(int id, string new_serie)
        {


            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Utilizatori SET SerieNumarBuletin=@New WHERE IdUtilizator=@Id";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@New",
                        Value = new_serie
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id
                    }
                );

                var reader = command.ExecuteReader();

            }


        }


        public void ModifyAdress(int id, string new_adress)
        {


            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Utilizatori SET AdresaBuletin=@New WHERE IdUtilizator=@Id";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@New",
                        Value = new_adress
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id
                    }
                );

                var reader = command.ExecuteReader();

            }


        }


        public void ModifyCountry(int id, string new_country)
        {


            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Utilizatori SET Tara=@New WHERE IdUtilizator=@Id";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@New",
                        Value = new_country
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id
                    }
                );

                var reader = command.ExecuteReader();

            }


        }

        public void ActivateServ(int id, string nume_serviciu)
        {

            int id_serviciu = 0;
            int verif = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT IdServiciu from Servicii where NumeServiciu=@New";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@New",
                        Value = nume_serviciu
                    }
                );

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id_serviciu = reader.GetInt32(0);
                }
                connection.Close();

                connection.Open();
                var command3 = connection.CreateCommand();
                command3.CommandType = CommandType.Text;
                command3.CommandText = "SELECT COUNT(StareServiciu) FROM ServiciiFolosite WHERE IdProprietar=@IdP and IdServiciu=@IdS";

                command3.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@IdP",
                        Value = id
                    }
                );

                command3.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@IdS",
                        Value = id_serviciu
                    }
                );

                var reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    verif = reader3.GetInt32(0);
                }
                connection.Close();




                if (verif == 0)
                {

                    connection.Open();
                    var command2 = connection.CreateCommand();
                    command2.CommandType = CommandType.Text;
                    command2.CommandText = "INSERT INTO ServiciiFolosite([IdProprietar],[IdServiciu],[StareServiciu]) VALUES (@IdP,@IdS ,'Activ')";

                    command2.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@IdP",
                            Value = id
                        }
                    );

                    command2.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@IdS",
                            Value = id_serviciu
                        }
                    );

                    var reader2 = command2.ExecuteReader();


                    connection.Close();
                }
                else
                {
                    connection.Open();
                    var command2 = connection.CreateCommand();
                    command2.CommandType = CommandType.Text;
                    command2.CommandText = "UPDATE ServiciiFolosite SET StareServiciu='Activ' WHERE IdProprietar=@IdP and IdServiciu=@IdS";

                    command2.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@IdP",
                            Value = id
                        }
                    );

                    command2.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@IdS",
                            Value = id_serviciu
                        }
                    );

                    var reader2 = command2.ExecuteReader();


                    connection.Close();

                }

            }




        }



        public int GetUserId(string username)
        {
            int id = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT  IdUtilizator FROM Utilizatori where NumeUtilizator=@user";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@user",
                        Value = username
                    }
                );

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                connection.Close();
            }

            return id;

        }
        public ObservableCollection<string> GetAllTaxes(int id_prop)
        {
            ObservableCollection<string> _tax = new ObservableCollection<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = " SELECT Servicii.NumeServiciu, ServiciiFolosite.StareServiciu FROM  Servicii INNER JOIN ServiciiFolosite ON    Servicii.IdServiciu =ServiciiFolosite.IdServiciu where IdProprietar=@Id";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id_prop
                    }
                );

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _tax.Add(reader.GetString(0) + "\t----> " + reader.GetString(1));
                }

            }

            return _tax;
        }

        public ObservableCollection<string> GetServicii()
        {
            ObservableCollection<string> _serv = new ObservableCollection<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT NumeServiciu FROM Servicii";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _serv.Add(reader.GetString(0));


                }

                connection.Close();
            }

            return _serv;
        }



        public string Convert(string from,string to,float amount)
        {
           
            double x=0, y=0;
           

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
               

                if (from == "RON")
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT Cumparare FROM CursuriValutare WHERE IdMoneda = (select IdMoneda from Monezi where Moneda = @Cumparare";

                    command.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@Cumparare",
                            Value = to
                        }
                    );

                    var reader3 = command.ExecuteReader();
                    while (reader3.Read())
                    {
                        x = reader3.GetDouble(0);
                    }
                    double a = (double)amount / x;
                    return a.ToString();
                }

                if (to == "RON")
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT Vanzare FROM CursuriValutare WHERE IdMoneda = (select IdMoneda from Monezi where Moneda = @Vanzare)";

                    command.Parameters.Add(
                        new SqlParameter
                        {
                            ParameterName = "@Vanzare",
                            Value = from
                        }
                    );

                    var reader3 = command.ExecuteReader();
                    while (reader3.Read())
                    {
                        x = reader3.GetDouble(0);
                    }
                    double a = (double)amount * x;
                    return a.ToString();
                }

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Cumparare FROM CursuriValutare WHERE IdMoneda = ( select IdMoneda from Monezi where Moneda = @Vanzare)";
                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Vanzare",
                        Value = from
                    }
                );

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    double b;
                    b= reader.GetDouble(0);
                    x = b;
                }

                connection.Close();

                connection.Open();
                var command2 = connection.CreateCommand();
               
                command2.CommandType = CommandType.Text;
                command2.CommandText = "SELECT Vanzare FROM CursuriValutare WHERE IdMoneda = ( select IdMoneda from Monezi where Moneda = @Cumparare )";

                command2.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Cumparare",
                        Value = to
                    }
                );

                var reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    y = reader2.GetDouble(0);
                }



                connection.Close();
            }
            double _rez = ((double)amount * x) / y;

            return _rez.ToString() ;
        }


        public ObservableCollection<string> GetCurrency()
        {
            ObservableCollection<string> _cur = new ObservableCollection<string>();
           // _cur.Add(" Moneda\t             Vanzare\t        Cumparare");
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = @"select M.Moneda, Vanzare, Cumparare from CursuriValutare as C
                                        inner join Monezi as M
                                        on M.IdMoneda = C.IdMoneda";

                var reader = command.ExecuteReader();
                int i= 0;
                while (reader.Read())
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    else {
                        double aux1, aux2;
                    string a = reader.GetString(0);
                    aux1 = reader.GetDouble(1);
                    aux2 = reader.GetDouble(2);
                    _cur.Add("   "+a+"\t\t" +aux1.ToString()+"\t         " +aux2.ToString());

                    }
                    
                }

                connection.Close();
            }

            return _cur;
        }


        public ObservableCollection<string> GetAllCurrency()
        {
            ObservableCollection<string> _stat = new ObservableCollection<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = @"select M.Moneda, Vanzare, Cumparare from CursuriValutare as C
                                        inner join Monezi as M
                                        on M.IdMoneda = C.IdMoneda";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _stat.Add(reader.GetString(0));

                }

                connection.Close();
            }

            return _stat;
        }


        public ObservableCollection<string> GetAllStatus()
        {
            ObservableCollection<string> _statuses = new ObservableCollection<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select NumeStatus from StatusFinanciar";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _statuses.Add(reader.GetString(0));


                }

                connection.Close();
            }

            return _statuses;
        }

        public int getStatusId(string _Status)
        {
            int id = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select IdStatusFinanciar from StatusFinanciar where NumeStatus = @StatusFinanciar";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@StatusFinanciar",
                        Value = _Status
                    }
                );

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                connection.Close();
            }

            return id;
        }


        public ObservableCollection<User> GetAllUsers()
        {
            ObservableCollection<User> _users = new ObservableCollection<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Utilizatori as U inner join StatusFinanciar on U.IdTaxa = IdStatusFinanciar";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _users.Add(
                    new User
                    {
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        SerieNumar = reader.GetString(3),
                        Adresa = reader.GetString(4),
                        Tara = reader.GetString(5),
                        StatusFinanciar = reader.GetString(9)
                    });
                }

                connection.Close();
            }

            return _users;
        }

        public void ChangeOldPass(string _userName,string _newpass,string SerieBuletin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "update Utilizatori set Parola=@NewPassword where NumeUtilizator=@username and SerierNumarBuletin=@serie";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@NewPassword",
                        Value = _newpass
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@username",
                        Value = _userName
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@serie",
                        Value = SerieBuletin
                    }
                );


                command.ExecuteNonQuery();
                connection.Close();
            }



        }

        public void AddPerson(string _userName, string _password, string _serie, string _adresa, string _tara, int _idTaxa, string _fullName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO Utilizatori VALUES(@NumeUtilizator, @Parola, @SerieBuletin, @Adresa, @Tara, @IdTaxa,@FullName)";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@NumeUtilizator",
                        Value = _userName
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@FullName",
                        Value = _fullName
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Parola",
                        Value = _password
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@SerieBuletin",
                        Value = _serie
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Adresa",
                        Value = _adresa
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Tara",
                        Value = _tara
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@IdTaxa",
                        Value = _idTaxa
                    }
                );

                command.ExecuteNonQuery();
                connection.Close();
            }
        }




        public User GetUser(string _userName)
        {
            User _returnUser = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select* from Utilizatori as U inner join StatusFinanciar on U.IdTaxa = IdStatusFinanciar where NumeUtilizator = @NumeUtilizator";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@NumeUtilizator",
                        Value = _userName
                    }
                );

                var reader = command.ExecuteReader();


                if (reader != null)
                {
                    while (reader.Read())
                    {
                        _returnUser = new User
                        {
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            SerieNumar = reader.GetString(3),
                            Adresa = reader.GetString(4),
                            Tara = reader.GetString(5),
                            StatusFinanciar = reader.GetString(9),
                            FullName = reader.GetString(7) // ------------------------- Camp 7 FULLNAME-----------------
                        };
                    }
                }
                else
                {
                    _returnUser = null;
                }


                connection.Close();

            }
            return _returnUser;
        }


        public List<string> GetCardsNumbers(string username)
        {
            List<string> cardNumbers = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"  select NumarCard
                                                         from Carduri as C
                                                         inner join Utilizatori as U
                                                         on C.IdProprietar = U.IdUtilizator
                                                         and U.NumeUtilizator = @username", connection)
                {
                    CommandType = CommandType.Text,
                };

                command.Parameters.Add(new SqlParameter("@username", username));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cardNumbers.Add((string)reader["NumarCard"]);
                    }
                }
            }

            return cardNumbers;
        }

        public List<string> GetTransactions(string username)
        {
            List<string> Transactions = new List<string>();
            string AuxIBAN = string.Empty;
            string AuxIBAN2 = string.Empty;
            string AuxIBAN3 = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();

                SqlCommand command1 = new SqlCommand(@"  select Conturi.IBAN from Conturi
                                                        inner join Utilizatori as U
                                                        on Conturi.IdProprietar=U.IdUtilizator
                                                        where U.NumeUtilizator=@username and IdTipCont = 1", connection)
                {
                    CommandType = CommandType.Text,
                };

                command1.Parameters.Add(new SqlParameter("@username", username));

                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    string Cont = string.Empty;
                    while (reader.Read())
                    {
                        Cont = (string)reader["IBAN"];
                        AuxIBAN2 = Cont;
                    }
                }

                connection.Close();


                connection.Open();

                SqlCommand command2 = new SqlCommand(@"  select Conturi.IBAN from Conturi
                                                        inner join Utilizatori as U
                                                        on Conturi.IdProprietar=U.IdUtilizator
                                                        where U.NumeUtilizator=@username and IdTipCont = 2", connection)
                {
                    CommandType = CommandType.Text,
                };

                command2.Parameters.Add(new SqlParameter("@username", username));

                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    string Cont = string.Empty;
                    while (reader.Read())
                    {
                        Cont = (string)reader["IBAN"];
                        AuxIBAN3 = Cont;
                    }
                }

                connection.Close();





                connection.Open();


                SqlCommand command = new SqlCommand(@"   select T.IdTranzactie,T.SumaTransferata,T.DataTransfer,T.IBANDestinatar,T.IBANExpeditor from Tranzactii as T
                                                    inner join Utilizatori as U
                                                    on U.IdUtilizator=T.IdExpeditor or U.IdUtilizator=T.IdDestinatar
                                                    where U.NumeUtilizator = @username", connection)

                {
                    CommandType = CommandType.Text,
                };

                command.Parameters.Add(new SqlParameter("@username", username));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int Id;
                        double Suma;
                        DateTime Data;
                        string IbanE;
                        string IbanD;  


                        Id = (int)reader["IdTranzactie"];
                        Suma = (double)reader["SumaTransferata"];
                        Data = (DateTime)reader["DataTransfer"];
                        IbanD = (string)reader["IBANDestinatar"];
                        IbanE = (string)reader["IBANExpeditor"];
                        AuxIBAN = IbanD;

                        if (AuxIBAN.ToString() == AuxIBAN2 || AuxIBAN.ToString() == AuxIBAN3)
                        {
                            string Total = "ID: " + Id.ToString() + " SUMA: +" + Suma.ToString() + " DATA: " + Data.ToString() + " IBAN Expeditor: " + IbanE.ToString() + " IBAN Destinatar: " + IbanD.ToString();
                            Transactions.Add(Total);
                        }
                        else
                        {
                            string Total = "ID: " + Id.ToString() + " SUMA: -" + Suma.ToString() + " DATA: " + Data.ToString() + " IBAN Expeditor: " + IbanE.ToString() + " IBAN Destinatar: " + IbanD.ToString();
                            Transactions.Add(Total);

                        }



                    }
                }
                connection.Close();



                return Transactions;

            }

        }




        public List<string> GetContNumbers(string username)
        {
            List<string> contNumbers = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"  select Conturi.IBAN from Conturi
                                                        inner join Utilizatori as U
                                                        on Conturi.IdProprietar=U.IdUtilizator
                                                        where U.NumeUtilizator=@username and IdTipCont=1", connection)
                {
                    CommandType = CommandType.Text,
                };

                command.Parameters.Add(new SqlParameter("@username", username));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string Cont = string.Empty;
                    while (reader.Read())
                    {
                        Cont = (string)reader["IBAN"];
                        Cont = Cont + " Cont Curent";
                        contNumbers.Add(Cont);
                    }
                }

                connection.Close();

                connection.Open();

                SqlCommand command1 = new SqlCommand(@"  select Conturi.IBAN from Conturi
                                                        inner join Utilizatori as U
                                                        on Conturi.IdProprietar=U.IdUtilizator
                                                        where U.NumeUtilizator=@username and IdTipCont = 2", connection)
                {
                    CommandType = CommandType.Text,
                };

                command1.Parameters.Add(new SqlParameter("@username", username));

                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    string Cont1 = string.Empty;
                    while (reader.Read())
                    {
                        Cont1 = (string)reader["IBAN"];
                        Cont1 = Cont1 + " Cont Economii";
                        contNumbers.Add(Cont1);
                    }
                }

                connection.Close();






            }


            return contNumbers;
        }




        public string GetMoney(string username, string ItemSelected)
        {
            string SumaCont = string.Empty;
            string SumaTotala = string.Empty;

            string[] moneysplited = ItemSelected.Split(' ');

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                if (moneysplited[2] == "Curent")
                {
                    SqlCommand command = new SqlCommand(@"select SumaCont from Conturi as C
                                                    inner join Utilizatori as U
                                                    on U.IdUtilizator=C.IdProprietar and U.NumeUtilizator = @username and IBAN= @iban and C.IdTipCont = 1", connection)

                    {
                        CommandType = CommandType.Text,
                    };

                    command.Parameters.Add(new SqlParameter("@username", username));
                    command.Parameters.Add(new SqlParameter("@iban", moneysplited[0]));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double aux = reader.GetDouble(0);
                            SumaCont = aux.ToString();


                        }




                    }

                }
                else if (moneysplited[2] == "Economii")
                {
                    SqlCommand command = new SqlCommand(@"select SumaCont from Conturi as C
                                                    inner join Utilizatori as U
                                                    on U.IdUtilizator=C.IdProprietar and U.NumeUtilizator = @username and IBAN= @iban and C.IdTipCont = 2", connection)

                    {
                        CommandType = CommandType.Text,
                    };

                    command.Parameters.Add(new SqlParameter("@username", username));
                    command.Parameters.Add(new SqlParameter("@iban", moneysplited[0]));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double aux = reader.GetDouble(0);
                            SumaCont = aux.ToString();


                        }




                    }


                }
                connection.Close();

                connection.Open();

                SqlCommand command1 = new SqlCommand(@"select Moneda from Monezi as M
                                                        inner join Conturi as C
                                                        on C.IdMoneda = M.IdMoneda
                                                        inner join Utilizatori as U
                                                        on C.IdProprietar = U.IdUtilizator
                                                        where U.NumeUtilizator = @username", connection)

                {
                    CommandType = CommandType.Text,
                };

                command1.Parameters.Add(new SqlParameter("@username", username));
                string aux1 = string.Empty;
                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aux1 = reader.GetString(0);

                    }




                }



                SumaTotala = SumaCont + " " + aux1;




                return SumaTotala;

            }
        }
#endregion



        #region Fidelity Interaction


        public void AddFidelityCard(string numeCard, string codCard, string frontPath, string backPath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO CarduriFidelitate VALUES(@NumeCard, @CodCard, @BackPath, @FrontPath)";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@NumeCard",
                        Value = numeCard
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@CodCard",
                        Value = codCard
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@BackPath",
                        Value = backPath
                    }
                );

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@FrontPath",
                        Value = frontPath
                    }
                );

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public ObservableCollection<CardFidelitate> GetFidelityCards()
        {
            ObservableCollection<CardFidelitate> _cards = new ObservableCollection<CardFidelitate>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from CarduriFidelitate";

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _cards.Add(
                    new CardFidelitate
                    {
                        NameCard = reader.GetString(1),
                        CodCard = reader.GetString(2),
                        FrontPath = reader.GetString(4),
                        BackPath = reader.GetString(3)
                    });
                }

                connection.Close();
            }

            return _cards;
        }


        #endregion


        #region Plati Interaction

        public PlataSalvata GetPlata(string name)
        {
            PlataSalvata _returnPlata = new PlataSalvata();
            _returnPlata = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select NumePlata, IBANDestinatar, BancaDestinatar from Plati inner join Utilizatori on Utilizatori.IdUtilizator = Plati.IdUtilizator where Utilizatori.NumeUtilizator = @UserName and Plati.NumePlata = @NumePlata";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@UserName",
                        Value = MasterUser.Instance.UserName
                    });

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@NumePlata",
                        Value = name
                    });

                var reader = command.ExecuteReader();

                if(reader!=null)
                {
                    while(reader.Read())
                    {
                        _returnPlata = new PlataSalvata
                        {
                            NumePlata = reader.GetString(0),
                            IbanDestinatar = reader.GetString(1),
                            BancaDestinatar = reader.GetString(2)

                        };
                    }
                }
                connection.Close();
            }
            return _returnPlata;
        }

        public void InsertPlata(PlataSalvata plata)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Plati values((select IdUtilizator from Utilizatori where NumeUtilizator = @MasterName), @NumePlata,@IBANDestinatar, @BancaDestinatar) ";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@MasterName",
                        Value = MasterUser.Instance.UserName
                    });


                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@NumePlata",
                        Value = plata.NumePlata
                    });


                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@IBANDestinatar",
                        Value = plata.IbanDestinatar
                    });

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@BancaDestinatar",
                        Value = plata.BancaDestinatar
                    });

                command.ExecuteNonQuery();
                connection.Close();

            }
        }


        public ObservableCollection<PlataSalvata> GetPlati()
        {
            ObservableCollection<PlataSalvata> _temp = new ObservableCollection<PlataSalvata>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select NumePlata, IBANDestinatar, BancaDestinatar from Plati as P inner join Utilizatori as U on U.IdUtilizator = P.IdUtilizator where U.FullName = @MasterUserName";

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@MasterUserName",
                    Value = MasterUser.Instance.FullName
                });

                var reader = command.ExecuteReader();

                if(reader != null)
                {
                    while(reader.Read())
                    {
                        _temp.Add(new PlataSalvata()
                        {
                            NumePlata = reader.GetString(0),
                            IbanDestinatar = reader.GetString(1),
                            BancaDestinatar = reader.GetString(2)
                        });
                    }
                }
                connection.Close();
            }
            return _temp;
        }


        public void RemovePlata(string plata)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from plati where NumePlata = @NumePlata";

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@NumePlata",
                    Value = plata
                });

                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        #endregion


        #region Accounts Interaction

        public ObservableCollection<Account> GetAccountsForMasterUser()
        {
            ObservableCollection<Account> UserAccounts = new ObservableCollection<Account>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select U.FullName, IBAN, SumaCont, M.Moneda, T.TipCont from Conturi as C " +
                                        "inner join Utilizatori as U " +
                                        "on U.IdUtilizator = c.IdProprietar "+
                                        "inner join Monezi as M "+
                                        "on M.IdMoneda = C.IdMoneda "+
                                        "inner join TipuriCont as T "+
                                        "on T.IdTip = C.IdTipCont "+
                                        "where U.NumeUtilizator = @UserName";

                command.Parameters.Add(
                  new SqlParameter
                  {
                      ParameterName = "@UserName",
                      Value = MasterUser.Instance.UserName
                  });


                  var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    UserAccounts.Add(
                        new Account
                        {
                            NumeProprietar = reader.GetString(0),
                            IBAN = reader.GetString(1),
                            SumaCont = reader.GetDouble(2),
                            Moneda = reader.GetString(3),
                            TipCont = reader.GetString(4)

                        });
                }

                connection.Close();
            }
            return UserAccounts;
        }


        public void CreateTranzaction(string IBANExpeditor, string IBANDestinatar, string Detalii, double Suma, string tip)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                if(tip == "Catre utilizator")
                    command.CommandText = "insert into Tranzactii values ((select IdProprietar from Conturi where IBAN = @IBANExpeditor) , (select IdProprietar from Conturi where IBAN = @IBANDestinatar) , 'BATM', GETDATE(), @Detalii, @IBANExpeditor, @IBANDestinatar, @Suma, (select IdTip from TipuriTranzactii where TipTranzactie = @TipTranzactie))";
                else
                    command.CommandText = "insert into Tranzactii values ((select IdProprietar from Conturi where IBAN = @IBANExpeditor) , (select IdFurnizor from Furnizori where IBAN = @IBANDestinatar) , 'BATM', GETDATE(), @Detalii, @IBANExpeditor, @IBANDestinatar, @Suma, (select IdTip from TipuriTranzactii where TipTranzactie = @TipTranzactie))";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@IBANExpeditor",
                        Value = IBANExpeditor
                    });

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@IBANDestinatar",
                        Value = IBANDestinatar
                    });

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Detalii",
                        Value = Detalii
                    });

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Suma",
                        Value = Suma
                    });

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@TipTranzactie",
                        Value = tip
                    });

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateAccounts(string IBANExpeditor, string IBANDestinatar, double Sum)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = "update Conturi set SumaCont -= @Sum  where IBAN = @IBANExpeditor ";

                command.Parameters.Add(
                   new SqlParameter
                   {
                       ParameterName = "@IBANExpeditor",
                       Value = IBANExpeditor
                   });

                if (IBANDestinatar != null)
                {
                    command.Parameters.Add(
                       new SqlParameter
                       {
                           ParameterName = "@IBANDestinatar",
                           Value = IBANDestinatar
                       });
                }

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@Sum",
                        Value = Sum
                    });

                command.ExecuteNonQuery();

                if (IBANDestinatar != null)
                {
                    command.CommandText = "update Conturi set SumaCont += @Sum  where IBAN = @IBANDestinatar ";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public Account GetAccountByIBAN(string IBAN)
        {
            Account _tempAccount;
            _tempAccount = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = command.CommandText = "select U.FullName, IBAN, SumaCont, M.Moneda, T.TipCont from Conturi as C " +
                                        "inner join Utilizatori as U " +
                                        "on U.IdUtilizator = c.IdProprietar " +
                                        "inner join Monezi as M " +
                                        "on M.IdMoneda = C.IdMoneda " +
                                        "inner join TipuriCont as T " +
                                        "on T.IdTip = C.IdTipCont " +
                                        "where C.IBAN = @IBAN";

                command.Parameters.Add(
                    new SqlParameter
                    {
                    ParameterName = "@IBAN",
                    Value = IBAN
                    
                    });

                var reader = command.ExecuteReader();
                if(reader != null)
                {
                    while(reader.Read())
                    {
                        _tempAccount = new Account()
                        {
                            NumeProprietar = reader.GetString(0),
                            IBAN = reader.GetString(1),
                            SumaCont = reader.GetDouble(2),
                            Moneda = reader.GetString(3),
                            TipCont = reader.GetString(4)
                        };
                    }
                }
                connection.Close();

            }
            return _tempAccount;
        }


        #endregion


        #region Furnizori Interaction
        public ObservableCollection<string> GetAbonatiForFurnizor(string FurnizorName)
        {
            ObservableCollection<string> _temp = new ObservableCollection<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "use BancaATM select CodAbonat from AbonatiFurnizori as A " +
                                        "inner join Furnizori as F " +
                                        "on F.IdFurnizor = A.IdFurnizor " +
                                        "where F.NumeFurnizor = @FurnizorName";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@FurnizorName",
                        Value = FurnizorName
                    });

                var reader = command.ExecuteReader();
                if(reader != null)
                {
                    while(reader.Read())
                    {
                        _temp.Add(reader.GetString(0));
                    }
                }
                connection.Close();

            }
            return _temp;
        }

        public ObservableCollection<Furnizor> GetFurnizors()
        {
            ObservableCollection<Furnizor> _temp = new ObservableCollection<Furnizor>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Furnizori";

                var reader = command.ExecuteReader();
                if(reader != null)
                {
                    while(reader.Read())
                    {
                        _temp.Add(
                            new Furnizor
                            {
                                NumeFurnizor = reader.GetString(1),
                                IBAN = reader.GetString(2),
                                CoduriAbonati = GetAbonatiForFurnizor(reader.GetString(1))
                            });
                    }
                }
                connection.Close();
            }
            return _temp;

        }

        #endregion


        #region Plati Planificate Interaction

        public ObservableCollection<PlataPlanificataUtilizator> GetPlatiPlanificateUtilizator()
        {
            ObservableCollection<PlataPlanificataUtilizator> _temp = new ObservableCollection<PlataPlanificataUtilizator>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select P.IdPlata, Ut.FullName, P.DataIncepere, P.FrecventaZile, P.Suma, P.IBANSursa, P.LastPayed from PlatiPlanificate as P inner join Conturi as C on C.IdCont = P.IdContUtilizator inner join Utilizatori as Ut on Ut.IdUtilizator = C.IdProprietar inner join Utilizatori as U on U.IdUtilizator = P.IdUtilizator where U.FullName = @MasterUserName";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@MasterUserName",
                        Value = MasterUser.Instance.FullName
                    });

                var reader = command.ExecuteReader();

                if(reader != null)
                {
                    while(reader.Read())
                    {
                        _temp.Add(new PlataPlanificataUtilizator()
                        {
                            IdPlata = reader.GetInt32(0),
                            NumeUtilizator = reader.GetString(1),
                            DataIncepere = reader.GetDateTime(2),
                            FrecventaZile = reader.GetInt32(3),
                            Suma = reader.GetDouble(4),
                            IBANSursa = reader.GetString(5),
                            LastPayed = reader.GetDateTime(6)
                        });
                    }
                }

                connection.Close();


            }
            return _temp;
        }


        public ObservableCollection<PlataPlanificataFurnizor> GetPlatiPlanificateFurnizor()
        {
            ObservableCollection<PlataPlanificataFurnizor> _temp = new ObservableCollection<PlataPlanificataFurnizor>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select  P.IdPlata,F.NumeFurnizor, DataIncepere, FrecventaZile, Suma, IBANSursa, LastPayed from PlatiPlanificate as P inner join Furnizori as F on F.IdFurnizor = P.IdFurnizor inner join Utilizatori as U on U.IdUtilizator = P.IdUtilizator where U.FullName = @MasterUserName";

                command.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "@MasterUserName",
                        Value = MasterUser.Instance.FullName
                    });

                var reader = command.ExecuteReader();

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        _temp.Add(new PlataPlanificataFurnizor()
                        {
                            IdPlata = reader.GetInt32(0),
                            NumeFurnizor = reader.GetString(1),
                            DataIncepere = reader.GetDateTime(2),
                            FrecventaZile = reader.GetInt32(3),
                            Suma = reader.GetDouble(4),
                            IBANSursa = reader.GetString(5),
                            LastPayed = reader.GetDateTime(6)
                        });
                    }
                }

                connection.Close();


            }
            return _temp;
        }

        public void RemovePlataPlanificata(int idPlata)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from PlatiPlanificate where IdPlata = @IdPlata";

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "IdPlata",
                    Value = idPlata
                });

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddPlataPlanificataUtilizator(string IBANDestinatie, double Suma, DateTime data, int frecventa, string IBANSursa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into PlatiPlanificate(IdUtilizator,IdContUtilizator,DataIncepere,FrecventaZile,Suma, IBANSursa, LastPayed) values ((select IdUtilizator from Utilizatori where NumeUtilizator = @MasterUserFullname),(select IdCont from Conturi where IBAN = @IBANDestinatie),@Data,@Frecventa,@Suma,@IBANSursa,@Data)";

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@MasterUserFullName",
                    Value = MasterUser.Instance.UserName
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IBANDestinatie",
                    Value = IBANDestinatie
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IBANSursa",
                    Value = IBANSursa
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Suma",
                    Value = Suma
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Data",
                    Value = data
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Frecventa",
                    Value = frecventa
                });

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddPlataPlanificataFurnizor(string IBANDestinatie, double Suma, DateTime data, int frecventa,string IBANSursa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into PlatiPlanificate(IdUtilizator,IdFurnizor,DataIncepere,FrecventaZile,Suma, IBANSursa, LastPayed) values ((select IdUtilizator from Utilizatori where Fullname = @MasterUserFullname),(select IdFurnizor from Furnizori where IBAN = @IBANDestinatie),@Data,@Frecventa,@Suma,@IBANSursa,@Data)";

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@MasterUserFullName",
                    Value = MasterUser.Instance.FullName
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IBANDestinatie",
                    Value = IBANDestinatie
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IBANSursa",
                    Value = IBANSursa
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Suma",
                    Value = Suma
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Data",
                    Value = data
                });

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Frecventa",
                    Value = frecventa
                });

                command.ExecuteNonQuery();
                connection.Close();
            }
        }



        public string GetIBANByIdPlataForUser(int id)
        {
            string iban = string.Empty;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select IBAN from Conturi where IdCont = ( select IdContUtilizator from PlatiPlanificate where IdPlata = @IdPlata)";

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@IdPlata",
                    Value = id
                });

                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    iban = reader.GetString(0);
                }
                connection.Close();
            }
            return iban;
        }


        public string GetIBANByIdPlataForFurnizor(int id)
        {
            string iban = string.Empty;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select IBAN from Furnizori where IdFurnizor = (select IdFurnizor from PlatiPlanificate where IdPlata = @IdPlata )";

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@IdPlata",
                    Value = id
                });

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    iban = reader.GetString(0);
                }
                connection.Close();
            }
            return iban;
        }



        public void UpdateLastPayedById(int id, DateTime data)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "update PlatiPlanificate set Lastpayed = @Data  where IdPlata = @IdPlata";

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Data",
                    Value = data
                });

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@IdPlata",
                    Value = id
                });

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion


    }
}
