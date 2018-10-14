using Common.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Common.DBServices
{
    public class NHibernateFactory
    {
        private static bool initGeral = false;
        private static bool initPacote = false;

        private static string connectionGeral = "server=dbrastreamentogeral.ce4i36i2gomh.us-west-1.rds.amazonaws.com;user=admin;password=Scopus2018;database=dbRastreamentoGeral";
        private static string connectionPacote = "server=dbrastreamentopacote.ce4i36i2gomh.us-west-1.rds.amazonaws.com;user=admin;password=Scopus2018;database=dbRastreamentoPacote";

        public static ISessionFactory CreateSessionGeral()
        {

            var _session = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(connectionGeral))
            .Mappings(m => m.FluentMappings
            .Add<UsuarioMap>()
            .Add<EnderecoMap>()
            .Add<EstacaoService>()
            .Add<VeiculoMap>()
            .Add<EmpresaMap>()
            );
            var session = _session.BuildConfiguration();

            if(!initGeral)
            {
                var schemaExport = new SchemaExport(session);
                schemaExport.Execute(false, true, true);
                schemaExport.Drop(false, true);
                schemaExport.Create(false, true);

                var updater = new SchemaUpdate(session);
                updater.Execute(true, true);
                var ex = updater.Exceptions;

                initGeral = true;
            }

            return _session.BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionPacote()
        {
            var _session = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(connectionPacote))
            .Mappings(m => m.FluentMappings
            .Add<PacoteMap>()
            .Add<RotaMap>()
            .Add<LocalizacaoMap>()
            );
            var session = _session.BuildConfiguration(); 

            if (!initPacote)
            {
                var schemaExport = new SchemaExport(session);
                schemaExport.Execute(false, true, true);
                schemaExport.Drop(false, true);
                schemaExport.Create(false, true);

                var updater = new SchemaUpdate(session);
                updater.Execute(true, true);
                var ex = updater.Exceptions;

                initPacote = true;
            }

            return _session.BuildSessionFactory();
        }
    }

    public class MySqlDriver : NHibernate.Driver.ReflectionBasedDriver
    {
        public MySqlDriver() : base(
            "MySql.Data, Version=5.6.39, Culture=neutral",
            "MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Version=5.6.39, Culture=neutral",
            "MySql.Data.MySqlClient.MySqlCommand, MySql.Data, Version=5.6.39, Culture=neutral"
        )
        { }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        public override string NamedPrefix
        {
            get { return "@"; }
        }

        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }
    }
}