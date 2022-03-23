
using System;
using System.Collections.Generic;

using System.IO; //Pour File....

using System.Data;
using System.Data.Common;

using Transverse.Types.Var; //Pour affichages/debuggage

using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Persistence.DB.Server.Handler.Interfaces;
using Transverse.Infrastructure.Persistence.DB.Server.Interfaces;

using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;

using Transverse.Infrastructure.Persistence.DB.Database;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql
{
    public abstract class ADBServerSqlRunnerBase<TDbCommand, TDbParameter, TDbType>
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
    {

        private DBServerAccess _oDBServerAccess;
        protected IDBServerHandler<TDbCommand, TDbParameter, TDbType> _oDBServerHandler;

        private DbConnection _oConnection = null;


        protected ADBServerSqlRunnerBase(
            DBServerAccess poDBServerAccess,
            IDBServerHandler<TDbCommand, TDbParameter, TDbType> poDBServerHandler
        )
        {
            this._oDBServerAccess = poDBServerAccess;
            this._oDBServerHandler = poDBServerHandler;

            //this._open(); this._close();
        }

        public IDBServerAccess getDBServerAccess()
        {
            return (this._oDBServerAccess);
        }

        public IDBServerSqlSyntaxer getDBServerSqlSyntaxer()
        {
            return (this._oDBServerHandler.getDBServerSqlSyntaxer());
        }

        public void setCurrentDatabaseName(DatabaseName poCurrentDatabaseName)
        {
            if (poCurrentDatabaseName != null)
            {
                this._close();

                this._oDBServerAccess.CurrentDatabaseName = poCurrentDatabaseName; //Sera pris en compte lors d'une prochaine connexion (cf. _getConnectionString())
            }
        }

        private bool _isOpened()
        {
            return (this._oConnection?.State == ConnectionState.Open);
        }

        private void _open()
        {
            if (!this._isOpened())
            {
                this._getConnection().Open();

            }
        }
        private void _close()
        {
            if (this._isOpened())
            {
                this._getConnection().Close();
                this._getConnection().Dispose();
                this._oConnection = null;
            }
        }

        private DbConnection _getConnection()
        {
            if (this._oConnection == null)
            {
                this._oConnection = this._oDBServerHandler.createConnection(this._getConnectionString());

            }
            return (this._oConnection);
        }

        private string _getConnectionString()
        {
            string retour;

            Dictionary<string, string> oConnectionStringKeyValues = this._oDBServerHandler.getConnectionStringKeyValues(
                this._oDBServerAccess
            );

            retour = this._createConnectionString(oConnectionStringKeyValues);

            //Console.WriteLine(retour);

            return (retour);
        }

        private string _createConnectionString(Dictionary<string, string> poConnectionStringKeyValues)
        {
            string retour;

            List<string> oConnectionString = new List<string>();

            foreach (var oKeyValue in poConnectionStringKeyValues)
            {
                if (oKeyValue.Value != null)
                {
                    oConnectionString.Add($"{oKeyValue.Key}={oKeyValue.Value}");
                }
            }

            retour = String.Join(";", oConnectionString);
            //Console.WriteLine($"Connection String : '{retour}'\n");

            return (retour);
        }



        //-----------------------------------------------------------------------------------------------------------------


        public void execSqlFile(string psSqlScriptFile, Action<TDbCommand> pfSetSqlQueryParameters = null)
        {
            {
                try
                {
                    if (File.Exists(psSqlScriptFile))
                    {
                        string sFileContentAsQuery = File.ReadAllText(psSqlScriptFile);
                        //Console.WriteLine(sFileContentAsQuery);

                        this.execQuery(sFileContentAsQuery, pfSetSqlQueryParameters);

                    }
                    else
                    {
                        throw new Exception($"FICHIER SCRIPT SQL, INTROUVABLE : '{psSqlScriptFile}' .");
                    }

                }
                catch (Exception oException)
                {
                    throw new Exception($"Erreur lors de la tentative d'exécution du script SQL : '{psSqlScriptFile}\n{oException.Message}");
                }

            }

        }


        public void execQuery(string psSqlQuery, Action<TDbCommand> pfSetSqlQueryParameters = null)
        {
            try
            {
                this._open();

                TDbCommand oSqlCommand = this._oDBServerHandler.createSqlQueryCommand(psSqlQuery, this._getConnection());
                if (pfSetSqlQueryParameters != null)
                {
                    pfSetSqlQueryParameters(oSqlCommand);
                }

                oSqlCommand.ExecuteNonQuery();

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête SQL : '{psSqlQuery}\n{oException.Message}");
            }
            finally
            {
                this._close();
            }
        }

        //@return {int?} inserted id.
        public int? getInsertQueryResult(string psSqlInsertQuery, string psAutoIncrementFieldName = null, Action<TDbCommand> pfSetSqlQueryParameters = null)
        {
            int? iInsertedId;
            string sSqlInsertQuery = psSqlInsertQuery;

            try
            {
                this._open();

                sSqlInsertQuery = this._oDBServerHandler.adjustInsertQueryForInstertId(psSqlInsertQuery, psAutoIncrementFieldName);
                TDbCommand oSqlCommand = this._oDBServerHandler.createSqlQueryCommand(sSqlInsertQuery, this._getConnection());
                if (pfSetSqlQueryParameters != null)
                {
                    pfSetSqlQueryParameters(oSqlCommand);
                }

                iInsertedId = this._oDBServerHandler.executeInsertAndGetLastInsertedId(oSqlCommand);

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête insert SQL : '{sSqlInsertQuery}\n{oException.Message}");
            }
            finally
            {
                this._close();
            }

            return (iInsertedId);
        }


        //@return {int} nb. updated rows
        public int getUpdateQueryResult(string psSqlUpdateQuery, Action<TDbCommand> pfSetSqlQueryParameters = null)
        {
            int iNbUpdatedRows;

            try
            {
                this._open();

                TDbCommand oSqlCommand = this._oDBServerHandler.createSqlQueryCommand(psSqlUpdateQuery, this._getConnection());
                if (pfSetSqlQueryParameters != null)
                {
                    pfSetSqlQueryParameters(oSqlCommand);
                }

                iNbUpdatedRows = oSqlCommand.ExecuteNonQuery();

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête update SQL : '{psSqlUpdateQuery}\n{oException.Message}");
            }
            finally
            {
                this._close();
            }

            return (iNbUpdatedRows);
        }



        //@return {int} nb. deleted rows
        public int getDeleteQueryResult(string psSqlDeleteQuery, Action<TDbCommand> pfSetSqlQueryParameters = null)
        {
            int iNbDeletedRows;

            try
            {
                this._open();

                TDbCommand oSqlCommand = this._oDBServerHandler.createSqlQueryCommand(psSqlDeleteQuery, this._getConnection());
                if (pfSetSqlQueryParameters != null)
                {
                    pfSetSqlQueryParameters(oSqlCommand);
                }

                iNbDeletedRows = oSqlCommand.ExecuteNonQuery();

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête delete SQL : '{psSqlDeleteQuery}\n{oException.Message}");
            }
            finally
            {
                this._close();
            }

            return (iNbDeletedRows);
        }


        //---------------------------------

        public List<TResultRecordType> getSelectQueryResults<TResultRecordType>(
            string psSelectQuery,
            Func<TResultRecordType> pfCreateResultNewRecord,
            Action<DbDataReader, TResultRecordType> pfSetResultRecordFieldsValue,
            Action<TDbCommand> pfSetSqlQueryParameters = null
        )
        {
            List<TResultRecordType> oResultRecords;

            try
            {
                this._open();

                TDbCommand oSqlCommand = this._oDBServerHandler.createSqlQueryCommand(psSelectQuery, this._getConnection());
                if (pfSetSqlQueryParameters != null)
                {
                    pfSetSqlQueryParameters(oSqlCommand);
                }

                oResultRecords = this._getResultRecords<TResultRecordType>(
                    oSqlCommand,
                    pfCreateResultNewRecord,
                    pfSetResultRecordFieldsValue
                );

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête select SQL : '{psSelectQuery}\n{oException.Message}");
            }
            finally
            {
                this._close();
            }
            return (oResultRecords);
        }


        private List<TResultRecordType> _getResultRecords<TResultRecordType>(
            TDbCommand poSqlCommand,
            Func<TResultRecordType> pfCreateResultNewRecord,
            Action<DbDataReader, TResultRecordType> pfSetResultRecordFieldsValue
        )
        {
            List<TResultRecordType> oResultRecords = new List<TResultRecordType>();

            using (DbDataReader oDbRecordReader = poSqlCommand.ExecuteReader())
            {
                if (oDbRecordReader.HasRows)
                {
                    TResultRecordType oResultNewRecord;
                    while (oDbRecordReader.Read()) //Pour chaque enregistrement parmi ceux retournés
                    {
                        oResultNewRecord = pfCreateResultNewRecord();
                        pfSetResultRecordFieldsValue(oDbRecordReader, oResultNewRecord);
                        oResultRecords.Add(oResultNewRecord);
                        //Console.WriteLine("\n");
                    }
                }
            }

            return (oResultRecords);
        }

        protected object _getResultRecordFieldValue(TDbType pFieldDbType, ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            object fieldValue = null;


            if (pFieldDbType.Equals(this._oDBServerHandler.getCharDbType()))
            {
                fieldValue = this._getSqlSelectQueryResultRecordFieldValueAsString(piFieldIndexInResultRecord, poRecordReader);

            }
            else if (pFieldDbType.Equals(this._oDBServerHandler.getIntDbType()))
            {
                fieldValue = this._getSqlSelectQueryResultRecordFieldValueAsInt(piFieldIndexInResultRecord, poRecordReader);

            }
            else if (pFieldDbType.Equals(this._oDBServerHandler.getLongIntDbType()))
            {
                fieldValue = this._getSqlSelectQueryResultRecordFieldValueAsLongInt(piFieldIndexInResultRecord, poRecordReader);

            }
            else if (pFieldDbType.Equals(this._oDBServerHandler.getDoubleDbType()))
            {
                fieldValue = this._getSqlSelectQueryResultRecordFieldValueAsDouble(piFieldIndexInResultRecord, poRecordReader);

            }
            else if (pFieldDbType.Equals(this._oDBServerHandler.getBoolDbType()))
            {
                fieldValue = this._getSqlSelectQueryResultRecordFieldValueAsBool(piFieldIndexInResultRecord, poRecordReader);

            }
            else if (pFieldDbType.Equals(this._oDBServerHandler.getDateTimeDbType()))
            {
                fieldValue = this._getSqlSelectQueryResultRecordFieldValueAsDateTime(piFieldIndexInResultRecord, poRecordReader);

            }
            else
            {
                throw new Exception($"Type de champ DbType non géré : '{pFieldDbType}'.");

            }

            //Console.WriteLine($"SqlDbType: {pFieldDbType} ; index={piFieldIndexInResultRecord} ; VALUE={VarConvert.toDebugString(fieldValue, ( pFieldDbType.Equals(this._oDBServerHandler.getCharDbType()) ) )}");

            return (fieldValue);
        }

        //Contrairement à int, bool, etc... pas besoin d'écrire string? quand on veut qu'une chaîne soit nullable.
        private string _getSqlSelectQueryResultRecordFieldValueAsString(ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            string retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : poRecordReader.GetString(piFieldIndexInResultRecord); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur)
            Console.WriteLine($"Colonne No{piFieldIndexInResultRecord} ; Chaîne = '{VarConvert.toDebugString(retour)}'");
            return (retour);
        }
        private int? _getSqlSelectQueryResultRecordFieldValueAsInt(ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            int? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : (int)poRecordReader.GetValue(piFieldIndexInResultRecord); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur)
            //Console.WriteLine($"Colonne No{piFieldIndexInResultRecord} ; Entier = {VarConvert.toDebugString(retour)}");
            return (retour);
        }
        private long? _getSqlSelectQueryResultRecordFieldValueAsLongInt(ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            //long? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : poRecordReader.GetInt64(piFieldIndexInResultRecord); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur) mais Fail à convertir un System.Int32 en System.Int64, lors d'un Sql Count(*) sur colonneChampEntier par ex., sous SQL Server !
            long? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : Convert.ToInt64(poRecordReader.GetValue(piFieldIndexInResultRecord)); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur)
            //Console.WriteLine($"Colonne No{piFieldIndexInResultRecord} ; EntierLong = {VarConvert.toDebugString(retour)}");
            return (retour);
        }
        private double? _getSqlSelectQueryResultRecordFieldValueAsDouble(ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            //double? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : poRecordReader.GetDouble(piFieldIndexInResultRecord); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur) mais Fail à convertir un System.Int32 en System.Double, lors d'un Sql SUM(colonneChampEntier) par ex., sous SQL Server !
            double? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : Convert.ToDouble(poRecordReader.GetValue(piFieldIndexInResultRecord)); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur)
            //Console.WriteLine($"Colonne No{piFieldIndexInResultRecord} ; DoubleValue = {VarConvert.toDebugString(retour)}");
            return (retour);
        }
        private bool? _getSqlSelectQueryResultRecordFieldValueAsBool(ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            bool? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : poRecordReader.GetBoolean(piFieldIndexInResultRecord); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur)
            //Console.WriteLine($"Colonne No{piFieldIndexInResultRecord} ; Bool = {VarConvert.toDebugString(retour)}");
            return (retour);
        }

        private DateTime? _getSqlSelectQueryResultRecordFieldValueAsDateTime(ushort piFieldIndexInResultRecord, DbDataReader poRecordReader)
        {
            DateTime? retour = (poRecordReader.IsDBNull(piFieldIndexInResultRecord)) ? null : poRecordReader.GetDateTime(piFieldIndexInResultRecord); //Syntaxe pour gérer un NULL en base (c-à-d pas d'erreur)
            //Console.WriteLine($"Colonne No{piFieldIndexInResultRecord} ; DateTime = {VarConvert.toDebugString(retour)}");
            return (retour);
        }



        //------------------------------------------------------------------------------------------------------------

        protected DbParameter _addSqlParameter(TDbCommand poSqlCommand, string psParameterId, TDbType pFieldDbType)
        {
            // *** ATTENTION : les élements @xxx dans la requête DOIVENT obligatoirement être des LEFT VALUE, ou
            //                 faire partie d'un : in (@id1, @id2, ...)  ou  concerner un : LIKE @filtre ,  etc...
            // ***************************************************************************************************
            TDbParameter oSqlParameter = this._oDBServerHandler.createSqlQueryParameter("@" + psParameterId, pFieldDbType);
            poSqlCommand.Parameters.Add(oSqlParameter);
            return (oSqlParameter);
        }


    }
}