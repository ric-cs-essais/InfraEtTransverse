
using System.Collections.Generic;

namespace Infrastructure.Persistence.DAO.PersonneDAO.Interfaces
{
    public interface IPersonneDAO<TPersonneRecord>
    {
        int insert();
        int update();
        int delete();

        List<TPersonneRecord> select();

        long getNbFemmes();
        void sommeNombresAVirgule();
        void sommeNombresEntiers();
        void selectGroupByHaving();

        void dropAndCeateAndFillTable();
        void insertWithNbAVirgule();
        void execSqlFile();
    }
}
