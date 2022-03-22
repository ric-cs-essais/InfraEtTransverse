using System;
using System.Collections.Generic;


namespace Transverse.Infrastructure.Persistence.DB.Sql.Interfaces
{
    public interface ISqlQuery
    {
        List<ISqlQueryParameter<int>> IntParametersList { get; init; }
        List<ISqlQueryParameter<double>> DoubleParametersList { get; init; }
        List<ISqlQueryParameter<string>> StringParametersList { get; init; }
        List<ISqlQueryParameter<bool>> BoolParametersList { get; init; }
        List<ISqlQueryParameter<DateTime>> DateTimeParametersList { get; init; }

        ISqlQuery setParameter(ISqlQueryParameter<int> poParameter);
        ISqlQuery setParameter(ISqlQueryParameter<double> poParameter);
        ISqlQuery setParameter(ISqlQueryParameter<string> poParameter);
        ISqlQuery setParameter(ISqlQueryParameter<bool> poParameter);
        ISqlQuery setParameter(ISqlQueryParameter<DateTime> poParameter);
    }
}
