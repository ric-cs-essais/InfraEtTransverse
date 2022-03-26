using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Transverse.Infrastructure.Persistence.DB.Database;
using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;
using Transverse.Infrastructure.Persistence.DB.Sql;
using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


using Infrastructure.Persistence.DAO.PersonneDAO.Interfaces;
using Infrastructure.Persistence.DAO.PersonneDAO.Record;


namespace Infrastructure.Persistence.DAO.PersonneDAO
{
    public class SqlPersonneDAO: IPersonneDAO
    {
        IDBServerSqlRunner _oSqlRunner;
        IDBServerSqlSyntaxer _oSqlSyntaxer;

        public SqlPersonneDAO(IDBServerSqlRunner poSqlRunner)
        {
            this._oSqlRunner = poSqlRunner;
            this._oSqlSyntaxer = poSqlRunner.getDBServerSqlSyntaxer();

            this._setCurrentDatabaseName();

        }

        private void _setCurrentDatabaseName()
        {
            this._oSqlRunner.getDBServerAccess().CurrentDatabaseName =
                DatabaseNameFactory.getSingleton().getDatabaseName(this._getDatabaseName());
            ;
        }

        private string _getDatabaseName()
        {
            return ("MY_TEST_DATABASE");
        }

        private string _getTableName()
        {
            return ($"dbo.Personnes");
        }
        private string _getAutoIncrementFieldName()
        {
            return ("ID");
        }

        private string _formatTableName(string psRawTableName = null)
        {
            if (psRawTableName == null)
            {
                psRawTableName = this._getTableName();
            }
            return this._oSqlSyntaxer.getSyntaxTableName(psRawTableName);
        }


        //--------------------------------------------------------------------------------------

        public int delete()
        {
            throw new System.NotImplementedException();
        }

        public void dropAndCeateAndFillTable()
        {
            throw new System.NotImplementedException();
        }

        public void execSqlFile()
        {
            throw new System.NotImplementedException();
        }

        public int insert()
        {
            throw new System.NotImplementedException();
        }

        public void insertWithNbAVirgule()
        {
            throw new System.NotImplementedException();
        }

        public List<PersonneRecord> select<PersonneRecord>()
        {
            throw new System.NotImplementedException();
        }

        public void selectGroupByHaving()
        {
            throw new System.NotImplementedException();
        }

        public void sommeNombresAVirgule()
        {
            throw new System.NotImplementedException();
        }

        public void sommeNombresEntiers()
        {
            throw new System.NotImplementedException();
        }

        public int update()
        {
            throw new System.NotImplementedException();
        }


        //================== SELECT COUNT() =======================
        public long getNbFemmes()
        {
            long iNbFemmes;
            List<PersonneNbFemmesRecord> oReturnedRecords; //En fait 1 seul record ici


            //--- Définition et Paramétrage de ma requête ---
            SqlSelectQuery<PersonneNbFemmesRecord> oSqlSelectQuery =
                new SqlSelectQuery<PersonneNbFemmesRecord>("SELECT count(*) as NbF " +
                                                           $"FROM {this._formatTableName()} WHERE homme=@HM")
                {
                    // - Valeur des paramétrables de la requête  (les @xxx..) -
                    BoolParametersList = new List<ISqlQueryParameter<bool>>
                    {
                        new BoolSqlQueryParameter("HM", false)
                    },

                    // - Gestion de la récup. de la valeur de chaque champ retourné, pour un Record retourné -
                    fCreateResultNewRecord = () => new PersonneNbFemmesRecord(), //<<Sera appelée automatiquement avant chaque lecture d'un Record.
                    FieldsAffectionOrderedList = new ReadOnlyCollection<FieldAffectation<PersonneNbFemmesRecord>>(new List<FieldAffectation<PersonneNbFemmesRecord>>
                    {
                        //--- Pour 1 Record donné, affectation des champs retournés, ceci dans l'ordre qu'ils avaient dans la requête SELECT (même ordre obligatoire en effet) ---

                        new LongIntFieldAffectation<PersonneNbFemmesRecord>( (object pFieldValue, PersonneNbFemmesRecord poPersonneNbFemmesRecord) =>
                            { poPersonneNbFemmesRecord.iNbFemmes = (long)pFieldValue; }
                        )

                    })
                };

            //--- Lancement de la requête ---
            oReturnedRecords = this._oSqlRunner.getSelectQueryResults<PersonneNbFemmesRecord>(oSqlSelectQuery);
            iNbFemmes = oReturnedRecords[0].iNbFemmes;

            Console.WriteLine($"NbFemmes = {iNbFemmes}");

            //Console.WriteLine("\n\nPersonneNbFemmesRecord : " + Transverse.Types.Var.VarConvert.getAsSerialized(oReturnedRecords[0]).Replace("{", "\n{"));


            return (iNbFemmes);
        }
    }
}
