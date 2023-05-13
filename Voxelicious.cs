using System;
using System.Collections.Generic;
using Voxelicious.Lexer.Token;
using Voxelicious.Parser;
using Voxelicious.Ast;
using Voxelicious.Runtime;
using Newtonsoft.Json;
using Voxelicious.Enviornment;
using Voxelicious.Ast.Statement;
using Voxelicious.Runtime.Variable;

namespace Voxelicious
{
    class Voxelicious
    {


        public static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = (() =>
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                return settings;
            });

            IEnviornment env = new ProgramEnviorment();
            Parser.Parser parser = new Parser.Parser();


            if (args.Length > 0)
            {

                string text = File.ReadAllText(args[0]);

                ProgramStatement p = parser.ProduceAST(text, new Lexer.Lexer());
                Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented));

                IRuntimeVariable result = new Interpreter().Evaluate(p, env);

                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            }
            else
            {
                Console.WriteLine("Idk v0.0.1");


                Console.WriteLine("Repl started! Type 'exit' to exit.");
                while (true)
                {
                    Console.Write(">>> ");
                    string? input = Console.ReadLine();
                    if (input == "exit")
                    {
                        break;
                    }
                    try
                    {
                        if (input == null)
                        {
                            Console.WriteLine("[Repl] >> Input is null");
                            return;
                        }
                        ProgramStatement p = parser.ProduceAST(input, new Lexer.Lexer());

                        Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented));
                        IRuntimeVariable result = new Interpreter().Evaluate(p, env);

                        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.None));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

        }

        public static string TokenToString(LangToken token)
        {

            return "{ " + $"{"value: " + token.Value + ", type: " + token.Type}" + " }";
        }
    }
}