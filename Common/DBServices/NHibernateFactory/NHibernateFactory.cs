using Common.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Common.DBServices
{
    public abstract class NHibernateFactory
    {
        private static ISessionFactory SessionPacoteFactory = null;
        private static ISessionFactory SessionGeralFactory = null;

        private static readonly string connectionGeral = "server=dbrastreamentogeral.ce4i36i2gomh.us-west-1.rds.amazonaws.com;user=admin;password=Scopus2018;database=dbRastreamentoGeral";
        private static readonly string connectionPacote = "server=dbrastreamentopacote.ce4i36i2gomh.us-west-1.rds.amazonaws.com;user=admin;password=Scopus2018;database=dbRastreamentoPacote";

        public static ISessionFactory GetSessionFactoryGeral()
        {
            if(SessionGeralFactory == null)
            {

                var _session = Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(connectionGeral))
                    .Mappings(m => m.FluentMappings
                    .Add<UsuarioMap>()
                    .Add<EnderecoMap>()
                    .Add<VeiculoMap>()
                    .Add<EmpresaMap>()
                    .Add<EstacaoMap>()
                    .Add<OcorrenciaMap>()
                );


                #if DEBUG
                var updater = new SchemaUpdate(_session.BuildConfiguration());
                updater.Execute(true, true);
                var ex = updater.Exceptions;
                #endif

                SessionGeralFactory = _session.BuildSessionFactory();
            }
            return SessionGeralFactory;
        }

        public static ISessionFactory GetSessionFactoryPacote()
        {
            if (SessionPacoteFactory == null)
            {
                var _session = Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(connectionPacote))
                    .Mappings(m => m.FluentMappings
                    .Add<PacoteMap>()
                    .Add<RotaMap>()
                    .Add<LocalizacaoMap>()
                );

                #if DEBUG
                var updater = new SchemaUpdate(_session.BuildConfiguration());
                updater.Execute(true, true);
                var ex = updater.Exceptions;
                #endif

                SessionPacoteFactory = _session.BuildSessionFactory();
            }
            return SessionPacoteFactory;
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