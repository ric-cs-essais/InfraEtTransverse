using System;
using System.Collections.Generic;

using System.Data.Common;

using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Persistence.DB.Sql.Enums;
using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql
{
    public abstract class ASqlDBRunner<TDbCommand, TDbParameter, TDbType>
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
    {
        //-- Ok "quel que soit" le type  de serveur de BDD, ET utilise les classes "Helper" spécifiques à Transverse.Infrastructure.Persistence.DB.Sql.*  --

        protected ASqlDBRunner(
                ISqlAccessHandler<TDbCommand, TDbParameter, TDbType> poSqlAccessHandler, //Remplacé par méthode abstract _getXxxx à implém. dans les enfants MySqlDBRunner, etc...
                DBServerAccess poDBServerAccess
            )
        {
        }

        public ISqlSyntaxer getSyntaxer()   //abstract ???
        {
            throw new NotImplementedException();
        }

        protected abstract ISqlHandler getSqlHandler();


        //-----------------------------------------------------------------------------------------------------


        public void execSqlFile(ISqlFileQuery poSqlFileQuery)
        {
            this.execSqlFile(
                poSqlFileQuery.SqlScriptFile,
                (TDbCommand poSqlCommand) =>
                {
                    this._setSqlQueryParameters(poSqlCommand, poSqlFileQuery);
                }
            );

        }

        public void execQuery(ISqlQuery poSqlQuery)
        {
            try
            {
                this.execQuery(
                    poSqlQuery.AsRawString,
                    (TDbCommand poSqlCommand) =>
                    {
                        this._setSqlQueryParameters(poSqlCommand, poSqlQuery);
                    }
                );

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête insert SQL : '{poSqlQuery.AsRawString}\n{oException.Message}");
            }
        }

        public int? getInsertQueryResult(ISqlInsertQuery poSqlInsertQuery)
        {
            int? iInsertedId;

            try
            {
                iInsertedId = this.getInsertQueryResult(
                    poSqlInsertQuery.AsRawString,
                    poSqlInsertQuery.AutoIncrementFieldName,
                    (TDbCommand poSqlCommand) =>
                    {
                        this._setSqlQueryParameters(poSqlCommand, poSqlInsertQuery);
                    }
                );

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête insert SQL : '{poSqlInsertQuery.AsRawString}\n{oException.Message}");
            }

            return (iInsertedId);
        }



        public int getUpdateQueryResult(ISqlUpdateQuery poSqlUpdateQuery)
        {
            int iNbUpdatedRows;

            try
            {
                iNbUpdatedRows = this.getUpdateQueryResult(
                    poSqlUpdateQuery.AsRawString,
                    (TDbCommand poSqlCommand) =>
                    {
                        this._setSqlQueryParameters(poSqlCommand, poSqlUpdateQuery);
                    }
                );

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête update SQL '{poSqlUpdateQuery.AsRawString}\n{oException.Message}");
            }

            return (iNbUpdatedRows);
        }



        public int getDeleteQueryResult(ISqlDeleteQuery poSqlDeleteQuery)
        {
            int iNbDeletedRows;

            try
            {
                iNbDeletedRows = this.getDeleteQueryResult(
                    poSqlDeleteQuery.AsRawString,
                    (TDbCommand poSqlCommand) =>
                    {
                        this._setSqlQueryParameters(poSqlCommand, poSqlDeleteQuery);
                    }
                );

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête update SQL '{poSqlDeleteQuery.AsRawString}\n{oException.Message}");
            }

            return (iNbDeletedRows);
        }



        public List<TResultRecordType> getSelectQueryResults<TResultRecordType>(ISqlSelectQuery<TResultRecordType> poSelectQuery)
        {
            List<TResultRecordType> oResultRecords;

            try
            {
                oResultRecords = this.getSelectQueryResults<TResultRecordType>(
                    poSelectQuery.AsRawString,
                    () => poSelectQuery.fCreateResultNewRecord(),
                    (DbDataReader poDbDataReader, TResultRecordType poResultRecord) =>
                    {
                        this._setSqlSelectQueryResultRecordFieldsValue<TResultRecordType>(poDbDataReader, poSelectQuery.FieldsAffectionOrderedList, poResultRecord);
                    },
                    (TDbCommand poSqlCommand) =>
                    {
                        this._setSqlQueryParameters(poSqlCommand, poSelectQuery);
                    }
                 );

            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la requête select SQL '{poSelectQuery.AsRawString}\n{oException.Message}");
            }

            return (oResultRecords);

        }

        //-----------------------------------------------------------------------------------------------------------------

        private void _setSqlQueryParameters(TDbCommand poSqlCommand, ISqlQuery poSelectQuery)
        {
            this._setSqlQueryParametersList<ISqlQueryParameter<string>, string>(poSqlCommand, poSelectQuery.StringParametersList);
            this._setSqlQueryParametersList<ISqlQueryParameter<int>, int>(poSqlCommand, poSelectQuery.IntParametersList);
            this._setSqlQueryParametersList<ISqlQueryParameter<double>, double>(poSqlCommand, poSelectQuery.DoubleParametersList);
            this._setSqlQueryParametersList<ISqlQueryParameter<bool>, bool>(poSqlCommand, poSelectQuery.BoolParametersList);
            this._setSqlQueryParametersList<ISqlQueryParameter<DateTime>, DateTime>(poSqlCommand, poSelectQuery.DateTimeParametersList);

        }

        private void _setSqlQueryParametersList<T, U>(TDbCommand poSqlCommand, List<T> poParametersList) where T : ISqlQueryParameter<U>
        {
            if (poParametersList.Count > 0)
            {
                foreach (T oParameter in poParametersList)
                {
                    this._addSqlParameter(
                        poSqlCommand,
                        oParameter.Key,
                        _convertFieldTypeToSqlDbType(oParameter.getType())
                    ).Value = oParameter.Value;
                    //Console.WriteLine(oParameter.Value);
                }
            }
        }



        //-----------------------------------------------------------------------------------------------------------------

        private void _setSqlSelectQueryResultRecordFieldsValue<TResultRecordType>(
            DbDataReader poRecordReader,
            IReadOnlyCollection<IFieldAffectation<TResultRecordType>> poFieldsAffectionOrderedList,
            TResultRecordType poResultRecord
        )
        {
            ushort iFieldIndexInResultRecord = 0;
            try
            {
                object fieldValue;
                foreach (IFieldAffectation<TResultRecordType> oFieldAffectation in poFieldsAffectionOrderedList) //Lecture pour chaque champ récupéré dans poRecordReader
                {
                    TDbType fieldDbType = this._convertFieldTypeToSqlDbType(oFieldAffectation.FieldType);

                    fieldValue = this._getResultRecordFieldValue(fieldDbType, iFieldIndexInResultRecord, poRecordReader);

                    oFieldAffectation.fFieldAffectation(fieldValue, poResultRecord);

                    iFieldIndexInResultRecord++;

                }
            }
            catch (Exception oException)
            {
                throw new Exception($"Erreur lors de la lecture de la colonne No {iFieldIndexInResultRecord} du résultat de la requête.\n{oException.Message}");
            }

        }

        protected TDbType _convertFieldTypeToSqlDbType(FieldType pFieldType)
        {
            TDbType retour;

            switch (pFieldType)
            {
                case FieldType.str:
                    retour = this._oSqlAccessHandler.getCharDbType();
                    break;

                case FieldType.integer:
                    retour = this._oSqlAccessHandler.getIntDbType();
                    break;

                case FieldType.longInteger:
                    retour = this._oSqlAccessHandler.getLongIntDbType();
                    break;

                case FieldType.dbl:
                    retour = this._oSqlAccessHandler.getDoubleDbType();
                    break;

                case FieldType.boolean:
                    retour = this._oSqlAccessHandler.getBoolDbType();
                    break;

                case FieldType.dateTime:
                    retour = this._oSqlAccessHandler.getDateTimeDbType();
                    break;

                default:
                    throw new Exception($"FieldType non reconnu : {pFieldType}");
            }


            return (retour);
        }

        //------------------------------------------------------------------------------------------
    }
}
}
