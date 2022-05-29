// See https://aka.ms/new-console-template for more information
using System;

namespace SimpleRegisterAppCSharp
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main( string[] args )
        {
            string opçãoUsuario = ObterOpçãoUsuario();
            while( opçãoUsuario != "X" )
            {
                switch( opçãoUsuario )
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opçãoUsuario = ObterOpçãoUsuario();
            }
            //Serie obj = new Serie();
            //Console.WriteLine( "Hello World" );
        }
        private static void ListarSeries()
        {
            Console.WriteLine( "Listar séries" );

            var lista = repositorio.Lista();

            if( lista.Count == 0 || lista.TrueForAll( serie => serie.IsExcluido() ) )
            {
                Console.WriteLine( "Nenhuma série cadastrada" );
                return;
            }
            foreach( var serie in lista )
            {
                if( !serie.IsExcluido() ) Console.WriteLine( $"#ID {serie.RetornaId()}: - {serie.RetornaTitulo()}" );
            }
        }
        private static void InserirSerie()
        {
            Console.WriteLine( "Inserir série" );

            foreach( int i in Enum.GetValues( typeof( Genero ) ) )
            {
                Console.WriteLine( $"{i} - {Enum.GetName( typeof( Genero ) , i )}" );
            }
            Console.WriteLine( "Digite o gênero dentre as opções acima: " );
            int entradaGenero = int.Parse( Console.ReadLine()! );

            Console.WriteLine( "Digite o título da série: " );
            string entradaTitulo = Console.ReadLine()!;

            Console.WriteLine( "Digite o ano da série: " );
            int entradaAno = int.Parse( Console.ReadLine()! );

            Console.WriteLine( "Digite uma descrição para a série: " );
            string entradaDescricao = Console.ReadLine()!;

            Serie novaSerie = new Serie( id: repositorio.ProximoId() ,
                                        genero: (Genero)entradaGenero ,
                                        titulo: entradaTitulo ,
                                        ano: entradaAno ,
                                        descricao: entradaDescricao
            );

            repositorio.Insere( novaSerie );

        }
        private static void AtualizarSerie()
        {
            Console.WriteLine( "Atualizar série" );

            Console.WriteLine( "Digite o id da série: " );
            int indiceSerie = int.Parse( Console.ReadLine()! );
            try
            {
                repositorio.RetornaPorId( indiceSerie );
            }
            catch
            {
                Console.WriteLine( "Série não existe" );
                Console.WriteLine();
                return;
            }

            foreach( int i in Enum.GetValues( typeof( Genero ) ) )
            {
                Console.WriteLine( $"{i} - {Enum.GetName( typeof( Genero ) , i )}" );
            }
            Console.WriteLine( "Digite o gênero dentre as opções acima: " );
            int entradaGenero = int.Parse( Console.ReadLine()! );

            Console.WriteLine( "Digite o título da série: " );
            string entradaTitulo = Console.ReadLine()!;

            Console.WriteLine( "Digite o ano da série: " );
            int entradaAno = int.Parse( Console.ReadLine()! );

            Console.WriteLine( "Digite uma descrição para a série: " );
            string entradaDescricao = Console.ReadLine()!;

            Serie novaSerie = new Serie( id: indiceSerie ,
                                        genero: (Genero)entradaGenero ,
                                        titulo: entradaTitulo ,
                                        ano: entradaAno ,
                                        descricao: entradaDescricao
            );
            repositorio.Atualiza( indiceSerie , novaSerie );
        }
        private static void ExcluirSerie()
        {
            Console.WriteLine( "Excluir série" );
            bool confirmação = false;
            int indiceSerie = -1;
            while( !confirmação )
            {
                try
                {
                    Console.WriteLine( "Digite o id da série ou pressione Enter para voltar: " );
                    indiceSerie = int.Parse( Console.ReadLine()! );
                    if( indiceSerie == -1 ) return;
                    try
                    {
                        if( repositorio.RetornaPorId( indiceSerie ).IsExcluido() )
                        {
                            Console.WriteLine( "Série foi excluída" );
                            Console.WriteLine();
                            return;

                        }
                        Console.WriteLine( $"Deseja mesmo excluir {repositorio.RetornaPorId( indiceSerie ).RetornaTitulo()}? [S/n]" );
                    }
                    catch
                    {
                        Console.WriteLine( "Série não existe" );
                        Console.WriteLine();
                        return;
                    }
                    confirmação = (Console.ReadLine()!.ToUpper() == "S") ? true : false;
                }
                catch
                {
                    return;
                }
            }
            repositorio.Exclui( indiceSerie );



        }
        private static void VisualizarSerie()
        {
            int indiceSerie = -1;
            Console.WriteLine( "Digite o id da série ou pressione Enter para voltar: " );
            try
            {
                indiceSerie = int.Parse( Console.ReadLine()! );
            }
            catch
            {
                return;
            }
            try
            {
                if( indiceSerie >= 0 )
                {
                    var serie = repositorio.RetornaPorId( indiceSerie );
                    if( !serie.IsExcluido() ) Console.WriteLine( serie );
                    else Console.WriteLine( "Série foi excluída" );
                }
                else return;
            }
            catch
            {
                Console.WriteLine( "Série não existe" );
                Console.WriteLine();
                return;
            }


        }


        private static string ObterOpçãoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine( "Informe a opção desejada:" );

            Console.WriteLine( "1 - Listar séries " );
            Console.WriteLine( "2 - Inserir nova série" );
            Console.WriteLine( "3 - Atualizar série" );
            Console.WriteLine( "4 - Excluir série" );
            Console.WriteLine( "5 - Visualizar série" );
            Console.WriteLine( "C - Limpar tela" );
            Console.WriteLine( "X - Sair" );
            Console.WriteLine();

            string opçãoUsuario = Console.ReadLine()!.ToUpper();
            Console.WriteLine();
            return opçãoUsuario;

        }
    }
}
