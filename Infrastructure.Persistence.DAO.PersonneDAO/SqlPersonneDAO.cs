
using System.Collections.Generic;

using Transverse.Infrastructure.Persistence.DB.Sql.Interfaces;

using Infrastructure.Persistence.DAO.PersonneDAO.Interfaces;
using Infrastructure.Persistence.DAO.PersonneDAO.Record;


namespace Infrastructure.Persistence.DAO.PersonneDAO
{
    public class SqlPersonneDAO: IPersonneDAO<PersonneRecord>
    {
        ISqlRunner _oSqlRunner;
        ISqlSyntaxer _oSqlSyntaxer;

        public SqlPersonneDAO(ISqlRunner poSqlRunner)
        {
            this._oSqlRunner = poSqlRunner;
            this._oSqlSyntaxer = poSqlRunner.getSyntaxer();

        }

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

        public long getNbFemmes()
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

        public List<PersonneRecord> select()
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

        // protected override string _getCommonDatabaseName()
        // {
        //     return ("MY_TEST_DATABASE");
        // }
    }
}
