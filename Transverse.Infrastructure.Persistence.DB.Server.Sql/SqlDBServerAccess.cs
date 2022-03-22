using System;
using System.Collections.Generic;

using System.Data.Common;

using Transverse.Infrastructure.Persistence.DB.Server.Sql.Interfaces;


namespace Transverse.Infrastructure.Persistence.DB.Server.Sql
{
    public class SqlDBServerAccess
    {

        public abstract class ASqlAccessExtended<TDbCommand, TDbParameter, TDbType> : ASqlAccess<TDbCommand, TDbParameter, TDbType>, ISqlAccessExtended
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            //-- Ok "quel que soit" le type  de serveur de BDD, ET utilise les classes "Helper" spécifiques à Transverse.Infrastructure.Persistence.DB.Sql.*  --

            protected ASqlAccessExtended(
                ISqlAccessHandler<TDbCommand, TDbParameter, TDbType> poSqlAccessHandler,
                ISqlDBServer poSqlDBServer
            ) :
                    base(poSqlAccessHandler, poSqlDBServer)
            {
            }


            //-----------------------------------------------------------------------------------------------------


            public void execSqlFile(SqlFileQuery poSqlFileQuery)
            {
                this.execSqlFile(
                    poSqlFileQuery.SqlScriptFile,
                    (TDbCommand poSqlCommand) =>
                    {
                        this._setSqlQueryParameters(poSqlCommand, poSqlFileQuery);
                    }
                );

            }

            public void execQuery(SqlQuery poSqlQuery)
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

            public int? getInsertQueryResult(SqlInsertQuery poSqlInsertQuery)
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



            public int getUpdateQueryResult(SqlUpdateQuery poSqlUpdateQuery)
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



            public int getDeleteQueryResult(SqlDeleteQuery poSqlDeleteQuery)
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



            public List<TResultRecordType> getSelectQueryResults<TResultRecordType>(SqlSelectQuery<TResultRecordType> poSelectQuery)
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

            private void _setSqlQueryParameters(TDbCommand poSqlCommand, SqlQuery poSelectQuery)
            {
                this._setSqlQueryParametersList<StringSqlQueryParameter, string>(poSqlCommand, poSelectQuery.StringParametersList);
                this._setSqlQueryParametersList<IntSqlQueryParameter, int>(poSqlCommand, poSelectQuery.IntParametersList);
                this._setSqlQueryParametersList<DoubleSqlQueryParameter, double>(poSqlCommand, poSelectQuery.DoubleParametersList);
                this._setSqlQueryParametersList<BoolSqlQueryParameter, bool>(poSqlCommand, poSelectQuery.BoolParametersList);
                this._setSqlQueryParametersList<DateTimeSqlQueryParameter, DateTime>(poSqlCommand, poSelectQuery.DateTimeParametersList);

            }

            private void _setSqlQueryParametersList<T, U>(TDbCommand poSqlCommand, List<T> poParametersList) where T : ASqlQueryParameter<U>
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
                IReadOnlyCollection<FieldAffectation<TResultRecordType>> poFieldsAffectionOrderedList,
                TResultRecordType poResultRecord
            )
            {
                ushort iFieldIndexInResultRecord = 0;
                try
                {
                    object fieldValue;
                    foreach (FieldAffectation<TResultRecordType> oFieldAffectation in poFieldsAffectionOrderedList) //Lecture pour chaque champ récupéré dans poRecordReader
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
